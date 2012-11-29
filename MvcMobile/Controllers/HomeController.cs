using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Json;
using System.Net.Http.Formatting;

using System.Web.Script.Serialization;

namespace MvcMobile.Controllers
{





    public class Person
    {

        public string givenName { get; set; }
        public string sn { get; set; }
        public string cn { get; set; }
        public string eduPersonPrincipalName { get; set; }


    }

    public class personList
    {
        public List<Person> Person { get; set; }
    }






    public class HomeController : Controller
    {



       


        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.pageid = "mainpage";
            return View();
        }



      

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }
    }
}
