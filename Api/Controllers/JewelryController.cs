using Api.Dtos;
using Application.Jewelry.Commands;
using Application.Jewelry.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/jewelry")]
public class JewelryController : ControllerBase
{
    private readonly IMediator _mediator;

    public JewelryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateJewelry([FromBody] CreateJewelryRequest request)
    {
        var command = new CreateJewelryCommand(
            request.Name,
            request.Description,
            request.JewelryType,
            request.Material,
            request.Price
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return CreatedAtAction(
            nameof(GetJewelryById),
            new { id = result.Value },
            new { id = result.Value }
        );
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<JewelryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllJewelry()
    {
        var query = new GetAllJewelryQuery();
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        var response = result.Value?.Select(j => new JewelryResponse(
            j.Id.Value,
            j.Name,
            j.Description,
            j.JewelryType,
            j.Material,
            j.Price,
            j.Status,
            j.CreatedAt,
            j.UpdatedAt,
            j.Certificate != null ? new JewelryCertificateResponse(
                j.Certificate.Id.Value,
                j.Certificate.CertificateNumber,
                j.Certificate.IssuedBy,
                j.Certificate.JewelryId.Value
            ) : null,
            j.Collections.Select(c => c.Title).ToList()
        ));

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(JewelryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetJewelryById(Guid id)
    {
        var query = new GetJewelryByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        var jewelry = result.Value!;
        
        var response = new JewelryResponse(
            jewelry.Id.Value,
            jewelry.Name,
            jewelry.Description,
            jewelry.JewelryType,
            jewelry.Material,
            jewelry.Price,
            jewelry.Status,
            jewelry.CreatedAt,
            jewelry.UpdatedAt,
            jewelry.Certificate != null ? new JewelryCertificateResponse(
                jewelry.Certificate.Id.Value,
                jewelry.Certificate.CertificateNumber,
                jewelry.Certificate.IssuedBy,
                jewelry.Certificate.JewelryId.Value
            ) : null,
            jewelry.Collections.Select(c => c.Title).ToList()
        );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateJewelry(Guid id, [FromBody] UpdateJewelryRequest request)
    {
        var command = new UpdateJewelryCommand(
            id,
            request.Name,
            request.Description,
            request.JewelryType,
            request.Material,
            request.Price
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateJewelryStatus(Guid id, [FromBody] UpdateJewelryStatusRequest request)
    {
        var command = new UpdateJewelryStatusCommand(id, request.Status);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }
}
