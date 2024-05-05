using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipperStation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewDatabse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Slots_SlotId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Users_SenderId",
                table: "Packages");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Packages_SenderId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "Pricings");

            migrationBuilder.DropColumn(
                name: "PriceCod",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsCod",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "PriceCod",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "DefaultPricings");

            migrationBuilder.RenameColumn(
                name: "UnitDuration",
                table: "Pricings",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "SlotId",
                table: "Packages",
                newName: "RackId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_SlotId",
                table: "Packages",
                newName: "IX_Packages_RackId");

            migrationBuilder.RenameColumn(
                name: "UnitDuration",
                table: "DefaultPricings",
                newName: "Price");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Racks_RackId",
                table: "Packages",
                column: "RackId",
                principalTable: "Racks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Racks_RackId",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Pricings",
                newName: "UnitDuration");

            migrationBuilder.RenameColumn(
                name: "RackId",
                table: "Packages",
                newName: "SlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_RackId",
                table: "Packages",
                newName: "IX_Packages_SlotId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "DefaultPricings",
                newName: "UnitDuration");

            migrationBuilder.AddColumn<double>(
                name: "PricePerUnit",
                table: "Pricings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceCod",
                table: "Payments",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Packages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsCod",
                table: "Packages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "PriceCod",
                table: "Packages",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "Packages",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<double>(
                name: "PricePerUnit",
                table: "DefaultPricings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RackId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Height = table.Column<double>(type: "double", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Length = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Volume = table.Column<double>(type: "double", nullable: false),
                    Width = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slots_Racks_RackId",
                        column: x => x.RackId,
                        principalTable: "Racks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_SenderId",
                table: "Packages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_RackId",
                table: "Slots",
                column: "RackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Slots_SlotId",
                table: "Packages",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Users_SenderId",
                table: "Packages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
