namespace ADO_EF_CodeFirst
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyDbContext : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ADO_EF_CodeFirst.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public MyDbContext()
            : base("name=Model1")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Vendor> Vendors { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<ProjectEmployees> ProjectEmployees { get; set; }
    }
}