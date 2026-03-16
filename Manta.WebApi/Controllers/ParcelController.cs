using Manta.Application.Commands.Parcel;
using Manta.Application.Events.Parcel;
using Manta.Application.Queries.Parcel;
using Manta.Application.Services;
using Manta.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manta.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class ParcelsController : ControllerBase
{
    private readonly IParcelRepository _parcelRepository;
    private readonly ParcelDeliveryService _parcelDeliveryService;
    private readonly ILogger<ParcelsController> _logger;
    private readonly IMediator _mediator;
    public record AcceptParcelRequest(int DeliveryPointId, int SenderId);

    public ParcelsController(IParcelRepository parcelRepository, ParcelDeliveryService parcelDeliveryService,
        ILogger<ParcelsController> logger, IMediator mediator)
    {
        _parcelRepository = parcelRepository;
        _parcelDeliveryService = parcelDeliveryService;
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var parcel = await _mediator.Send(new GetParcelByIdQuery(id));
        if (parcel == null) return NotFound($"Посилку з ID {id} не знайдено.");
        return Ok(parcel);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateParcelCommand command)
    {
        try 
        {
            var parcelId = await _mediator.Send(command);
            var parcel = await _mediator.Send(new GetParcelByIdQuery(parcelId));
            return CreatedAtAction(nameof(GetById), new { id = parcelId }, parcel);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("{id}/accept")]
    public async Task<IActionResult> Accept(int id, [FromBody] AcceptParcelRequest request)
    {
        try
        {
            var command = new AcceptParcelAtDeliveryPointCommand(id, request.DeliveryPointId, request.SenderId);
            var parcelId = await _mediator.Send(command);
            var parcel = await _mediator.Send(new GetParcelByIdQuery(parcelId));
            return Ok(parcel);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}