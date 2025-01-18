using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryService
{
    private List<Inventory> data;

    public InventoryService(List<Inventory> inventories)
    {
        data = inventories;
    }

    public List<Inventory> GetInventories()
    {
        return data;
    }

    public Inventory GetInventory(int inventoryId)
    {
        return data.FirstOrDefault(x => x.id == inventoryId);
    }

    public List<Inventory> GetInventoriesForItem(string itemId)
    {
        return data.Where(x => x.item_id == itemId).ToList();
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

        foreach (var inventory in data.Where(x => x.item_id == itemId))
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
        data.Add(inventory);
    }

    public void UpdateInventory(int inventoryId, Inventory updatedInventory)
    {
        updatedInventory.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].id == inventoryId)
            {
                data[i] = updatedInventory;
                break;
            }
        }
    }

    public void RemoveInventory(int inventoryId)
    {
        var inventoryToRemove = data.FirstOrDefault(x => x.id == inventoryId);
        if (inventoryToRemove != null)
        {
            data.Remove(inventoryToRemove);
        }
    }
}
