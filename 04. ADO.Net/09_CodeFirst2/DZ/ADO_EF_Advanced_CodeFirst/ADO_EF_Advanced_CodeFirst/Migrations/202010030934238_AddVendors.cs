namespace ADO_EF_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVendors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        VendorID = c.Int(nullable: false, identity: true),
                        VendorName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.VendorID);
            
            AddColumn("dbo.Products", "Vendor_VendorID", c => c.Int());
            CreateIndex("dbo.Products", "Vendor_VendorID");
            AddForeignKey("dbo.Products", "Vendor_VendorID", "dbo.Vendors", "VendorID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Vendor_VendorID", "dbo.Vendors");
            DropIndex("dbo.Products", new[] { "Vendor_VendorID" });
            DropColumn("dbo.Products", "Vendor_VendorID");
            DropTable("dbo.Vendors");
        }
    }
}
