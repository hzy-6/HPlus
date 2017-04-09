using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Aop;

namespace HPlus.Controllers
{
    [@ActionFilter(false)]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Login", new { area = "Admin" });
        }

        public ActionResult TestPage()
        {
            return View();
        }

    }
}
