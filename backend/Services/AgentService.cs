using System.Text.Json;
using ECommerceApi.DTOs;
using ECommerceApi.Services;

namespace ECommerceApi.Services;

public interface IAgentService
{
    Task<ChatResponseDto> ProcessMessageAsync(string message, List<ChatMessageDto> conversationHistory);
}

public class AgentService : IAgentService
{
    private readonly IAgentToolService _toolService;
    private readonly ILogger<AgentService> _logger;

    public AgentService(IAgentToolService toolService, ILogger<AgentService> logger)
    {
        _toolService = toolService;
        _logger = logger;
    }

    public async Task<ChatResponseDto> ProcessMessageAsync(string message, List<ChatMessageDto> conversationHistory)
    {
        try
        {
            // Always use rule-based routing (no OpenAI required)
            // This provides full order management functionality without external dependencies
            return await HandleMessageAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing chat message");
            return new ChatResponseDto { Content = "I'm having trouble processing your request. Please try again." };
        }
    }

    private async Task<ChatResponseDto> HandleMessageAsync(string message)
    {
        // Handle special messages
        if (message.Equals("INIT_CHAT", StringComparison.OrdinalIgnoreCase))
        {
            var menu = _toolService.GetActionMenu();
            return new ChatResponseDto
            {
                Content = "",
                ToolCall = new ToolCallDto
                {
                    FunctionName = "DisplayActionMenu",
                    Arguments = new Dictionary<string, object>
                    {
                        { "greetingText", menu.GreetingText },
                        { "options", menu.Options }
                    }
                }
            };
        }

        // Extract order ID from message if present
        var orderIdMatch = System.Text.RegularExpressions.Regex.Match(message, @"#?(\d+)");
        int? orderId = orderIdMatch.Success ? int.Parse(orderIdMatch.Groups[1].Value) : null;

        // Route based on user intent
        if (message.Contains("check order", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("order status", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("where", StringComparison.OrdinalIgnoreCase))
        {
            if (!orderId.HasValue)
            {
                return new ChatResponseDto
                {
                    Content = "I'd be happy to check your order status! Could you please provide your Order ID?"
                };
            }

            var status = await _toolService.CheckOrderStatusAsync(orderId.Value);
            return new ChatResponseDto { Content = status };
        }

        if (message.Contains("cancel", StringComparison.OrdinalIgnoreCase))
        {
            if (!orderId.HasValue)
            {
                return new ChatResponseDto
                {
                    Content = "I can help you cancel an order. Please provide your Order ID."
                };
            }

            var result = await _toolService.CancelOrderAsync(orderId.Value);
            return new ChatResponseDto { Content = result };
        }

        if (message.Contains("refund", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("return", StringComparison.OrdinalIgnoreCase))
        {
            if (!orderId.HasValue)
            {
                return new ChatResponseDto
                {
                    Content = "I can help you process a refund. Please provide your Order ID."
                };
            }

            var reason = "Customer requested refund";
            var result = await _toolService.ProcessRefundAsync(orderId.Value, reason);
            return new ChatResponseDto { Content = result };
        }

        if (message.Contains("product", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("available", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("what can i buy", StringComparison.OrdinalIgnoreCase))
        {
            var products = await _toolService.ListProductsAsync();
            return new ChatResponseDto { Content = products };
        }

        // Default response
        var actionMenu = _toolService.GetActionMenu();
        return new ChatResponseDto
        {
            Content = $"I'm not sure how to help with that. Here are the things I can do:\n\n{string.Join("\n", actionMenu.Options)}\n\nHow can I assist you?"
        };
    }

    private async Task<string> DisplayActionMenuAsync()
    {
        var menu = _toolService.GetActionMenu();
        return JsonSerializer.Serialize(menu);
    }

    private async Task<string> CheckOrderStatusAsync(int orderId)
    {
        return await _toolService.CheckOrderStatusAsync(orderId);
    }

    private async Task<string> CancelOrderAsync(int orderId)
    {
        return await _toolService.CancelOrderAsync(orderId);
    }

    private async Task<string> ProcessRefundAsync(int orderId, string reason)
    {
        return await _toolService.ProcessRefundAsync(orderId, reason);
    }

    private async Task<string> ListProductsAsync()
    {
        return await _toolService.ListProductsAsync();
    }
}
