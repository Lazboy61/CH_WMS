// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace CargoHubTeam2.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ItemTypesController : ControllerBase
//     {
//         private readonly ModelContext _context;

//         public ItemTypesController(ModelContext context)
//         {
//             _context = context;
//         }

//         // GET: api/ItemTypes
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<ItemType>>> GetItemTypes()
//         {
//             return await _context.ItemTypes.ToListAsync();
//         }

//         // GET: api/ItemTypes/{id}
//         [HttpGet("{id}")]
//         public async Task<ActionResult<ItemType>> GetItemType(int id)
//         {
//             var itemType = await _context.ItemTypes.FindAsync(id);

//             if (itemType == null)
//             {
//                 return NotFound();
//             }

//             return itemType;
//         }

//         // GET: api/ItemTypes/{id}/items
//         [HttpGet("{id}/items")]
//         public async Task<ActionResult<IEnumerable<Item>>> GetItemsOfType(int id)
//         {
//             var itemType = await _context.ItemTypes.FindAsync(id);

//             if (itemType == null)
//             {
//                 return NotFound();
//             }

//             var items = await _context.Items
//                 .Where(i => i.item_type == id)
//                 .ToListAsync();

//             return items;
//         }

//         // PUT: api/ItemTypes/{id}
//         [HttpPut("{id}")]
//         public async Task<IActionResult> UpdateItemType(int id, ItemType itemType)
//         {
//             if (id != itemType.id)
//             {
//                 return BadRequest();
//             }

//             _context.Entry(itemType).State = EntityState.Modified;

//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!ItemTypeExists(id))
//                 {
//                     return NotFound();
//                 }
//                 throw;
//             }

//             return NoContent();
//         }

//         // DELETE: api/ItemTypes/{id}
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteItemType(int id)
//         {
//             var itemType = await _context.ItemTypes.FindAsync(id);
//             if (itemType == null)
//             {
//                 return NotFound();
//             }

//             // Check ofda er items zijn die deze type gebruiken
//             var hasItems = await _context.Items.AnyAsync(i => i.item_type == id);
//             if (hasItems)
//             {
//                 return BadRequest("Cannot delete item type while it has associated items");
//             }

//             _context.ItemTypes.Remove(itemType);
//             await _context.SaveChangesAsync();

//             return NoContent();
//         }

//         private bool ItemTypeExists(int id)
//         {
//             return _context.ItemTypes.Any(e => e.id == id);
//         }
//     }
// }
