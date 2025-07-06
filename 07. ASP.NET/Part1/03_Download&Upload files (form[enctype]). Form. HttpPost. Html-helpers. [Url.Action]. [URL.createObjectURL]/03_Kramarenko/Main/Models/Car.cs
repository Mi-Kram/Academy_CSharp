using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
    public class CarType
    {
        public int ID { get; set; }
        public string Value { get; set; }

        public CarType() { }
        public CarType(int id)
        {
            ID = id;
            Value = CarTypes.Types.Where(x => x.ID == id).First().Value;
        }
    }
    public static class CarTypes
    {
        public static List<CarType> Types { get; }
        static CarTypes()
        {
            Types = new List<CarType>(new CarType[]
            {
                new CarType{ ID=0, Value="Sedan"},
                new CarType{ ID=1, Value="PickUp"},
                new CarType{ ID=2, Value="Convertible"},
                new CarType{ ID=3, Value="Roadster"}
            });
        }
    }

    public class Car
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Speed { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public CarType Type { get; set; }
        public string ImgSrc { get; set; }
    }

    public static class CarTestClass
    {
        public static List<Car> GetCars()
        {
            List<Car> result = new List<Car>();

            result.Add(new Car { ID=0, Brand="BMW", Model="A", Price=11000, Speed=310, Date=new DateTime(2010,1,1), Type=new CarType(1), ImgSrc="Images/Cars/0.jpg" });
            result.Add(new Car { ID=1, Brand="Mercedes", Model="A", Price=10000, Speed=300, Date=new DateTime(2006,1,1), Type= new CarType(3), ImgSrc="Images/Cars/1.jpg" });
            result.Add(new Car { ID=2, Brand="Toyota", Model="A", Price=7000, Speed=240, Date=new DateTime(1996,1,1), Type= new CarType(1), ImgSrc="Images/Cars/2.jpg" });
            result.Add(new Car { ID=3, Brand="Volkswagen", Model="B", Price=8000, Speed=215, Date=new DateTime(1995,1,1), Type= new CarType(0), ImgSrc="Images/Cars/3.jpg" });
            result.Add(new Car { ID=4, Brand="Porsche", Model="B", Price=25000, Speed=365, Date=new DateTime(2018,1,1), Type= new CarType(2), ImgSrc="Images/Cars/4.jpg" });
            result.Add(new Car { ID=5, Brand="Honda", Model="B", Price=12000, Speed=280, Date=new DateTime(2009,1,1), Type= new CarType(0), ImgSrc="Images/Cars/5.jpg" });
            result.Add(new Car { ID=6, Brand="Ford", Model="C", Price=11000, Speed=220, Date=new DateTime(2001,1,1), Type= new CarType(1), ImgSrc="Images/Cars/6.jpg" });
            result.Add(new Car { ID=7, Brand="Nissan", Model="C", Price=6000, Speed=210, Date=new DateTime(1986,1,1), Type= new CarType(3), ImgSrc="Images/Cars/7.jpg" });
            result.Add(new Car { ID=8, Brand="BMW", Model="C", Price=16000, Speed=330, Date=new DateTime(2014,1,1), Type= new CarType(3), ImgSrc="Images/Cars/8.jpg" });
            result.Add(new Car { ID=9, Brand="BMW", Model="D", Price=18000, Speed=330, Date=new DateTime(2003,1,1), Type= new CarType(2), ImgSrc="Images/Cars/9.jpg" });
            result.Add(new Car { ID=10, Brand="Ford", Model="D", Price=9000, Speed=190, Date=new DateTime(1999,1,1), Type= new CarType(1), ImgSrc="Images/Cars/10.jpg" });
            result.Add(new Car { ID=11, Brand="Honda", Model="D", Price=10000, Speed=300, Date=new DateTime(2003,1,1), Type= new CarType(0), ImgSrc="Images/Cars/11.jpg" });

            return result;
        }
    }
}