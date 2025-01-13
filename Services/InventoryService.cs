using System;
using System.Collections.Generic;
using System.Linq;
public class InventoryService : IInventoryService
{
    private readonly ModelContext _context;

    public InventoryService(ModelContext context)
    {
        _context = context;
    }

    public List<Inventory> GetInventories()
    {
        return _context.Inventorys.ToList(); // Gebruik de database in plaats van een lijst
    }

    public Inventory GetInventory(int inventoryId)
    {
        return _context.Inventorys.FirstOrDefault(x => x.id == inventoryId);
    }

    public List<Inventory> GetInventoriesForItem(string itemId)
    {
        return _context.Inventorys.Where(x => x.item_id == itemId).ToList();
    }

    public Dictionary<string, int> GetInventoryTotalsForItem(string itemId)
    {
        var result = new Dictionary<string, int>
        {
            {"total_expected", 0},
            {"total_ordered", 0},
            {"total_allocated", 0},
            {"total_available", 0}
        };

        foreach (var inventory in _context.Inventorys.Where(x => x.item_id == itemId))
        {
            result["total_expected"] += inventory.total_expected;
            result["total_ordered"] += inventory.total_ordered;
            result["total_allocated"] += inventory.total_allocated;
            result["total_available"] += inventory.total_available;
        }

        return result;
    }

    public void AddInventory(Inventory inventory)
    {
        inventory.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        inventory.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _context.Inventorys.Add(inventory);
        _context.SaveChanges();
    }

    public void UpdateInventory(int inventoryId, Inventory updatedInventory)
    {
        updatedInventory.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        var existingInventory = _context.Inventorys.FirstOrDefault(x => x.id == inventoryId);
        if (existingInventory != null)
        {
            _context.Entry(existingInventory).CurrentValues.SetValues(updatedInventory);
            _context.SaveChanges();
        }
    }

    public void RemoveInventory(int inventoryId)
    {
        var inventoryToRemove = _context.Inventorys.FirstOrDefault(x => x.id == inventoryId);
        if (inventoryToRemove != null)
        {
            _context.Inventorys.Remove(inventoryToRemove);
            _context.SaveChanges();
        }
    }
}
