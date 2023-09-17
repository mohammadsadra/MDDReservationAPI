using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDDReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class addClassIdToSchoolClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolsClasses_Schools_SchoolId",
                table: "SchoolsClasses");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "SchoolsClasses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 17, 6, 36, 33, 224, DateTimeKind.Utc).AddTicks(7677));

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolsClasses_Schools_SchoolId",
                table: "SchoolsClasses",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolsClasses_Schools_SchoolId",
                table: "SchoolsClasses");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "SchoolsClasses",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 17, 6, 24, 26, 102, DateTimeKind.Utc).AddTicks(9907));

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolsClasses_Schools_SchoolId",
                table: "SchoolsClasses",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id");
        }
    }
}
