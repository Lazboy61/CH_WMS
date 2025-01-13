public interface IInventoryService
{
    List<Inventory> GetInventories();
    Inventory GetInventory(int inventoryId);
    List<Inventory> GetInventoriesForItem(string itemId);
    Dictionary<string, int> GetInventoryTotalsForItem(string itemId);
    void AddInventory(Inventory inventory);
    void UpdateInventory(int inventoryId, Inventory updatedInventory);
    void RemoveInventory(int inventoryId);
}
