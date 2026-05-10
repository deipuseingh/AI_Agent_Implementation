using Microsoft.EntityFrameworkCore;
using ECommerceApi.Data;
using ECommerceApi.Models;
using Microsoft.SemanticKernel;
using System.ComponentModel;


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

    [KernelFunction("CheckOrderStatus")]
    [Description("Retrieves the current status and details of an order using its Order ID.")]
    public async Task<string> CheckOrderStatusAsync(
        [Description("The unique identifier of the order, usually a number.")] int orderId)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null) return $"Order #{orderId} not found.";

        var items = order.OrderItems.Select(oi => $"{oi.Product?.Name} (Qty: {oi.Quantity})").ToList();
        return $"Order #{orderId} Status: {order.Status}\nTotal: ${order.TotalAmount:F2}\nItems: {string.Join(", ", items)}";
    }

    [KernelFunction("FindOrderByProductName")]
    [Description("Finds an order based on the name of the product purchased. Use this when the user asks about an order using a product name instead of an order ID.")]
    public async Task<string> FindOrderByProductNameAsync(
        [Description("The name of the product to search for in the orders.")] string productName)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.OrderItems.Any(oi => oi.Product != null && oi.Product.Name.ToLower().Contains(productName.ToLower())))
            .OrderByDescending(o => o.OrderDate)
            .FirstOrDefaultAsync();

        if (order == null) return $"No order found containing a product matching '{productName}'.";

        var items = order.OrderItems.Select(oi => $"{oi.Product?.Name} (Qty: {oi.Quantity})").ToList();
        return $"Found Order #{order.Id}. Status: {order.Status}\nTotal: ${order.TotalAmount:F2}\nItems: {string.Join(", ", items)}";
    }

    [KernelFunction("CancelOrder")]
    [Description("Cancels an order if it is currently in 'Processing' status.")]
    public async Task<string> CancelOrderAsync(
        [Description("The unique identifier of the order to cancel.")] int orderId)
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

    [KernelFunction("ProcessRefund")]
    [Description("Initiates a refund process for a specific order.")]
    public async Task<string> ProcessRefundAsync(
        [Description("The unique identifier of the order to refund.")] int orderId, 
        [Description("The reason the customer is requesting a refund.")] string reason)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null) return $"Order #{orderId} not found.";

        order.Status = "Returned";
        await _dbContext.SaveChangesAsync();
        return $"Refund for Order #{orderId} initiated. Reason: {reason}.";
    }

    [KernelFunction("ListProducts")]
    [Description("Retrieves a list of all available products in the catalog.")]
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