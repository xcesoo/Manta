using Manta.Application.Queries.DeliveryPoint;
using Manta.Domain.Interfaces;
using MediatR;
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


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var deliveryPoint = await _mediator.Send(new GetDeliveryPointByIdQuery(id));
        if (deliveryPoint == null) return NotFound($"Відділення з ID {id} не знайдено.");
        return Ok(deliveryPoint);
    }
}