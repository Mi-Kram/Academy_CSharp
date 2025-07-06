namespace Main.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class SeedUsersProjects : DbMigration
  {
    public override void Up()
    {
      Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [ImgSrc], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Age], [Address]) VALUES (N'4fe5e894-b33e-4ee1-9808-26a679cb6d7c', N'Images/Users/4fe5e894-b33e-4ee1-9808-26a679cb6d7c.jpg', N'Petr', 0, N'AAGySl4KC+NYaNW1QepshQ04dVxMI1Oa3FD4IsOXZVXtT/lSyC1XkkysCKYzrxGmXw==', N'354dde3b-7c85-466a-9540-2777d2db9c4b', NULL, 0, 0, NULL, 1, 0, N'Petr', 46, N'Petr address')
INSERT INTO [dbo].[AspNetUsers] ([Id], [ImgSrc], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Age], [Address]) VALUES (N'6c45bdd5-d4cf-4f78-a353-919ca93e8fc8', N'Images/Users/6c45bdd5-d4cf-4f78-a353-919ca93e8fc8.jpg', N'Julia', 0, N'AGcDFhVbjSK/wnEB02z90iR/1aD84LBW+FWsn10286Vz69SX8R4yYwYsD2HBaxuofw==', N'daa02c71-fa59-4a71-91c1-c26e78c405c8', NULL, 0, 0, NULL, 1, 0, N'Julia', 26, N'Julia address')
INSERT INTO [dbo].[AspNetUsers] ([Id], [ImgSrc], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Age], [Address]) VALUES (N'8860a7cc-299d-486f-b35c-673da4f218c1', N'Images/Users/8860a7cc-299d-486f-b35c-673da4f218c1.jpg', N'Ivan', 0, N'AB4E1waNUiAVlpNMP9BmddsZ6YIg10zeZ8gsJfNf9x9++Q8doyLFlL1qMg8lCbyOzg==', N'6dc79e3c-e12c-4eab-8a40-ba191972333e', NULL, 0, 0, NULL, 1, 0, N'Ivan', 25, N'Ivan address')
INSERT INTO [dbo].[AspNetUsers] ([Id], [ImgSrc], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Age], [Address]) VALUES (N'b29b69b9-0753-47f4-8f96-8adf3bb7e782', N'Images/Users/b29b69b9-0753-47f4-8f96-8adf3bb7e782.jpg', N'Roma', 0, N'APct9qXEk69Gss2MMqZHLtOQONP1iAR6ezXBtGxlJ7ytrDAW4RdUthSCt0MuB+tARg==', N'a79299ff-6ed8-47c5-b589-c73205fa8a1b', NULL, 0, 0, NULL, 1, 0, N'Roma', 39, N'Roma address')
INSERT INTO [dbo].[AspNetUsers] ([Id], [ImgSrc], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Age], [Address]) VALUES (N'ca10b217-e002-4613-86e8-f19d66f9ccdc', N'Images/Users/ca10b217-e002-4613-86e8-f19d66f9ccdc.jpg', N'Anna', 0, N'AKAxI5w+JuoV5QJffs+V79FBBCGqWWbVlm7M7e6I6XGLBnM1KVjlrTvj9rn6OxnxTg==', N'b65197ee-6c7e-4d63-9989-e087e76d38d0', NULL, 0, 0, NULL, 1, 0, N'Anna', 32, N'Anna address')
INSERT INTO [dbo].[AspNetUsers] ([Id], [ImgSrc], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Age], [Address]) VALUES (N'cf5b03c0-7f70-4613-85d6-937b5003037a', N'Images/Users/cf5b03c0-7f70-4613-85d6-937b5003037a.jpg', N'Alex', 0, N'AEVOmwP9dWgRWbk79uBicGj2QGc9xXY3rw08l3w0U0Bn4tb7XKIvPzOXbGtuiy3Z0g==', N'37c030fa-38e4-4967-985d-2276b130d988', NULL, 0, 0, NULL, 1, 0, N'Alex', 17, N'Alex address')
");

      Sql(@"
SET IDENTITY_INSERT [dbo].[Projects] ON
INSERT INTO [dbo].[Projects] ([ID], [Caption], [Description]) VALUES (1, N'MyStat', N'MyStat description')
INSERT INTO [dbo].[Projects] ([ID], [Caption], [Description]) VALUES (2, N'School', N'School description')
INSERT INTO [dbo].[Projects] ([ID], [Caption], [Description]) VALUES (3, N'Monster', N'Monster description')
SET IDENTITY_INSERT [dbo].[Projects] OFF
");

      Sql(@"
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'8860a7cc-299d-486f-b35c-673da4f218c1', 1)
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'8860a7cc-299d-486f-b35c-673da4f218c1', 2)
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'8860a7cc-299d-486f-b35c-673da4f218c1', 3)
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'4fe5e894-b33e-4ee1-9808-26a679cb6d7c', 1)
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'4fe5e894-b33e-4ee1-9808-26a679cb6d7c', 3)
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'6c45bdd5-d4cf-4f78-a353-919ca93e8fc8', 2)
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'b29b69b9-0753-47f4-8f96-8adf3bb7e782', 3)
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'ca10b217-e002-4613-86e8-f19d66f9ccdc', 1)
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'ca10b217-e002-4613-86e8-f19d66f9ccdc', 3)
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'cf5b03c0-7f70-4613-85d6-937b5003037a', 1)
INSERT INTO [dbo].[ApplicationUserProjects] ([ApplicationUser_Id], [Project_ID]) VALUES (N'cf5b03c0-7f70-4613-85d6-937b5003037a', 2)
");

      Sql(@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'User')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Admin')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'MainAdmin')
");

      Sql(@"
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4fe5e894-b33e-4ee1-9808-26a679cb6d7c', N'1')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b29b69b9-0753-47f4-8f96-8adf3bb7e782', N'1')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cf5b03c0-7f70-4613-85d6-937b5003037a', N'1')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6c45bdd5-d4cf-4f78-a353-919ca93e8fc8', N'2')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ca10b217-e002-4613-86e8-f19d66f9ccdc', N'2')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8860a7cc-299d-486f-b35c-673da4f218c1', N'3')
");

    }

    public override void Down()
    {
    }
  }
}
