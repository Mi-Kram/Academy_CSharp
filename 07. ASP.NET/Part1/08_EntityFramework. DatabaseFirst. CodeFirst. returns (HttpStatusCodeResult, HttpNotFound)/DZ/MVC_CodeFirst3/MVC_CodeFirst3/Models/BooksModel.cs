namespace MVC_CodeFirst3.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BooksModel : DbContext
    {
        // Your context has been configured to use a 'BooksModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MVC_CodeFirst3.Models.BooksModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BooksModel' 
        // connection string in the application configuration file.
        public BooksModel()
            : base("name=BooksModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }
    }
}