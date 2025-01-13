public interface IOrderService
{
    Task AddOrder(Order order);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task UpdateOrderAsync(Order order);
    void UpdateOrder(Order order);
    Task DeleteOrderAsync(int id);
}
