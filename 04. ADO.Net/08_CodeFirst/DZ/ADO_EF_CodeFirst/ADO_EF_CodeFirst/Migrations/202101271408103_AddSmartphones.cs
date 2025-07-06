namespace ADO_EF_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSmartphones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmartPhones",
                c => new
                    {
                        SmartPhoneId = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                        Manufacturer_ManufacturerId = c.Int(),
                    })
                .PrimaryKey(t => t.SmartPhoneId)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_ManufacturerId)
                .Index(t => t.Manufacturer_ManufacturerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SmartPhones", "Manufacturer_ManufacturerId", "dbo.Manufacturers");
            DropIndex("dbo.SmartPhones", new[] { "Manufacturer_ManufacturerId" });
            DropTable("dbo.SmartPhones");
        }
    }
}
