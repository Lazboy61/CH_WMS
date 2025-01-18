using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class TransferController : ControllerBase
{
    private readonly TransferService _transferService;

    public TransferController(TransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpGet]
    public ActionResult<List<Transfer>> GetTransfers()
    {
        return Ok(_transferService.GetTransfers());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Transfer> GetTransfer(int id)
    {
        var transfer = _transferService.GetTransfer(id);
        if (transfer == null)
        {
            return NotFound($"Transfer with ID {id} not found.");
        }
        return Ok(transfer);
    }

    [HttpGet("{id:int}/items")]
    public ActionResult<List<TransferItem>> GetItemsInTransfer(int id)
    {
        var items = _transferService.GetItemsInTransfer(id);
        if (items == null)
        {
            return NotFound($"No items found for Transfer with ID {id}.");
        }
        return Ok(items);
    }

    [HttpPost]
    public ActionResult AddTransfer([FromBody] Transfer transfer)
    {
        if (transfer == null)
        {
            return BadRequest("Transfer cannot be null.");
        }

        _transferService.AddTransfer(transfer);
        return CreatedAtAction(nameof(GetTransfer), new { id = transfer.id }, transfer);
    }

    [HttpPut("{id:int}")]
    public ActionResult UpdateTransfer(int id, [FromBody] Transfer updatedTransfer)
    {
        if (updatedTransfer == null || id != updatedTransfer.id)
        {
            return BadRequest("Invalid transfer data.");
        }

        var existingTransfer = _transferService.GetTransfer(id);
        if (existingTransfer == null)
        {
            return NotFound($"Transfer with ID {id} not found.");
        }

        _transferService.UpdateTransfer(id, updatedTransfer);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult RemoveTransfer(int id)
    {
        var transfer = _transferService.GetTransfer(id);
        if (transfer == null)
        {
            return NotFound($"Transfer with ID {id} not found.");
        }

        _transferService.RemoveTransfer(id);
        return NoContent();
    }
}
