namespace ADO_EF_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                SET IDENTITY_INSERT [dbo].[Manufacturers] ON
                INSERT INTO [dbo].[Manufacturers] ([ManufacturerId], [Name], [Address], [PhoneNumber], [City], [Country]) VALUES (1, N'Samsung', N'Lao, 12', N'123213', N'Seul', N'South-Korea')
                INSERT INTO [dbo].[Manufacturers] ([ManufacturerId], [Name], [Address], [PhoneNumber], [City], [Country]) VALUES (3, N'Apple', N'Apple', N'2434234', N'Palo Alto', N'USA')
                SET IDENTITY_INSERT [dbo].[Manufacturers] OFF
            ");

            Sql(@"
                SET IDENTITY_INSERT [dbo].[SmartPhones] ON
                INSERT INTO [dbo].[SmartPhones] ([SmartPhoneId], [Model], [Price], [Description], [Manufacturer_ManufacturerId]) VALUES (1, N'Galaxy S20', 123, N'Cool phone', 1)
                INSERT INTO [dbo].[SmartPhones] ([SmartPhoneId], [Model], [Price], [Description], [Manufacturer_ManufacturerId]) VALUES (2, N'Galaxy S3', 342, N'Not very nice', 1)
                INSERT INTO [dbo].[SmartPhones] ([SmartPhoneId], [Model], [Price], [Description], [Manufacturer_ManufacturerId]) VALUES (3, N'iPhone 5', 1234, N'Very cool phone', 3)
                INSERT INTO [dbo].[SmartPhones] ([SmartPhoneId], [Model], [Price], [Description], [Manufacturer_ManufacturerId]) VALUES (4, N'iPhone 7', 1232, N'Another phone', 3)
                SET IDENTITY_INSERT [dbo].[SmartPhones] OFF
            ");
        }
        
        public override void Down()
        {
        }
    }
}
