using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Aop;
using Utility;
using BLL;
using DbFrame;
using DbFrame.Class;
using Model;

namespace HPlus.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
        T_Users tuser = new T_Users();
        T_MenuBL tmenubl = new T_MenuBL();
        DBContext db = new DBContext();
        List<string> li = new List<string>();

        public ActionResult Index()
        {
            tuser = db.Find<T_Users>(w => w.uUsers_ID == Tools.getSession("UserID").To_Guid());
            ViewData["userName"] = tuser.cUsers_Name;
            //获取页面菜单
            ViewData["Menu"] = tmenubl.CreateSysMenu();
            return View();
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult HomePage()
        {
            return View();
        }

    }
}
