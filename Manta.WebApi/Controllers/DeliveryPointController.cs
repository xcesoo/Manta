using Manta.Application.Commands.DeliveryPoint;
using Manta.Application.Queries.DeliveryPoint;
using Manta.Contracts.Responses;
using Manta.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manta.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryPointController(
    ILogger<DeliveryPointController> logger,
    IMediator mediator,
    IDeliveryPointRepository repository)
    : ControllerBase
{
    private readonly ILogger<DeliveryPointController> _logger = logger;
    private readonly IMediator _mediator = mediator;
    private readonly IDeliveryPointRepository _repository = repository;

    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var deliveryPoint = await _mediator.Send(new GetDeliveryPointByIdQuery(id));
        if (deliveryPoint == null) return NotFound($"Відділення з ID {id} не знайдено.");
        return Ok(deliveryPoint);
    }
    
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDeliveryPointCommand command)
    {
        try
        {
            var deliveryPointId = await _mediator.Send(command);
            var deliveryPoint = await _mediator.Send(new GetDeliveryPointByIdQuery(deliveryPointId));
            return CreatedAtAction(nameof(GetById), new { id = deliveryPoint.Id }, deliveryPoint);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}