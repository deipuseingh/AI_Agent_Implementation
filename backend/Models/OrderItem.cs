using System.Text.Json.Serialization;

namespace ECommerceApi.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;

    // Navigation properties
    [JsonIgnore]
    public virtual Order? Order { get; set; }

    [JsonIgnore]
    public virtual Product? Product { get; set; }
}
