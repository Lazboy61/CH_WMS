using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoHubTeam2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ModelContext _context;

        public ItemController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/Item/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(string id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // GET: api/Item/{id}/inventories
        [HttpGet("{id}/inventories")]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetItemInventories(string id)
        {
            var inventories = await _context.Inventorys
                .Where(i => i.item_id == id)
                .ToListAsync();

            if (!inventories.Any())
            {
                return NotFound();
            }

            return inventories;
        }

        // GET: api/Item/{id}/total-profiles
        [HttpGet("{id}/total-profiles")]
        public async Task<ActionResult<int>> GetItemTotalProfiles(string id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            // Implement your calculation logic here
            // This is a placeholder implementation
            return Ok(0);
        }

        // POST: api/Item
        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Items.Add(item);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemExists(item.uid))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtAction(nameof(GetItem), new { id = item.uid }, item);
        }

        // PUT: api/Item/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(string id, Item item)
        {
            if (id != item.uid)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Item/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            // Check of da er inventorYs zijn die deze item gebruiken
            var hasInventories = await _context.Inventorys.AnyAsync(i => i.item_id == id);
            if (hasInventories)
            {
                return BadRequest("Cannot delete item with existing inventories");
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(string id)
        {
            return _context.Items.Any(e => e.uid == id);
        }
    }
}
