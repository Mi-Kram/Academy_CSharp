using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Categories
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (1, N'Холодильник')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (2, N'Автомобиль')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (3, N'Принтер')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (4, N'Ноутбук')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (5, N'Стиральная машина')
INSERT INTO [dbo].[Categories] ([ID], [Value]) VALUES (6, N'Телефон')
SET IDENTITY_INSERT [dbo].[Categories] OFF");

            // Products
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Products] ON
INSERT INTO [dbo].[Products] ([ID], [CategoryID], [SerialNumber], [Price], [YearIssue], [Brand], [Model]) VALUES (1, 2, N'123123123', 12000000, 2017, N'BMW', N'X5')
INSERT INTO [dbo].[Products] ([ID], [CategoryID], [SerialNumber], [Price], [YearIssue], [Brand], [Model]) VALUES (2, 4, N'1111', 40000, 2018, N'Dell', N'HS3')
INSERT INTO [dbo].[Products] ([ID], [CategoryID], [SerialNumber], [Price], [YearIssue], [Brand], [Model]) VALUES (3, 3, N'2222', 9000, 2019, N'HP', N'SR-17')
INSERT INTO [dbo].[Products] ([ID], [CategoryID], [SerialNumber], [Price], [YearIssue], [Brand], [Model]) VALUES (4, 6, N'3333', 80000, 2018, N'Sumsung', N'A30')
SET IDENTITY_INSERT [dbo].[Products] OFF");

            // Clients
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Clients] ON
INSERT INTO [dbo].[Clients] ([ID], [FirstName], [LastName], [Address], [PassportID], [Birthday]) VALUES (1, N'Alex', N'Petrov', N'old street 17', N'123123123', N'1999-06-12 00:00:00')
INSERT INTO [dbo].[Clients] ([ID], [FirstName], [LastName], [Address], [PassportID], [Birthday]) VALUES (2, N'Nick', N'Roy', N'new street 12', N'1111', N'2005-01-06 00:00:00')
INSERT INTO [dbo].[Clients] ([ID], [FirstName], [LastName], [Address], [PassportID], [Birthday]) VALUES (3, N'Luck', N'Rostern', N'super street 3', N'3333', N'1994-11-18 00:00:00')
SET IDENTITY_INSERT [dbo].[Clients] OFF");

            // Orders
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Orders] ON
INSERT INTO [dbo].[Orders] ([ID], [ProductID], [MasterID], [ClientID], [StartDate], [EndDate], [IsDone], [Salary]) VALUES (1, 1, N'd78907cd-6527-4ccf-830a-eb0c4bc855fa', 2, N'2021-09-30 16:08:18', N'2021-09-30 16:14:23', 1, 100000)
INSERT INTO [dbo].[Orders] ([ID], [ProductID], [MasterID], [ClientID], [StartDate], [EndDate], [IsDone], [Salary]) VALUES (2, 2, N'd78907cd-6527-4ccf-830a-eb0c4bc855fa', 1, N'2021-09-30 16:10:02', NULL, 0, 20000)
INSERT INTO [dbo].[Orders] ([ID], [ProductID], [MasterID], [ClientID], [StartDate], [EndDate], [IsDone], [Salary]) VALUES (3, 3, N'7aba73cc-f1fd-42fa-9332-f4914b2f0cbf', 2, N'2021-09-30 16:10:36', N'2021-09-30 16:18:42', 1, 6000)
INSERT INTO [dbo].[Orders] ([ID], [ProductID], [MasterID], [ClientID], [StartDate], [EndDate], [IsDone], [Salary]) VALUES (4, 4, N'd78907cd-6527-4ccf-830a-eb0c4bc855fa', 3, N'2021-09-30 16:17:10', NULL, 0, 7000)
INSERT INTO [dbo].[Orders] ([ID], [ProductID], [MasterID], [ClientID], [StartDate], [EndDate], [IsDone], [Salary]) VALUES (5, 4, N'7aba73cc-f1fd-42fa-9332-f4914b2f0cbf', 1, N'2021-09-30 16:18:01', N'2021-09-30 16:18:41', 1, 16000)
INSERT INTO [dbo].[Orders] ([ID], [ProductID], [MasterID], [ClientID], [StartDate], [EndDate], [IsDone], [Salary]) VALUES (6, 2, N'7aba73cc-f1fd-42fa-9332-f4914b2f0cbf', 2, N'2021-09-30 16:18:25', N'2021-09-30 16:18:43', 1, 17000)
SET IDENTITY_INSERT [dbo].[Orders] OFF");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
