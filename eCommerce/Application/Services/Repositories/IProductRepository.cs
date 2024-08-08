using eCommerce.Domain.Entities;

namespace eCommerce.Application.Services.Repositories
{
    // Ürün repository arayüzü
    public interface IProductRepository
    {
        // Ürünü ID ile getirir
        Task<Product> GetByIdAsync(int id);
        // Ürünü günceller
        Task UpdateAsync(Product product);
    }
}
