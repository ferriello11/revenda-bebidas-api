using Revenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Revenda.Infrastructure.Persistence;

public class ClienteRepository : IClienteRepository
{
    private readonly RevendaDbContext _context;

    public ClienteRepository(RevendaDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AdicionarAsync(Cliente cliente)
    {
        if (cliente == null) return false;

        _context.Cliente.Add(cliente);
        return await _context.SaveChangesAsync() > 0 ? true : false;
    }

    public async Task<Cliente> ObterPorIdAsync(int id)
    {
        var cliente = await _context.Cliente.FindAsync(id);

        if (cliente == null)
            return null;

        return cliente;
    }

    public async Task<Cliente?> ObterClienteAsync(string? email = null, string? telefone = null)
    {
        return await _context.Cliente
            .FirstOrDefaultAsync(c =>
                (email != null && c.Email == email) ||
                (telefone != null && c.Telefone == telefone));
    }

}
