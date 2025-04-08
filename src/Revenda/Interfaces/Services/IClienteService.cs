using Revenda.Domain.Entities;

namespace SeuProjeto.Services.Interfaces
{
    public interface IClienteService
    {
        Task<ApiResponse> CriarClienteAsync(Cliente clientes);
        Task<Cliente> ObterClientePorIdAsync(int id);
    }
}
