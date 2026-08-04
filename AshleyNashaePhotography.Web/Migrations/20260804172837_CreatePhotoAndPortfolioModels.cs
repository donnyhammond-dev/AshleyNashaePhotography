using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AshleyNashaePhotography.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreatePhotoAndPortfolioModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomePageSettings");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortfolioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeroImageId = table.Column<int>(type: "int", nullable: true),
                    Quote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portfolios_Photos_HeroImageId",
                        column: x => x.HeroImageId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PortfolioId",
                table: "Photos",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_HeroImageId",
                table: "Portfolios",
                column: "HeroImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Portfolios_PortfolioId",
                table: "Photos",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Portfolios_PortfolioId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.CreateTable(
                name: "HomePageSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroHeading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeroImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeroSubheading = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageSettings", x => x.Id);
                });
        }
    }
}
