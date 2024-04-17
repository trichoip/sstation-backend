using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipperStation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelPricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExprireReceiveGoods",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "Pricings",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Pricings",
                newName: "UnitDuration");

            migrationBuilder.RenameColumn(
                name: "FromDate",
                table: "Pricings",
                newName: "EndTime");

            migrationBuilder.AddColumn<double>(
                name: "PricePerUnit",
                table: "Pricings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Notifications",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "Pricings");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "UnitDuration",
                table: "Pricings",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Pricings",
                newName: "ToDate");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Pricings",
                newName: "FromDate");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExprireReceiveGoods",
                table: "Packages",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
