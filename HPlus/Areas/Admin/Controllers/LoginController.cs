using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Aop;
using Application;
using Utility;
using BLL;
using Model;
using Utility.ValidateHelper;
using DBAccess;
using DBAccess.Entity;

namespace HPlus.Areas.Admin.Controllers
{
    [@ActionFilter(false)]
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/

        T_Users user = new T_Users();
        T_Roles troles = new T_Roles();
        T_UsersRoles tuserroles = new T_UsersRoles();
        DBContext db = new DBContext();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Checked(string username, string userpwd, string loginCode)
        {
            if (string.IsNullOrEmpty(username))
                throw new MessageBox("请输入用户名");
            if (string.IsNullOrEmpty(userpwd))
                throw new MessageBox("请输入密码");
            //if (string.IsNullOrEmpty(loginCode))
            //    throw new MessageBox("请输入验证码");
            user = new T_Users();
            user.cUsers_LoginName = username;
            user = db.Find<T_Users>(user);
            if (Tools.getGuid(user.uUsers_ID).Equals(Guid.Empty))
                throw new MessageBox("用户不存在");
            if (!Tools.getString(user.cUsers_LoginPwd).Trim().Equals(userpwd))//Tools.MD5Encrypt(userpwd)))//
                throw new MessageBox("密码错误");
            //string code = Tools.GetCookie("loginCode");
            //if (string.IsNullOrEmpty(code))
            //    throw new MessageBox("验证码失效");
            //if (!code.ToLower().Equals(loginCode))
            //    throw new MessageBox("验证码不正确");
            tuserroles = new T_UsersRoles();
            tuserroles.uUsersRoles_UsersID = user.uUsers_ID;
            tuserroles = db.Find<T_UsersRoles>(tuserroles);
            troles = new T_Roles();
            troles.uRoles_ID = tuserroles.uUsersRoles_RoleID;
            troles = db.Find<T_Roles>(troles);
            Session["UserID"] = user.uUsers_ID;

            if (user.cUsers_LoginName.Equals("admin"))
                Session["RoleID"] = "admin";
            else
                Session["RoleID"] = tuserroles.uUsersRoles_RoleID;
            return Json(new { status = 1, jumpurl = "/Admin/Home/" }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult yzm()
        {
            ValidateCodeHelper vch = new ValidateCodeHelper();
            string code = vch.GetRandomNumberString(4);
            Tools.WriteCookie("loginCode", code, 2);
            return File(vch.CreateImage(code), "image/jpeg");
        }

    }
}
