using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipperStation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "StationSettings");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Slots");

            migrationBuilder.AddColumn<string>(
                name: "CustomValue",
                table: "StationSettings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Racks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Width = table.Column<double>(type: "double", nullable: false),
                    Height = table.Column<double>(type: "double", nullable: false),
                    Length = table.Column<double>(type: "double", nullable: false),
                    Volume = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Racks_SizeId",
                table: "Racks",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Racks_Sizes_SizeId",
                table: "Racks",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racks_Sizes_SizeId",
                table: "Racks");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Racks_SizeId",
                table: "Racks");

            migrationBuilder.DropColumn(
                name: "CustomValue",
                table: "StationSettings");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Racks");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "StationSettings",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Slots",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "Slots",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Volume",
                table: "Slots",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Slots",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
