using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class OrderService : IOrderService
{
    private readonly ModelContext _dbContext;

    public OrderService(ModelContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddOrder(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    // Haal een specifieke order op via ID
    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _dbContext.Orders
            .Include(o => o.items) // Inclusief items
            .FirstOrDefaultAsync(o => o.id == id);
    }

    // Update een bestaande order
    public async Task UpdateOrderAsync(Order order)
    {
        var existingOrder = await _dbContext.Orders
            .Include(o => o.items) // Inclusief items
            .FirstOrDefaultAsync(o => o.id == order.id);

        if (existingOrder == null)
        {
            throw new KeyNotFoundException($"Order met ID {order.id} niet gevonden.");
        }

        // Update velden
        existingOrder.order_date = order.order_date;
        existingOrder.request_date = order.request_date;
        existingOrder.reference = order.reference;
        existingOrder.reference_extra = order.reference_extra;
        existingOrder.order_status = order.order_status;
        existingOrder.notes = order.notes;
        existingOrder.shipping_notes = order.shipping_notes;
        existingOrder.picking_note = order.picking_note;
        existingOrder.warehouse_id = order.warehouse_id;
        existingOrder.ship_to = order.ship_to;
        existingOrder.bill_to = order.bill_to;
        existingOrder.shipment_id = order.shipment_id;
        existingOrder.total_amount = order.total_amount;
        existingOrder.total_discount = order.total_discount;
        existingOrder.total_tax = order.total_tax;
        existingOrder.total_surcharge = order.total_surcharge;

        // Update items
        existingOrder.items = order.items;

        // Sla wijzigingen op
        _dbContext.Orders.Update(existingOrder);
        await _dbContext.SaveChangesAsync();
    }

    // Verwijder een order
    public async Task DeleteOrderAsync(int id)
    {
        var order = await _dbContext.Orders.FindAsync(id);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order met ID {id} niet gevonden.");
        }

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
    }

    public void UpdateOrder(Order order)
    {
        _dbContext.Orders.Update(order);
        _dbContext.SaveChanges();
    }

}
