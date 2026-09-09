using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AshleyNashaePhotography.Web.Migrations
{
    /// <inheritdoc />
    public partial class RefactorHomePagePortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_Photos_HeroLoveId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_Photos_HeroRadiantId",
                table: "HomePages");

            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_Photos_HeroWildId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_HeroLoveId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_HeroRadiantId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_HeroWildId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "HeroLoveId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "HeroRadiantId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "HeroWildId",
                table: "HomePages");

            migrationBuilder.AddColumn<int>(
                name: "HomePageHeroImageId",
                table: "Portfolios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_HomePageHeroImageId",
                table: "Portfolios",
                column: "HomePageHeroImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Photos_HomePageHeroImageId",
                table: "Portfolios",
                column: "HomePageHeroImageId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Photos_HomePageHeroImageId",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_HomePageHeroImageId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "HomePageHeroImageId",
                table: "Portfolios");

            migrationBuilder.AddColumn<int>(
                name: "HeroLoveId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeroRadiantId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeroWildId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_HeroLoveId",
                table: "HomePages",
                column: "HeroLoveId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_HeroRadiantId",
                table: "HomePages",
                column: "HeroRadiantId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_HeroWildId",
                table: "HomePages",
                column: "HeroWildId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_Photos_HeroLoveId",
                table: "HomePages",
                column: "HeroLoveId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_Photos_HeroRadiantId",
                table: "HomePages",
                column: "HeroRadiantId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_Photos_HeroWildId",
                table: "HomePages",
                column: "HeroWildId",
                principalTable: "Photos",
                principalColumn: "Id");
        }
    }
}
