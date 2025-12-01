using Api.Dtos;
using Application.Orders.Commands;
using Application.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/jewelry-orders")]
public class JewelryOrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public JewelryOrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateJewelryOrderRequest request)
    {
        var command = new CreateJewelryOrderCommand(
            request.OrderNumber,
            request.JewelryId,
            request.CustomerName,
            request.Notes,
            request.Priority,
            request.ScheduledDate
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return CreatedAtAction(
            nameof(GetOrderById),
            new { id = result.Value },
            new { id = result.Value }
        );
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<JewelryOrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllOrders()
    {
        var query = new GetAllJewelryOrdersQuery();
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        var response = result.Value?.Select(o => new JewelryOrderResponse(
            o.Id.Value,
            o.OrderNumber,
            o.JewelryId.Value,
            o.CustomerName,
            o.Notes,
            o.Priority,
            o.Status,
            o.ScheduledDate,
            o.CompletedAt,
            o.CreatedAt,
            o.UpdatedAt
        ));

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(JewelryOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var query = new GetJewelryOrderByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        var order = result.Value;
        var response = new JewelryOrderResponse(
            order!.Id.Value,
            order.OrderNumber,
            order.JewelryId.Value,
            order.CustomerName,
            order.Notes,
            order.Priority,
            order.Status,
            order.ScheduledDate,
            order.CompletedAt,
            order.CreatedAt,
            order.UpdatedAt
        );

        return Ok(response);
    }

    [HttpGet("jewelry/{jewelryId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<JewelryOrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersByJewelryId(Guid jewelryId)
    {
        var query = new GetJewelryOrdersByJewelryIdQuery(jewelryId);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        var response = result.Value?.Select(o => new JewelryOrderResponse(
            o.Id.Value,
            o.OrderNumber,
            o.JewelryId.Value,
            o.CustomerName,
            o.Notes,
            o.Priority,
            o.Status,
            o.ScheduledDate,
            o.CompletedAt,
            o.CreatedAt,
            o.UpdatedAt
        ));

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateJewelryOrderRequest request)
    {
        var command = new UpdateJewelryOrderCommand(
            id,
            request.CustomerName,
            request.Notes,
            request.Priority,
            request.ScheduledDate
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpPatch("{id:guid}/start")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> StartOrder(Guid id)
    {
        var command = new StartJewelryOrderCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CompleteOrder(Guid id, [FromBody] CompleteJewelryOrderRequest? request)
    {
        var command = new CompleteJewelryOrderCommand(id, request?.CompletionNotes);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        var command = new CancelJewelryOrderCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }
}
