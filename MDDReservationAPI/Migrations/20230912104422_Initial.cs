using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDDReservationAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    SchoolType = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", maxLength: 2, nullable: true),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolsClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Grade = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: false),
                    IsProgrammer = table.Column<bool>(type: "INTEGER", maxLength: 10, nullable: false),
                    ProgrammingLanguage = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", maxLength: 2, nullable: true),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: false),
                    SchoolId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolsClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolsClasses_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IsVerify = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Position = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    SchoolId = table.Column<int>(type: "INTEGER", nullable: true),
                    SchoolClassId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_SchoolsClasses_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalTable: "SchoolsClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ManagerId = table.Column<int>(type: "INTEGER", nullable: false),
                    SchoolId = table.Column<int>(type: "INTEGER", nullable: false),
                    SchoolClassId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationForms_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistrationForms_SchoolsClasses_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalTable: "SchoolsClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrationForms_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrationForms_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Discriminator", "Email", "IsVerify", "Name", "Password", "Phone", "Role" },
                values: new object[] { 1, new DateTime(2023, 9, 12, 10, 44, 22, 103, DateTimeKind.Utc).AddTicks(4845), "Admin", "mohammadsadrahaeri@gmail.com", true, "MohammadSadra Haeri", null, "09127959211", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationForms_ManagerId",
                table: "RegistrationForms",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationForms_ProjectId",
                table: "RegistrationForms",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationForms_SchoolClassId",
                table: "RegistrationForms",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationForms_SchoolId",
                table: "RegistrationForms",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolsClasses_SchoolId",
                table: "SchoolsClasses",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectId",
                table: "Users",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SchoolClassId",
                table: "Users",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SchoolId",
                table: "Users",
                column: "SchoolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationForms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "SchoolsClasses");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
