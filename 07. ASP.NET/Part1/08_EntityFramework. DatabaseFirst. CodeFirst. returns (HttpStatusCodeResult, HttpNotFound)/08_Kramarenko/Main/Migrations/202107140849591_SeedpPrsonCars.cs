namespace Main.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class SeedpPrsonCars : DbMigration
  {
    public override void Up()
    {
      Sql(@"
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Ivan', 1)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Ivan', 2)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Ivan', 3)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Ivan', 4)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Petr', 3)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Petr', 5)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Petr', 7)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Petr', 9)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Petr', 11)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Anna', 4)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Anna', 7)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Anna', 10)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Lisa', 1)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Lisa', 2)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Lisa', 3)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Lisa', 4)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Lisa', 9)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Lisa', 10)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Roma', 5)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Roma', 6)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Roma', 7)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Roma', 8)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Roma', 9)
INSERT INTO [dbo].[personcars] ([person_login], [car_carID]) VALUES (N'Roma', 10)");
    }

    public override void Down()
    {
    }
  }
}
