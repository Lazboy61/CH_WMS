using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    // GET: api/Locations/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLocationById(int id)
    {
        var location = await _locationService.GetLocationByIdAsync(id);
        if (location == null)
        {
            return NotFound($"Location with ID {id} not found.");
        }
        return Ok(location);
    }

    // GET: api/Locations/warehouse/{warehouseId}
    [HttpGet("warehouse/{warehouseId}")]
    public async Task<IActionResult> GetLocationsByWarehouseId(int warehouseId)
    {
        var locations = await _locationService.GetLocationsByWarehouseIdAsync(warehouseId);
        return Ok(locations);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocation(int id, [FromBody] Location location)
    {
        if (location == null || location.id != id)
        {
            return BadRequest("Invalid location data.");
        }

        var updated = await _locationService.UpdateLocationAsync(id, location);
        if (!updated)
        {
            return NotFound($"Location with ID {id} not found.");
        }

        return Ok(location);
    }


    // POST: api/Locations
    [HttpPost]
    public async Task<IActionResult> CreateLocation([FromBody] Location location)
    {
        if (location == null)
        {
            return BadRequest("Location data is required.");
        }

        var createdLocation = await _locationService.CreateLocationAsync(location);
        return CreatedAtAction(nameof(GetLocationById), new { id = createdLocation.id }, createdLocation);
    }
}
