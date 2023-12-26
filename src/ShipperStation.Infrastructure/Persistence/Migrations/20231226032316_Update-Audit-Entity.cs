using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipperStation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Zones",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Zones",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Zones",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Zones",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Zones",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Zones",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhone",
                table: "Stations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Stations",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Stations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Stations",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Stations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "Stations",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Stations",
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
                table: "DataImages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DataImages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "DataImages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "DataImages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedAt",
                table: "DataImages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "DataImages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Stations");

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
                table: "DataImages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DataImages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DataImages");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "DataImages");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "DataImages");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DataImages");

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "ContactPhone",
                keyValue: null,
                column: "ContactPhone",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPhone",
                table: "Stations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
