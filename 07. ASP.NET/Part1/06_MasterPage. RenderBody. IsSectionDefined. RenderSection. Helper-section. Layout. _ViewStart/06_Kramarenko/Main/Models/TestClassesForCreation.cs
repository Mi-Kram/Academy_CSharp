using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public static class CarTypeTestClass
	{
		public static List<CarType> GetTypes()
		{
			return new List<CarType>(new CarType[]
			{
				new CarType{ ID=1, Value="Sedan"},
				new CarType{ ID=2, Value="PickUp"},
				new CarType{ ID=3, Value="Convertible"},
				new CarType{ ID=4, Value="Roadster"}
			});
		}
	}

	public static class UserTestClass
	{
		public static List<User> GetUsers()
		{
			return new List<User>(new User[]
			{
        new User() { Login = "Ivan", IsAdmin=true, Password = "qwerty", CarIDs = new List<int>(new int[] { 0,1,2,3 }) },
        new User() { Login = "Petr", IsAdmin=false, Password = "qwerty", CarIDs = new List<int>(new int[] { 2,4,6,8,10 }) },
        new User() { Login = "Anna", IsAdmin=true, Password = "qwerty", CarIDs = new List<int>(new int[] { 3,6,9 }) },
        new User() { Login = "Lisa", IsAdmin=false, Password = "qwerty", CarIDs = new List<int>(new int[] { 0,1,2,3,8,9 }) },
        new User() { Login = "Roma", IsAdmin=false, Password = "qwerty", CarIDs = new List<int>(new int[] { 4,5,6,7,8,9 }) }
      });
		}
	}

  public static class CarTestClass
  {
    public static List<Car> GetCars()
    {
      List<Car> result = new List<Car>();

      result.Add(new Car { ID = 0, Brand = "BMW", Model = "A", Price = 11000, Speed = 310, Date = new DateTime(2010, 1, 1), TypeID = 1, ImgSrc = "Images/Cars/0.jpg" });
      result.Add(new Car { ID = 1, Brand = "Mercedes", Model = "A", Price = 10000, Speed = 300, Date = new DateTime(2006, 1, 1), TypeID = 3, ImgSrc = "Images/Cars/1.jpg" });
      result.Add(new Car { ID = 2, Brand = "Toyota", Model = "A", Price = 7000, Speed = 240, Date = new DateTime(1996, 1, 1), TypeID = 1, ImgSrc = "Images/Cars/2.jpg" });
      result.Add(new Car { ID = 3, Brand = "Volkswagen", Model = "B", Price = 8000, Speed = 215, Date = new DateTime(1995, 1, 1), TypeID = 4, ImgSrc = "Images/Cars/3.jpg" });
      result.Add(new Car { ID = 4, Brand = "Porsche", Model = "B", Price = 25000, Speed = 365, Date = new DateTime(2018, 1, 1), TypeID = 2, ImgSrc = "Images/Cars/4.jpg" });
      result.Add(new Car { ID = 5, Brand = "Honda", Model = "B", Price = 12000, Speed = 280, Date = new DateTime(2009, 1, 1), TypeID = 4, ImgSrc = "Images/Cars/5.jpg" });
      result.Add(new Car { ID = 6, Brand = "Ford", Model = "C", Price = 11000, Speed = 220, Date = new DateTime(2001, 1, 1), TypeID = 1, ImgSrc = "Images/Cars/6.jpg" });
      result.Add(new Car { ID = 7, Brand = "Nissan", Model = "C", Price = 6000, Speed = 210, Date = new DateTime(1986, 1, 1), TypeID = 3, ImgSrc = "Images/Cars/7.jpg" });
      result.Add(new Car { ID = 8, Brand = "BMW", Model = "C", Price = 16000, Speed = 330, Date = new DateTime(2014, 1, 1), TypeID = 3, ImgSrc = "Images/Cars/8.jpg" });
      result.Add(new Car { ID = 9, Brand = "BMW", Model = "D", Price = 18000, Speed = 330, Date = new DateTime(2003, 1, 1), TypeID = 2, ImgSrc = "Images/Cars/9.jpg" });
      result.Add(new Car { ID = 10, Brand = "Ford", Model = "D", Price = 9000, Speed = 190, Date = new DateTime(1999, 1, 1), TypeID = 1, ImgSrc = "Images/Cars/10.jpg" });
      result.Add(new Car { ID = 11, Brand = "Honda", Model = "D", Price = 10000, Speed = 300, Date = new DateTime(2003, 1, 1), TypeID = 4, ImgSrc = "Images/Cars/11.jpg" });

      #region Test

      for (int i = 0; i < result.Count; i++)
      {
        result[i].TypeID = 1 + (i % 3);
      }

      #endregion

      return result;
    }
  }
}