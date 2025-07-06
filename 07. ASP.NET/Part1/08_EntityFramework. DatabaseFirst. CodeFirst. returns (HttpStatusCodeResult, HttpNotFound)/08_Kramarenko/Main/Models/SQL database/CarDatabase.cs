using System;
using System.Data.Entity;
using System.Linq;

namespace Main.Models.SQL_database
{
	public class CarDatabase : DbContext
	{
		// Контекст настроен для использования строки подключения "CarsModel" из файла конфигурации  
		// приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
		// "Main.Models.CarsModel" в экземпляре LocalDb. 
		// 
		// Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "CarsModel" 
		// в файле конфигурации приложения.
		public CarDatabase()
				: base("name=CarDatabase")
		{
		}

		// Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
		// о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

		public virtual DbSet<person> people { get; set; }
		public virtual DbSet<car> cars { get; set; }
		public virtual DbSet<carType> carTypes { get; set; }
	}
}