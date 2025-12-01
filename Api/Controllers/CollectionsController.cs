using Api.Dtos;
using Application.Collections.Commands.AddJewelryToCollection;
using Application.Collections.Commands.CreateCollection;
using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/collections")]
public class CollectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICollectionQueries _collectionQueries;

    public CollectionsController(IMediator mediator, ICollectionQueries collectionQueries)
    {
        _mediator = mediator;
        _collectionQueries = collectionQueries;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionRequest request)
    {
        var command = new CreateCollectionCommand(request.Title);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return CreatedAtAction(
            nameof(GetCollectionById),
            new { id = result.Value },
            new { id = result.Value }
        );
    }

    [HttpPost("{id:guid}/jewelries")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddJewelryToCollection(Guid id, [FromBody] AddJewelryToCollectionRequest request)
    {
        var command = new AddJewelryToCollectionCommand(id, request.JewelryId);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CollectionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCollections()
    {
        var collections = await _collectionQueries.GetAllAsync();

        var response = collections.Select(c => new CollectionResponse(
            c.Id.Value,
            c.Title,
            new List<Guid>()
        ));

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CollectionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCollectionById(Guid id)
    {
        var collection = await _collectionQueries.GetByIdWithJewelriesAsync(new CollectionId(id));

        if (collection is null)
            return NotFound(new { message = "Collection not found" });

        var response = new CollectionResponse(
            collection.Id.Value,
            collection.Title,
            collection.Jewelries.Select(j => j.Id.Value).ToList()
        );

        return Ok(response);
    }
}
