using Microsoft.AspNetCore.Mvc; // Voor [ApiController]
using System.Threading.Tasks;  // Voor async/await
using System.Collections.Generic; // Voor IEnumerable
[ApiController]
[Route("api/[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly IShipmentService _shipmentService;

    public ShipmentsController(IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShipments()
    {
        var shipments = await _shipmentService.GetAllShipmentsAsync();
        return Ok(shipments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShipmentById(int id)
    {
        var shipment = await _shipmentService.GetShipmentByIdAsync(id);
        if (shipment == null)
        {
            return NotFound($"Shipment with ID {id} not found.");
        }
        return Ok(shipment);
    }

    [HttpGet("{id}/orders")]
    public async Task<IActionResult> GetOrdersInShipment(int id)
    {
        var orders = await _shipmentService.GetOrdersInShipmentAsync(id);
        if (orders == null || !orders.Any())
        {
            return NotFound($"No orders found in shipment with ID {id}.");
        }
        return Ok(orders);
    }

    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetItemsInShipment(int id)
    {
        var items = await _shipmentService.GetItemsInShipmentAsync(id);
        if (items == null || !items.Any())
        {
            return NotFound($"No items found in shipment with ID {id}.");
        }
        return Ok(items);
    }
}
