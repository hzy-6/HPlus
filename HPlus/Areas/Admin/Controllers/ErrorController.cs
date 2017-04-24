using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Aop;
using Application;

namespace HPlus.Areas.Admin.Controllers
{
    [@ActionFilter(false)]
    [ExceptionFilter(false)]
    public class ErrorController : Controller
    {
        //
        // GET: /Admin/Error/

        [ValidateInput(false)]
        public ActionResult Index(CustomErrorModel cem)
        {
            ViewData = new ViewDataDictionary<CustomErrorModel>(cem);
            return View();
        }

    }
}
