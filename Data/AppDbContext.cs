using CacauShowApi325219722.Models;
using Microsoft.EntityFrameworkCore;

namespace CacauShowApi325219722.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos => Set<Produto>();
        public DbSet<Franquia> Franquias => Set<Franquia>();
        public DbSet<LoteProducao> LotesProducao => Set<LoteProducao>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoteProducao>()
                .HasOne(l => l.Produto)
                .WithMany()
                .HasForeignKey(l => l.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Produto)
                .WithMany()
                .HasForeignKey(p => p.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Unidade)
                .WithMany()
                .HasForeignKey(p => p.UnidadeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}