public interface IItemService : ICRUDinterface<Item>{
    Task<bool> ValidateItemForeignKeys(Item item);
    Task<bool> ItemIdAlreadyExists(Item item);
}