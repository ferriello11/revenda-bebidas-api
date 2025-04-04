using System.Threading.Tasks;
using Revenda.Domain.Entities;

public interface IClienteRepository
{
    Task<bool> AdicionarAsync(Clientes cliente);
    Task<Clientes> ObterPorIdAsync(int id);
    Task<Clientes?> ObterClienteAsync(string email, string telefone);
}
