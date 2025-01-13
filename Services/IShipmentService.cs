public interface IShipmentService
{
    Task<IEnumerable<Shipment>> GetAllShipmentsAsync();
    Task<Shipment> GetShipmentByIdAsync(int id);
    Task<IEnumerable<Order>> GetOrdersInShipmentAsync(int shipmentId);
    Task<IEnumerable<ShipmentItem>> GetItemsInShipmentAsync(int shipmentId);
}
