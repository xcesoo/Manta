using Manta.Application.Commands.Parcel;
using Manta.Application.Events.Parcel;
using Manta.Application.Queries.Parcel;
using Manta.Application.Services;
using Manta.Contracts.Responses;
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
    public ParcelsController(IParcelRepository parcelRepository, ParcelDeliveryService parcelDeliveryService,
        ILogger<ParcelsController> logger, IMediator mediator)
    {
        _parcelRepository = parcelRepository;
        _parcelDeliveryService = parcelDeliveryService;
        _logger = logger;
        _mediator = mediator;
    }
    
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var parcel = await _mediator.Send(new GetParcelByIdQuery(id));
        if (parcel == null) return NotFound($"Посилку з ID {id} не знайдено.");
        return Ok(parcel);
    }
    
    [ProducesResponseType(typeof(Guid), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateParcelCommand command)
    {
        try 
        {
            var parcelId = await _mediator.Send(command);
            return Accepted(parcelId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [ProducesResponseType(typeof(Guid), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]    [HttpPost("accept")]
    [Authorize]
    public async Task<IActionResult> Accept([FromBody]AcceptParcelAtDeliveryPointCommand command)
    {
        try
        {
            var parcelId = await _mediator.Send(command);
            return Accepted(parcelId);
        }
        catch (InvalidOperationException ex) 
        {
            return Conflict(new ApiErrorResponse 
            { 
                Error = "slot_unavailable", 
                Message = ex.Message 
            });
        }
        catch (Exception ex)
        {
            return UnprocessableEntity(new ApiErrorResponse 
            { 
                Error = "invariant_violation", 
                Message = ex.Message 
            });
        }
    }
}