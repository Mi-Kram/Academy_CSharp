using Main.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		[AllowAnonymous]
		public ActionResult Index()
		{
			return View();
		}

		[Authorize(Roles = "User,Admin,MainAdmin")]
		public ActionResult MyProjects()
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				string login = User.Identity.Name;
				var user = db.Users.SingleOrDefault(x => x.UserName == login);
				if (user == null) return RedirectToAction("Index");
				return View(user.Projects.ToList());
			}
		}

		[Authorize(Roles = "User,Admin,MainAdmin")]
		public ActionResult MyProjectDetail(int? projectID)
		{
			if (!projectID.HasValue) return RedirectToAction("MyProjects");

			string login = User.Identity.Name;
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var user = db.Users.SingleOrDefault(x => x.UserName == login);
				if (user == null) return RedirectToAction("Index");
				if (user.Projects.Count(x => x.ID == projectID.Value) != 1)
					return RedirectToAction("MyProjects");

				var project = user.Projects.SingleOrDefault(x => x.ID == projectID.Value);
				project.Employees = project.Employees.ToList();
				return View(project);
			}
		}

		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult Employees()
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var users = db.Users.ToList();
				return View(users);
			}
		}

		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult EmployeeDetail(string employeeLogin)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var user = db.Users.SingleOrDefault(x => x.UserName == employeeLogin);
				if (user == null) return RedirectToAction("Employees");
				user.Projects = user.Projects.ToList();
				return View(user);
			}
		}

		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult DeleteProjectByEmployee(string employeeLogin, int? projectID)
		{
			if (projectID.HasValue)
			{
				using (ApplicationDbContext db = new ApplicationDbContext())
				{
					var user = db.Users.SingleOrDefault(x => x.UserName == employeeLogin);
					if (user == null) return RedirectToAction("Employees");

					for (int i = 0; i < user.Projects.Count; i++)
					{
						var project = user.Projects.ElementAt(i);
						if (project.ID == projectID)
						{
							user.Projects.Remove(project);
							i--;
							db.SaveChanges();
						}
					}
				}
			}

			return RedirectToAction("EmployeeDetail", new { employeeLogin = employeeLogin });
		}

		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult AddProjectToEmployee(string employeeLogin)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var user = db.Users.SingleOrDefault(x => x.UserName == employeeLogin);
				if (user == null) return RedirectToAction("Employees");

				var newProjects = db.Projects.ToList().Except(user.Projects.ToList()).ToList();

				AddProjectToEmployeeViewModel viewModel = new AddProjectToEmployeeViewModel()
				{
					Projects = newProjects,
					UserLogin = employeeLogin
				};
				return View(viewModel);
			}
		}

		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult AddProjectToEmployeeValue(string employeeLogin, int? projectID)
		{
			if (!projectID.HasValue)
				return RedirectToAction("EmployeeDetail", new { employeeLogin = employeeLogin });

			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var user = db.Users.SingleOrDefault(x => x.UserName == employeeLogin);
				if (user == null) return RedirectToAction("Employees");

				if (!user.Projects.Select(x => x.ID).Contains(projectID.Value))
				{
					var project = db.Projects.SingleOrDefault(x => x.ID == projectID);
					if (project != null)
					{
						user.Projects.Add(project);
						db.SaveChanges();
					}
				}
			}

			return RedirectToAction("EmployeeDetail", new { employeeLogin = employeeLogin });
		}


		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult Projects()
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var projects = db.Projects.ToList();
				return View(projects);
			}
		}

		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult ProjectDetail(int? projectID)
		{
			if (!projectID.HasValue) return RedirectToAction("Projects");

			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var project = db.Projects.SingleOrDefault(x => x.ID == projectID);
				if (project == null) return RedirectToAction("Projects");
				project.Employees = project.Employees.ToList();
				return View(project);
			}
		}

		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult DeleteEmployeeByProject(int? projectID, string employeeLogin)
		{
			if (!projectID.HasValue) return RedirectToAction("Projects");

			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var project = db.Projects.SingleOrDefault(x => x.ID == projectID);
				if (project == null) return RedirectToAction("Projects");

				for (int i = 0; i < project.Employees.Count; i++)
				{
					var employee = project.Employees.ElementAt(i);
					if (employee.UserName == employeeLogin)
					{
						project.Employees.Remove(employee);
						i--;
						db.SaveChanges();
					}
				}
			}

			return RedirectToAction("ProjectDetail", new { projectID = projectID });
		}

		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult AddEmployeeToProject(int? projectID)
		{
			if (!projectID.HasValue) return RedirectToAction("Projects");

			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var project = db.Projects.SingleOrDefault(x => x.ID == projectID);
				if (project == null) return RedirectToAction("Projects");

				var newEmployees = db.Users.ToList().Except(project.Employees.ToList()).ToList();

				AddEmployeeToProjectViewModel viewModel = new AddEmployeeToProjectViewModel()
				{
					Employees = newEmployees,
					ProjectID = projectID.Value
				};
				return View(viewModel);
			}
		}

		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult AddEmployeeToProjectValue(int? projectID, string employeeLogin)
		{
			if (!projectID.HasValue) return RedirectToAction("Projects");

			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var project = db.Projects.SingleOrDefault(x => x.ID == projectID);
				if (project == null) return RedirectToAction("Projects");

				if (!project.Employees.Select(x => x.UserName).Contains(employeeLogin))
				{
					var employee = db.Users.SingleOrDefault(x => x.UserName == employeeLogin);
					if (employee != null)
					{
						project.Employees.Add(employee);
						db.SaveChanges();
					}
				}
			}

			return RedirectToAction("ProjectDetail", new { projectID = projectID });
		}

		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult CreateProjectForm()
		{
			ProjectFormViewModel viewModel = new ProjectFormViewModel();
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin,MainAdmin")]
		public ActionResult CreateProject(ProjectFormViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				Project project = new Project()
				{
					Caption = viewModel.Caption,
					Description = viewModel.Description
				};

				using(ApplicationDbContext db = new ApplicationDbContext())
				{
					db.Projects.Add(project);
					db.SaveChanges();
				}

				return RedirectToAction("Projects");
			}
			return View("CreateProjectForm", viewModel);
		}


		[Authorize(Roles = "MainAdmin")]
		public ActionResult Admins()
		{
			using(ApplicationDbContext db = new ApplicationDbContext())
			{
				var adminRole = db.Roles.Where(x => x.Name == "Admin").SingleOrDefault();
				if (adminRole == null) return RedirectToAction("Index");
				var adminIDs = adminRole.Users.Select(x => x.UserId);
				var admins = db.Users.Where(x => adminIDs.Contains(x.Id)).ToList();
				return View(admins);
			}
		}

		[Authorize(Roles = "MainAdmin")]
		public ActionResult DeleteAdmin(string adminLogin)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var admin = db.Users.SingleOrDefault(x => x.UserName == adminLogin);
				if (admin != null)
				{
					var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
					if (userManager.IsInRole(admin.Id, "Admin"))
					{
						userManager.RemoveFromRole(admin.Id, "Admin");
						userManager.AddToRole(admin.Id, "User");
						db.SaveChanges();
					}
				}
				return RedirectToAction("Admins");
			}
		}

		[Authorize(Roles = "MainAdmin")]
		public ActionResult AddAdmin()
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var userRole = db.Roles.Where(x => x.Name == "User").SingleOrDefault();
				if (userRole == null) return RedirectToAction("Index");
				var userIDs = userRole.Users.Select(x => x.UserId);
				var users = db.Users.Where(x => userIDs.Contains(x.Id)).ToList();
				return View(users);
			}
		}

		[Authorize(Roles = "MainAdmin")]
		public ActionResult AddAdminValue(string userLogin)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				var user = db.Users.SingleOrDefault(x => x.UserName == userLogin);
				if(user != null)
				{
					var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
					if (userManager.IsInRole(user.Id, "User"))
					{
						userManager.RemoveFromRole(user.Id, "User");
						userManager.AddToRole(user.Id, "Admin");
						db.SaveChanges();
					}
				}

				return RedirectToAction("Admins");
			}
		}
	}
}