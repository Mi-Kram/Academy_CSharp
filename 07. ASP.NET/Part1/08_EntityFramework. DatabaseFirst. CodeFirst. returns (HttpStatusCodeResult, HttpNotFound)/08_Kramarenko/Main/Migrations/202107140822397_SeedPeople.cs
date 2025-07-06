namespace Main.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class SeedPeople : DbMigration
  {
    public override void Up()
    {
      Sql(@"
INSERT INTO [dbo].[people] ([login], [password], [isAdmin]) VALUES (N'Ivan', N'M/ZLkc/v4dWFNKFHQsmEQ6PKLlAaxcGMUiRo2ChXkcE+ZxJCWeFbFtgjss+Wr+NN7TI0wNZx5VA3pg7gZ1N4HA==', 1)
INSERT INTO [dbo].[people] ([login], [password], [isAdmin]) VALUES (N'Petr', N'M/ZLkc/v4dWFNKFHQsmEQ6PKLlAaxcGMUiRo2ChXkcE+ZxJCWeFbFtgjss+Wr+NN7TI0wNZx5VA3pg7gZ1N4HA==', 0)
INSERT INTO [dbo].[people] ([login], [password], [isAdmin]) VALUES (N'Anna', N'M/ZLkc/v4dWFNKFHQsmEQ6PKLlAaxcGMUiRo2ChXkcE+ZxJCWeFbFtgjss+Wr+NN7TI0wNZx5VA3pg7gZ1N4HA==', 1)
INSERT INTO [dbo].[people] ([login], [password], [isAdmin]) VALUES (N'Lisa', N'M/ZLkc/v4dWFNKFHQsmEQ6PKLlAaxcGMUiRo2ChXkcE+ZxJCWeFbFtgjss+Wr+NN7TI0wNZx5VA3pg7gZ1N4HA==', 0)
INSERT INTO [dbo].[people] ([login], [password], [isAdmin]) VALUES (N'Roma', N'M/ZLkc/v4dWFNKFHQsmEQ6PKLlAaxcGMUiRo2ChXkcE+ZxJCWeFbFtgjss+Wr+NN7TI0wNZx5VA3pg7gZ1N4HA==', 0)");
    }

    public override void Down()
    {
    }
  }
}
