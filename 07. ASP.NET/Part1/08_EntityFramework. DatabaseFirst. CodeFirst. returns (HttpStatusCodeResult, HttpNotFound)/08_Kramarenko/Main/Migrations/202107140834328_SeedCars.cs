namespace Main.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class SeedCars : DbMigration
  {
    public override void Up()
    {
      Sql(@"
SET IDENTITY_INSERT [dbo].[cars] ON
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (1, N'BMW', N'A', 310, 11000, N'2010-01-01 00:00:00', 1, N'Images/Cars/1.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (2, N'Mercedes', N'A', 300, 10000, N'2006-01-01 00:00:00', 2, N'Images/Cars/2.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (3, N'Toyota', N'A', 240, 7000, N'1996-01-01 00:00:00', 3, N'Images/Cars/3.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (4, N'Volkswagen', N'B', 215, 8000, N'1995-01-01 00:00:00', 1, N'Images/Cars/4.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (5, N'Porsche', N'B', 365, 25000, N'2018-01-01 00:00:00', 2, N'Images/Cars/5.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (6, N'Honda', N'B', 280, 12000, N'2009-01-01 00:00:00', 3, N'Images/Cars/6.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (7, N'Ford', N'C', 220, 11000, N'2001-01-01 00:00:00', 1, N'Images/Cars/7.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (8, N'Nissan', N'C', 210, 6000, N'1986-01-01 00:00:00', 2, N'Images/Cars/8.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (9, N'BMW', N'C', 330, 16000, N'2014-01-01 00:00:00', 4, N'Images/Cars/9.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (10, N'BMW', N'D', 330, 18000, N'2003-01-01 00:00:00', 1, N'Images/Cars/10.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (11, N'Ford', N'D', 190, 9000, N'1999-01-01 00:00:00', 2, N'Images/Cars/11.jpg')
INSERT INTO [dbo].[cars] ([carID], [brand], [model], [speed], [price], [date], [typeID], [imgSrc]) VALUES (12, N'Honda', N'D', 300, 10000, N'2003-01-01 00:00:00', 4, N'Images/Cars/12.jpg')
SET IDENTITY_INSERT [dbo].[cars] OFF");
    }

    public override void Down()
    {
    }
  }
}
