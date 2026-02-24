using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoyalVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villa",
                columns: new[] { "Id", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 24, 21, 22, 52, 679, DateTimeKind.Local).AddTicks(5871), "This is the Royal Villa", "", "Royal Villa", 4, 200.0, 550, null },
                    { 2, new DateTime(2026, 2, 24, 21, 22, 52, 681, DateTimeKind.Local).AddTicks(3919), "This is the Premium Pool Villa", "", "Premium Pool Villa", 4, 300.0, 550, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
