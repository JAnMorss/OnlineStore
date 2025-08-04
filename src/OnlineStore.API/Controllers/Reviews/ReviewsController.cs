using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Reviews.Commands.CreateReview;
using OnlineStore.Application.Reviews.Commands.DeleteReview;
using OnlineStore.Application.Reviews.Commands.UpdateReview;
using OnlineStore.Application.Reviews.Queries.GetReviewById;
using OnlineStore.Application.Reviews.Queries.GetReviewByProduct;

namespace OnlineStore.API.Controllers.Reviews;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly ISender _sender;
    public ReviewsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetReviewById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetReviewByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }

    [HttpGet("product/{productId:guid}")]
    public async Task<IActionResult> GetReviewsByProduct(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetReviewsByProductQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview(
        [FromBody] CreateReviewCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetReviewById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateReview(
        [FromRoute] Guid id,
        [FromBody] UpdateReviewCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ReviewId)
            BadRequest("Review ID mismatch");

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteReview(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteReviewCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Error);
    }
}