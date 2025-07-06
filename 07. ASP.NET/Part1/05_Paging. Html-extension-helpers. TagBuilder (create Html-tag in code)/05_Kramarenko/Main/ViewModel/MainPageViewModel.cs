using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.ViewModel
{
	public class PageInfo
	{
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
	}

	public class MainPageViewModel
	{
		public IEnumerable<Car> CarsForView { get; set; }
		public PageInfo CarTypePageInfo { get; set; }
		public PageInfo PageInfo { get; set; }

		public static int MaxPageLength { get; } = 3;

		public MainPageViewModel(IEnumerable<Car> cars, IEnumerable<CarType> types, 
			int carTypePage, int page)
		{
			CarTypePageInfo = new PageInfo { CurrentPage = carTypePage, TotalPages = types.Count() };

			var carTypeID = types.ElementAt(carTypePage - 1).ID;
			var carsForType = cars.Where(x => x.Type.ID == carTypeID);

			PageInfo = new PageInfo { CurrentPage = page, TotalPages = Ceil((double)carsForType.Count() / MaxPageLength) };

			CarsForView = carsForType.Skip((page - 1) * MaxPageLength).Take(MaxPageLength);
		}

		private int Ceil(double val)
		{
			int integer = (int)val;
			return val > integer ? ++integer : integer;
		}
	}
}