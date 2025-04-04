using System.Threading.Tasks;
using Revenda.Domain.Entities;

public interface IClienteService
{
    Task<ApiResponse> CriarClienteAsync(Clientes clientes);
    Task<Clientes> ObterClientePorIdAsync(int id);
}
