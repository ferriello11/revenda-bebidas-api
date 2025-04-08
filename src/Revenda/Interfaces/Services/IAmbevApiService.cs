using Revenda.Domain.Dto;
using Revenda.Domain.Entities;

namespace SeuProjeto.Services.Interfaces
{
    public interface IAmbevApiService
    {
        Task<PedidoAmbevResponse> EnviarPedidoAsync(PedidoAmbevMessage pedido);

        Task<bool> VerificarDisponibilidadeAsync();

    }

}