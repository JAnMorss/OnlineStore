using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Orders.Commands.AttachPaymentToOrder;
using OnlineStore.Application.Orders.Commands.CancelOrder;
using OnlineStore.Application.Orders.Commands.MarkOrderAsShipped;
using OnlineStore.Application.Orders.Commands.PlaceOrder;
using OnlineStore.Application.Orders.Queries.GetOrderById;
using OnlineStore.Application.Orders.Queries.GetOrdersByUser;

namespace OnlineStore.API.Controllers.Orders
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;
        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderById(
            [FromRoute] Guid id, 
            CancellationToken cancellationToken)
        {
            var query = new GetOrderByIdQuery(id);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetOrdersByUser(
            [FromRoute] Guid userId,
            CancellationToken cancellationToken)
        {
            var query = new GetOrdersByUserQuery(userId);

            var result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(
            [FromBody] PlaceOrderCommand command, 
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetOrderById), new { id = result.Value }, result.Value);
        }

        [HttpPut("{id:guid}/ship")]
        public async Task<IActionResult> MarkOrderAsSpipped(
            [FromRoute] Guid id, 
            CancellationToken cancellationToken)
        {
            var command = new MarkOrderAsShippedCommand(id);

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? NoContent()
                : NotFound(result.Error);

        }

        [HttpPut("{id:guid}/atach-payment")]
        public async Task<IActionResult> AttachPayment(
            [FromRoute] Guid id, 
            [FromBody] AttachPaymentToOrderCommand command, 
            CancellationToken cancellationToken)
        {
            if(id != command.OrderId)
                return BadRequest("Order ID is mismatch");

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? NoContent()
                : NotFound(result.Error);
        }

        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> CancelOrder(
            [FromRoute] Guid id, 
            CancellationToken cancellationToken)
        {
            var query = new CancelOrderCommand(id);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? NoContent()
                : NotFound(result.Error);
        }

    }
}
