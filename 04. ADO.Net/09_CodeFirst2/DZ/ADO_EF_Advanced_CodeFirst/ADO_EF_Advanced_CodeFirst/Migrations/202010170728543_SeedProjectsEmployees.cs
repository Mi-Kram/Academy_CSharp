namespace ADO_EF_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedProjectsEmployees : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                SET IDENTITY_INSERT [dbo].[Vendors_Employees] ON
                INSERT INTO [dbo].[Vendors_Employees] ([EmployeeId], [Name], [Address], [Age]) VALUES (2, N'Alex Petrov', N'Donetsk', 23)
                INSERT INTO [dbo].[Vendors_Employees] ([EmployeeId], [Name], [Address], [Age]) VALUES (3, N'Ivan Matveev', N'Moskow', 34)
                SET IDENTITY_INSERT [dbo].[Vendors_Employees] OFF
            ");

            Sql(@"
                SET IDENTITY_INSERT [dbo].[Projects] ON
                INSERT INTO [dbo].[Projects] ([ProjectId], [ProjectName], [StartDate], [EndDate]) VALUES (3, N'Project Zero', N'2012-12-12 00:00:00', N'2014-12-23 00:00:00')
                INSERT INTO [dbo].[Projects] ([ProjectId], [ProjectName], [StartDate], [EndDate]) VALUES (6, N'Buran', N'2012-12-12 00:00:00', N'2018-01-12 00:00:00')
                SET IDENTITY_INSERT [dbo].[Projects] OFF
            ");

            Sql(@"
                SET IDENTITY_INSERT [dbo].[Projects] ON
                INSERT INTO [dbo].[ProjectEmployees] ([EmployeeId], [ProjectId]) VALUES (2, 3)
                INSERT INTO [dbo].[ProjectEmployees] ([EmployeeId], [ProjectId]) VALUES (2, 6) 
                INSERT INTO [dbo].[ProjectEmployees] ([EmployeeId], [ProjectId]) VALUES (3, 3) 
                SET IDENTITY_INSERT [dbo].[Projects] OFF
            ");
        }
        
        public override void Down()
        {
        }
    }
}
