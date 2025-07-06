namespace MVC_CodeFirst3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedGenres : DbMigration
    {
        public override void Up()
        {
            Sql(@"
SET IDENTITY_INSERT [dbo].[Genres] ON
INSERT INTO[dbo].[Genres]([Id], [Name]) VALUES(1, N'Poetry')
INSERT INTO[dbo].[Genres]([Id], [Name]) VALUES(2, N'Fairy tales')
INSERT INTO[dbo].[Genres]([Id], [Name]) VALUES(3, N'Detective')
INSERT INTO[dbo].[Genres]([Id], [Name]) VALUES(4, N'Novell')
INSERT INTO[dbo].[Genres]([Id], [Name]) VALUES(5, N'Comedy')
SET IDENTITY_INSERT[dbo].[Genres] OFF");
        }
        
        public override void Down()
        {
        }
    }
}
