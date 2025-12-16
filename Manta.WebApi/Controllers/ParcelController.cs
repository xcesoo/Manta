using Manta.Application.Services;
using Manta.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Manta.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class ParcelsController : ControllerBase
{
    private readonly IParcelRepository _parcelRepository;
    private readonly ParcelDeliveryService _parcelDeliveryService;
    private readonly ILogger<ParcelsController> _logger;

    public ParcelsController(IParcelRepository parcelRepository, ParcelDeliveryService parcelDeliveryService,
        ILogger<ParcelsController> logger)
    {
        _parcelRepository = parcelRepository;
        _parcelDeliveryService = parcelDeliveryService;
        _logger = logger;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var parcel = await _parcelRepository.GetByIdAsync(id);
        if (parcel == null) return NotFound($"Parcel with id {id} not found");
        return Ok(parcel);
    }
}