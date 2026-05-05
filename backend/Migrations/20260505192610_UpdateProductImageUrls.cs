using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductImageUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastModified", "OrderDate", "ShippedDate" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(4260), new DateTime(2026, 4, 30, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(4264), new DateTime(2026, 5, 1, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(4273) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveredDate", "LastModified", "OrderDate", "ShippedDate" },
                values: new object[] { new DateTime(2026, 5, 3, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(4281), new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(4278), new DateTime(2026, 4, 25, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(4279), new DateTime(2026, 4, 26, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(4280) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3893), "/images/laptop.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3906), "/images/wireless_mouse.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3908), "/images/usb_cable.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3909), "/images/monitor.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3910), "/images/mechanical_keyboard.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3917), "/images/webcam.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3918), "/images/desk_lamp.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3919), "/images/phone_stand.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3920), "/images/ssd.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3922), "/images/headphone.jpg" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3278));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 19, 26, 10, 466, DateTimeKind.Utc).AddTicks(3282));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastModified", "OrderDate", "ShippedDate" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(303), new DateTime(2026, 4, 30, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(306), new DateTime(2026, 5, 1, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(311) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveredDate", "LastModified", "OrderDate", "ShippedDate" },
                values: new object[] { new DateTime(2026, 5, 3, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(315), new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(313), new DateTime(2026, 4, 25, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(314), new DateTime(2026, 4, 26, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(315) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(253), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(258), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(260), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(261), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(262), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(265), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(266), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(267), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(268), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(271), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(145));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(150));
        }
    }
}
