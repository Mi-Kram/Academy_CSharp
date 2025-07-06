namespace ADO_EF_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        ManufacturerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ManufacturerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Manufacturers");
        }
    }
}
