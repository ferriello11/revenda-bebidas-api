using Revenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Revenda.Infrastructure.Persistence;

public class PedidoRepository : IPedidoRepository
{
    private readonly RevendaDbContext _context;

    public PedidoRepository(RevendaDbContext context)
    {
        _context = context;
    }

    public async Task<int> AdicionarAsync(Pedido pedido)
    {

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        return pedido.Id;
    }

    public async Task<Pedido> ObterPorIdAsync(int id)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null)
            return null;

        return pedido;
    }

}
