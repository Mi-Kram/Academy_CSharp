using Main.Models;
using Main.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Main
{
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      FileDB fileDB = new FileDB()
      {
        Users = UserTestClass.GetUsers(),
        Cars = CarTestClass.GetCars(),
        CarTypes = CarTypeTestClass.GetTypes()
      };

      Serializer.Serelize(fileDB, FileDB.DBFilePath, fileDB.GetType());
    }
  } 
}


/*
1. TypeDescriptor не добавляет атрибут
   File: AuthViewModel.cs
   line: 39


2. ViewModel не обновляется
   File: AuthController.cs
   line: 59, 68, 90


3. Save не принимает пераметр
   File: CarController
   line: 88

 */

