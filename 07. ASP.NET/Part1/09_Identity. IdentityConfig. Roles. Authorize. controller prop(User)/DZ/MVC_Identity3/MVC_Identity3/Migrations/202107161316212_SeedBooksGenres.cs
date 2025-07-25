namespace MVC_Identity3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedBooksGenres : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (1, N'Comedy')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (2, N'Poetry')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (3, N'Novell')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (4, N'Fairy tale')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (5, N'Detective')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (6, N'Sci-fi')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (7, N'Documentary')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (8, N'Roman')
INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (9, N'Drama')
            ");

            Sql(@"
SET IDENTITY_INSERT [dbo].[Books] ON
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [GenreId], [PubDate], [Price], [hasAudioBook]) VALUES (1, N'Сказки', N'Пушкин', 4, N'1847-06-16 00:00:00', 123, 1)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [GenreId], [PubDate], [Price], [hasAudioBook]) VALUES (2, N'Лошадиная фамилия', N'Чехов', 1, N'1914-06-16 00:00:00', 234, 1)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [GenreId], [PubDate], [Price], [hasAudioBook]) VALUES (3, N'Капитанская дочка', N'Пушкин', 3, N'1832-02-16 00:00:00', 134, 1)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [GenreId], [PubDate], [Price], [hasAudioBook]) VALUES (4, N'Сказки', N'Nekrasov', 2, N'1870-06-16 00:00:00', 345, 0)
INSERT INTO [dbo].[Books] ([Id], [Title], [Author], [GenreId], [PubDate], [Price], [hasAudioBook]) VALUES (5, N'Руслан и Людмила', N'Пушкин', 2, N'1843-06-16 00:00:00', 123, 1)
SET IDENTITY_INSERT [dbo].[Books] OFF
            ");
        }
        
        public override void Down()
        {
        }
    }
}
