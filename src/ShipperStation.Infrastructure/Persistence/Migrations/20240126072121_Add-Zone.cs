using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipperStation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racks_Stations_StationId",
                table: "Racks");

            migrationBuilder.RenameColumn(
                name: "StationId",
                table: "Racks",
                newName: "ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Racks_StationId",
                table: "Racks",
                newName: "IX_Racks_ZoneId");

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_StationId",
                table: "Zones",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Racks_Zones_ZoneId",
                table: "Racks",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racks_Zones_ZoneId",
                table: "Racks");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.RenameColumn(
                name: "ZoneId",
                table: "Racks",
                newName: "StationId");

            migrationBuilder.RenameIndex(
                name: "IX_Racks_ZoneId",
                table: "Racks",
                newName: "IX_Racks_StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Racks_Stations_StationId",
                table: "Racks",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
