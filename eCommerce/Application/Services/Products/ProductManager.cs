using eCommerce.Application.Services.Cache;
using eCommerce.Application.Services.Repositories;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Services.Products
{
    // Ürün yönetim sınıfı
    public class ProductManager : IProductService
    {
        // Cache ve ürün repository bağımlılıkları
        private readonly ICacheService _cacheService;
        private readonly IProductRepository _productRepository;

        // Yapılandırıcı, bağımlılıkları alır
        public ProductManager(ICacheService cacheService, IProductRepository productRepository)
        {
            _cacheService = cacheService;
            _productRepository = productRepository;
        }

        // Ürünü ID ile getirir, önce cache'den kontrol eder, yoksa veritabanından çeker
        public async Task<Product> GetProductAsync(int productId)
        {
            var cacheKey = $"product_{productId}";
            var cachedProduct = await _cacheService.GetAsync<Product>(cacheKey);
            if (cachedProduct != null) return cachedProduct;

            var product = await _productRepository.GetByIdAsync(productId);
            if (product != null) await _cacheService.SetAsync(cacheKey, product, TimeSpan.FromMinutes(5));

            return product;
        }

        // Ürünü günceller ve cache'i de günceller
        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
            var cacheKey = $"product_{product.Id}";
            await _cacheService.SetAsync(cacheKey, product, TimeSpan.FromHours(1));
        }
    }
}
