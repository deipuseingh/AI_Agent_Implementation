using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(253));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(258));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(260));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(261));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(262));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(265));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(267));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(268));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 7, 6, 58, 120, DateTimeKind.Utc).AddTicks(271));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastModified", "OrderDate", "ShippedDate" },
                values: new object[] { new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7423), new DateTime(2026, 4, 30, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7426), new DateTime(2026, 5, 1, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeliveredDate", "LastModified", "OrderDate", "ShippedDate" },
                values: new object[] { new DateTime(2026, 5, 3, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7435), new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7432), new DateTime(2026, 4, 25, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7433), new DateTime(2026, 4, 26, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7434) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7367));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7372));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7373));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7374));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7375));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7383));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7385));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7386));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7387));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7389));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7269));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 5, 4, 47, 28, 252, DateTimeKind.Utc).AddTicks(7272));
        }
    }
}
