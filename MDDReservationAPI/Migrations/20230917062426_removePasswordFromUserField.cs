using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDDReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class removePasswordFromUserField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 17, 6, 24, 26, 102, DateTimeKind.Utc).AddTicks(9907));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 9, 13, 9, 46, 35, 85, DateTimeKind.Utc).AddTicks(9803), null });
        }
    }
}
