using eCommerce.Application.Services.Products;
using eCommerce.Domain.Entities;
using MediatR;

namespace eCommerce.Application.Features.Products.Queries.GetById
{
    // Ürünü ID ile getirme sorgusu
    public class GetProductByIdQuery : IRequest<Product>
    {
        // Getirilecek ürünün ID'si
        public int Id { get; set; }

        public GetProductByIdQuery()
        {
            Id = default!;
        }

        // Ürünü getirme sorgusunu işleyen handler
        public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
        {
            private readonly IProductService _productService;

            public GetProductByIdHandler(IProductService productService)
            {
                _productService = productService;
            }

            // Sorgunun işlenmesi
            public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                return await _productService.GetProductAsync(request.Id);
            }
        }
    }
}
