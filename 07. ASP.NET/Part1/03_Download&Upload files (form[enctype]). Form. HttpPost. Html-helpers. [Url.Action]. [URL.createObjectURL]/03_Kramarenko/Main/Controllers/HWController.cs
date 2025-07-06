using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Models;
using Main.Serialization;
using Main.ViewModel;

namespace Main.Controllers
{
    public class HWController : Controller
    {
        string CarsFilePath() => HttpRuntime.AppDomainAppPath + "/CarsData.xml";

        private List<Car> GetCars()
        {
            return (List<Car>)Serializer.Deserelize(CarsFilePath(), typeof(List<Car>)) ?? new List<Car>();
        }
        private void SaveCars(List<Car> lst)
        {
            Serializer.Serelize(lst, CarsFilePath(), lst.GetType());
        }


        static List<Car> currentList = new List<Car>();


        // GET: HW
        public ActionResult Index()
        {
            currentList = GetCars();
            return View(currentList);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue) return new HttpNotFoundResult();
            List<Car> cars = GetCars();
            var result = cars.Where(x => x.ID == id.Value).ToList();
            if (result.Count() == 0) return new HttpNotFoundResult();

            return View(result[0]);
        }

        [Route("HW/name/{brand}")]
        public ActionResult GetCarsByBrand(string brand)
        { 
            currentList = GetCars().Where(x => x.Brand.ToLower() == brand.ToLower()).ToList();
            return View("Show", currentList);
        }
        [Route("HW/year/{year}")]
        public ActionResult GetCarsByYear(int? year)
        {
            if (!year.HasValue) return new HttpNotFoundResult();
            currentList = GetCars().Where(x => x.Date.Year == year.Value).ToList(); ;
            return View("Show", currentList);
        }
        [Route("HW/speed/{speed}")]
        public ActionResult GetCarsBySpeed(double? speed)
        {
            if (!speed.HasValue) return new HttpNotFoundResult();
            currentList = GetCars().Where(x => x.Speed >= speed.Value).ToList();
            return View("Show", currentList);
        }
        [Route("HW/price/{price}")]
        public ActionResult GetCarsByPrice(double? price)
        {
            if (!price.HasValue) return new HttpNotFoundResult();
            currentList = GetCars().Where(x => x.Price >= price.Value).ToList();
            return View("Show", currentList);
        }

        public ActionResult ShowCurentList()
        {
            return View("Show", currentList);
        }

        public ActionResult Remove(string IDsStr, bool? isGoBack)
        {
            List<Car> cars = GetCars();
            IEnumerable<int> arr = IDsStr.Split(',').Select(x => int.Parse(x));
            cars.RemoveAll(x => arr.Contains(x.ID));
            SaveCars(cars);

            if(isGoBack.HasValue && isGoBack.Value == true)
            {
                currentList.RemoveAll(x => arr.Contains(x.ID));
                return View("Show", currentList);
            }

            currentList = cars;
            return RedirectToAction("Index", "HW");
        }

        public ActionResult Add()
        {
            var viewModel = new CarFormViewModel(true);
            return View("EditCarForm", viewModel);
        }
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index", "HW");
            var cars = GetCars().Where(x => x.ID == id.Value);
            if(cars.Count() != 1) return RedirectToAction("Index", "HW");
            var car = cars.First();

            var viewModel = new CarFormViewModel(false)
            {
                Brand = car.Brand,
                Model = car.Model,
                Speed = car.Speed,
                Price = car.Price,
                TypeID = car.Type.ID,
                ID = car.ID,
                ImgSrc = car.ImgSrc,
                DateStr = car.Date.ToString(@"yyyy-MM-dd")
            };
            return View("EditCarForm", viewModel);
        }

        public ActionResult Save(CarFormViewModel car)
        {
            var cars = GetCars();
            var flag = currentList.Count == cars.Count;
            Car editCar = null;

            if (car.IsNew)
            {
                int newID = GetNewCarID(cars);

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
                    Type = new CarType(car.TypeID),
                    ImgSrc = localPath
                };
                cars.Add(editCar);

                SaveCars(cars);
            }
            else
            {
                var edits = cars.Where(x => x.ID == car.ID);
                if(edits.Count() == 1)
                {
                    editCar = edits.First();
                    string localPath = editCar.ImgSrc;

                    if(car.Files.Length == 1 && car.Files[0] != null)
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
                    editCar.Type = new CarType(car.TypeID);
                    editCar.ImgSrc = localPath;

                    SaveCars(cars);
                }
            }

            if (!flag)
            {
                if (car.IsNew)
                {
                    currentList.Add(editCar);
                }
                else
                {
                    for (int i = 0; i < currentList.Count; i++)
                    {
                        if (currentList[i].ID == editCar.ID)
                        {
                            currentList[i] = editCar;
                            break;
                        }
                    }
                }

                return View("Show", currentList);
            }

            return RedirectToAction("Index", "HW");
        }


        private int GetNewCarID(List<Car> cars = null)
        {
            if (cars == null) cars = GetCars();

            int id = 0;
            while (cars.Where(x => x.ID == id).Count() != 0) id++;

            return id;
        }


        public ActionResult Test(int[] arr)
        {
            return Content($"length: {arr?.Length.ToString()}");
        }
    }
}