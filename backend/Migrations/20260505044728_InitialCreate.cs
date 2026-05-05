using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CanceledDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7367), "High-performance laptop", null, "Laptop Pro", 1299.99m, 10 },
                    { 2, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7372), "Ergonomic wireless mouse", null, "Wireless Mouse", 29.99m, 50 },
                    { 3, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7373), "Fast charging USB-C cable", null, "USB-C Cable", 9.99m, 100 },
                    { 4, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7374), "27-inch 4K monitor", null, "Monitor 4K", 449.99m, 8 },
                    { 5, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7375), "RGB mechanical gaming keyboard", null, "Mechanical Keyboard", 149.99m, 25 },
                    { 6, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7383), "1080p HD webcam with mic", null, "Webcam HD", 79.99m, 15 },
                    { 7, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7385), "LED desk lamp with USB charging", null, "Desk Lamp", 39.99m, 30 },
                    { 8, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7386), "Adjustable phone stand", null, "Phone Stand", 14.99m, 60 },
                    { 9, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7387), "1TB portable SSD", null, "Portable SSD", 129.99m, 20 },
                    { 10, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7389), "Noise-cancelling wireless headphones", null, "Headphones", 199.99m, 12 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7269), "john@example.com", "John Doe", null },
                    { 2, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7272), "jane@example.com", "Jane Smith", null }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CanceledDate", "DeliveredDate", "LastModified", "OrderDate", "ShippedDate", "Status", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7423), new DateTime(2026, 4, 30, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7426), new DateTime(2026, 5, 1, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7430), "Shipped", 1339.98m, 1 },
                    { 2, null, new DateTime(2026, 5, 3, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7435), new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7432), new DateTime(2026, 4, 25, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7433), new DateTime(2026, 4, 26, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7434), "Delivered", 449.99m, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1299.99m },
                    { 2, 1, 2, 1, 29.99m },
                    { 3, 2, 4, 1, 449.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
