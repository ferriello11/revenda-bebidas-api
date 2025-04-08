using Revenda.Domain.Dto;
using Revenda.Domain.Entities;
using SeuProjeto.Services.Interfaces;

public class AmbevApiServiceMock : IAmbevApiService
{
    private readonly ILogger<AmbevApiServiceMock> _logger;
    private readonly Random _random = new();

    public AmbevApiServiceMock(ILogger<AmbevApiServiceMock> logger)
    {
        _logger = logger;
    }

    public async Task<PedidoAmbevResponse> EnviarPedidoAsync(PedidoAmbevMessage pedido)
    {
        if (_random.Next(0, 5) == 0)
        {
            _logger.LogError("⚠️ Falha na API AMBEV... Response status code: 500 Internal Server Error - Ocorreu um erro inesperado no servidor ao processar o pedido.");
        }

        await Task.Delay(300);
        _logger.LogInformation($"✅ Pedido {pedido.PedidoId} aceito pela AMBEV. Quantidade de Items: {pedido.Itens.Sum(i => i.Quantidade)} unidades");

        return new PedidoAmbevResponse
        {
            PedidoId = pedido.PedidoId,
            Itens = pedido.Itens,
            Mensagem = "Pedido aceito com sucesso",
            ProcessadoEm = DateTime.UtcNow
        };
    }

    public async Task<bool> VerificarDisponibilidadeAsync()
    {
        await Task.Delay(100);
        return _random.Next(0, 10) > 2;
    }
}