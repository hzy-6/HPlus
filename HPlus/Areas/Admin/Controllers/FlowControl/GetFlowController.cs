using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPlus.Areas.Admin.Controllers.FlowControl
{
    [ChildActionOnly]
    public class GetFlowController : Controller
    {
        //
        // GET: /Admin/GetFlow/

        public ActionResult Index(string FormID)
        {
            return PartialView("", FormID);
        }

    }
}
