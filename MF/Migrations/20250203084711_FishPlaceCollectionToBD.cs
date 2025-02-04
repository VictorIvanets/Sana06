using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MF.Migrations
{
    /// <inheritdoc />
    public partial class FishPlaceCollectionToBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FishPlaceCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    FishesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishPlaceCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FishPlaceCollection_Fishes_FishesId",
                        column: x => x.FishesId,
                        principalTable: "Fishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FishPlaceCollection_PlaceDb_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "PlaceDb",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FishPlaceCollection_FishesId",
                table: "FishPlaceCollection",
                column: "FishesId");

            migrationBuilder.CreateIndex(
                name: "IX_FishPlaceCollection_PlaceId",
                table: "FishPlaceCollection",
                column: "PlaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FishPlaceCollection");
        }
    }
}
