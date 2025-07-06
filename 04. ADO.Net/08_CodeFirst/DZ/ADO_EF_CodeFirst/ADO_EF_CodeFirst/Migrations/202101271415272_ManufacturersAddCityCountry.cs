namespace ADO_EF_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManufacturersAddCityCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Manufacturers", "City", c => c.String(maxLength: 20));
            AddColumn("dbo.Manufacturers", "Country", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Manufacturers", "Country");
            DropColumn("dbo.Manufacturers", "City");
        }
    }
}
