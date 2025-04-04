using Revenda.Domain.Entities;
using Revenda.Domain.Enums;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<Pedido> CriarPedidoAsync(Pedido pedido)
    {
        if (pedido.TipoPedido == TipoPedido.Loja)
        {
            var totalItens = pedido.Itens.Sum(i => i.Quantidade);
            if (totalItens < 1000)
                throw new InvalidOperationException("Pedidos de loja devem conter no mínimo 1000 unidades.");
        }

        await _pedidoRepository.AdicionarAsync(pedido);

        var pedidoResponse = new Pedido
        {
            Itens = pedido.Itens.Select(i => new ItemPedido
            {
                PedidoId = i.PedidoId,
                Produto = i.Produto,
                Quantidade = i.Quantidade,
            }).ToList()
        };

        return pedidoResponse;
    }

    public async Task<Pedido> ObterPorIdAsync(int id)
    {
        return await _pedidoRepository.ObterPorIdAsync(id);
    }
}
