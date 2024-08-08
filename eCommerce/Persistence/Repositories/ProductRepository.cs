using eCommerce.Application.Services.Repositories;
using eCommerce.Domain.Entities;
using eCommerce.Persistence.Context;

namespace eCommerce.Persistence.Repositories
{
    // Ürün repository sınıfı
    public class ProductRepository : IProductRepository
    {
        // Veritabanı bağlamı
        private readonly ECommerceDbContext _dbContext;

        public ProductRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Ürünü ID ile getirir
        public async Task<Product> GetByIdAsync(int id)
        {
            Product? product = await _dbContext.Products.FindAsync(id);
            return product is null ? throw new Exception($"Product with id {id} not found") : product;
        }

        // Ürünü günceller
        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
