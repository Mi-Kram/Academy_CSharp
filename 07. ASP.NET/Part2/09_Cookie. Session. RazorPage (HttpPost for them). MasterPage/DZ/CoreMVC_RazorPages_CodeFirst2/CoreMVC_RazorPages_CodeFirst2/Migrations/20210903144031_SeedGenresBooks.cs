using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC_RazorPages_CodeFirst2.Migrations
{
    public partial class SeedGenresBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (1, N'Comedy')
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (2, N'Drama')
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (3, N'Roman')
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (4, N'Sci-fi')
INSERT INTO [dbo].[MyGenre] ([Id], [Name]) VALUES (5, N'Novell')
            ");

            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[MyBook] ON
INSERT INTO [dbo].[MyBook] ([Id], [Title], [Author], [GenreId], [PubDate], [Price]) VALUES (1, N'Капитанская дочка', N'Пушкин', 3, N'2021-09-03 17:39:00', 123)
SET IDENTITY_INSERT [dbo].[MyBook] OFF
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
