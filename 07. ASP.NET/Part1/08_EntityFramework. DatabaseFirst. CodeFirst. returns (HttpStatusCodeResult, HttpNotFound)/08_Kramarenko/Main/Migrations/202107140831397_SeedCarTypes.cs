namespace Main.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class SeedCarTypes : DbMigration
  {
    public override void Up()
    {

      Sql(@"
SET IDENTITY_INSERT [dbo].[carTypes] ON
INSERT INTO [dbo].[carTypes] ([typeID], [value]) VALUES (1, N'Sedan')
INSERT INTO [dbo].[carTypes] ([typeID], [value]) VALUES (2, N'PickUp')
INSERT INTO [dbo].[carTypes] ([typeID], [value]) VALUES (3, N'Convertible')
INSERT INTO [dbo].[carTypes] ([typeID], [value]) VALUES (4, N'Roadster')
SET IDENTITY_INSERT [dbo].[carTypes] OFF");
    }

    public override void Down()
    {
    }
  }
}
