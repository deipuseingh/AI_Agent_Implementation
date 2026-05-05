using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApi.Data;
using ECommerceApi.DTOs;
using ECommerceApi.Models;

namespace ECommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ECommerceDbContext _dbContext;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ECommerceDbContext dbContext, ILogger<OrdersController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [HttpGet("{id}/status")]
    public async Task<ActionResult<OrderStatusDto>> GetOrderStatus(int id)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound();

        // Returning the DTO prevents circular references[cite: 3, 5]
        return Ok(MapToStatusDto(order));
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder(CreateOrderDto dto)
    {
        // 1. Validate User exists to avoid Foreign Key errors[cite: 4]
        var userExists = await _dbContext.Users.AnyAsync(u => u.Id == dto.UserId);
        if (!userExists) return BadRequest("User not found.");

        var order = new Order
        {
            UserId = dto.UserId,
            TotalAmount = 0,
            Status = "Processing",
            OrderDate = DateTime.UtcNow,
            OrderItems = new List<OrderItem>()
        };

        decimal totalAmount = 0;

        foreach (var item in dto.Items)
        {
            var product = await _dbContext.Products.FindAsync(item.ProductId);
            if (product == null) return BadRequest($"Product {item.ProductId} not found");

            if (product.StockQuantity < item.Quantity)
                return BadRequest($"Insufficient stock for product {product.Name}");

            var orderItem = new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            };

            order.OrderItems.Add(orderItem);
            totalAmount += product.Price * item.Quantity;
            product.StockQuantity -= item.Quantity;
        }

        order.TotalAmount = totalAmount;
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Order {order.Id} created for user {dto.UserId}");

        // FIX: Return a flat anonymous object instead of the full 'order' model
        return Ok(new { id = order.Id, status = order.Status, totalAmount = order.TotalAmount });
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult> GetUserOrders(int userId)
    {
        var orders = await _dbContext.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        // FIX: Project to a flat object to stop the JSON circular loop
        var result = orders.Select(o => new {
            id = o.Id,
            orderDate = o.OrderDate,
            status = o.Status,
            totalAmount = o.TotalAmount
        });

        return Ok(result);
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> CancelOrder(int id)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound($"Order #{id} not found.");

        if (order.Status == "Canceled" || order.Status == "Delivered")
            return BadRequest($"Cannot cancel order in {order.Status} status.");

        order.Status = "Canceled";
        order.CanceledDate = DateTime.UtcNow;

        // Restore stock for all items in the order
        foreach (var item in order.OrderItems)
        {
            var product = item.Product;
            if (product != null)
            {
                product.StockQuantity += item.Quantity;
            }
        }

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Order {order.Id} has been canceled.");

        return Ok(new { orderId = order.Id, status = order.Status, message = "Order canceled successfully." });
    }

    [HttpPost("{id}/refund")]
    public async Task<ActionResult> RefundOrder(int id, [FromBody] RefundRequestDto dto)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound($"Order #{id} not found.");

        if (order.Status != "Delivered" && order.Status != "Canceled")
            return BadRequest($"Cannot refund order in {order.Status} status. Order must be Delivered or Canceled.");

        order.Status = "Returned";
        order.LastModified = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Refund initiated for Order {order.Id}. Reason: {dto.Reason ?? "Not provided"}");

        return Ok(new { orderId = order.Id, status = order.Status, message = "Refund processed successfully." });
    }

    // Helper to map Order to DTO correctly[cite: 3, 5]
    private OrderStatusDto MapToStatusDto(Order order)
    {
        return new OrderStatusDto
        {
            OrderId = order.Id,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            OrderDate = order.OrderDate,
            ShippedDate = order.ShippedDate,
            DeliveredDate = order.DeliveredDate,
            Items = order.OrderItems.Select(oi => new OrderItemDto
            {
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name ?? "Unknown",
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                TotalPrice = oi.UnitPrice * oi.Quantity,
                ImageUrl = oi.Product?.ImageUrl ?? string.Empty
            }).ToList()
        };
    }
}