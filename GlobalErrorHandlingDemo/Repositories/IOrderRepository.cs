using GlobalErrorHandlingDemo.Models;

namespace GlobalErrorHandlingDemo.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int id);
}