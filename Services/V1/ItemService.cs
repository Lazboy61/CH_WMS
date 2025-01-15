// using Microsoft.EntityFrameworkCore;

// public class ItemService : IItemService{ //interface?
//     private readonly ModelContext _context;
//     public ItemService(ModelContext context)
//     {
//         _context = context;
//     }

//     public List<Item> GetAll(){
//         return _context.Set<Item>().ToList();
//     } 

//     public Item Get(int id){
//         return _context.Set<Item>().Find(id);
//     }

//     public async Task<bool> Post(Item item){
//         // other checks and validations?
//         // different return types?

//         _context.items.Add(item);
//         int entries = await _context.SaveChangesAsync();
//         return entries == 1; //post succesfull        
//     }



//     public bool Patch(Item target){ // wel of geen patch doen?
//         throw new NotImplementedException();
//     }

//     public async Task<bool> Put(Item target){
//         List<Item> original = _context.Set<Item>().Where(i => i.Id == target.Id).ToList();
//         if (original.Count != 1){
//             return false; // meer dan 1 gevonden, of helemaal niet gevonden (niet nodig miss?)
//         }
//         _context.Set<Item>().Remove(original[0]);
//         _context.Set<Item>().Add(target);
//         _context.SaveChanges();
//         return true;
//     }

// // -m0 3319b01eb591e3f268bcddc7bcf3ae871dc341c7

//     public bool Delete(int id){
//         throw new NotImplementedException();
//     }
//     public bool Delete(Item target){
//         // shpuld it check if the item stored is identical to the entity given???
//         // or just check the ID??
//         var result = _context.Set<Item>().Remove(target);
//         _context.SaveChanges();
//         return true;
//     }


//     public async Task<bool> ValidateItemForeignKeys(Item item)
//     {
//         bool supplierExists = await _context.Set<Supplier>().AnyAsync(s => s.Id == item.SupplierId);
//         bool groupExists = await _context.Set<ItemGroup>().AnyAsync(g => g.Id == item.ItemGroupId);
//         bool typeExists = await _context.Set<ItemType>().AnyAsync(t => t.Id == item.ItemTypeId);
//         bool lineExists = await _context.Set<ItemLine>().AnyAsync(l => l.Id == item.ItemLineId);

//         return supplierExists && groupExists && typeExists && lineExists;
//     }

//     public async Task<bool> ItemIdAlreadyExists(Item item){
//         return await _context.Set<Item>().AnyAsync(i => i.Id == item.Id);
//     }
// }
