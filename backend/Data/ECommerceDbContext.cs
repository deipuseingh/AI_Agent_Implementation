using Microsoft.EntityFrameworkCore;
using ECommerceApi.Models;

namespace ECommerceApi.Data;

public class ECommerceDbContext : DbContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed dummy users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Email = "john@example.com", Name = "John Doe" },
            new User { Id = 2, Email = "jane@example.com", Name = "Jane Smith" }
        );

        // Seed dummy products
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop Pro", Description = "High-performance laptop", Price = 1299.99m, StockQuantity = 10, ImageUrl = "/images/laptop.jpg" },
            new Product { Id = 2, Name = "Wireless Mouse", Description = "Ergonomic wireless mouse", Price = 29.99m, StockQuantity = 50, ImageUrl = "/images/wireless_mouse.jpg" },
            new Product { Id = 3, Name = "USB-C Cable", Description = "Fast charging USB-C cable", Price = 9.99m, StockQuantity = 100, ImageUrl = "/images/usb_cable.jpg" },
            new Product { Id = 4, Name = "Monitor 4K", Description = "27-inch 4K monitor", Price = 449.99m, StockQuantity = 8, ImageUrl = "/images/monitor.jpg" },
            new Product { Id = 5, Name = "Mechanical Keyboard", Description = "RGB mechanical gaming keyboard", Price = 149.99m, StockQuantity = 25, ImageUrl = "/images/mechanical_keyboard.jpg" },
            new Product { Id = 6, Name = "Webcam HD", Description = "1080p HD webcam with mic", Price = 79.99m, StockQuantity = 15, ImageUrl = "/images/webcam.jpg" },
            new Product { Id = 7, Name = "Desk Lamp", Description = "LED desk lamp with USB charging", Price = 39.99m, StockQuantity = 30, ImageUrl = "/images/desk_lamp.jpg" },
            new Product { Id = 8, Name = "Phone Stand", Description = "Adjustable phone stand", Price = 14.99m, StockQuantity = 60, ImageUrl = "/images/phone_stand.jpg" },
            new Product { Id = 9, Name = "Portable SSD", Description = "1TB portable SSD", Price = 129.99m, StockQuantity = 20, ImageUrl = "/images/ssd.jpg" },
            new Product { Id = 10, Name = "Headphones", Description = "Noise-cancelling wireless headphones", Price = 199.99m, StockQuantity = 12, ImageUrl = "/images/headphone.jpg" }
        };
        modelBuilder.Entity<Product>().HasData(products);

        // Seed dummy orders
        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, UserId = 1, OrderDate = DateTime.UtcNow.AddDays(-5), Status = "Shipped", TotalAmount = 1339.98m, ShippedDate = DateTime.UtcNow.AddDays(-4) },
            new Order { Id = 2, UserId = 2, OrderDate = DateTime.UtcNow.AddDays(-10), Status = "Delivered", TotalAmount = 449.99m, ShippedDate = DateTime.UtcNow.AddDays(-9), DeliveredDate = DateTime.UtcNow.AddDays(-2) }
        );

        // Seed order items
        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 1299.99m },
            new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 29.99m },
            new OrderItem { Id = 3, OrderId = 2, ProductId = 4, Quantity = 1, UnitPrice = 449.99m }
        );
    }
}
