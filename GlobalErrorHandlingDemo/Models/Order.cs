namespace GlobalErrorHandlingDemo.Models;

public class Order(int id, int customerId, DateTime orderDate, OrderStatus status)
{
    public int Id { get; set; } = id;
    private int CustomerId { get; set; } = customerId;
    public DateTime? OrderDate { get; set; } = orderDate;
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public OrderStatus Status { get; set; } = status;
    public string? Comments { get; set; }
}

public enum OrderStatus : int
{
    Created = 1,
    Shipped = 2,
    Cancelled = 3,
    Completed = 4,
}