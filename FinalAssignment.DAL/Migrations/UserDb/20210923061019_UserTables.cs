using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalAssignment.DAL.Migrations.UserDb
{
    public partial class UserTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2021, 9, 23, 11, 40, 19, 712, DateTimeKind.Local).AddTicks(9209)),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Minutes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinuteType = table.Column<int>(nullable: false),
                    Topic = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    CrewID = table.Column<int>(nullable: false),
                    ApprovalStatus = table.Column<bool>(nullable: false, defaultValue: false),
                    ApprovalHistory = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2021, 9, 23, 11, 40, 19, 713, DateTimeKind.Local).AddTicks(4546)),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minutes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Minutes_Crews_CrewID",
                        column: x => x.CrewID,
                        principalTable: "Crews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    EmployeeType = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true, defaultValue: new DateTime(2021, 9, 23, 11, 40, 19, 712, DateTimeKind.Local).AddTicks(3815)),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValue: true),
                    SupervisorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Minutes_SupervisorID",
                        column: x => x.SupervisorID,
                        principalTable: "Minutes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supervisor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorID = table.Column<int>(nullable: false),
                    MinuteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Supervisor_Minutes_MinuteID",
                        column: x => x.MinuteID,
                        principalTable: "Minutes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SupervisorID",
                table: "Employees",
                column: "SupervisorID");

            migrationBuilder.CreateIndex(
                name: "IX_Minutes_CrewID",
                table: "Minutes",
                column: "CrewID");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_MinuteID",
                table: "Supervisor",
                column: "MinuteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Supervisor");

            migrationBuilder.DropTable(
                name: "Minutes");

            migrationBuilder.DropTable(
                name: "Crews");
        }
    }
}
