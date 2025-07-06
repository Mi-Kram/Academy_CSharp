using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC_CodeFirst2.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyGenre",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyGenre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    GenreId = table.Column<byte>(type: "tinyint", nullable: true),
                    PubDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyBook_MyGenre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "MyGenre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyBook_GenreId",
                table: "MyBook",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyBook");

            migrationBuilder.DropTable(
                name: "MyGenre");
        }
    }
}
