namespace ADO_EF_CodeFirst
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhonesDbContext : DbContext
    {
        // Your context has been configured to use a 'phones' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ADO_EF_CodeFirst.phones' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'phones' 
        // connection string in the application configuration file.
        public PhonesDbContext()
            : base("name=phones")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Manufacturer> Manufacturers { get; set; }

        public virtual DbSet<SmartPhone> SmartPhones { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}