using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoHubTeam2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemGroupController : ControllerBase
    {
        private readonly ModelContext _context;

        public ItemGroupController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/ItemGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemGroup>>> GetItemGroups()
        {
            return await _context.ItemGroups.ToListAsync();
        }

        // GET: api/ItemGroup/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemGroup>> GetItemGroup(int id)
        {
            var itemGroup = await _context.ItemGroups.FindAsync(id);

            if (itemGroup == null)
            {
                return NotFound();
            }

            return itemGroup;
        }

        // GET: api/ItemGroup/{id}/items
        [HttpGet("{id}/items")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsInGroup(int id)
        {
            var itemGroup = await _context.ItemGroups.FindAsync(id);

            if (itemGroup == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .Where(i => i.item_group == id)
                .ToListAsync();

            return items;
        }

        // PUT: api/ItemGroup/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemGroup(int id, ItemGroup itemGroup)
        {
            if (id != itemGroup.id)
            {
                return BadRequest();
            }

            _context.Entry(itemGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemGroupExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/ItemGroup/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemGroup(int id)
        {
            var itemGroup = await _context.ItemGroups.FindAsync(id);
            if (itemGroup == null)
            {
                return NotFound();
            }

            // Check ofda er items zijn die deze group gebruiken
            var hasItems = await _context.Items.AnyAsync(i => i.item_group == id);
            if (hasItems)
            {
                return BadRequest("Cannot delete item group while it has associated items");
            }

            _context.ItemGroups.Remove(itemGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemGroupExists(int id)
        {
            return _context.ItemGroups.Any(e => e.id == id);
        }
    }
}
