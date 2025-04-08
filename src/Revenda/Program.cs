using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using Microsoft.EntityFrameworkCore;
using Revenda.Infrastructure.Persistence;
using SeuProjeto.Workers;
using System.Globalization;
using DotNetEnv;
using SeuProjeto.Services.Interfaces;
using Amazon.Runtime;

// Carrega variáveis de ambiente do arquivo .env 
if (File.Exists(".env"))
{
    Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

// Configurações básicas da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

// Configuração do banco de dados
builder.Services.AddDbContext<RevendaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Registro de serviços
builder.Services.AddScoped<ILojaService, LojaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

// registro de repositórios
builder.Services.AddScoped<ILojaRepository, LojaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

// Configuração AWS - USANDO VARIÁVEIS DE AMBIENTE 
var awsOptions = new AWSOptions
{
    Region = Amazon.RegionEndpoint.GetBySystemName(
        Environment.GetEnvironmentVariable("AWS_REGION") ??
        builder.Configuration["AWS:Region"] ??
        "us-east-1")
};

// credenciais aws
var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") ?? builder.Configuration["AWS:AccessKey"];
var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") ?? builder.Configuration["AWS:SecretKey"];

if (!string.IsNullOrEmpty(accessKey) && !string.IsNullOrEmpty(secretKey))
{
    awsOptions.Credentials = new BasicAWSCredentials(accessKey, secretKey);
}

builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonSQS>();

// Serviços para o Worker
builder.Services.AddSingleton<IAmbevApiService, AmbevApiServiceMock>();
builder.Services.AddSingleton<ISqsService, SqsService>();
builder.Services.AddHostedService<SqsPedidoIntegration>();

var app = builder.Build();

// pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Criação do banco de dados
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RevendaDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();