namespace Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedData : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Employees] ON
                  INSERT INTO[dbo].[Employees] ([id], [name], [age], [address], [phone]) VALUES(1, N'Johnson',  23, N'10932 Bigge Rd.',         N'408 496-7223')
                  INSERT INTO[dbo].[Employees] ([id], [name], [age], [address], [phone]) VALUES(2, N'Marjorie', 32, N'309 63rd St. #411',       N'415 986-7020')
                  INSERT INTO[dbo].[Employees] ([id], [name], [age], [address], [phone]) VALUES(3, N'Cheryl',   28, N'589 Darwin Ln.',          N'415 548-7723')
                  INSERT INTO[dbo].[Employees] ([id], [name], [age], [address], [phone]) VALUES(4, N'Michael',  25, N'22 Cleveland Av. #14',    N'408 286-2428')
                  INSERT INTO[dbo].[Employees] ([id], [name], [age], [address], [phone]) VALUES(5, N'Dean',     46, N'5420 College Av.',        N'415 834-2919')
                  SET IDENTITY_INSERT[dbo].[Employees] OFF"
            );

            Sql(@"SET IDENTITY_INSERT [dbo].[Projects] ON
                  INSERT INTO [dbo].[Projects] ([id], [name], [startDate], [endDate], [description]) VALUES (1, N'Big Spaceship',           '1995-02-28',  '2001-07-03',  N'very cool project')
                  INSERT INTO [dbo].[Projects] ([id], [name], [startDate], [endDate], [description]) VALUES (3, N'Evolution Tower',         '1999-06-12',  '2002-12-25',  NULL)
                  INSERT INTO [dbo].[Projects] ([id], [name], [startDate], [endDate], [description]) VALUES (4, N'Golden City',             '2004-03-23',  '2005-01-02',  N'cool project')
                  INSERT INTO [dbo].[Projects] ([id], [name], [startDate], [endDate], [description]) VALUES (5, N'One World Trade Center',  '2004-05-22',  '2016-09-16',  N'big project')
                  INSERT INTO [dbo].[Projects] ([id], [name], [startDate], [endDate], [description]) VALUES (6, N'Cold Fusion',             '2008-10-07',  '2010-02-04',  NULL)
                  SET IDENTITY_INSERT [dbo].[Projects] OFF"
            );

            Sql(@"INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (2, 1)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (3, 1)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (4, 1)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (1, 3)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (2, 3)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (3, 4)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (5, 4)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (4, 4)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (3, 5)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (2, 5)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (1, 6)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (3, 6)
                  INSERT INTO [dbo].[EmployeeProjects] ([EmployeeId], [ProjectId]) VALUES (5, 6)"
            );
        }

        public override void Down()
        {
        }
    }
}
