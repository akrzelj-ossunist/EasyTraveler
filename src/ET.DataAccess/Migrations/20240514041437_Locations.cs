using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Locations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Route",
                newName: "Status");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Route",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Route",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation",
                table: "Bus",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "CurrentLocation",
                table: "Bus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Route",
                newName: "status");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "Route",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
