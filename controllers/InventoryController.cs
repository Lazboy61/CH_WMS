using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly InventoryService _inventoryService;

    public InventoryController(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public ActionResult<List<Inventory>> GetInventories()
    {
        return Ok(_inventoryService.GetInventories());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Inventory> GetInventory(int id)
    {
        var inventory = _inventoryService.GetInventory(id);
        if (inventory == null)
        {
            return NotFound($"Inventory with ID {id} not found.");
        }
        return Ok(inventory);
    }

    [HttpGet("item/{itemId}")]
    public ActionResult<List<Inventory>> GetInventoriesForItem(string itemId)
    {
        var inventories = _inventoryService.GetInventoriesForItem(itemId);
        return Ok(inventories);
    }

    [HttpGet("item/{itemId}/totals")]
    public ActionResult<Dictionary<string, int>> GetInventoryTotalsForItem(string itemId)
    {
        var totals = _inventoryService.GetInventoryTotalsForItem(itemId);
        return Ok(totals);
    }

    [HttpPost]
    public ActionResult AddInventory([FromBody] Inventory inventory)
    {
        if (inventory == null)
        {
            return BadRequest("Inventory cannot be null.");
        }

        _inventoryService.AddInventory(inventory);
        return CreatedAtAction(nameof(GetInventory), new { id = inventory.id }, inventory);
    }

    [HttpPut("{id:int}")]
    public ActionResult UpdateInventory(int id, [FromBody] Inventory updatedInventory)
    {
        if (updatedInventory == null || id != updatedInventory.id)
        {
            return BadRequest("Invalid inventory data.");
        }

        var existingInventory = _inventoryService.GetInventory(id);
        if (existingInventory == null)
        {
            return NotFound($"Inventory with ID {id} not found.");
        }

        _inventoryService.UpdateInventory(id, updatedInventory);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult RemoveInventory(int id)
    {
        var inventory = _inventoryService.GetInventory(id);
        if (inventory == null)
        {
            return NotFound($"Inventory with ID {id} not found.");
        }

        _inventoryService.RemoveInventory(id);
        return NoContent();
    }
}
