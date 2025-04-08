using Revenda.Domain.Dto;
using Revenda.Domain.Entities;

namespace SeuProjeto.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<Pedido> CriarPedidoAsync(PedidoDto pedido);
        Task<Pedido> ObterPorIdAsync(int id);
    }
}
