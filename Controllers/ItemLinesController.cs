using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoHubTeam2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemLinesController : ControllerBase
    {
        private readonly ModelContext _context;

        public ItemLinesController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/ItemLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemLine>>> GetItemLines()
        {
            return await _context.ItemLines.ToListAsync();
        }

        // GET: api/ItemLines/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemLine>> GetItemLine(int id)
        {
            var itemLine = await _context.ItemLines.FindAsync(id);

            if (itemLine == null)
            {
                return NotFound();
            }

            return itemLine;
        }

        // GET: api/ItemLines/{id}/items
        [HttpGet("{id}/items")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsInLine(int id)
        {
            var itemLine = await _context.ItemLines.FindAsync(id);

            if (itemLine == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .Where(i => i.item_line == id)
                .ToListAsync();

            return items;
        }

        // PUT: api/ItemLines/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemLine(int id, ItemLine itemLine)
        {
            if (id != itemLine.id)
            {
                return BadRequest();
            }

            _context.Entry(itemLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemLineExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/ItemLines/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemLine(int id)
        {
            var itemLine = await _context.ItemLines.FindAsync(id);
            if (itemLine == null)
            {
                return NotFound();
            }

            // Check ofda er code is die deze lijn gebruikt
            var hasItems = await _context.Items.AnyAsync(i => i.item_line == id);
            if (hasItems)
            {
                return BadRequest("Cannot delete item line while it has associated items");
            }

            _context.ItemLines.Remove(itemLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemLineExists(int id)
        {
            return _context.ItemLines.Any(e => e.id == id);
        }
    }
}
