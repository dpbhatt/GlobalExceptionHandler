using GlobalErrorHandlingDemo.Models;
using GlobalErrorHandlingDemo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GlobalErrorHandlingDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet(Name = "GetOrders")]
    [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        IEnumerable<Order> orders = await _orderRepository.GetOrdersAsync();

        return Ok(orders);
    }

    [HttpGet("{Id:int}", Name = "GetOrderById")]
    [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        Order? order = await _orderRepository.GetOrderByIdAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }
}