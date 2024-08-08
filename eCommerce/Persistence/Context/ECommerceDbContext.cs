using eCommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Persistence.Context
{
    public class ECommerceDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ECommerce;User Id=sa;Password=AdenBusra9361.;Trusted_Connection=False;TrustServerCertificate=True;");
        }
    }
}


