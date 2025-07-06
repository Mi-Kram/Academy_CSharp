namespace Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cars",
                c => new
                    {
                        carID = c.Int(nullable: false, identity: true),
                        brand = c.String(maxLength: 50),
                        model = c.String(maxLength: 50),
                        speed = c.Double(nullable: false),
                        price = c.Double(nullable: false),
                        date = c.DateTime(nullable: false),
                        typeID = c.Int(nullable: false),
                        imgSrc = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.carID)
                .ForeignKey("dbo.carTypes", t => t.typeID, cascadeDelete: true)
                .Index(t => t.typeID);
            
            CreateTable(
                "dbo.people",
                c => new
                    {
                        login = c.String(nullable: false, maxLength: 20),
                        password = c.String(maxLength: 100),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.login);
            
            CreateTable(
                "dbo.carTypes",
                c => new
                    {
                        typeID = c.Int(nullable: false, identity: true),
                        value = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.typeID);
            
            CreateTable(
                "dbo.personcars",
                c => new
                    {
                        person_login = c.String(nullable: false, maxLength: 20),
                        car_carID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.person_login, t.car_carID })
                .ForeignKey("dbo.people", t => t.person_login, cascadeDelete: true)
                .ForeignKey("dbo.cars", t => t.car_carID, cascadeDelete: true)
                .Index(t => t.person_login)
                .Index(t => t.car_carID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.cars", "typeID", "dbo.carTypes");
            DropForeignKey("dbo.personcars", "car_carID", "dbo.cars");
            DropForeignKey("dbo.personcars", "person_login", "dbo.people");
            DropIndex("dbo.personcars", new[] { "car_carID" });
            DropIndex("dbo.personcars", new[] { "person_login" });
            DropIndex("dbo.cars", new[] { "typeID" });
            DropTable("dbo.personcars");
            DropTable("dbo.carTypes");
            DropTable("dbo.people");
            DropTable("dbo.cars");
        }
    }
}
