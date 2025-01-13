using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ShipmentService : IShipmentService
{
    private readonly ModelContext _context;

    public ShipmentService(ModelContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shipment>> GetAllShipmentsAsync()
    {
        return await _context.Shipments
                             .Include(s => s.items)
                             .ToListAsync();
    }

    public async Task<Shipment> GetShipmentByIdAsync(int id)
    {
        return await _context.Shipments
                             .Include(s => s.items)
                             .FirstOrDefaultAsync(s => s.id == id);
    }

    public async Task<IEnumerable<Order>> GetOrdersInShipmentAsync(int shipmentId)
    {
        // Assuming an Order table is related to Shipments (many-to-one relationship)
        return await _context.Orders
                             .Where(o => o.shipment_id == shipmentId)
                             .ToListAsync();
    }

    public async Task<IEnumerable<ShipmentItem>> GetItemsInShipmentAsync(int shipmentId)
    {
        var shipment = await _context.Shipments
            .Include(s => s.items) // Zorg ervoor dat de items worden geladen
            .FirstOrDefaultAsync(s => s.id == shipmentId);

        return shipment?.items ?? Enumerable.Empty<ShipmentItem>();
    }

}
