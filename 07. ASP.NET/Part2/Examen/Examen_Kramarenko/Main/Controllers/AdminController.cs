using Main.Areas.Identity.Data;
using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Main.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly ApplicationDBContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public AdminController(
			ApplicationDBContext context,
			UserManager<ApplicationUser> userManager,
			IWebHostEnvironment hostingEnvironment)
		{
			_context = context;
			_userManager = userManager;
			_hostingEnvironment = hostingEnvironment;
		}

		#region Orders

		public async Task<IActionResult> Index()
		{
			AdminOrdersViewModel viewModel = new AdminOrdersViewModel()
			{
				Masters = await _context.Users.ToListAsync(),
				Orders = await _context.Orders.Include(x => x.Client).Include(x => x.Master)
					.Include(x => x.Product).ThenInclude(x => x.Category)
					.OrderBy(x => x.Product.Category.Value).ThenBy(x => x.Product.Brand)
					.ThenBy(x => x.Product.Model).ToListAsync()
			};

			for (int i = 0; i < viewModel.Masters.Count; i++)
			{
				if (!await _userManager.IsInRoleAsync(viewModel.Masters[i], "Master"))
				{
					viewModel.Masters.RemoveAt(i--);
				}
			}

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> OrderByMaster(string masterID)
		{
			List<Order> result = new List<Order>();
			if(masterID == null || masterID == "0")
			{
				result = await _context.Orders.Include(x => x.Client).Include(x => x.Master)
					.Include(x => x.Product).ThenInclude(x => x.Category).ToListAsync();
			}
			else
			{
				result = await _context.Orders.Where(x => x.MasterID == masterID).Include(x => x.Client)
					.Include(x => x.Master).Include(x => x.Product).ThenInclude(x => x.Category).ToListAsync();
			}

			return Json(new
			{
				orders = result.OrderBy(x => x.Product.Category.Value).ThenBy(x => x.Product.Brand)
					.ThenBy(x => x.Product.Model).Select(x => new
					{
						id = x.ID,
						product = $"{x.Product.Category.Value} {x.Product.Brand} {x.Product.Model}",
						client = $"{x.Client.FirstName} {x.Client.LastName}",
						master = x.Master.UserName,
						salary = x.Salary,
						startDate = x.StartDate.ToString(@"dd.MM.yyyy"),
						isDone = x.IsDone,
						endDate = x.IsDone && x.EndDate.HasValue ? x.EndDate.Value.ToString(@"dd.MM.yyyy") : "В работе"
					})
			});
		}

		[HttpPost]
		public async Task<IActionResult> SortOrders(string masterID, int? column, bool? direction)
		{
			if(!column.HasValue || !direction.HasValue)
			{
				column = 1;
				direction = true;
			}

			List<Order> result = new List<Order>();
			if(masterID == null || masterID == "0")
			{
				result = await _context.Orders.Include(x => x.Client).Include(x => x.Master)
					.Include(x => x.Product).ThenInclude(x => x.Category).ToListAsync();
			}
			else
			{
				result = await _context.Orders.Where(x => x.MasterID == masterID).Include(x => x.Client)
					.Include(x => x.Master).Include(x => x.Product).ThenInclude(x => x.Category).ToListAsync();
			}

			switch (column)
			{
				case 1:
					if (direction.Value) result = result.OrderBy(x => x.Product.Category.Value)
							.ThenBy(x => x.Product.Brand).ThenBy(x => x.Product.Model).ToList();
					else result = result.OrderByDescending(x => x.Product.Category.Value)
							.ThenByDescending(x => x.Product.Brand).ThenByDescending(x => x.Product.Model).ToList();
					break;
				case 2:
					if (direction.Value) result = result.OrderBy(x => x.Client.FirstName)
							.ThenBy(x => x.Client.LastName).ToList();
					else result = result.OrderByDescending(x => x.Client.FirstName)
							.ThenByDescending(x => x.Client.LastName).ToList();
					break;
				case 3:
					if (direction.Value) result = result.OrderBy(x => x.Master.UserName).ToList();
					else result = result.OrderByDescending(x => x.Master.UserName).ToList();
					break;
				case 4:
					if (direction.Value) result = result.OrderBy(x => x.Salary).ThenBy(x => x.Product.Category.Value)
							.ThenBy(x => x.Product.Brand).ThenBy(x => x.Product.Model).ToList();
					else result = result.OrderByDescending(x => x.Salary).ThenByDescending(x => x.Product.Category.Value)
							.ThenByDescending(x => x.Product.Brand).ThenByDescending(x => x.Product.Model).ToList();
					break;
				case 5:
					if (direction.Value) result = result.OrderBy(x => x.StartDate).ThenBy(x => x.Product.Category.Value)
							.ThenBy(x => x.Product.Brand).ThenBy(x => x.Product.Model).ToList();
					else result = result.OrderByDescending(x => x.StartDate).ThenByDescending(x => x.Product.Category.Value)
							.ThenByDescending(x => x.Product.Brand).ThenByDescending(x => x.Product.Model).ToList();
					break;
				case 6:
					if (direction.Value) result = result.OrderBy(x => x.IsDone)
							.ThenBy(x => x.EndDate).ThenBy(x => x.Product.Category.Value)
							.ThenBy(x => x.Product.Brand).ThenBy(x => x.Product.Model).ToList();
					else result = result.OrderByDescending(x => x.IsDone)
							.ThenByDescending(x => x.EndDate).ThenByDescending(x => x.Product.Category.Value)
							.ThenByDescending(x => x.Product.Brand).ThenByDescending(x => x.Product.Model).ToList();
					break;
				default:
					if (direction.Value) result = result.OrderBy(x => x.Product.Category.Value)
							.ThenBy(x => x.Product.Brand).ThenBy(x => x.Product.Model).ToList();
					else result = result.OrderByDescending(x => x.Product.Category.Value)
							.ThenByDescending(x => x.Product.Brand).ThenByDescending(x => x.Product.Model).ToList();
					break;
			}

			return Json(new
			{
				orders = result.Select(x => new
					{
						id = x.ID,
						product = $"{x.Product.Category.Value} {x.Product.Brand} {x.Product.Model}",
						client = $"{x.Client.FirstName} {x.Client.LastName}",
						master = x.Master.UserName,
						salary = x.Salary,
						startDate = x.StartDate.ToString(@"dd.MM.yyyy"),
						isDone = x.IsDone,
						endDate = x.IsDone && x.EndDate.HasValue ? x.EndDate.Value.ToString(@"dd.MM.yyyy") : "В работе"
					})
			});
		}

		public async Task<IActionResult> AddOrder(bool? isNotFirst)
		{
			AdminCreateOrderModel model = new AdminCreateOrderModel();
			if (!isNotFirst.HasValue || isNotFirst == false)
			{
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.OrderID)}");
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ProductID)}");
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ClientID)}");
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.MasterID)}");
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.Salary)}");
				HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.FormType)}", "create");
			}
			else
			{
				model.ProductID = GetNullableInt32(HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ProductID)}"));
				model.ClientID = GetNullableInt32(HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ClientID)}"));
				model.MasterID = HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.MasterID)}");
				model.Salary = GetNullableDouble(HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.Salary)}"));

				if (model.ProductID.HasValue)
				{
					Product product = await _context.Products.Where(x => x.ID == model.ProductID)
						.Include(x => x.Category).FirstOrDefaultAsync();
					if(product == null)
					{
						model.ProductID = null;
						HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ProductID)}", null);
					}
					else
					{
						model.Product = $"{product.Category.Value} {product.Brand} {product.Model}";
						model.ProductYear = product.YearIssue.ToString();
						model.ProductSerialNumber = product.SerialNumber;
					}
				}
				if (model.ClientID.HasValue)
				{
					ApplicationClient client = await _context.Clients
						.Where(x => x.ID == model.ClientID).FirstOrDefaultAsync();
					if (client == null)
					{
						model.ClientID = null;
						HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ClientID)}", null);
					}
					else
					{
						model.Client = $"{client.FirstName} {client.LastName}";
						model.ClientBirthday = client.Birthday.Value.ToString(@"dd.MM.yyyy");
					}
				}
				if (model.MasterID != null && model.MasterID.Length != 0)
				{
					ApplicationUser master = await _context.Users
						.Where(x => x.Id == model.MasterID).FirstOrDefaultAsync();
					if (master != null && await _userManager.IsInRoleAsync(master, "Master"))
					{
						model.Master = master.UserName;
						model.MasterPhoto = master.PhotoPath;
					}
					else
					{
						model.MasterID = null;
						HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.MasterID)}", null);
					}
				}
			}

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateOrder(AdminCreateOrderModel model)
		{
			if (ModelState.IsValid)
			{
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.OrderID)}");
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ProductID)}");
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ClientID)}");
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.MasterID)}");
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.Salary)}");
				HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.FormType)}");

				Order order = new Order()
				{
					ProductID = model.ProductID.Value,
					ClientID = model.ClientID.Value,
					MasterID = model.MasterID,
					StartDate = DateTime.Now,
					IsDone = false,
					Salary = model.Salary.Value
				};

				await _context.Orders.AddAsync(order);
				await _context.SaveChangesAsync();

				return Json(new
				{
					url = Url.Action("Index", "Admin")
				});
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

		#region ChooseProduct

		[HttpGet]
		public async Task<IActionResult> ChooseProduct()
		{
			ViewBag.OrderFormType = HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.FormType)}");
			return View(await _context.Products.Include(x => x.Category).OrderBy(x => x.Category.Value)
				.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToListAsync());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ChooseProduct(int? productID)
		{
			if (productID.HasValue) HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ProductID)}", productID.ToString());
			return Json(null);
		}

		[HttpPost]
		public async Task<IActionResult> ChooseProductSort(int? column, bool? direction)
		{
			if (!column.HasValue || !direction.HasValue)
			{
				column = 1;
				direction = true;
			}

			List<Product> result = await _context.Products.Include(x => x.Category).ToListAsync();

			switch (column)
			{
				case 1:
					if (direction.Value) result = result.OrderBy(x => x.Category.Value)
							.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToList();
					else result = result.OrderByDescending(x => x.Category.Value)
							.ThenByDescending(x => x.Brand).ThenByDescending(x => x.Model).ToList();
					break;
				case 2:
					if (direction.Value) result = result.OrderBy(x => x.YearIssue).ThenBy(x => x.Category.Value)
							.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToList();
					else result = result.OrderByDescending(x => x.YearIssue).ThenByDescending(x => x.Category.Value)
							.ThenByDescending(x => x.Brand).ThenByDescending(x => x.Model).ToList();
					break;
				case 3:
					if (direction.Value) result = result.OrderBy(x => x.Price).ThenBy(x => x.Category.Value)
							.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToList();
					else result = result.OrderByDescending(x => x.Price).ThenByDescending(x => x.Category.Value)
							.ThenByDescending(x => x.Brand).ThenByDescending(x => x.Model).ToList();
					break;
				case 4:
					if (direction.Value) result = result.OrderBy(x => x.SerialNumber).ThenBy(x => x.Category.Value)
							.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToList();
					else result = result.OrderByDescending(x => x.SerialNumber).ThenByDescending(x => x.Category.Value)
							.ThenByDescending(x => x.Brand).ThenByDescending(x => x.Model).ToList();
					break;
				default:
					if (direction.Value) result = result.OrderBy(x => x.Category.Value)
							.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToList();
					else result = result.OrderByDescending(x => x.Category.Value)
							.ThenByDescending(x => x.Brand).ThenByDescending(x => x.Model).ToList();
					break;
			}

			return Json(new
			{
				items = result.Select(x => new
				{
					id = x.ID,
					product = $"{x.Category.Value} {x.Brand} {x.Model}",
					price = x.Price,
					year = x.YearIssue,
					snum = x.SerialNumber
				})
			});
		}

		#endregion

		#region ChooseClient

		[HttpGet]
		public async Task<IActionResult> ChooseClient()
		{
			ViewBag.OrderFormType = HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.FormType)}");
			return View(await _context.Clients.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToListAsync());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ChooseClient(int? clientID)
		{
			if (clientID.HasValue) HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ClientID)}", clientID.ToString());
			return Json(null);
		}

		[HttpPost]
		public async Task<IActionResult> ChooseClientSort(int? column, bool? direction)
		{
			if (!column.HasValue || !direction.HasValue)
			{
				column = 1;
				direction = true;
			}

			List<ApplicationClient> result = await _context.Clients.ToListAsync();

			switch (column)
			{
				case 1:
					if (direction.Value) result = result.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
					else result = result.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName).ToList();
					break;
				case 2:
					if (direction.Value) result = result.OrderBy(x => x.Address).ToList();
					else result = result.OrderByDescending(x => x.Address).ToList();
					break;
				case 3:
					if (direction.Value) result = result.OrderBy(x => x.Birthday).ToList();
					else result = result.OrderByDescending(x => x.Birthday).ToList();
					break;
				default:
					if (direction.Value) result = result.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
					else result = result.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName).ToList();
					break;
			}

			return Json(new
			{
				items = result.Select(x => new
				{
					id = x.ID,
					client = $"{x.FirstName} {x.LastName}",
					address = x.Address,
					birthday = x.Birthday.Value.ToString(@"dd.MM.yyyy")
				})
			});
		}

		#endregion

		#region ChooseMaster

		[HttpGet]
		public async Task<IActionResult> ChooseMaster()
		{
			ViewBag.OrderFormType = HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.FormType)}");
			
			List<ApplicationUser> masters = await _context.Users.OrderBy(x => x.UserName).ToListAsync();
			for (int i = 0; i < masters.Count; i++)
			{
				if(!await _userManager.IsInRoleAsync(masters[i], "Master"))
				{
					masters.RemoveAt(i--);
				}
			}

			return View(masters);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ChooseMaster(string masterID)
		{
			if (masterID != null) HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.MasterID)}", masterID.ToString());
			return Json(null);
		}

		[HttpPost]
		public async Task<IActionResult> ChooseMasterSort(int? column, bool? direction)
		{
			if (!column.HasValue || !direction.HasValue)
			{
				column = 1;
				direction = true;
			}

			List<ApplicationUser> lst = await _context.Users.ToListAsync();
			for (int i = 0; i < lst.Count; i++)
			{
				if (!await _userManager.IsInRoleAsync(lst[i], "Master"))
				{
					lst.RemoveAt(i--);
				}
			}

			switch (column)
			{
				case 1:
					if (direction.Value) lst = lst.OrderBy(x => x.UserName).ToList();
					else lst = lst.OrderByDescending(x => x.UserName).ToList();
					break;
				default:
					if (direction.Value) lst = lst.OrderBy(x => x.UserName).ToList();
					else lst = lst.OrderByDescending(x => x.UserName).ToList();
					break;
			}

			return Json(new
			{
				items = lst.Select(x => new
				{
					id = x.Id,
					master = x.UserName,
					photo = x.PhotoPath
				})
			});
		}

		#endregion

		[HttpGet]
		public async Task<IActionResult> EditOrder(int? orderID)
		{
			AdminCreateOrderModel model = new AdminCreateOrderModel();
			if (!orderID.HasValue || orderID == -1) 
			{
				orderID = GetNullableInt32(HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.OrderID)}"));
				if (!orderID.HasValue) return NotFound();

				model.OrderID = orderID;
				model.ProductID = GetNullableInt32(HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ProductID)}"));
				model.ClientID = GetNullableInt32(HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ClientID)}"));
				model.MasterID = HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.MasterID)}");
				model.Salary = GetNullableDouble(HttpContext.Session.GetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.Salary)}"));
			}
			else
			{
				Order order = await _context.Orders.Where(x => x.ID == orderID).FirstOrDefaultAsync();
				if (order == null) return NotFound();

				model.OrderID = order.ID;
				model.ProductID = order.ProductID;
				model.ClientID = order.ClientID;
				model.MasterID = order.MasterID;
				model.Salary = order.Salary;

				HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.OrderID)}", order.ID.ToString());
				HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ProductID)}", order.ProductID.ToString());
				HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ClientID)}", order.ClientID.ToString());
				HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.MasterID)}", order.MasterID);
				HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.Salary)}", order.Salary.ToString());
				HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.FormType)}", "edit");
			}

			if (model.ProductID.HasValue)
			{
				Product product = await _context.Products.Where(x => x.ID == model.ProductID)
					.Include(x => x.Category).FirstOrDefaultAsync();
				if (product == null)
				{
					model.ProductID = null;
					HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ProductID)}", null);
				}
				else
				{
					model.Product = $"{product.Category.Value} {product.Brand} {product.Model}";
					model.ProductYear = product.YearIssue.ToString();
					model.ProductSerialNumber = product.SerialNumber;
				}
			}
			if (model.ClientID.HasValue)
			{
				ApplicationClient client = await _context.Clients
					.Where(x => x.ID == model.ClientID).FirstOrDefaultAsync();
				if (client == null)
				{
					model.ClientID = null;
					HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ClientID)}", null);
				}
				else
				{
					model.Client = $"{client.FirstName} {client.LastName}";
					model.ClientBirthday = client.Birthday.Value.ToString(@"dd.MM.yyyy");
				}
			}
			if (model.MasterID != null && model.MasterID.Length != 0)
			{
				ApplicationUser master = await _context.Users
					.Where(x => x.Id == model.MasterID).FirstOrDefaultAsync();
				if (master != null && await _userManager.IsInRoleAsync(master, "Master"))
				{
					model.Master = master.UserName;
					model.MasterPhoto = master.PhotoPath;
				}
				else
				{
					model.MasterID = null;
					HttpContext.Session.SetString($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.MasterID)}", null);
				}
			}

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateOrder(AdminCreateOrderModel model)
		{
			if (!model.OrderID.HasValue) return Json(new { url = Url.Action("Index", "Admin") });
			if (ModelState.IsValid)
			{
				try
				{
					Order order = await _context.Orders.Where(x => x.ID == model.OrderID).FirstOrDefaultAsync();
					if(order == null) return Json(new { url = Url.Action("Index", "Admin") });

					order.ProductID = model.ProductID.Value;
					order.ClientID = model.ClientID.Value;
					order.MasterID = model.MasterID;
					order.Salary = model.Salary.Value;

					await _context.SaveChangesAsync();

					HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.OrderID)}");
					HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ProductID)}");
					HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.ClientID)}");
					HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.MasterID)}");
					HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.Salary)}");
					HttpContext.Session.Remove($"{nameof(AdminCreateOrderModel)}_{nameof(AdminCreateOrderModel.FormType)}");

					return Json(new { url = Url.Action("Index", "Admin") });
				}
				catch 
				{
					ModelState.AddModelError(string.Empty, "Choose another values");
				}
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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteOrder(List<int?> orderIDs)
		{
			List<int> result = new List<int>();

			if(orderIDs?.Count > 0)
			{
				foreach (int? id in orderIDs)
				{
					if (id.HasValue)
					{
						var orders = _context.Orders.Where(x => x.ID == id);
						result.AddRange(orders.Select(x => x.ID));
						_context.RemoveRange(orders);
					}
				}
				await _context.SaveChangesAsync();
			}

			return Json(result);
		}

		#endregion

		#region Masters

		public async Task<IActionResult> Masters()
		{
			AdminMastersViewModel viewModel = new AdminMastersViewModel();
			viewModel.Masters = await _context.Users.Include(x => x.Orders).ToListAsync();
			for (int i = 0; i < viewModel.Masters.Count; i++)
			{
				if (!await _userManager.IsInRoleAsync(viewModel.Masters[i], "Master"))
				{
					viewModel.Masters.RemoveAt(i--);
				}
			}
			return View(viewModel);
		}

		public async Task<IActionResult> SortMaster(int? column, bool? direction)
		{
			if (!column.HasValue || !direction.HasValue)
			{
				column = 1;
				direction = true;
			}

			List<ApplicationUser> lst = await _context.Users.Include(x => x.Orders).ToListAsync();
			for (int i = 0; i < lst.Count; i++)
			{
				if (!await _userManager.IsInRoleAsync(lst[i], "Master"))
				{
					lst.RemoveAt(i--);
				}
			}

			switch (column)
			{
				case 1:
					if (direction.Value) lst = lst.OrderBy(x => x.UserName).ToList();
					else lst = lst.OrderByDescending(x => x.UserName).ToList();
					break;
				case 2:
					if (direction.Value) lst = lst.OrderBy(x => x.Orders.Where(x => x.IsDone).Count()).ToList();
					else lst = lst.OrderByDescending(x => x.Orders.Where(x => x.IsDone).Count()).ToList();
					break;
				case 3:
					if (direction.Value) lst = lst.OrderBy(x => x.Orders.Where(x => !x.IsDone).Count()).ToList();
					else lst = lst.OrderByDescending(x => x.Orders.Where(x => !x.IsDone).Count()).ToList();
					break;
				default:
					if (direction.Value) lst = lst.OrderBy(x => x.UserName).ToList();
					else lst = lst.OrderByDescending(x => x.UserName).ToList();
					break;
			}

			return Json(lst.Select(x => new
			{
				id = x.Id,
				username = x.UserName,
				photo = x.PhotoPath,
				done = x.Orders.Where(o => o.IsDone).Count(),
				notdone = x.Orders.Where(o => !o.IsDone).Count()
			}));
		}

		public IActionResult AddMaster()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateMaster(MasterRegisterModel master)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = master.UserName,
					PhotoPath = "#"
				};
				var result = await _userManager.CreateAsync(user, master.Password);
				if (result.Succeeded)
				{
					string id = user.Id;
					var shortPath = $"/Images/UserPhotos/{id}{Path.GetExtension(master.Photo.FileName)}";
					var serverPath = $"{_hostingEnvironment.WebRootPath}{shortPath}";

					using (var fileStream = new FileStream(serverPath, FileMode.OpenOrCreate))
					{
						await master.Photo.CopyToAsync(fileStream);
					}
					user.PhotoPath = shortPath;
					await _userManager.UpdateAsync(user);

					await _userManager.AddToRoleAsync(user, "Master");
					return new JsonResult(new
					{
						url = Url.Action("Masters", "Admin")
					});
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
					break;
				}
			}

			return new JsonResult(new
			{
				errors = ModelState
				.Where(x => x.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
				.Select(x => new
				{
					key = x.Key,
					value = x.Value.Errors?.FirstOrDefault()?.ErrorMessage ?? ""
				})
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteMaster(List<string> masterIDs)
		{
			if (masterIDs == null) return Json(null);

			List<string> result = new List<string>();
			foreach (string id in masterIDs)
			{
				if (id == null || id.Length == 0) continue;

				ApplicationUser user = await _context.Users.Where(x => x.Id == id)
					.Include(x => x.Orders).FirstOrDefaultAsync();

				if (user == null) continue;
				_context.Orders.RemoveRange(user.Orders);
				_context.UserRoles.RemoveRange(_context.UserRoles.Where(x => x.UserId == user.Id));
				_context.UserClaims.RemoveRange(_context.UserClaims.Where(x => x.UserId == user.Id));
				_context.UserTokens.RemoveRange(_context.UserTokens.Where(x => x.UserId == user.Id));
				_context.UserLogins.RemoveRange(_context.UserLogins.Where(x => x.UserId == user.Id));
				_context.Users.Remove(user);
				result.Add(id);
			}

			await _context.SaveChangesAsync();
			return Json(result);
		}

		public async Task<IActionResult> EditMaster(string id)
		{
			ApplicationUser user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
			if (user == null) return NotFound();
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateMaster(UpdateMasterModel model)
		{
			if (model.Id == null || model.Id.Length == 0) return Json(new { url = Url.Action("Masters", "Admin") });

			if (model.UserName == null || model.UserName.Length == 0)
				ModelState.AddModelError(nameof(model.UserName), $"The {nameof(model.UserName)} field is required!");
			else if (model.UserName.Length < 4)
				ModelState.AddModelError(nameof(model.UserName), $"The {nameof(model.UserName)} is too short!");

			if (model.ChangePassword)
			{
				if (model.NewPassword == null || model.NewPassword.Length == 0)
					ModelState.AddModelError(nameof(model.NewPassword), $"The {nameof(model.NewPassword)} field is required!");
				else if (model.UserName.Length < 6)
					ModelState.AddModelError(nameof(model.NewPassword), $"The {nameof(model.NewPassword)} is too short!");

				if (model.NewPassword != model.ConfirmNewPassword)
					ModelState.AddModelError(nameof(model.ConfirmNewPassword), $"The {nameof(model.NewPassword)} and {nameof(model.ConfirmNewPassword)} is not match!");
			}

			if (ModelState.IsValid)
			{
				ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
				if (user == null) return Json(new { url = Url.Action("Masters", "Admin") });

				if (model.ChangePassword)
				{
					using(var passwordTran = await _context.Database.BeginTransactionAsync())
					{
						await _userManager.RemovePasswordAsync(user);
						var passwordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);

						if (passwordResult.Succeeded) await passwordTran.CommitAsync();
						else await passwordTran.RollbackAsync();
					}
				}

				user.UserName = model.UserName;

				if (model.Photo != null)
				{
					string id = user.Id;
					var shortPath = $"/Images/UserPhotos/{id}{Path.GetExtension(model.Photo.FileName)}";
					var serverPath = $"{_hostingEnvironment.WebRootPath}{shortPath}";
					if (user.PhotoPath != shortPath)
					{
						try
						{
							System.IO.File.Delete($"{_hostingEnvironment.WebRootPath}{user.PhotoPath}");
						}
						catch { }
					}

					try
					{
						using (var fileStream = new FileStream(serverPath, FileMode.OpenOrCreate))
						{
							await model.Photo.CopyToAsync(fileStream);
						}
					}
					catch { }
					user.PhotoPath = shortPath;
				}

				await _context.SaveChangesAsync();
				return Json(new { url = Url.Action("Masters", "Admin") });
			}

			return new JsonResult(new
			{
				errors = ModelState
				.Where(x => x.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
				.Select(x => new
				{
					key = x.Key,
					value = x.Value.Errors?.FirstOrDefault()?.ErrorMessage ?? ""
				})
			});
		}

		#endregion

		#region Categories

		public async Task<IActionResult> ProductCategories()
		{
			return View(await _context.Categories.OrderBy(x => x.Value).ToListAsync());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				if(!_context.Categories.Select(x => x.Value.ToLower()).Contains(category.Value.ToLower()))
				{
					await _context.Categories.AddAsync(category);
					await _context.SaveChangesAsync();

					return Json(new
					{
						items = _context.Categories.OrderBy(x => x.Value).Select(x => new
						{
							id = x.ID,
							value = x.Value
						})
					});
				}
				ModelState.AddModelError(nameof(category.Value), "The value already exists!");
			}

			return new JsonResult(new
			{
				errors = ModelState
				.Where(x => x.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
				.Select(x => new
				{
					key = x.Key,
					value = x.Value.Errors?.FirstOrDefault()?.ErrorMessage ?? ""
				})
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				Category _category = await _context.Categories.Where(x => x.ID == category.ID).FirstOrDefaultAsync();
				if(_category != null)
				{
					if(_context.Categories.Select(x => x.Value.ToLower()).Contains(category.Value.ToLower()))
					{
						ModelState.AddModelError(nameof(category.Value), "The value already exists!");
					}
					else
					{
						_category.Value = category.Value;
						await _context.SaveChangesAsync();

						return Json(new
						{
							items = _context.Categories.OrderBy(x => x.Value).Select(x => new
							{
								id = x.ID,
								value = x.Value
							})
						});
					}
				}
				else
				{
					ModelState.AddModelError(nameof(category.Value), "The category is not found!");
				}
			}

			return new JsonResult(new
			{
				errors = ModelState
				.Where(x => x.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
				.Select(x => new
				{
					key = x.Key,
					value = x.Value.Errors?.FirstOrDefault()?.ErrorMessage ?? ""
				})
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteCategory(List<int?> categoryIDs)
		{
			List<int> result = new List<int>();

			if (categoryIDs?.Count > 0)
			{
				foreach (int? id in categoryIDs)
				{
					if (id.HasValue)
					{
						var orders = _context.Orders.Include(x => x.Product).Where(x => x.Product.CategoryID == id);
						var products = orders.Select(x => x.Product);

						_context.Orders.RemoveRange(orders);
						_context.Products.RemoveRange(products);

						var categories = _context.Categories.Where(x => x.ID == id.Value);
						result.AddRange(categories.Select(x => x.ID));
						_context.Categories.RemoveRange(categories);
					}
				}
				await _context.SaveChangesAsync();
			}

			return Json(result);
		}


		#endregion

		#region Products

		public async Task<IActionResult> Products()
		{
			List<Product> products = await _context.Products.Include(x => x.Category)
				.OrderBy(x => x.Category.Value).ThenBy(x => x.Brand).ThenBy(x => x.Model).ToListAsync();

			return View(products);
		}

		[HttpPost]
		public async Task<IActionResult> SortProducts(int? column, bool? direction)
		{
			if(!column.HasValue || !direction.HasValue)
			{
				column = 1;
				direction = true;
			}

			List<Product> result = await _context.Products.Include(x => x.Category).ToListAsync();

			switch (column)
			{
				case 1:
					if (direction.Value) result = result.OrderBy(x => x.Category.Value)
							.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToList();
					else result = result.OrderByDescending(x => x.Category.Value)
							.ThenByDescending(x => x.Brand).ThenByDescending(x => x.Model).ToList();
					break;
				case 2:
					if (direction.Value) result = result.OrderBy(x => x.Price).ThenBy(x => x.Category.Value)
							.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToList();
					else result = result.OrderByDescending(x => x.Price).ThenByDescending(x => x.Category.Value)
							.ThenByDescending(x => x.Brand).ThenByDescending(x => x.Model).ToList();
					break;
				case 3:
					if (direction.Value) result = result.OrderBy(x => x.YearIssue).ThenBy(x => x.Category.Value)
							.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToList();
					else result = result.OrderByDescending(x => x.YearIssue).ThenByDescending(x => x.Category.Value)
							.ThenByDescending(x => x.Brand).ThenByDescending(x => x.Model).ToList();
					break;
				case 4:
					if (direction.Value) result = result.OrderBy(x => x.SerialNumber).ThenBy(x => x.Category.Value)
							.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToList();
					else result = result.OrderByDescending(x => x.SerialNumber).ThenByDescending(x => x.Category.Value)
							.ThenByDescending(x => x.Brand).ThenByDescending(x => x.Model).ToList();
					break;
				default:
					if (direction.Value) result = result.OrderBy(x => x.Category.Value)
							.ThenBy(x => x.Brand).ThenBy(x => x.Model).ToList();
					else result = result.OrderByDescending(x => x.Category.Value)
							.ThenByDescending(x => x.Brand).ThenByDescending(x => x.Model).ToList();
					break;
			}

			return Json(new
			{
				items = result.Select(x => new
				{
					id = x.ID,
					product = $"{x.Category.Value} {x.Brand} {x.Model}",
					price = x.Price,
					year = x.YearIssue,
					snum = x.SerialNumber
				})
			});
		}

		public async Task<IActionResult> AddProduct()
		{
			return View(await _context.Categories.OrderBy(x => x.Value).ToListAsync());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateProduct(Product product)
		{
			if (ModelState.IsValid)
			{
				await _context.Products.AddAsync(product);
				await _context.SaveChangesAsync();
				return Json(new
				{
					url = Url.Action("Products", "Admin")
				});
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

		public async Task<IActionResult> EditProduct(int? id)
		{
			if (!id.HasValue) return NotFound();

			Product product = await _context.Products.Where(x => x.ID == id).FirstOrDefaultAsync();
			if (product == null) return NotFound();

			AdminEditProductViewModel viewModel = new AdminEditProductViewModel()
			{
				Product = product,
				Categories = await _context.Categories.OrderBy(x => x.Value).ToListAsync()
			};
			return View(viewModel);
		}
	
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateProduct(Product product)
		{
			if (ModelState.IsValid)
			{
				Product prod = await _context.Products.Where(x => x.ID == product.ID).FirstOrDefaultAsync();
				if(prod != null)
				{
					prod.CategoryID = product.CategoryID;
					prod.Brand = product.Brand;
					prod.Model = product.Model;
					prod.Price = product.Price;
					prod.YearIssue = product.YearIssue;
					prod.SerialNumber = product.SerialNumber;
					await _context.SaveChangesAsync();
				}

				return Json(new
				{
					url = Url.Action("Products", "Admin")
				});
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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteProduct(List<int?> productIDs)
		{
			List<int> result = new List<int>();

			if (productIDs?.Count > 0)
			{
				foreach (int? id in productIDs)
				{
					if (id.HasValue)
					{
						var orders = _context.Orders.Where(x => x.ProductID == id);

						_context.Orders.RemoveRange(orders);

						var products = _context.Products.Where(x => x.ID == id.Value);
						result.AddRange(products.Select(x => x.ID));
						_context.Products.RemoveRange(products);
					}
				}
				await _context.SaveChangesAsync();
			}

			return Json(result);
		}

		#endregion

		#region Clients

		public async Task<IActionResult> Clients()
		{
			return View(await _context.Clients.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToListAsync());
		}

		[HttpPost]
		public async Task<IActionResult> SortClients(int? column, bool? direction)
		{
			if (!column.HasValue || !direction.HasValue)
			{
				column = 1;
				direction = true;
			}

			List<ApplicationClient> result = await _context.Clients.ToListAsync();

			switch (column)
			{
				case 1:
					if (direction.Value) result = result.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
					else result = result.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName).ToList();
					break;
				case 2:
					if (direction.Value) result = result.OrderBy(x => x.Address).ToList();
					else result = result.OrderByDescending(x => x.Address).ToList();
					break;
				case 3:
					if (direction.Value) result = result.OrderBy(x => x.Birthday).ToList();
					else result = result.OrderByDescending(x => x.Birthday).ToList();
					break;
				default:
					if (direction.Value) result = result.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
					else result = result.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName).ToList();
					break;
			}

			return Json(new
			{
				items = result.Select(x => new
				{
					id = x.ID,
					client = $"{x.FirstName} {x.LastName}",
					address = x.Address,
					birthday = x.Birthday.Value.ToString(@"dd.MM.yyyy")
				})
			});
		}

		public IActionResult AddClient()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateClient(ApplicationClient client)
		{
			if (ModelState.IsValid)
			{
				await _context.Clients.AddAsync(client);
				await _context.SaveChangesAsync();
				return Json(new
				{
					url = Url.Action("Clients", "Admin")
				});
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

		public async Task<IActionResult> EditClient(int? id)
		{
			if (!id.HasValue) return NotFound();

			ApplicationClient client = await _context.Clients.Where(x => x.ID == id).FirstOrDefaultAsync();
			if(client == null) return NotFound();

			return View(client);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateClient(ApplicationClient client)
		{
			if (ModelState.IsValid)
			{
				ApplicationClient _client = await _context.Clients.Where(x => x.ID == client.ID).FirstOrDefaultAsync();
				if(_client != null)
				{
					_client.FirstName = client.FirstName;
					_client.LastName = client.LastName;
					_client.Address = client.Address;
					_client.PassportID = client.PassportID;
					_client.Birthday = client.Birthday;
					await _context.SaveChangesAsync();
				}

				return Json(new
				{
					url = Url.Action("Clients", "Admin")
				});
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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteClient(List<int?> clientIDs)
		{
			List<int> result = new List<int>();

			if (clientIDs?.Count > 0)
			{
				foreach (int? id in clientIDs)
				{
					if (id.HasValue)
					{
						var orders = _context.Orders.Where(x => x.ClientID == id);

						_context.Orders.RemoveRange(orders);

						var clients = _context.Clients.Where(x => x.ID == id.Value);
						result.AddRange(clients.Select(x => x.ID));
						_context.Clients.RemoveRange(clients);
					}
				}
				await _context.SaveChangesAsync();
			}

			return Json(result);
		}


		#endregion

		private int? GetNullableInt32(string val)
		{
			if (val == null || val.Length == 0) return null;
			return int.TryParse(val, out int result) ? result : null;
		}
		private double? GetNullableDouble(string val)
		{
			if (val == null || val.Length == 0) return null;
			return double.TryParse(val, out double result) ? result : null;
		}
	}
}
