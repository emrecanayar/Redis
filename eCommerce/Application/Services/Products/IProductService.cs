using eCommerce.Domain.Entities;

namespace eCommerce.Application.Services.Products
{
    // Ürün servisi arayüzü
    public interface IProductService
    {
        // Ürünü ID ile getirir
        Task<Product> GetProductAsync(int productId);
        // Ürünü günceller
        Task UpdateProductAsync(Product product);
    }
}
