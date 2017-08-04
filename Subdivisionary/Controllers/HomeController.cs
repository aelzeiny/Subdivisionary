using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Subdivisionary.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        /*public ActionResult Contact()
        {
            ViewBag.Message = "Public Works, Street-Use and Mapping.";

            return View();
        }*/
    }
}