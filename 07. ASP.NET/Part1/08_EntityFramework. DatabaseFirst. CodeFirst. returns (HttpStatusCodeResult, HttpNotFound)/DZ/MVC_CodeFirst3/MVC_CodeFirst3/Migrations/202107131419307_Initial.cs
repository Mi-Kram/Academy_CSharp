namespace MVC_CodeFirst3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyBook",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Author = c.String(nullable: false),
                        GenreId = c.Int(nullable: false),
                        PubDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Pages = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyBook", "GenreId", "dbo.Genres");
            DropIndex("dbo.MyBook", new[] { "GenreId" });
            DropTable("dbo.Genres");
            DropTable("dbo.MyBook");
        }
    }
}
