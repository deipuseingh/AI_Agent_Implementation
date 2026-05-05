namespace ECommerceApi.DTOs;

public class OrderStatusDto
{
    public int OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}

public class CreateOrderDto
{
    public int UserId { get; set; }
    public List<OrderItemRequestDto> Items { get; set; } = new();
}

public class OrderItemRequestDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class ChatMessageDto
{
    public required string Role { get; set; } // "user" or "assistant"
    public required string Content { get; set; }
    public ToolCallDto? ToolCall { get; set; }
}

public class ToolCallDto
{
    public required string FunctionName { get; set; }
    public Dictionary<string, object>? Arguments { get; set; }
}

public class ChatRequestDto
{
    public required string Message { get; set; }
}

public class ChatResponseDto
{
    public required string Content { get; set; }
    public ToolCallDto? ToolCall { get; set; }
}

public class RefundRequestDto
{
    public string? Reason { get; set; }
}

public class ActionMenuDto
{
    public required string GreetingText { get; set; }
    public List<string> Options { get; set; } = new();
}
