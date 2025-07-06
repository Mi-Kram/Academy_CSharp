namespace ADO_EF_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectsEmployees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vendors_Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(maxLength: 50),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.ProjectEmployees",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.EmployeeId })
                .ForeignKey("dbo.Vendors_Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 50),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectEmployees", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectEmployees", "EmployeeId", "dbo.Vendors_Employees");
            DropIndex("dbo.ProjectEmployees", new[] { "EmployeeId" });
            DropIndex("dbo.ProjectEmployees", new[] { "ProjectId" });
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectEmployees");
            DropTable("dbo.Vendors_Employees");
        }
    }
}
