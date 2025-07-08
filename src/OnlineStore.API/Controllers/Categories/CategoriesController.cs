using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Categories.Commands.CreateCategory;
using OnlineStore.Application.Categories.Commands.DeleteCategory;
using OnlineStore.Application.Categories.Commands.UpdateCategory;
using OnlineStore.Application.Categories.Queries.GetCategories;
using OnlineStore.Application.Categories.Queries.GetCategoriesByName;
using OnlineStore.Application.Categories.Queries.GetCategoryById;

namespace OnlineStore.API.Controllers.Categories
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoriesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoriesQuery query, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCategoryByIdQuery(id);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetCategoriesByName([FromQuery] string name, CancellationToken cancellationToken)
        {
            var query = new GetCategoriesByNameQuery(name);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Value }, result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Category ID mismatch.");

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? NoContent()
                : NotFound(result.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCategoryCommand(id);

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? NoContent()
                : NotFound(result.Error);
        }
    }
}
