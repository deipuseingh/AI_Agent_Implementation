using System.Threading.Tasks;
using System.Collections.Generic;

namespace ECommerceApi.Services;

public interface IAgentToolService
{
    // Task 1: Check real-time order status
    Task<string> CheckOrderStatusAsync(int orderId);

    // Task 2: Handle cancellations
    Task<string> CancelOrderAsync(int orderId);

    // Task 3: Process refunds with a reason
    Task<string> ProcessRefundAsync(int orderId, string reason);

    // Task 4: Browse the product catalog
    Task<string> ListProductsAsync();
    
    // This provides the initial interactive menu
    ActionMenuResult GetActionMenu();
}

public class ActionMenuResult
{
    public required string GreetingText { get; set; }
    public List<string> Options { get; set; } = new();
}