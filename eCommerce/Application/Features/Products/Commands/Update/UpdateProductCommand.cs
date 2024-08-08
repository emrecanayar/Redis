using eCommerce.Application.Services.Products;
using eCommerce.Domain.Entities;
using MediatR;

namespace eCommerce.Application.Features.Products.Commands.Update
{
    // Ürünü güncelleme komutu
    public class UpdateProductCommand : IRequest<Product>
    {
        // Güncellenecek ürün
        public Product Product { get; set; }

        public UpdateProductCommand()
        {
            Product = default!;
        }

        // Ürünü güncelleme komutunu işleyen handler
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
        {
            private readonly IProductService _productService;

            public UpdateProductCommandHandler(IProductService productService)
            {
                _productService = productService;
            }

            // Komutun işlenmesi
            public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                await _productService.UpdateProductAsync(request.Product);
                return request.Product;
            }
        }
    }
}