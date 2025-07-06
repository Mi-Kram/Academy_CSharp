namespace Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        car_id = c.String(nullable: false, maxLength: 128),
                        pathImg = c.String(maxLength: 50),
                        brand = c.String(maxLength: 30),
                        body_type = c.String(maxLength: 30),
                        registrDate = c.String(),
                        own_own_id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.car_id)
                .ForeignKey("dbo.Owners", t => t.own_own_id)
                .Index(t => t.own_own_id);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        own_id = c.String(nullable: false, maxLength: 128),
                        pathImg = c.String(maxLength: 100),
                        name = c.String(maxLength: 100),
                        address = c.String(maxLength: 50),
                        phone = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.own_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "own_own_id", "dbo.Owners");
            DropIndex("dbo.Cars", new[] { "own_own_id" });
            DropTable("dbo.Owners");
            DropTable("dbo.Cars");
        }
    }
}
