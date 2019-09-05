using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Himvc.Controllers
{
    public class HellowWorldController : Controller
    {
        // GET: HellowWorld

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome(string name, int numTimes = 1)
        {
            ViewBag.Message = "黑 " + name;
            ViewBag.NumTimes = numTimes;

            return View();
        }
    }
}