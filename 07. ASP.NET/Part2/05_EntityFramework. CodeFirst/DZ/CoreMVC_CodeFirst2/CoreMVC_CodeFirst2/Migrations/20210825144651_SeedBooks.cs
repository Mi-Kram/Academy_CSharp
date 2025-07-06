using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreMVC_CodeFirst2.Migrations
{
    public partial class SeedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[MyBook] ON
INSERT INTO [dbo].[MyBook] ([Id], [Title], [Author], [GenreId], [PubDate], [Price]) VALUES (2, N'Сказки', N'Пушкин', 2, N'2021-08-04 17:45:00', 12)
INSERT INTO [dbo].[MyBook] ([Id], [Title], [Author], [GenreId], [PubDate], [Price]) VALUES (3, N'Лошадиная фамилия', N'Чехов', 7, N'2021-08-23 17:46:00', 234)
SET IDENTITY_INSERT [dbo].[MyBook] OFF
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
