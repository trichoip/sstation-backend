using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipperStation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelDefaultPricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "DefaultPricings",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "DefaultPricings",
                newName: "UnitDuration");

            migrationBuilder.RenameColumn(
                name: "FromDate",
                table: "DefaultPricings",
                newName: "EndTime");

            migrationBuilder.AddColumn<double>(
                name: "PricePerUnit",
                table: "DefaultPricings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "DefaultPricings");

            migrationBuilder.RenameColumn(
                name: "UnitDuration",
                table: "DefaultPricings",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "DefaultPricings",
                newName: "ToDate");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "DefaultPricings",
                newName: "FromDate");
        }
    }
}
