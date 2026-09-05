using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApplication.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBookModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 8, 15, 18, 54, 18, 953, DateTimeKind.Local).AddTicks(8935));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 8, 17, 18, 54, 18, 953, DateTimeKind.Local).AddTicks(8984));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 8, 15, 18, 35, 16, 857, DateTimeKind.Local).AddTicks(3243));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 8, 17, 18, 35, 16, 857, DateTimeKind.Local).AddTicks(3312));
        }
    }
}
