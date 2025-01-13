using Microsoft.AspNetCore.Mvc; // Voor [ApiController]
using System.Threading.Tasks;  // Voor async/await
using System.Collections.Generic; // Voor IEnumerable

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public IActionResult GetAllSuppliers()
    {
        var suppliers = _supplierService.GetAllSuppliers();
        return Ok(suppliers);
    }

    [HttpGet("{supplierId}/items")]
    public IActionResult GetItemsForSupplier(int supplierId)
    {
        var items = _supplierService.GetItemsForSupplier(supplierId);

        if (!items.Any())
        {
            return NotFound($"No items found for supplier with ID {supplierId}.");
        }

        return Ok(items);
    }


    [HttpGet("{id}")]
    public IActionResult GetSupplierById(int id)
    {
        var supplier = _supplierService.GetSupplierById(id);
        if (supplier == null)
        {
            return NotFound($"Supplier with ID {id} not found.");
        }
        return Ok(supplier);
    }



    [HttpPost]
    public IActionResult CreateSupplier([FromBody] Supplier supplier)
    {
        if (supplier == null)
        {
            return BadRequest("Supplier data is required.");
        }

        _supplierService.AddSupplier(supplier);
        return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.id }, supplier);
    }



}
