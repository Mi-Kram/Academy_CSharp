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
  public class AuthController : Controller
  {
    // GET: Auth
    public ActionResult Login()
    {
      AuthViewModel viewModel = new AuthViewModel(true);
      return View(viewModel);
    }
    public ActionResult Register()
    {
      AuthViewModel viewModel = new AuthViewModel(false);
      return View(viewModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult CheckLoginExist([Bind(Prefix = "Login")] string login)
    {
      try
      {
        login = login.ToLower();
        FileDB database = FileDB.GetDatabase();

        return Json(!database.Users.Select(x => x.Login.ToLower()).Contains(login));
      }
      catch { }

      return Json(false);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Save(AuthViewModel model)
		{
      FileDB database = FileDB.GetDatabase();

      if (model.IsLoginForm)
			{
        var findUsers = database.Users.Where(x => x.Login == model.Login);
        if (findUsers.Count() == 1)
				{
          if (findUsers.First().Password == model.Password)
          {
            Session[SessionKeys.USER_LOGIN] = model.Login;
            return RedirectToAction("Index", "Car");
          }
					else
					{
            model = new AuthViewModel(true)
            {
              Login = model.Login
            };
            return View("Login", model);
          }
        }
				else
				{
          model = new AuthViewModel(true);
          return View("Login", model);
				}
      }
			else
			{
        var findUsers = database.Users.Where(x => x.Login == model.Login);
        if (findUsers.Count() == 0)
        {
          database.Users.Add(new User()
          {
            Login = model.Login,
            Password = model.Password,
            IsAdmin = false,
            CarIDs = new List<int>()
          });

          FileDB.SetDatabase(database);
          Session[SessionKeys.USER_LOGIN] = model.Login;
          return RedirectToAction("Index", "Car");
        }
        else
        {
          model = new AuthViewModel(false)
          {
            Login = model.Login
          };
          return View("Register", model);
        }
      }
    }
  }
}