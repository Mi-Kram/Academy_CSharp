using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class SeedSiteThemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[SiteThemas] ON
INSERT INTO [dbo].[SiteThemas] ([ID], [Background], [Name]) VALUES (1, N'#fff', N'Default')
INSERT INTO [dbo].[SiteThemas] ([ID], [Background], [Name]) VALUES (2, N'/Images/Themas/2.jpg', N'Thema1')
INSERT INTO [dbo].[SiteThemas] ([ID], [Background], [Name]) VALUES (3, N'/Images/Themas/3.jpg', N'Thema2')
SET IDENTITY_INSERT [dbo].[SiteThemas] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
