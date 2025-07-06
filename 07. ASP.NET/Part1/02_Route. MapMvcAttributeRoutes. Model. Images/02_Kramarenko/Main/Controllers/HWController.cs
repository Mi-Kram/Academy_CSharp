using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Models;

namespace Main.Controllers
{
    public class HWController : Controller
    {
        List<Car> MyCars = CarTestClass.GetCars();

        // GET: HW
        public ActionResult Index()
        {
            return View(MyCars);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue) return new HttpNotFoundResult();
            var result = MyCars.Where(x => x.ID == id.Value).ToList();
            if(result.Count() == 0)  return new HttpNotFoundResult();

            return View(result[0]);
        }

        [Route("HW/name/{brand}")]
        public ActionResult GetCarsByBrand(string brand)
        {
            return View(MyCars.Where(x => x.Brand.ToLower() == brand.ToLower()));
        }
        [Route("HW/year/{year}")]
        public ActionResult GetCarsByYear(int? year)
        {
            if (!year.HasValue) return new HttpNotFoundResult();
            return View(MyCars.Where(x => x.Date.Year == year.Value));
        }
        [Route("HW/speed/{speed}")]
        public ActionResult GetCarsBySpeed(double? speed)
        {
            if (!speed.HasValue) return new HttpNotFoundResult();
            return View(MyCars.Where(x => x.Speed >= speed.Value));
        }
        [Route("HW/price/{price}")]
        public ActionResult GetCarsByPrice(double? price)
        {
            if (!price.HasValue) return new HttpNotFoundResult();
            return View(MyCars.Where(x => x.Price >= price.Value));
        }
    }
}