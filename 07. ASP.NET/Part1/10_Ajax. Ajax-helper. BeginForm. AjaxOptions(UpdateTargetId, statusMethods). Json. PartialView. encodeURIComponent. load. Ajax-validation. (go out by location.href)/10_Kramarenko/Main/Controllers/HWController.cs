using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.Controllers
{
  public class HWController : Controller
  {
    // GET: HW
    public ActionResult Task1()
    {
      using(DB_Context db = new DB_Context())
			{
        var viewModel = db.authors.ToList();
        return View(viewModel);
			}
    }
    public ActionResult Task1Search(string au_search)
    {
      if (au_search == null) au_search = "";
      au_search = au_search.ToLower();

      using(DB_Context db = new DB_Context())
			{
        var viewModel = db.authors.Where(x => 
          x.au_fname.ToLower().Contains(au_search) ||
          x.au_lname.ToLower().Contains(au_search) ||
          x.address.ToLower().Contains(au_search) ||
          x.phone.ToLower().Contains(au_search) ||
          x.city.ToLower().Contains(au_search) ||
          x.state.ToLower().Contains(au_search)).ToList();

        return PartialView(viewModel);
			}
    }

    public ActionResult Task2()
    {
      using (DB_Context db = new DB_Context())
      {
        var viewModel = db.authors.ToList();
        return View(viewModel);
      }
    }

    public ActionResult Task2Search(string au_search)
    {
      if (au_search == null) au_search = "";
      au_search = au_search.ToLower();

      using (DB_Context db = new DB_Context())
      {
        var viewModel = db.authors.Where(x =>
          x.au_fname.ToLower().Contains(au_search) ||
          x.au_lname.ToLower().Contains(au_search) ||
          x.address.ToLower().Contains(au_search) ||
          x.phone.ToLower().Contains(au_search) ||
          x.city.ToLower().Contains(au_search) ||
          x.state.ToLower().Contains(au_search)).ToList();

        return Json(viewModel);
      }
    }

    public ActionResult Task3()
    {
      using (DB_Context db = new DB_Context())
      {
        var viewModel = db.authors.ToList();
        return View(viewModel);
      }
    }

    public ActionResult Task3Search(string au_search)
    {
      if (au_search == null) au_search = "";
      au_search = au_search.ToLower();

      using (DB_Context db = new DB_Context())
      {
        var viewModel = db.authors.Where(x =>
          x.au_fname.ToLower().Contains(au_search) ||
          x.au_lname.ToLower().Contains(au_search) ||
          x.address.ToLower().Contains(au_search) ||
          x.phone.ToLower().Contains(au_search) ||
          x.city.ToLower().Contains(au_search) ||
          x.state.ToLower().Contains(au_search)).ToList();

        return PartialView(viewModel);
      }
    }

    public ActionResult Task4()
    {
      using (DB_Context db = new DB_Context())
      {
        var viewModel = db.authors.ToList();
        return View(viewModel);
      }
    }

    public ActionResult Task4Search(string au_search)
    {
      if (au_search == null) au_search = "";
      au_search = au_search.ToLower();

      using (DB_Context db = new DB_Context())
      {
        var viewModel = db.authors.Where(x =>
          x.au_fname.ToLower().Contains(au_search) ||
          x.au_lname.ToLower().Contains(au_search) ||
          x.address.ToLower().Contains(au_search) ||
          x.phone.ToLower().Contains(au_search) ||
          x.city.ToLower().Contains(au_search) ||
          x.state.ToLower().Contains(au_search)).ToList();

        return Json(viewModel);
      }
    }









    public ActionResult Task5()
    {
      person person = new person();
      return View(person);
    }

    [ValidateAntiForgeryToken]
    public ActionResult Task5Show(person person)
    {
      if (!ModelState.IsValid)
      {
        return PartialView("Task5Wrong", person);
      }

      Dictionary<string, object> dic = new Dictionary<string, object>();
      dic.Add("person.FirsName", person.FirsName);
      dic.Add("person.LastName", person.LastName);
      dic.Add("person.Age", person.Age);

      return Json(new { url = Url.Action("Task5ShowResult", new System.Web.Routing.RouteValueDictionary(dic) )});
    }

    public ActionResult Task5ShowResult(person person)
		{
      return View("Task5Show", person);
		}


    public ActionResult Task6()
    {
      person person = new person();
      return View(person);
    }

    [ValidateAntiForgeryToken]
    public ActionResult Task6Show(person person)
		{
			if (!ModelState.IsValid)
			{
        var errorModel =
        from x in ModelState.Keys
        where ModelState[x].Errors.Count > 0
        select new
        {
          key = x,
          errors = ModelState[x].Errors.Select(y => y.ErrorMessage).ToArray()
        };

        return Json(new { success = false, errors = errorModel });
      }

      Dictionary<string, object> dic = new Dictionary<string, object>();
      dic.Add("person.FirsName", person.FirsName);
      dic.Add("person.LastName", person.LastName);
      dic.Add("person.Age", person.Age);

      return Json(new { url = Url.Action("Task6ShowResult", new System.Web.Routing.RouteValueDictionary(dic)) });
    }

    public ActionResult Task6ShowResult(person person)
    {
      return View("Task6Show", person);
    }
  }
}