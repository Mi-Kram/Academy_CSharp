namespace ADO_EF_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedVendors : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    SET IDENTITY_INSERT [dbo].[Vendors] ON
                    INSERT INTO [dbo].[Vendors] ([VendorID], [VendorName], [Address], [Phone]) VALUES (1, N'Sony', N'Japan', N'234234')
                    INSERT INTO [dbo].[Vendors] ([VendorID], [VendorName], [Address], [Phone]) VALUES (2, N'Samsung', N'South-Korea', N'234235')
                    SET IDENTITY_INSERT [dbo].[Vendors] OFF
                ");
        }
        
        public override void Down()
        {
        }
    }
}
