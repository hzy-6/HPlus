using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Aop;
using Application;
using DBAccess;
using DBAccess.Entity;
using Utility;
using BLL;
using Model;
using System.Collections;

namespace HPlus.Areas.SysManage.Controllers.Sys
{
    public class CanvasController : BaseController
    {
        //
        // GET: /SysManage/Canvas/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.MenuID = "Z-160";
        }

    }
}
