using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Aop;
using Application;
using DbFrame;
using DbFrame.Class;
using Utility;
using BLL;
using Model;

namespace HPlus.Areas.Admin.Controllers.Sys
{
    public class ChangePwdController : BaseController
    {
        //
        // GET: /ManageSys/ChangePwd/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            base.MenuID = "Z-150";
        }

        T_Users tuser = new T_Users();

        public override ActionResult Index()
        {
            tuser = db.Find<T_Users>(w => w.uUsers_ID == Tools.getSession("UserID").To_Guid());
            ViewData["userName"] = tuser.cUsers_Name;
            return View();
        }

        [HttpPost]
        public ActionResult ChangePwd(string oldpwd, string newpwd, string newlypwd)
        {
            if (string.IsNullOrEmpty(oldpwd))
                throw new MessageBox("旧密码不能为空");
            if (string.IsNullOrEmpty(newpwd))
                throw new MessageBox("新密码不能为空");
            if (string.IsNullOrEmpty(newlypwd))
                throw new MessageBox("确认新密码不能为空");
            if (!newpwd.Equals(newlypwd))
                throw new MessageBox("两次密码不一致");

            tuser = db.Find<T_Users>(w => w.uUsers_ID == Tools.getSession("UserID").To_Guid());
            if (!tuser.cUsers_LoginPwd.Equals(oldpwd.Trim()))//Tools.MD5Encrypt(oldpwd.Trim())))
                throw new MessageBox("旧密码不正确");
            tuser = new T_Users();
            tuser.uUsers_ID = Tools.getGuid(Tools.getSession("UserID"));
            tuser.cUsers_LoginPwd = newlypwd.Trim();//Tools.MD5Encrypt(newlypwd.Trim());
            if (!db.Edit<T_Users>(() => new T_Users()
            {
                cUsers_LoginPwd = newlypwd.Trim()
            }, w => w.uUsers_ID == Tools.getSession("UserID").To_Guid(), li))
                throw new MessageBox(db.ErrorMessge);
            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);
            return Json(new { status = 1 }, JsonRequestBehavior.DenyGet);
        }

    }
}
