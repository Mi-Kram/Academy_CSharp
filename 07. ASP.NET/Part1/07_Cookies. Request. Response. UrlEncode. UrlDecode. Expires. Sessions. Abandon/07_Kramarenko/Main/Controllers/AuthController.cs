using Main.Models;
using Main.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
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
    [ValidateAntiForgeryToken]
    public ActionResult Save(AuthViewModel model)
		{
      using(CarDatabase db = new CarDatabase())
			{
        if (model.IsLoginForm)
        {
          var findUsers = db.people.Where(x => x.login == model.Login);
          if (findUsers.Count() == 1)
          {
            if (findUsers.First().password == MyHasher.GetHash(model.Password))
            {
              Session[SessionKeys.USER_LOGIN] = model.Login;
              if(Session[SessionKeys.USER_ADMIN_TOGGLE] != null) 
                Session[SessionKeys.USER_ADMIN_TOGGLE] = false;

              Session[SessionKeys.CAR_TABLE_SORTED_COLUMN] = 1;
              Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION] = true;

              return RedirectToAction("Index", "Car");
            }
            else
            {
              ModelState.AddModelError("Password", "Wrong password!");
              model = new AuthViewModel(true){ Login = model.Login };
              return View("Login", model);
            }
          }
          else
          {
            ModelState.AddModelError("Login", "Wrong login!");
            model = new AuthViewModel(true) { Login = model.Login };
            return View("Login", model);
          }
        }
        else
        {
          SqlConnection sqlConnection = EF_Helper.GetDatabaseConnection("ASP_NET_CAR_DB", ".\\SQLExpress", true);
          sqlConnection.Open();

          SqlCommand sqlCommand = new SqlCommand("select dbo.IsLoginExsist(@login)", sqlConnection);
          SqlParameter loginParam = new SqlParameter("@login", SqlDbType.VarChar, 20) { Value = model.Login };
          sqlCommand.Parameters.Add(loginParam);

          bool isLoginExsist = (bool)sqlCommand.ExecuteScalar();
          sqlConnection.Close();

          if (isLoginExsist)
          {
            ModelState.AddModelError("Login", "Login is busy! Choose another one!");
            model = new AuthViewModel(false) { Login = model.Login };
            return View("Register", model);
          }
          else
          {
            db.people.Add(new person()
            {
              login = model.Login,
              password = MyHasher.GetHash(model.Password),
              isAdmin = false
            });
            db.SaveChanges();

            Session[SessionKeys.USER_LOGIN] = model.Login;
            if (Session[SessionKeys.USER_ADMIN_TOGGLE] != null)
              Session[SessionKeys.USER_ADMIN_TOGGLE] = false;

            Session[SessionKeys.CAR_TABLE_SORTED_COLUMN] = 1;
            Session[SessionKeys.CAR_TABLE_SORTED_COLUMN_DIRECTION] = true;

            return RedirectToAction("Index", "Car");
          }
        }
      }
    }
  }
}