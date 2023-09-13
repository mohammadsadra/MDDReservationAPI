using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDDReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNationalIdToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "Users",
                type: "TEXT",
                maxLength: 30,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 13, 9, 31, 57, 427, DateTimeKind.Utc).AddTicks(3045));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 13, 7, 52, 36, 43, DateTimeKind.Utc).AddTicks(4242));
        }
    }
}
