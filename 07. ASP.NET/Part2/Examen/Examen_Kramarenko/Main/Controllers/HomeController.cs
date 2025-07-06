using Main.Areas.Identity.Data;
using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Controllers
{
	[Authorize(Roles = "Master")]
	public class HomeController : Controller
	{
		private readonly ApplicationDBContext _context;
		public HomeController(ApplicationDBContext context)
		{
			_context = context;
		}

		private async Task<string> GetMyIDAsync()
		{
			if (!User.Identity.IsAuthenticated) throw new Exception("Fail user authentication!");
			var users = _context.Users.Where(x => x.UserName == User.Identity.Name);
			if (users.Count() != 1) throw new Exception("Fail Identity!");
			return (await users.FirstAsync()).Id;
		}

		public async Task<IActionResult> IndexAsync()
		{
			MasterIndexViewModel viewModel = new MasterIndexViewModel();
			string id = await GetMyIDAsync();

			viewModel.Categories = await _context.Categories.OrderBy(x => x.Value).ToListAsync();
			viewModel.Orders = await _context.Orders
				.Where(x => x.MasterID == id && !x.IsDone)
				.Include(x => x.Product)
				.ThenInclude(x => x.Category)
				.Include(x => x.Client)
				.OrderBy(x => x.Product.Category.Value)
				.ToListAsync();

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> SortOrders(int? categoryID, int? column, bool? direction)
		{
			if(!column.HasValue || !direction.HasValue)
			{
				categoryID = 0;
				column = 1;
				direction = true;
			}
			IEnumerable<Order> orders = null;
			string myID = await GetMyIDAsync();

			if (categoryID == 0) orders = _context.Orders
				 .Where(x => x.MasterID == myID && !x.IsDone).Include(x => x.Client)
				 .Include(x => x.Product).ThenInclude(x => x.Category);
			else
				orders = _context.Orders.Include(x => x.Client)
				.Include(x => x.Product).ThenInclude(x => x.Category)
				.Where(x => x.MasterID == myID && !x.IsDone && x.Product.CategoryID == categoryID);

			switch (column)
			{
				case 1:
					if (direction.Value) orders = orders.OrderBy(x => x.Product.Category.Value).ThenBy(x => x.Product.Brand).ThenBy(x => x.Product.Model);
					else orders = orders.OrderByDescending(x => x.Product.Category.Value).ThenByDescending(x => x.Product.Brand).ThenByDescending(x => x.Product.Model);
					break;
				case 2:
					if (direction.Value) orders = orders.OrderBy(x => x.Client.FirstName).ThenBy(x => x.Client.LastName);
					else orders = orders.OrderByDescending(x => x.Client.FirstName).ThenByDescending(x => x.Client.LastName);
					break;
				case 3:
					if (direction.Value) orders = orders.OrderBy(x => x.Salary);
					else orders = orders.OrderByDescending(x => x.Salary);
					break;
				case 4:
					if (direction.Value) orders = orders.OrderBy(x => x.StartDate);
					else orders = orders.OrderByDescending(x => x.StartDate);
					break;
				default:
					if (direction.Value) orders = orders.OrderBy(x => x.Product.Category.Value).ThenBy(x => x.Product.Brand).ThenBy(x => x.Product.Model);
					else orders = orders.OrderByDescending(x => x.Product.Category.Value).ThenByDescending(x => x.Product.Brand).ThenByDescending(x => x.Product.Model);
					break;
			}

			return Json(new
			{
				orders = orders.Select(x => new
				{
					id = x.ID,
					category = x.Product.Category.Value,
					client = $"{x.Client.FirstName} {x.Client.LastName}",
					startDate = x.StartDate.ToString(@"dd.MM.yyyy HH:mm"),
					brand = x.Product.Brand,
					model = x.Product.Model,
					yearIssue = x.Product.YearIssue,
					salary = x.Salary
				}),
				completeUrl = Url.Action("CompleteOrder", "Home")
			});
		}

		[HttpGet]
		public async Task<IActionResult> CreateOrderAsync()
		{
			MasterCreateOrderViewModel viewModel = new MasterCreateOrderViewModel();
			viewModel.MyID = await GetMyIDAsync();
			viewModel.Categories = await _context.Categories.OrderBy(x => x.Value).ToListAsync();
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateProduct(Product product)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _context.Products.AddAsync(product);
					await _context.SaveChangesAsync();
					return Json(new
					{
						success = new
						{
							id = product.ID,
							category = (await _context.Categories.Where(x => x.ID == product.CategoryID).FirstOrDefaultAsync())?.Value ?? "",
							brand = product.Brand,
							model = product.Model,
							serialNumber = product.SerialNumber,
							yearIssue = product.YearIssue
						}
					});
				}
				catch { }

				ModelState.AddModelError(string.Empty, "Неудачная попытка создания товара!");
			}

			return Json(new
			{
				errors = ModelState
				.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
				.Select(x => new
				{
					key = x.Key,
					value = (x.Value.Errors?.FirstOrDefault()?.ErrorMessage ?? "")
				}).Select(x => new 
				{ 
					key = x.key,
					value = x.value.Contains(@"''") ? $"The {x.key} field is required." : x.value
				})
			});
		}

		[HttpPost]
		public async Task<IActionResult> FindProduct(string serialNumber)
		{
			if (serialNumber == null) Json(null);
			return Json(_context.Products
				.Where(x => x.SerialNumber == serialNumber)
				.Include(x => x.Category)
				.Select(x => new
				{
					id = x.ID,
					category = x.Category.Value,
					brand = x.Brand,
					model = x.Model,
					serialNumber = x.SerialNumber,
					yearIssue = x.YearIssue
				}));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateClient(ApplicationClient client)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _context.Clients.AddAsync(client);
					await _context.SaveChangesAsync();
					return Json(new
					{
						success = new
						{
							id = client.ID,
							fName = client.FirstName,
							lName = client.LastName,
							passportId = client.PassportID,
							birthday = client.Birthday.Value.ToString(@"dd.MM.yyyy HH:mm")
						}
					});
				}
				catch { }

				ModelState.AddModelError(string.Empty, "Неудачная попытка создания товара!");
			}

			return Json(new
			{
				errors = ModelState
				.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
				.Select(x => new
				{
					key = x.Key,
					value = (x.Value.Errors?.FirstOrDefault()?.ErrorMessage ?? "")
				}).Select(x => new
				{
					key = x.key,
					value = x.value.Contains(@"''") ? $"The {x.key} field is required." : x.value
				})
			});
		}

		[HttpPost]
		public async Task<IActionResult> FindClient(string passportID)
		{
			if (passportID == null) Json(null);
			return Json(_context.Clients
				.Where(x => x.PassportID == passportID)
				.Select(x => new
				{
					id = x.ID,
					fName = x.FirstName,
					lName = x.LastName,
					passportId = x.PassportID,
					birthday = x.Birthday.Value.ToString(@"dd.MM.yyyy HH:mm")
				}));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateOrder(Order order)
		{
			if (ModelState.IsValid)
			{
				try
				{
					order.StartDate = DateTime.Now;
					order.IsDone = false;
					await _context.Orders.AddAsync(order);
					await _context.SaveChangesAsync();
					return Json(new
					{
						url = Url.Action("Index", "Home")
					});
				}
				catch { }

				ModelState.AddModelError(string.Empty, "Неудачная попытка создания товара!");
			}

			return Json(new
			{
				errors = ModelState
				.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
				.Select(x => new
				{
					key = x.Key,
					value = (x.Value.Errors?.FirstOrDefault()?.ErrorMessage ?? "")
				}).Select(x => new
				{
					key = x.key,
					value = x.value.Contains(@"''") ? $"The {x.key} field is required." : x.value
				})
			});
		}

		[HttpPost]
		public async Task<IActionResult> OrderByCategory(int? categoryID)
		{
			if (categoryID.HasValue) Json(null);
			List<Order> orders = null;
			string myID = await GetMyIDAsync();

			if (categoryID == 0) 
				orders = await _context.Orders
				.Include(x => x.Product).ThenInclude(x => x.Category)
				.Where(x => !x.IsDone && x.MasterID == myID)
				.Include(x => x.Client).ToListAsync();
			else
				orders = await _context.Orders
				.Include(x => x.Product).ThenInclude(x => x.Category)
				.Where(x => x.Product.CategoryID == categoryID && !x.IsDone && x.MasterID == myID)
				.Include(x => x.Client).ToListAsync();

			return Json(new {
				orders = orders.OrderBy(x => x.Product.Category.Value).Select(x => new
				{
					id = x.ID,
					category = x.Product.Category.Value,
					client = $"{x.Client.FirstName} {x.Client.LastName}",
					startDate = x.StartDate.ToString(@"dd.MM.yyyy HH:mm"),
					brand = x.Product.Brand,
					model = x.Product.Model,
					yearIssue = x.Product.YearIssue,
					salary = x.Salary
				}),
				completeUrl = Url.Action("CompleteOrder", "Home")
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CompleteOrder(int? orderID)
		{
			if (orderID.HasValue)
			{
				string myID = await GetMyIDAsync();
				var orders = _context.Orders.Where(x => x.ID == orderID);
				
				if (orders.Count() == 1)
				{
					Order order = await orders.FirstAsync();
					if (order.MasterID == myID)
					{
						order.IsDone = true;
						order.EndDate = DateTime.Now;
						await _context.SaveChangesAsync();
						return Json(true);
					}
				}
			}
			return Json(null);
		}

		public async Task<IActionResult> Completed()
		{
			MasterCompletedViewModel viewModel = new MasterCompletedViewModel();
			string id = await GetMyIDAsync();

			viewModel.Categories = await _context.Categories.OrderBy(x => x.Value).ToListAsync();
			viewModel.Orders = await _context.Orders
				.Where(x => x.MasterID == id && x.IsDone)
				.Include(x => x.Product)
				.ThenInclude(x => x.Category)
				.Include(x => x.Client)
				.OrderBy(x => x.Product.Category.Value)
				.ToListAsync();

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> CompletedOrderByCategory(int? categoryID)
		{
			if (categoryID.HasValue) Json(null);
			List<Order> orders = null;
			string myID = await GetMyIDAsync();

			if (categoryID == 0) 
				orders = await _context.Orders
				.Include(x => x.Product).ThenInclude(x => x.Category)
				.Where(x => x.IsDone && x.MasterID == myID)
				.Include(x => x.Client).ToListAsync();
			else
				orders = await _context.Orders
				.Include(x => x.Product).ThenInclude(x => x.Category)
				.Where(x => x.Product.CategoryID == categoryID && x.IsDone && x.MasterID == myID)
				.Include(x => x.Client).ToListAsync();

			return Json(new {
				orders = orders.OrderBy(x => x.Product.Category.Value).Select(x => new
				{
					id = x.ID,
					category = x.Product.Category.Value,
					client = $"{x.Client.FirstName} {x.Client.LastName}",
					startDate = x.StartDate.ToString(@"dd.MM.yyyy HH:mm"),
					endDate = x.EndDate.Value.ToString(@"dd.MM.yyyy HH:mm"),
					brand = x.Product.Brand,
					model = x.Product.Model,
					yearIssue = x.Product.YearIssue,
					salary = x.Salary
				})
			});
		}

		[HttpPost]
		public async Task<IActionResult> SortCompletedOrders(int? categoryID, int? column, bool? direction)
		{
			if (!column.HasValue || !direction.HasValue)
			{
				categoryID = 0;
				column = 1;
				direction = true;
			}
			IEnumerable<Order> orders = null;
			string myID = await GetMyIDAsync();

			if (categoryID == 0) orders = _context.Orders
				 .Where(x => x.MasterID == myID && x.IsDone).Include(x => x.Client)
				 .Include(x => x.Product).ThenInclude(x => x.Category);
			else
				orders = _context.Orders.Include(x => x.Client)
				.Include(x => x.Product).ThenInclude(x => x.Category)
				.Where(x => x.MasterID == myID && x.IsDone && x.Product.CategoryID == categoryID);

			switch (column)
			{
				case 1:
					if (direction.Value) orders = orders.OrderBy(x => x.Product.Category.Value).ThenBy(x => x.Product.Brand).ThenBy(x => x.Product.Model);
					else orders = orders.OrderByDescending(x => x.Product.Category.Value).ThenByDescending(x => x.Product.Brand).ThenByDescending(x => x.Product.Model);
					break;
				case 2:
					if (direction.Value) orders = orders.OrderBy(x => x.Client.FirstName).ThenBy(x => x.Client.LastName);
					else orders = orders.OrderByDescending(x => x.Client.FirstName).ThenByDescending(x => x.Client.LastName);
					break;
				case 3:
					if (direction.Value) orders = orders.OrderBy(x => x.Salary);
					else orders = orders.OrderByDescending(x => x.Salary);
					break;
				case 4:
					if (direction.Value) orders = orders.OrderBy(x => x.StartDate);
					else orders = orders.OrderByDescending(x => x.StartDate);
					break;
				case 5:
					if (direction.Value) orders = orders.OrderBy(x => x.EndDate);
					else orders = orders.OrderByDescending(x => x.EndDate);
					break;
				default:
					if (direction.Value) orders = orders.OrderBy(x => x.Product.Category.Value).ThenBy(x => x.Product.Brand).ThenBy(x => x.Product.Model);
					else orders = orders.OrderByDescending(x => x.Product.Category.Value).ThenByDescending(x => x.Product.Brand).ThenByDescending(x => x.Product.Model);
					break;
			}

			return Json(orders.Select(x => new
				{
					id = x.ID,
					category = x.Product.Category.Value,
					client = $"{x.Client.FirstName} {x.Client.LastName}",
					startDate = x.StartDate.ToString(@"dd.MM.yyyy HH:mm"),
					endDate = x.EndDate.Value.ToString(@"dd.MM.yyyy HH:mm"),
					brand = x.Product.Brand,
					model = x.Product.Model,
					yearIssue = x.Product.YearIssue,
					salary = x.Salary
				}));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
