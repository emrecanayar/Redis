using eCommerce.Application.Features.Products.Commands.Update;
using eCommerce.Application.Features.Products.Queries.GetById;
using eCommerce.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    //E-Ticaret Uygulaması İçin Ürün Bilgileri Caching
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // MediatR bağımlılığı
        private IMediator? _mediator;

        // MediatR'ı yapılandırır
        protected IMediator Mediator =>
            _mediator ??=
                HttpContext.RequestServices.GetService<IMediator>()
                ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");


        // Ürünü ID ile getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await Mediator.Send(new GetProductByIdQuery { Id = id });
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // Ürünü günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(new UpdateProductCommand { Product = product });
            return NoContent();
        }
    }
}
