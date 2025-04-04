using Revenda.Domain.Entities;

public interface IPedidoService
{
    Task<Pedido> CriarPedidoAsync(Pedido pedido);
    Task<Pedido> ObterPorIdAsync(int id);
}
