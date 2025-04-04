using Microsoft.EntityFrameworkCore;
using Revenda.Infrastructure.Persistence;

public class ProdutoRepository : IProdutoRepository
{
    private readonly RevendaDbContext _context;
    public ProdutoRepository(RevendaDbContext context)
    {
        _context = context;
    }

    public async Task<int> AdicionarAsync(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto.Id;
    }

    public async Task<Produto?> ObterPorIdAsync(int id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        return await _context.Produtos.ToListAsync();
    }
}
