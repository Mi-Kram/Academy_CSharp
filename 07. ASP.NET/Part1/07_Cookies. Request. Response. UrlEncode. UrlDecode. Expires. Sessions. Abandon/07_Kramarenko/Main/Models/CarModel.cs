using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public class CarModel
	{
    public int ID { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public double Speed { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public string ImgSrc { get; set; }
  }
}