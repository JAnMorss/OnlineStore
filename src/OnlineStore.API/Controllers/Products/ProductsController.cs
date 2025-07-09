using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Products.Commands.CreateProduct;
using OnlineStore.Application.Products.Commands.DecreaseProductStock;
using OnlineStore.Application.Products.Commands.DeleteProduct;
using OnlineStore.Application.Products.Commands.IncreaseProductStock;
using OnlineStore.Application.Products.Commands.UpdateProductDetails;
using OnlineStore.Application.Products.Commands.UpdateProductPrice;
using OnlineStore.Application.Products.Commands.UpdateProductStock;
using OnlineStore.Application.Products.Queries.GetAllProducts;
using OnlineStore.Application.Products.Queries.GetProductById;
using OnlineStore.Application.Products.Queries.GetProductsByCategory;
using OnlineStore.Application.Products.Queries.SearchProducts;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.API.Controllers.Products
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] GetAllProductsQuery query, 
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(
            [FromRoute] Guid id, 
            CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery(id);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        [HttpGet("category/{categoryId:guid}")]
        public async Task<IActionResult> GetProductsByCategory(
            [FromRoute] Guid categoryId, 
            CancellationToken cancellationToken)
        {
            var queryId = new GetProductsByCategoryQuery(categoryId);

            var result = await _sender.Send(queryId, cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts(
            [FromQuery] SearchProductsQuery query, 
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProductCommand command, 
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetProductById), new { id = result.Value }, result.Value);
        }

        [HttpPut("{id:guid}/details")]
        public async Task<IActionResult> UpdateProductDetails(
            [FromRoute] Guid id, 
            [FromBody] UpdateProductDetailsCommand command, 
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Product ID mismatch.");

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPut("{id:guid}/stock")]
        public async Task<IActionResult> UpdateProductStock(
            [FromRoute] Guid id, 
            [FromBody] UpdateProductStockCommand command, 
            CancellationToken cancellationToken)
        {
            if (id != command.ProductId)
                return BadRequest("Product ID mismatch.");

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPut("{id:guid}/stock/increase")]
        public async Task<IActionResult> IncreaseProductStock(
            [FromRoute] Guid id,
            [FromBody] IncreaseProductStockCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.ProductId)
                return BadRequest("Product ID mismatch.");

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPut("{id:guid}/stock/decrease")]
        public async Task<IActionResult> DecreaseProductStock(
            [FromRoute] Guid id, 
            [FromBody] DecreaseProductStockCommand command, 
            CancellationToken cancellationToken)
        {
            if (id != command.ProductId)
                return BadRequest("Product ID mismatch.");

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPut("{id:guid}/price")]
        public async Task<IActionResult> UpdateProductPrice(
            [FromRoute] Guid id, 
            [FromBody] UpdateProductPriceCommand command, 
            CancellationToken cancellationToken)
        {
            if (id != command.ProductId)
                return BadRequest("Product ID mismatch.");

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(
            [FromRoute] Guid id, 
            CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(id);

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Error);
        }
    }
}
