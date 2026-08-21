using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AshleyNashaePhotography.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotoSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoSessions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoSessions");
        }
    }
}
