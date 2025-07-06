using Main.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public class FileDB
	{
		public static string DBFilePath { get; } = HttpRuntime.AppDomainAppPath + "/FileDatabase.xml";

		public static FileDB GetDatabase()
		{
			return (FileDB)Serializer.Deserelize(FileDB.DBFilePath, typeof(FileDB));
		}
		public static void SetDatabase(FileDB database)
		{
			Serializer.Serelize(database, FileDB.DBFilePath, database.GetType());
		}

		public List<User> Users { get; set; }
		public List<Car> Cars { get; set; }
		public List<CarType> CarTypes { get; set; }

		public FileDB()
		{
			Users = new List<User>();
			Cars = new List<Car>();
			CarTypes = new List<CarType>();
		}

		public FileDB(List<User> users, List<Car> cars, List<CarType> carTypes)
		{
			Users = users;
			Cars = cars;
			CarTypes = carTypes;
		}
	}
}