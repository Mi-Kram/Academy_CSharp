namespace Main
{
    using Main.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EmployeesProjectsDBContext : DbContext
    {
        // Контекст настроен для использования строки подключения "EmployeesProjects" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "Main.EmployeesProjects" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "EmployeesProjects" 
        // в файле конфигурации приложения.
        public EmployeesProjectsDBContext()
            : base("name=EmployeesProjects")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<EmployeeProject> EmployeesProjects { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}