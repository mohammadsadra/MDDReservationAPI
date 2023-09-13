using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDDReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchoolEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Schools",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 13, 7, 50, 35, 479, DateTimeKind.Utc).AddTicks(4551));

            migrationBuilder.CreateIndex(
                name: "IX_Schools_ManagerId",
                table: "Schools",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Users_ManagerId",
                table: "Schools",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Users_ManagerId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_ManagerId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Schools");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 12, 11, 6, 17, 154, DateTimeKind.Utc).AddTicks(6772));
        }
    }
}
