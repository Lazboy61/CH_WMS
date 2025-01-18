using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly WarehouseService _warehouseService;

    public WarehouseController(WarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet]
    public ActionResult<List<Warehouse>> GetWarehouses()
    {
        return Ok(_warehouseService.GetWarehouses());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Warehouse> GetWarehouse(int id)
    {
        var warehouse = _warehouseService.GetWarehouse(id);
        if (warehouse == null)
        {
            return NotFound($"Warehouse with ID {id} not found.");
        }
        return Ok(warehouse);
    }

    [HttpPost]
    public ActionResult AddWarehouse([FromBody] Warehouse warehouse)
    {
        if (warehouse == null)
        {
            return BadRequest("Warehouse cannot be null.");
        }

        _warehouseService.AddWarehouse(warehouse);
        return CreatedAtAction(nameof(GetWarehouse), new { id = warehouse.id }, warehouse);
    }

    [HttpPut("{id:int}")]
    public ActionResult UpdateWarehouse(int id, [FromBody] Warehouse updatedWarehouse)
    {
        if (updatedWarehouse == null || id != updatedWarehouse.id)
        {
            return BadRequest("Invalid warehouse data.");
        }

        var existingWarehouse = _warehouseService.GetWarehouse(id);
        if (existingWarehouse == null)
        {
            return NotFound($"Warehouse with ID {id} not found.");
        }

        _warehouseService.UpdateWarehouse(id, updatedWarehouse);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult RemoveWarehouse(int id)
    {
        var warehouse = _warehouseService.GetWarehouse(id);
        if (warehouse == null)
        {
            return NotFound($"Warehouse with ID {id} not found.");
        }

        _warehouseService.RemoveWarehouse(id);
        return NoContent();
    }
}
