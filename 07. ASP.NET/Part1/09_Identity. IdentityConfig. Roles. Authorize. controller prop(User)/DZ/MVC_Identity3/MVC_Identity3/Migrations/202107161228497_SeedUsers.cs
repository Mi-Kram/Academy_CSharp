namespace MVC_Identity3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'118d8b49-b77b-4973-a030-52b521f364d5', N'Admin')
            ");

            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Age], [Address]) VALUES (N'072b85b6-c1e2-4e38-8ee9-99aef2bd65f0', N'anna@ya.ru', 0, N'ADyO/cmdg+XLbwX2KIw5tSVm/2sPzHkFRvgj8tWuHjuz/0XIqLwsn8cS+uUZbngo8w==', N'8c88115e-647f-485b-8fc9-8f10e50e0d6c', NULL, 0, 0, NULL, 1, 0, N'anna@ya.ru', 34, N'Donetsk')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Age], [Address]) VALUES (N'625f5a28-2ffa-4cff-a031-622211096ecd', N'admin@ya.ru', 0, N'AFx1AfXtmtLqkwPMPl4wo7m3/rsHye7coyokV/10wB6stQVkTHsy3kJ3sfNDYQQe1Q==', N'755833ca-3dea-4576-b315-9a033cdcc6c3', NULL, 0, 0, NULL, 1, 0, N'admin@ya.ru', 23, N'Donetsk')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Age], [Address]) VALUES (N'ca346559-61b9-42f2-8b0e-ec373eec2594', N'alex@ya.ru', 0, N'APEuYspU0vvPYZ035wYMNEV9qYee6SXr8EymMHiHUqDv2lYXjIV2zLN/EYBgzdKlWQ==', N'4dd3331b-2d5b-42ab-bfd7-df5eb06be77c', NULL, 0, 0, NULL, 1, 0, N'alex@ya.ru', 23, N'Donetsk')
            ");

            Sql(@"
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'625f5a28-2ffa-4cff-a031-622211096ecd', N'118d8b49-b77b-4973-a030-52b521f364d5')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
