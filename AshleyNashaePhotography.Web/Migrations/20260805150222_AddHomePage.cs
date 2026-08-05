using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AshleyNashaePhotography.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddHomePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroWildId = table.Column<int>(type: "int", nullable: true),
                    HeroLoveId = table.Column<int>(type: "int", nullable: true),
                    HeroCelebrationId = table.Column<int>(type: "int", nullable: true),
                    HeroQuote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePages_Photos_HeroCelebrationId",
                        column: x => x.HeroCelebrationId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HomePages_Photos_HeroLoveId",
                        column: x => x.HeroLoveId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HomePages_Photos_HeroWildId",
                        column: x => x.HeroWildId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_HeroCelebrationId",
                table: "HomePages",
                column: "HeroCelebrationId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_HeroLoveId",
                table: "HomePages",
                column: "HeroLoveId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_HeroWildId",
                table: "HomePages",
                column: "HeroWildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomePages");
        }
    }
}
