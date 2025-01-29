using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MF.Migrations
{
    /// <inheritdoc />
    public partial class initModelsPart1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coords",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypesFish",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesFish", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceDb",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceDb", x => x.id);
                    table.ForeignKey(
                        name: "FK_PlaceDb_Coords_CoordsId",
                        column: x => x.CoordsId,
                        principalTable: "Coords",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fishes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypesFishId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fishes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Fishes_TypesFish_TypesFishId",
                        column: x => x.TypesFishId,
                        principalTable: "TypesFish",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceFishCollection",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FishesId = table.Column<int>(type: "int", nullable: false),
                    PlaceDbId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceFishCollection", x => x.id);
                    table.ForeignKey(
                        name: "FK_PlaceFishCollection_Fishes_FishesId",
                        column: x => x.FishesId,
                        principalTable: "Fishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceFishCollection_PlaceDb_PlaceDbId",
                        column: x => x.PlaceDbId,
                        principalTable: "PlaceDb",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fishes_TypesFishId",
                table: "Fishes",
                column: "TypesFishId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceDb_CoordsId",
                table: "PlaceDb",
                column: "CoordsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceFishCollection_FishesId",
                table: "PlaceFishCollection",
                column: "FishesId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceFishCollection_PlaceDbId",
                table: "PlaceFishCollection",
                column: "PlaceDbId");

            migrationBuilder.CreateIndex(
                name: "IX_User_roleId",
                table: "User",
                column: "roleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceFishCollection");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Fishes");

            migrationBuilder.DropTable(
                name: "PlaceDb");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TypesFish");

            migrationBuilder.DropTable(
                name: "Coords");
        }
    }
}
