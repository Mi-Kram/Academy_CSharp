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
    public ActionResult Index(int page = 1)
    {
      using(CarDatabase db = new CarDatabase())
			{
        var myLogin = Session[SessionKeys.USER_LOGIN].ToString();
        if (Session[SessionKeys.USER_ADMIN_TOGGLE] == null) 
          Session[SessionKeys.USER_ADMIN_TOGGLE] = false;

        var users = db.people.Where(x => x.login == myLogin);
        if (users.Count() != 1) return RedirectToAction("Login", "Auth");

        if(Session[SessionKeys.CAR_TABLE_SORTED_COLUMN] == null)
          Session[SessionKeys.CAR_TABLE_SORTED_COLUMN] = 1;
        if(Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION] == null)
          Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION] = true;

        var user = users.First();
        List<CarModel> cars = new List<CarModel>();
        List<car> tmp = null;

        if (user.isAdmin && (bool)Session[SessionKeys.USER_ADMIN_TOGGLE])
          tmp = db.cars.ToList();
        else tmp = user.cars.ToList();

        Func<car, object> sortedProp = null;
        switch ((int)Session[SessionKeys.CAR_TABLE_SORTED_COLUMN])
        {
          case 1:
            sortedProp = x => x.brand;
            break;
          case 2:
            sortedProp = x => x.model;
            break;
          case 3:
            sortedProp = x => x.speed;
            break;
          case 4:
            sortedProp = x => x.price;
            break;
          case 5:
            sortedProp = x => x.date;
            break;
          case 6: break;
          default: break;
        }

        if ((int)Session[SessionKeys.CAR_TABLE_SORTED_COLUMN] == 6)
        {
          cars = tmp.Select(x => new CarModel()
          {
            ID = x.carID,
            Brand = x.brand,
            Model = x.model,
            Speed = x.speed,
            Price = x.price,
            Date = x.date,
            ImgSrc = x.imgSrc,
            Type = db.carTypes.Where(a => a.typeID == x.typeID).FirstOrDefault().value
          }).ToList();

          if ((bool)Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION])
            cars = cars.OrderBy(x => x.Type).ToList();
          else
            cars = cars.OrderByDescending(x => x.Type).ToList();
        }
        else
        {
          if ((bool)Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION])
            tmp = tmp.OrderBy(sortedProp).ToList();
          else
            tmp = tmp.OrderByDescending(sortedProp).ToList();

          cars = tmp.Select(x => new CarModel()
          {
            ID = x.carID,
            Brand = x.brand,
            Model = x.model,
            Speed = x.speed,
            Price = x.price,
            Date = x.date,
            ImgSrc = x.imgSrc,
            Type = db.carTypes.Where(a => a.typeID == x.typeID).FirstOrDefault().value
          }).ToList();
        }


        MainCarPageViewModel viewModel = new MainCarPageViewModel(cars, page)
        {
          User = new User(user),
          IsAdminToggle = (bool)Session[SessionKeys.USER_ADMIN_TOGGLE]
        };
        return View(viewModel);
      }
    }

    public ActionResult SetUser()
		{
      Session[SessionKeys.CAR_TABLE_SORTED_COLUMN] = 1;
      Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION] = true;

      Session[SessionKeys.USER_ADMIN_TOGGLE] = false;
      return RedirectToAction("Index");
		}
    public ActionResult SetAdmin()
		{
      Session[SessionKeys.CAR_TABLE_SORTED_COLUMN] = 1;
      Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION] = true;

      Session[SessionKeys.USER_ADMIN_TOGGLE] = true;
      return RedirectToAction("Index");
		}

    public ActionResult Add()
    {
      using(CarDatabase db = new CarDatabase())
			{
        List<CarType> carTypes = db.carTypes.Select(x => new CarType() { ID = x.typeID, Value = x.value }).ToList();
        var viewModel = new CarFormViewModel(true, carTypes);
        return View("EditCarForm", viewModel);
      }
    }

    [ValidateAntiForgeryToken]
    public ActionResult Save(CarFormViewModel car)
    {
      using(CarDatabase db = new CarDatabase())
			{
        if (car.IsNew)
        {
          int[] dateArr = car.DateStr.Split('-').Select(x => int.Parse(x)).ToArray();
          car newCar = db.cars.Add(new car()
          {
            brand = car.Brand,
            model = car.Model, 
            date = new DateTime(dateArr[0], dateArr[1], dateArr[2]), 
            price = car.Price, 
            speed = car.Speed, 
            typeID = car.TypeID
          });
          db.SaveChanges();
          
          HttpPostedFileBase imgFile = car.Files[0];
          var localPath = "Images/Cars/" + newCar.carID + System.IO.Path.GetExtension(imgFile.FileName);

          var pathToImg = Server.MapPath($"~/{localPath}");
          imgFile.SaveAs(pathToImg);

          newCar.imgSrc = localPath;
        }
        else
        {
          var edits = db.cars.Where(x => x.carID == car.ID);
          if (edits.Count() == 1)
          {
            car editCar = edits.First();
            string localPath = editCar.imgSrc;

            if (car.Files.Length == 1 && car.Files[0] != null)
            {
              HttpPostedFileBase imgFile = car.Files[0];
              localPath = "Images/Cars/" + car.ID + System.IO.Path.GetExtension(imgFile.FileName);

              var pathToImg = Server.MapPath($"~/{localPath}");
              imgFile.SaveAs(pathToImg);
            }

            int[] dateArr = car.DateStr.Split('-').Select(x => int.Parse(x)).ToArray();

            editCar.brand = car.Brand;
            editCar.model = car.Model;
            editCar.speed = car.Speed;
            editCar.price = car.Price;
            editCar.date = new DateTime(dateArr[0], dateArr[1], dateArr[2]);
            editCar.typeID = car.TypeID;
          }
        }

        db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Edit(int? id)
    {
      if (!id.HasValue) return RedirectToAction("Index");

      car car = null;
      List<CarType> carTypes = new List<CarType>();
      using (CarDatabase db = new CarDatabase())
			{
        var cars = db.cars.Where(x => x.carID == id);
        if (cars.Count() != 1) return RedirectToAction("Index");
        car = cars.First();
        carTypes = db.carTypes.Select(x => new CarType() { ID = x.typeID, Value = x.value }).ToList();
      }

      var viewModel = new CarFormViewModel(false, carTypes)
      {
        Brand = car.brand,
        Model = car.model,
        Speed = car.speed,
        Price = car.price,
        TypeID = car.typeID,
        ID = car.carID,
        ImgSrc = car.imgSrc,
        DateStr = car.date.ToString(@"yyyy-MM-dd")
      };
      return View("EditCarForm", viewModel);
    }


    public ActionResult Details(int? id)
    {
      if (!id.HasValue) return RedirectToAction("Index");

      using(CarDatabase db = new CarDatabase())
			{
        List<car> cars = db.cars.ToList();
        var result = cars.Where(x => x.carID == id).ToList();
        if (result.Count() == 0) return RedirectToAction("Index");

        var car = result[0];
        var detail = new CarModel()
        {
          ID = car.carID,
          Brand = car.brand,
          Date = car.date,
          ImgSrc = car.imgSrc,
          Model = car.model,
          Price = car.price,
          Speed = car.speed,
          Type = db.carTypes.Where(x => x.typeID == car.typeID).FirstOrDefault().value
        };

        if(Session[SessionKeys.USER_LOGIN] == null) return RedirectToAction("Login", "Auth");
        string myLogin = Session[SessionKeys.USER_LOGIN].ToString();
        var users = db.people.Where(x => x.login == myLogin).ToList();
        if(users.Count() != 1) return RedirectToAction("Login", "Auth");
        ViewBag.IsAdmin = users.FirstOrDefault().isAdmin && (bool)Session[SessionKeys.USER_ADMIN_TOGGLE];

        return View(detail);
      }
    }


    public ActionResult Remove(string IDsStr)
    {
      using(CarDatabase db = new CarDatabase())
			{
        IEnumerable<int> arr = IDsStr.Split(',').Select(x => int.Parse(x));
				foreach (person person in db.people)
				{
          for(int i = 0; i < person.cars.Count(); i++)
					{
						if (arr.Contains(person.cars.ElementAt(i).carID))
						{
              person.cars.Remove(person.cars.ElementAt(i--));
						}
					}
				}
        db.cars.RemoveRange(db.cars.Where(x => arr.Contains(x.carID)));
        db.SaveChanges();

        return RedirectToAction("Index");
      }
    }

    public ActionResult Exit()
		{
      Session[SessionKeys.USER_LOGIN] = "";
      return RedirectToAction("Login", "Auth");
		}

    public ActionResult Sort(int? sortColumn, int? page)
		{
      if (!sortColumn.HasValue) sortColumn = 1;
      if (!page.HasValue) page = 1;

      int? sc = (int?)Session[SessionKeys.CAR_TABLE_SORTED_COLUMN];
      bool? sd = (bool?)Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION];

      if (sc == null)
      {
        Session[SessionKeys.CAR_TABLE_SORTED_COLUMN] = sortColumn;
        Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION] = true;
      }
			else
			{
        if (sc == sortColumn)
        {
          if (sd == null) 
            Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION] = true;
          else
            Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION] = !sd.Value;
        }
        else
        {
          Session[SessionKeys.CAR_TABLE_SORTED_COLUMN] = sortColumn;
          Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION] = true;
        }
      }

      return RedirectToAction("Index", new { @page = page });
		}
  }
}


