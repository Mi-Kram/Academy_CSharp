using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC_CodeFirst2.Migrations
{
    public partial class SeedGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (1, N'Poetry')
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (2, N'Fairy-tales')
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (3, N'Sci-fi')
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (4, N'Detective')
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (5, N'Novell')
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (6, N'Roman')
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (7, N'Comedy')
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
