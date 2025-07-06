using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
  public class Car
  {
    public int ID { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public double Speed { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public int TypeID { get; set; }
    public string ImgSrc { get; set; }
  }
}