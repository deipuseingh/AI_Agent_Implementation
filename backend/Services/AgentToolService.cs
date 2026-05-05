using Microsoft.EntityFrameworkCore;
using ECommerceApi.Data;
using ECommerceApi.Models;

namespace ECommerceApi.Services;

public class AgentToolService : IAgentToolService
{
    private readonly ECommerceDbContext _dbContext;
    private readonly ILogger<AgentToolService> _logger;

    public AgentToolService(ECommerceDbContext dbContext, ILogger<AgentToolService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<string> CheckOrderStatusAsync(int orderId)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null) return $"Order #{orderId} not found.";

        var items = order.OrderItems.Select(oi => $"{oi.Product?.Name} (Qty: {oi.Quantity})").ToList();
        return $"Order #{orderId} Status: {order.Status}\nTotal: ${order.TotalAmount:F2}\nItems: {string.Join(", ", items)}";
    }

    public async Task<string> CancelOrderAsync(int orderId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null) return $"Order #{orderId} not found.";

        if (order.Status == "Shipped" || order.Status == "Delivered")
            return $"Cannot cancel Order #{orderId} because it is already {order.Status}.";

        order.Status = "Canceled";
        order.CanceledDate = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return $"Order #{orderId} has been successfully canceled.";
    }

    public async Task<string> ProcessRefundAsync(int orderId, string reason)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null) return $"Order #{orderId} not found.";

        order.Status = "Returned";
        await _dbContext.SaveChangesAsync();
        return $"Refund for Order #{orderId} initiated. Reason: {reason}.";
    }

    public async Task<string> ListProductsAsync()
    {
        var products = await _dbContext.Products.ToListAsync();
        if (!products.Any()) return "No products available.";

        return "Available Products:\n" + string.Join("\n", products.Select(p => $"- {p.Name} (${p.Price:F2})"));
    }

    public ActionMenuResult GetActionMenu()
    {
        return new ActionMenuResult
        {
            GreetingText = "Hello! 👋 I'm your assistant. How can I help today?",
            Options = new List<string> { "Check Order Status", "Cancel Order", "Process Refund", "View Products" }
        };
    }
}