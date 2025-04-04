using Microsoft.EntityFrameworkCore;
using Revenda.Domain.Entities;

namespace Revenda.Infrastructure.Persistence;

public class RevendaDbContext : DbContext
{
    public RevendaDbContext(DbContextOptions<RevendaDbContext> options) : base(options) { }

    public DbSet<Loja> Lojas { get; set; }
    public DbSet<LojaTelefone> LojaTelefones { get; set; }
    public DbSet<Contato> Contatos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Loja>()
        .HasMany(l => l.Telefones)
        .WithOne(t => t.Loja)
        .HasForeignKey(t => t.LojaId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Loja>()
            .HasMany(l => l.Contatos)
            .WithOne(c => c.Loja)
            .HasForeignKey(c => c.LojaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Loja>()
            .HasMany(l => l.Enderecos)
            .WithOne(e => e.Loja)
            .HasForeignKey(e => e.LojaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pedido>()
            .HasMany(p => p.Itens)
            .WithOne()
            .HasForeignKey(i => i.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pedido>()
            .HasOne<Clientes>()
            .WithMany()
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ItemPedido>()
            .HasOne<Produto>()
            .WithMany()
            .HasForeignKey(i => i.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
