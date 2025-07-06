namespace Main
{
    using Main.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class pubsDB : DbContext
    {
        // Контекст настроен для использования строки подключения "pubsBD" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "Main.pubsBD" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "pubsBD" 
        // в файле конфигурации приложения.
        public pubsDB()
            : base("name=pubsBD")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
    }
    // enable-migrations
    // add-migration InitialModel

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}