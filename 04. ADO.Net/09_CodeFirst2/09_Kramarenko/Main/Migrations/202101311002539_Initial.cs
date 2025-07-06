namespace Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50),
                        age = c.Int(nullable: false),
                        address = c.String(maxLength: 50),
                        phone = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.EmployeeProjects",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.ProjectId })
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        startDate = c.String(maxLength: 20),
                        endDate = c.String(maxLength: 20),
                        description = c.String(maxLength: 200, defaultValue:"Не указано"),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.EmployeeProjects", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeeProjects", new[] { "ProjectId" });
            DropIndex("dbo.EmployeeProjects", new[] { "EmployeeId" });
            DropTable("dbo.Projects");
            DropTable("dbo.EmployeeProjects");
            DropTable("dbo.Employees");
        }
    }
}
