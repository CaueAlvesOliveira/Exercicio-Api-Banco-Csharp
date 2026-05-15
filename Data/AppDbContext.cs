using Microsoft.EntityFrameworkCore;
using LojaApi.Models;
namespace LojaApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {   
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
    }
}