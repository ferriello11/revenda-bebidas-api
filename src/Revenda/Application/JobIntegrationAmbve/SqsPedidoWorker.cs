using System.Text.Json;
using Revenda.Domain.Entities;
using SeuProjeto.Services.Interfaces;

namespace SeuProjeto.Workers
{
    public class SqsPedidoIntegration : BackgroundService
    {
        private readonly ISqsService _sqsService;
        private readonly IAmbevApiService _ambevService;
        private readonly ILogger<SqsPedidoIntegration> _logger;

        public SqsPedidoIntegration(
            ISqsService sqsService,
            IAmbevApiService ambevService,
            ILogger<SqsPedidoIntegration> logger)
        {
            _sqsService = sqsService;
            _ambevService = ambevService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Job de integração AMBEV iniciado");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (!await _ambevService.VerificarDisponibilidadeAsync())
                    {
                        _logger.LogWarning("API AMBEV indisponível. Aguardando...");
                        await Task.Delay(10000, stoppingToken);
                        continue;
                    }

                    await ProcessarMensagensAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro no worker de pedidos");
                }

                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task ProcessarMensagensAsync()
        {
            var messages = await _sqsService.ReceiveMessagesAsync();

            foreach (var sqsMessage in messages)
            {
                try
                {
                    var pedido = JsonSerializer.Deserialize<PedidoAmbevMessage>(sqsMessage.Body);
                    await ProcessarPedidoAsync(pedido);
                    await _sqsService.DeleteMessageAsync(sqsMessage.ReceiptHandle);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar mensagem");
                }
            }
        }

        private async Task ProcessarPedidoAsync(PedidoAmbevMessage pedido)
        {
            try
            {
                await _ambevService.EnviarPedidoAsync(pedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao enviar pedido {pedido.PedidoId} para AMBEV");
                throw;
            }
        }
    }
}