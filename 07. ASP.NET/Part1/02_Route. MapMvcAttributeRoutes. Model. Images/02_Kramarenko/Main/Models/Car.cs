using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
    public enum CarType
    {
        Sedan, 
        PickUp, 
        Convertible, 
        Roadster
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

            result.Add(new Car { ID=0, Brand="BMW", Model="A", Price=11000, Speed=310, Date=new DateTime(2010,1,1), Type=CarType.PickUp, ImgSrc="Images/Cars/car01.jpg" });
            result.Add(new Car { ID=1, Brand="Mercedes", Model="A", Price=10000, Speed=300, Date=new DateTime(2006,1,1), Type=CarType.Convertible, ImgSrc="Images/Cars/car02.jpg" });
            result.Add(new Car { ID=2, Brand="Toyota", Model="A", Price=7000, Speed=240, Date=new DateTime(1996,1,1), Type=CarType.Sedan, ImgSrc="Images/Cars/car03.jpg" });
            result.Add(new Car { ID=3, Brand="Volkswagen", Model="B", Price=8000, Speed=215, Date=new DateTime(1995,1,1), Type=CarType.Sedan, ImgSrc="Images/Cars/car04.jpg" });
            result.Add(new Car { ID=4, Brand="Porsche", Model="B", Price=25000, Speed=365, Date=new DateTime(2018,1,1), Type=CarType.Convertible, ImgSrc="Images/Cars/car05.jpg" });
            result.Add(new Car { ID=5, Brand="Honda", Model="B", Price=12000, Speed=280, Date=new DateTime(2009,1,1), Type=CarType.Roadster, ImgSrc="Images/Cars/car06.jpg" });
            result.Add(new Car { ID=6, Brand="Ford", Model="C", Price=11000, Speed=220, Date=new DateTime(2001,1,1), Type=CarType.PickUp, ImgSrc="Images/Cars/car07.jpg" });
            result.Add(new Car { ID=7, Brand="Nissan", Model="C", Price=6000, Speed=210, Date=new DateTime(1986,1,1), Type=CarType.Sedan, ImgSrc="Images/Cars/car01.jpg" });
            result.Add(new Car { ID=8, Brand="BMW", Model="C", Price=16000, Speed=330, Date=new DateTime(2014,1,1), Type=CarType.Convertible, ImgSrc="Images/Cars/car02.jpg" });
            result.Add(new Car { ID=9, Brand="BMW", Model="D", Price=18000, Speed=330, Date=new DateTime(2003,1,1), Type=CarType.Roadster, ImgSrc="Images/Cars/car03.jpg" });
            result.Add(new Car { ID=10, Brand="Ford", Model="D", Price=9000, Speed=190, Date=new DateTime(1999,1,1), Type=CarType.Convertible, ImgSrc="Images/Cars/car04.jpg" });
            result.Add(new Car { ID=11, Brand="Honda", Model="D", Price=10000, Speed=300, Date=new DateTime(2003,1,1), Type=CarType.Sedan, ImgSrc="Images/Cars/car05.jpg" });

            return result;
        }
    }
}