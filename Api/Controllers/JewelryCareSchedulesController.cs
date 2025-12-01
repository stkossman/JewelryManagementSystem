using Api.Dtos;
using Application.CareSchedules.Commands;
using Application.CareSchedules.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/care-schedules")]
public class JewelryCareSchedulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public JewelryCareSchedulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateCareScheduleRequest request)
    {
        var command = new CreateCareScheduleCommand(
            request.JewelryId,
            request.NextServiceDate,
            request.Interval,
            request.Description
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return CreatedAtAction(
            nameof(GetScheduleById),
            new { id = result.Value },
            new { id = result.Value }
        );
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CareScheduleResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSchedules()
    {
        var query = new GetAllCareSchedulesQuery();
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        var response = result.Value?.Select(s => new CareScheduleResponse(
            s.Id.Value,
            s.JewelryId.Value,
            s.NextServiceDate,
            s.Interval,
            s.Description,
            s.IsActive,
            s.CreatedAt,
            s.UpdatedAt
        ));

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CareScheduleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetScheduleById(Guid id)
    {
        var query = new GetCareScheduleByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        var schedule = result.Value;
        var response = new CareScheduleResponse(
            schedule!.Id.Value,
            schedule.JewelryId.Value,
            schedule.NextServiceDate,
            schedule.Interval,
            schedule.Description,
            schedule.IsActive,
            schedule.CreatedAt,
            schedule.UpdatedAt
        );

        return Ok(response);
    }

    [HttpGet("jewelry/{jewelryId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<CareScheduleResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSchedulesByJewelryId(Guid jewelryId)
    {
        var query = new GetCareSchedulesByJewelryIdQuery(jewelryId);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        var response = result.Value?.Select(s => new CareScheduleResponse(
            s.Id.Value,
            s.JewelryId.Value,
            s.NextServiceDate,
            s.Interval,
            s.Description,
            s.IsActive,
            s.CreatedAt,
            s.UpdatedAt
        ));

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSchedule(Guid id, [FromBody] UpdateCareScheduleRequest request)
    {
        var command = new UpdateCareScheduleCommand(
            id,
            request.NextServiceDate,
            request.Interval,
            request.Description
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateSchedule(Guid id)
    {
        var command = new DeactivateCareScheduleCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpPost("{id:guid}/reactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReactivateSchedule(Guid id, [FromBody] ReactivateCareScheduleRequest request)
    {
        var command = new ReactivateCareScheduleCommand(id, request.NextServiceDate);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }
}
