using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class SeedComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
" SET IDENTITY_INSERT [dbo].[Comments] ON" +
" INSERT INTO [dbo].[Comments] ([ID], [Content], [Date], [PictureID]) VALUES (1, N'Классный закат!!!', N'2021-08-28 22:46:11', 2)" +
" INSERT INTO [dbo].[Comments] ([ID], [Content], [Date], [PictureID]) VALUES (2, N'У этого комментария\nнесколько строк!\n:)', N'2021-08-28 22:46:36', 2)" +
" INSERT INTO [dbo].[Comments] ([ID], [Content], [Date], [PictureID]) VALUES (3, N'максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий\n(200)...', N'2021-08-28 22:48:23', 2)" +
" INSERT INTO [dbo].[Comments] ([ID], [Content], [Date], [PictureID]) VALUES (4, N'максимально длинный комментарий\nмаксимально длинный комментарий\nмаксимально длинный комментарий\nмаксимально длинный комментарий\nмаксимально длинный комментарий\nмаксимально длинный комментарий\nконец...', N'2021-08-28 22:49:12', 2)" +
" SET IDENTITY_INSERT [dbo].[Comments] OFF");






/*
SET IDENTITY_INSERT[dbo].[Comments] ON
INSERT INTO[dbo].[Comments]([ID], [Content], [Date], [PictureID]) VALUES(1, N'Классный закат!!!', N'2021-08-28 22:46:11', 2)
INSERT INTO[dbo].[Comments] ([ID], [Content], [Date], [PictureID]) VALUES(2, N'У этого комментария
несколько строк!
:)', N'2021 - 08 - 28 22:46:36', 2)
INSERT INTO[dbo].[Comments] ([ID], [Content], [Date], [PictureID]) VALUES(3, N'максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий максимально длинный комментарий
(200)...', N'2021 - 08 - 28 22:48:23', 2)
INSERT INTO[dbo].[Comments]([ID], [Content], [Date], [PictureID]) VALUES(4, N'максимально длинный комментарий
максимально длинный комментарий
максимально длинный комментарий
максимально длинный комментарий
максимально длинный комментарий
максимально длинный комментарий
конец...', N'2021 - 08 - 28 22:49:12', 2)
SET IDENTITY_INSERT[dbo].[Comments] OFF
*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
