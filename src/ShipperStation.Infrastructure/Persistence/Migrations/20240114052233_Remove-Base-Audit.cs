using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipperStation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBaseAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StationSettings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "StationSettings");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "StationSettings");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "StationSettings");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "StationSettings");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "StationSettings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StationImages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "StationImages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "StationImages");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "StationImages");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "StationImages");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "StationImages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Racks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Racks");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Racks");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Racks");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Racks");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Racks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PackageImages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PackageImages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PackageImages");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PackageImages");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "PackageImages");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PackageImages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Devices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "StationSettings",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "StationSettings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "StationSettings",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "StationSettings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "StationSettings",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "StationSettings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "StationImages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "StationImages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "StationImages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "StationImages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "StationImages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "StationImages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Slots",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Slots",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Slots",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Slots",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Slots",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Slots",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Shelves",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Shelves",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Shelves",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Shelves",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Shelves",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Shelves",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Settings",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Settings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Settings",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Settings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Settings",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Settings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Racks",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Racks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Racks",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Racks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Racks",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Racks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "PackageImages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PackageImages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "PackageImages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PackageImages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "PackageImages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PackageImages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Devices",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Devices",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Devices",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Devices",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Devices",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Devices",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
