using System;
using Microsoft.EntityFrameworkCore;
using Revenda.Domain.Entities;
using Revenda.Infrastructure.Persistence;

public class LojaRepository : ILojaRepository
{
    private readonly RevendaDbContext _context;

    public LojaRepository(RevendaDbContext context)
    {
        _context = context;
    }

    public async Task<int> AdicionarAsync(Loja loja)
    {
        _context.Lojas.Add(loja);
        await _context.SaveChangesAsync();
        return loja.Id;
    }

    public async Task<Loja> ObterPorIdAsync(int id)
    {
        var loja = await _context.Lojas
            .Include(l => l.Telefones)
            .Include(l => l.Contatos)
            .Include(l => l.Enderecos)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (loja == null)
            throw new KeyNotFoundException($"Loja com ID {id} não encontrada.");

        return loja;
    }

    public async Task<Loja?> ObterPorCnpjOuEmailAsync(string cnpj, string email)
    {
        return await _context.Lojas
            .FirstOrDefaultAsync(l => l.Cnpj == cnpj || l.Email == email);
    }
}
