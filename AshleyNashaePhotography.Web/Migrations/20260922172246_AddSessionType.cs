using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AshleyNashaePhotography.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionTypeId",
                table: "PhotoSessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SessionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSessions_SessionTypeId",
                table: "PhotoSessions",
                column: "SessionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_SessionTypes_SessionTypeId",
                table: "PhotoSessions",
                column: "SessionTypeId",
                principalTable: "SessionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_SessionTypes_SessionTypeId",
                table: "PhotoSessions");

            migrationBuilder.DropTable(
                name: "SessionTypes");

            migrationBuilder.DropIndex(
                name: "IX_PhotoSessions_SessionTypeId",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "SessionTypeId",
                table: "PhotoSessions");
        }
    }
}
