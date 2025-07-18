namespace MVC_CodeFirst3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedBooks : DbMigration
    {
        public override void Up()
        {
            Sql(@"
SET IDENTITY_INSERT[dbo].[MyBook] ON
INSERT INTO[dbo].[MyBook]([Id], [Title], [Author], [GenreId], [PubDate], [Price], [Pages]) VALUES(1, N'Сказки', N'Nekrasov', 2, N'2021-07-13 00:00:00', 12, 123)
INSERT INTO[dbo].[MyBook]([Id], [Title], [Author], [GenreId], [PubDate], [Price], [Pages]) VALUES(2, N'Лошадиная фамилия', N'Чехов', 5, N'2021-07-13 00:00:00', 234, 34)
SET IDENTITY_INSERT[dbo].[MyBook] OFF
                ");
        }
        
        public override void Down()
        {
        }
    }
}
