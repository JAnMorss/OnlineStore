using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Payments.Commands.ChangePaymentAmount;
using OnlineStore.Application.Payments.Commands.ChangePaymentMethod;
using OnlineStore.Application.Payments.Commands.ChangePaymentStatus;
using OnlineStore.Application.Payments.Commands.CreatePayment;
using OnlineStore.Application.Payments.Commands.UpdatePaymentDetails;
using OnlineStore.Application.Payments.Queries.GetPaymentById;
using OnlineStore.Application.Payments.Queries.GetPaymentsByOrder;

namespace OnlineStore.API.Controllers.Payments;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly ISender _sender;
    public PaymentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPaymentById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetPaymentByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }

    [HttpGet("order/{id:guid}")]
    public async Task<IActionResult> GetPaymentsByOrder(
        [FromRoute] Guid orderId,
        CancellationToken cancellationToken)
    {
        var query = new GetPaymentsByOrderQuery(orderId);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment(
        [FromBody] CreatePaymentCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetPaymentById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id:guid}/details")]
    public async Task<IActionResult> UpdatePaymentDetails(
        [FromRoute] Guid id,
        [FromBody] UpdatePaymentDetailsCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.PaymentId)
            return BadRequest("Payment ID is mismatch");

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }

    [HttpPut("{id:guid}/amount")]
    public async Task<IActionResult> ChangePaymentAmount(
        [FromRoute] Guid id,
        [FromBody] ChangePaymentAmountCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.PaymentId)
            return BadRequest("Payment ID is mismatch.");

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }

    [HttpPut("{id:guid}/method")]
    public async Task<IActionResult> ChangePaymentMethod(
        [FromRoute] Guid id,
        ChangePaymentMethodCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.PaymentId)
            return BadRequest("Payment ID is mismatch.");

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> ChangePaymentStatus(
        [FromRoute] Guid id,
        [FromBody] ChangePaymentStatusCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.PaymentId)
            return BadRequest("Payment ID mismatch.");

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

}