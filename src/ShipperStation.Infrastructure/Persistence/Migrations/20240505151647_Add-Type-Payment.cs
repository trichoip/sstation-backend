using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipperStation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTypePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Payments",
                type: "nvarchar(24)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Payments");
        }
    }
}
