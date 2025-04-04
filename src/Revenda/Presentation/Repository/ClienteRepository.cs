using System.Threading.Tasks;
using Revenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Revenda.Infrastructure.Persistence;

public class ClienteRepository : IClienteRepository
{
    private readonly RevendaDbContext _context;

    public ClienteRepository(RevendaDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AdicionarAsync(Clientes cliente)
    {
        if (cliente == null) return false;

        _context.Clientes.Add(cliente);
        return await _context.SaveChangesAsync() > 0 ? true : false;
    }

    public async Task<Clientes> ObterPorIdAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
            throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");

        return cliente;
    }

    public async Task<Clientes?> ObterClienteAsync(string? email = null, string? telefone = null)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(c =>
                (email != null && c.Email == email) ||
                (telefone != null && c.Telefone == telefone));
    }

}
