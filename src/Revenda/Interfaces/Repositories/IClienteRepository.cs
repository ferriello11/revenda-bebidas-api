using Revenda.Domain.Entities;

public interface IClienteRepository
{
    Task<bool> AdicionarAsync(Cliente cliente);
    Task<Cliente> ObterPorIdAsync(int id);
    Task<Cliente?> ObterClienteAsync(string email, string telefone);
}
