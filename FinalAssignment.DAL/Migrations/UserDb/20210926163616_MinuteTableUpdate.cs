using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalAssignment.DAL.Migrations.UserDb
{
    public partial class MinuteTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Minutes",
                nullable: true,
                defaultValue: new DateTime(2021, 9, 26, 22, 6, 15, 890, DateTimeKind.Local).AddTicks(5793),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 26, 21, 52, 27, 873, DateTimeKind.Local).AddTicks(1858));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Employees",
                nullable: true,
                defaultValue: new DateTime(2021, 9, 26, 22, 6, 15, 889, DateTimeKind.Local).AddTicks(7427),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 26, 21, 52, 27, 872, DateTimeKind.Local).AddTicks(3343));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Crews",
                nullable: true,
                defaultValue: new DateTime(2021, 9, 26, 22, 6, 15, 890, DateTimeKind.Local).AddTicks(2684),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 26, 21, 52, 27, 872, DateTimeKind.Local).AddTicks(8420));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Minutes",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 9, 26, 21, 52, 27, 873, DateTimeKind.Local).AddTicks(1858),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 26, 22, 6, 15, 890, DateTimeKind.Local).AddTicks(5793));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Employees",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 9, 26, 21, 52, 27, 872, DateTimeKind.Local).AddTicks(3343),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 26, 22, 6, 15, 889, DateTimeKind.Local).AddTicks(7427));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Crews",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 9, 26, 21, 52, 27, 872, DateTimeKind.Local).AddTicks(8420),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 9, 26, 22, 6, 15, 890, DateTimeKind.Local).AddTicks(2684));
        }
    }
}
