namespace ADO_EF_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
