using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        SMSReader db = new SMSReader();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult UpdateTime()
        {
            return View();
        }
        public ActionResult Winner()
        {
            return View();
        }
       
    }

   
}
