using Revenda.Domain.Entities;

public interface IPedidoRepository
{
    Task<int> AdicionarAsync(Pedido pedido);
    Task<Pedido> ObterPorIdAsync(int id);
}
