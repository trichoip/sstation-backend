using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipperStation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNullBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "Stations",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "Stations",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
