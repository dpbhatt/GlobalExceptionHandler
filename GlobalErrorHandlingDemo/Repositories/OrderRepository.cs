using GlobalErrorHandlingDemo.Models;

namespace GlobalErrorHandlingDemo.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly List<Order> _orders =
        [
            new Order(1, 101, DateTime.Now, OrderStatus.Created),
            new Order(1, 101, DateTime.Now, OrderStatus.Created),
            new Order(1, 101, DateTime.Now, OrderStatus.Created),
            new Order(1, 101, DateTime.Now, OrderStatus.Created),
        ];
    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        // Let's simulate here that a database connection error occurred and an exception is thrown
        throw new InvalidOperationException("The database connection is closed.");

        return _orders;
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        if (id < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "The order Id must be greater than 0.");
        }
        return await Task.FromResult<Order?>(_orders.Find(order => order.Id == id));
    }
}