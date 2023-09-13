using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDDReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNationalIdToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "SchoolsClasses");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "NationalId" },
                values: new object[] { new DateTime(2023, 9, 13, 9, 46, 35, 85, DateTimeKind.Utc).AddTicks(9803), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "SchoolsClasses",
                type: "INTEGER",
                maxLength: 2,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 13, 9, 31, 57, 427, DateTimeKind.Utc).AddTicks(3045));
        }
    }
}
