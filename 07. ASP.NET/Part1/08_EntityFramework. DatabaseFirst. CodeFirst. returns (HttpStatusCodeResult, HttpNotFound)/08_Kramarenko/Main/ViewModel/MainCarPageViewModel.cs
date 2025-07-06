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

	public class MainCarPageViewModel
	{
		public User User { get; set; }
		public bool IsAdminToggle { get; set; }
		public List<CarModel> Cars { get; set; }
		public PageInfo PageInfo { get; set; }
		public static int MaxItemsInPage { get; } = 7;

		public MainCarPageViewModel() { }
		public MainCarPageViewModel(IEnumerable<CarModel> cars, int page)
		{
			PageInfo = new PageInfo { CurrentPage = page, TotalPages = Ceil((double)cars.Count() / MaxItemsInPage) };

			Cars = cars.Skip((page - 1) * MaxItemsInPage).Take(MaxItemsInPage).ToList();
		}
		private int Ceil(double val)
		{
			int integer = (int)val;
			return val > integer ? ++integer : integer;
		}

	}
}