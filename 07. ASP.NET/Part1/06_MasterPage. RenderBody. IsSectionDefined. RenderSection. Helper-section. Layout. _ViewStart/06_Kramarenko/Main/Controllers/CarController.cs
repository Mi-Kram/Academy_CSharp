using Main.Models;
using Main.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.Controllers
{
  public class CarController : Controller
  {
    // GET: Car
    public ActionResult Index()
    {
      //return Content();

      FileDB database = FileDB.GetDatabase();
      var myLogin = Session[SessionKeys.USER_LOGIN].ToString();
      if (Session[SessionKeys.USER_ADMIN_TOGGLE] == null) Session[SessionKeys.USER_ADMIN_TOGGLE] = false;

      var users = database.Users.Where(x => x.Login == myLogin);
      if (users.Count() != 1) return new HttpNotFoundResult();

      var user = users.First();
      List<CarModel> cars = new List<CarModel>();

      if(user.IsAdmin && (bool)Session[SessionKeys.USER_ADMIN_TOGGLE])
			{
        cars = database.Cars.Select(x => new CarModel()
        {
          ID = x.ID,
          Brand = x.Brand,
          Model = x.Model,
          Speed = x.Speed,
          Price = x.Price,
          Date = x.Date,
          ImgSrc = x.ImgSrc,
          Type = database.CarTypes.Where(a => a.ID == x.TypeID).First().Value
        }).ToList();
      }
      else
			{
        cars = database.Cars.Where(x => user.CarIDs.Contains(x.ID))
        .Select(x => new CarModel()
        {
          ID = x.ID,
          Brand = x.Brand,
          Model = x.Model,
          Speed = x.Speed,
          Price = x.Price,
          Date = x.Date,
          ImgSrc = x.ImgSrc,
          Type = database.CarTypes.Where(a => a.ID == x.TypeID).First().Value
        }).ToList();
      }

      MainCarPageViewModel viewModel = new MainCarPageViewModel()
      {
        User = user,
        IsAdminToggle = (bool)Session[SessionKeys.USER_ADMIN_TOGGLE],
        Cars = cars
      };
      return View(viewModel);
    }

    public ActionResult SetUser()
		{
      Session[SessionKeys.USER_ADMIN_TOGGLE] = false;
      return RedirectToAction("Index");
		}
    public ActionResult SetAdmin()
		{
      Session[SessionKeys.USER_ADMIN_TOGGLE] = true;
      return RedirectToAction("Index");
		}



    public ActionResult Add()
    {
      var viewModel = new CarFormViewModel(true, FileDB.GetDatabase().CarTypes);
      return View("EditCarForm", viewModel);
    }

    [ValidateAntiForgeryToken]
    public ActionResult Save(CarFormViewModel car)
    {
			/*CarFormViewModel car = new CarFormViewModel(Convert.ToBoolean(Request.Form["IsNew"]), null)
      {
        ID = int.Parse(Request.Form["ID"]), 
        Brand = Request.Form["Brand"], 
        DateStr = Request.Form["DateStr"],
        Model = Request.Form["Model"],
        Files = new HttpPostedFileBase[] { },
        ImgSrc = Request.Form["ImgSrc"],
        Price = double.Parse(Request.Form["Price"]),
        Speed = double.Parse(Request.Form["Speed"]),
        TypeID = int.Parse(Request.Form["TypeID"])
      };
      var files = Request.Files;
      if(files.Count > 0 && files[0] != null && files[0].ContentLength != 0 && files[0].FileName != "")
			{
        car.Files = new HttpPostedFileBase[] { files[0] };
			}*/

      FileDB database = FileDB.GetDatabase();
      Car editCar = null;

      if (car.IsNew)
      {
        int newID = GetNewCarID(database.Cars);

        HttpPostedFileBase imgFile = car.Files[0];
        var localPath = "Images/Cars/" + newID + System.IO.Path.GetExtension(imgFile.FileName);

        var pathToImg = Server.MapPath($"~/{localPath}");
        imgFile.SaveAs(pathToImg);

        int[] dateArr = car.DateStr.Split('-').Select(x => int.Parse(x)).ToArray();
        editCar = new Car
        {
          ID = newID,
          Brand = car.Brand,
          Model = car.Model,
          Date = new DateTime(dateArr[0], dateArr[1], dateArr[2]),
          Price = car.Price,
          Speed = car.Speed,
          TypeID = car.TypeID,
          ImgSrc = localPath
        };
        database.Cars.Add(editCar);
      }
      else
      {
        var edits = database.Cars.Where(x => x.ID == car.ID);
        if (edits.Count() == 1)
        {
          editCar = edits.First();
          string localPath = editCar.ImgSrc;

          if (car.Files.Length == 1 && car.Files[0] != null)
          {
            HttpPostedFileBase imgFile = car.Files[0];
            localPath = "Images/Cars/" + car.ID + System.IO.Path.GetExtension(imgFile.FileName);

            var pathToImg = Server.MapPath($"~/{localPath}");
            imgFile.SaveAs(pathToImg);
          }

          int[] dateArr = car.DateStr.Split('-').Select(x => int.Parse(x)).ToArray();

          editCar.Brand = car.Brand;
          editCar.Model = car.Model;
          editCar.Speed = car.Speed;
          editCar.Price = car.Price;
          editCar.Date = new DateTime(dateArr[0], dateArr[1], dateArr[2]);
          editCar.TypeID = car.TypeID;
          editCar.ImgSrc = localPath;
        }
      }

      FileDB.SetDatabase(database);
      return RedirectToAction("Index");
    }


    private int GetNewCarID(List<Car> cars = null)
    {
      FileDB database = FileDB.GetDatabase();
      if (cars == null) cars = database.Cars;

      int id = 0;
      while (cars.Where(x => x.ID == id).Count() != 0) id++;

      return id;
    }

    public ActionResult Edit(int? id)
    {
      if (!id.HasValue) return RedirectToAction("Index");
      FileDB database = FileDB.GetDatabase();
      var cars = database.Cars.Where(x => x.ID == id.Value);
      if (cars.Count() != 1) return RedirectToAction("Index");
      var car = cars.First();


      var viewModel = new CarFormViewModel(false, FileDB.GetDatabase().CarTypes)
      {
        Brand = car.Brand,
        Model = car.Model,
        Speed = car.Speed,
        Price = car.Price,
        TypeID = car.TypeID,
        ID = car.ID,
        ImgSrc = car.ImgSrc,
        DateStr = car.Date.ToString(@"yyyy-MM-dd")
      };
      return View("EditCarForm", viewModel);
    }


    [AllowAnonymous]
    [HttpPost]
    public ActionResult ValidateFiles([Bind(Prefix = "Files")] HttpPostedFileBase[] files)
    {
      try
      {
        return Json(files[0] != null);
      }
      catch { }

      return Json(false);
    }

    public ActionResult Details(int? id)
    {
      if (!id.HasValue) return new HttpNotFoundResult();
      FileDB database = FileDB.GetDatabase();
      List<Car> cars = database.Cars;
      var result = cars.Where(x => x.ID == id.Value).ToList();
      if (result.Count() == 0) return new HttpNotFoundResult();

      var car = result[0];

      var detail = new CarModel()
      {
        ID = car.ID,
        Brand = car.Brand,
        Date = car.Date,
        ImgSrc = car.ImgSrc,
        Model = car.Model,
        Price = car.Price,
        Speed = car.Speed,
        Type = database.CarTypes.Where(x => x.ID == car.TypeID).First().Value
      };

      return View(detail);
    }



    public ActionResult Remove(string IDsStr)
    {
      FileDB database = FileDB.GetDatabase();
      IEnumerable<int> arr = IDsStr.Split(',').Select(x => int.Parse(x));
      database.Cars.RemoveAll(x => arr.Contains(x.ID));
      FileDB.SetDatabase(database);

      return RedirectToAction("Index");
    }
  }

  public class CarTypeCheckAttribute : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      try
      {
        return Convert.ToInt32(value) > 0;
      }
      catch { }
      return false;
    }
  }

}