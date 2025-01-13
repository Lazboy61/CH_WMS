// using Microsoft.AspNetCore.Mvc;

// [Route("api/v2/[controller]")]
// public class NewItemController: Controller
// {
//     private readonly IItemService _itemService;
//     private readonly IInventoryService _inventoryService;
//     public NewItemController(IItemService itemService, IInventoryService inventoryService){ //add service class
//         _itemService = itemService;
//         _inventoryService = inventoryService;
//     }

//     [HttpGet]
//     public async Task<IActionResult> GetAllItems() {
//         return Ok(_itemService.GetAll());
//     }

//     [HttpGet("{id}")]
//     public async Task<IActionResult> GetItemById(int id){
//         Item? item = _itemService.Get(id);
//         if (item == null){
//             return NotFound($"Item with id {id} not found.");
//         }
//         else{
//             return Ok(item);
//         }
//     }

//     [HttpGet("{id}/inventory")]
//     public async Task<IActionResult> GetInventoriesForItem(int id){
//         return Ok(_inventoryService.GetInventoriesForItem(id));
//     }

//     [HttpGet("{id}/inventory/totals")]
//     public async Task<IActionResult> GetInventoryTotalsForItem(int id){
//         return Ok(_inventoryService.GetInventoryTotalsForItem(id));
//     }

//     [HttpPost]
//     public async Task<IActionResult> CreateNewItem([FromBody] Item item){
//         // what about inventory?
//         if (await _itemService.ItemIdAlreadyExists(item)){
//             return Conflict($"An Item with ID {item.Id} already exists.");
//         }
//         if (!await _itemService.ValidateItemForeignKeys(item)){
//             return BadRequest("Invalid foreign keys were given.");
//         }
//         bool result = await _itemService.Post(item);
//         if (result){
//             return Ok("item has been added succesfully");
//         }
//         return BadRequest("Item was not succesfully added.");
//     }

//     [HttpPut]
//     public async Task<IActionResult> UpdateItem([FromBody] Item item){
//         if (!await _itemService.ItemIdAlreadyExists(item)){
//             return Conflict($"An Item with ID {item.Id} does not exists.");
//         }
//         if (!await _itemService.ValidateItemForeignKeys(item)){
//             return BadRequest("Invalid foreign keys were given.");
//         }
//         bool result = await _itemService.Put(item);
//         if (result){
//             return Ok("item has been updated succesfully");
//         }
//         return BadRequest("Item was not succesfully updated.");
//     }

//     [HttpDelete]
//     public async Task<IActionResult> DeleteItem([FromBody] Item item){
//         if (!await _itemService.ItemIdAlreadyExists(item)){
//             return Conflict($"An Item with ID {item.Id} does not exists.");
//         }
//         bool result = _itemService.Delete(item);
//         if (result){
//             return Ok("item has been deleted succesfully");
//         }
//         return BadRequest("Item was not succesfully deleted.");
//     }

//     // FOR PUT
//     /*
//         check if id even exists

//         validate new fk values

//         should anything else be updated along with it?
    
//     */
// }