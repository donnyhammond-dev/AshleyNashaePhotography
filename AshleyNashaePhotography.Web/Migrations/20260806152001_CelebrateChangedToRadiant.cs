using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AshleyNashaePhotography.Web.Migrations
{
    /// <inheritdoc />
    public partial class CelebrateChangedToRadiant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_Photos_HeroCelebrationId",
                table: "HomePages");

            migrationBuilder.RenameColumn(
                name: "HeroCelebrationId",
                table: "HomePages",
                newName: "HeroRadiantId");

            migrationBuilder.RenameIndex(
                name: "IX_HomePages_HeroCelebrationId",
                table: "HomePages",
                newName: "IX_HomePages_HeroRadiantId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_Photos_HeroRadiantId",
                table: "HomePages",
                column: "HeroRadiantId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_Photos_HeroRadiantId",
                table: "HomePages");

            migrationBuilder.RenameColumn(
                name: "HeroRadiantId",
                table: "HomePages",
                newName: "HeroCelebrationId");

            migrationBuilder.RenameIndex(
                name: "IX_HomePages_HeroRadiantId",
                table: "HomePages",
                newName: "IX_HomePages_HeroCelebrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_Photos_HeroCelebrationId",
                table: "HomePages",
                column: "HeroCelebrationId",
                principalTable: "Photos",
                principalColumn: "Id");
        }
    }
}
