using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AshleyNashaePhotography.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAboutMeToHomePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMeDescription",
                table: "HomePages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AboutMePhotoId",
                table: "HomePages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomePages_AboutMePhotoId",
                table: "HomePages",
                column: "AboutMePhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomePages_Photos_AboutMePhotoId",
                table: "HomePages",
                column: "AboutMePhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomePages_Photos_AboutMePhotoId",
                table: "HomePages");

            migrationBuilder.DropIndex(
                name: "IX_HomePages_AboutMePhotoId",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "AboutMeDescription",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "AboutMePhotoId",
                table: "HomePages");
        }
    }
}
