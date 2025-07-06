namespace Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PeopleImg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImgSrc", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ImgSrc");
        }
    }
}
