using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceApp.Models;

namespace EcommerceApp.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext dc= new DataClasses1DataContext();
        public ActionResult Index()
        {

            return View(dc.Products.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}