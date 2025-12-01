using Api.Dtos;
using Application.Common.Interfaces.Queries;
using Application.Jewelries.Commands.AddCertificate;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/certificates")]
public class JewelryCertificatesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJewelryCertificateQueries _certificateQueries;

    public JewelryCertificatesController(IMediator mediator, IJewelryCertificateQueries certificateQueries)
    {
        _mediator = mediator;
        _certificateQueries = certificateQueries;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCertificate([FromBody] CreateJewelryCertificateRequest request)
    {
        var command = new AddCertificateCommand(
            request.JewelryId,
            request.CertificateNumber,
            request.IssuedBy
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return CreatedAtAction(
            nameof(GetCertificateByJewelryId),
            new { jewelryId = request.JewelryId },
            new { id = result.Value }
        );
    }

    [HttpGet("jewelry/{jewelryId:guid}")]
    [ProducesResponseType(typeof(JewelryCertificateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCertificateByJewelryId(Guid jewelryId)
    {
        var certificate = await _certificateQueries.GetByJewelryIdAsync(new JewelryId(jewelryId));

        if (certificate is null)
            return NotFound(new { message = "Certificate not found for this jewelry" });

        var response = new JewelryCertificateResponse(
            certificate.Id.Value,
            certificate.CertificateNumber,
            certificate.IssuedBy,
            certificate.JewelryId.Value
        );

        return Ok(response);
    }
}
