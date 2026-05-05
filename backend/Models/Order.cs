using System.Text.Json.Serialization;

namespace ECommerceApi.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Processing"; // Processing, Shipped, Delivered, Canceled, Returned
    public decimal TotalAmount { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public DateTime? CanceledDate { get; set; }
    public DateTime? LastModified { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [JsonIgnore]
    public virtual User? User { get; set; }

    [JsonIgnore]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
