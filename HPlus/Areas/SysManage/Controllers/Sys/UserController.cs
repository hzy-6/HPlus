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
    public class UserController : BaseController
    {
        // 用户管理
        // GET: /ManageSys/User/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.MenuID = "Z-100";
        }

        T_Users tuser = new T_Users();
        T_UsersRoles tuserrole = new T_UsersRoles();
        T_Roles troles = new T_Roles();
        T_UsersBL tuserbl = new T_UsersBL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }

        #region  查询数据列表
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [NonAction]
        public override PagingEntity GetPagingEntity(FormCollection fc, int page = 1, int rows = 20)
        {
            //检索
            var query = this.FormCollectionToHashtable(fc);
            //获取列表
            return tuserbl.GetDataSource(query, page, rows);
        }
        #endregion  查询数据列表

        #region  基本操作，增删改查
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(T_Users model, string uRoles_ID)
        {
            tuser = new T_Users();
            tuser = model;
            switch (Tools.getGuid(model.uUsers_ID).Equals(Guid.Empty))
            {
                case true:
                    if (string.IsNullOrEmpty(tuser.cUsers_LoginPwd))
                        tuser.cUsers_LoginPwd = Tools.MD5Encrypt("123456");
                    else
                        tuser.cUsers_LoginPwd = Tools.MD5Encrypt(model.cUsers_LoginPwd);
                    this.KeyID = db.Add(tuser, ref li);
                    if (Tools.getGuid(KeyID).Equals(Guid.Empty))
                        throw new MessageBox(db.ErrorMessge);
                    //用户角色
                    tuserrole.uUsersRoles_UsersID = tuser.uUsers_ID;
                    tuserrole.uUsersRoles_RoleID = Tools.getGuid(uRoles_ID);
                    if (Tools.getGuid(db.Add(tuserrole, ref li)).Equals(Guid.Empty))
                        throw new MessageBox(db.ErrorMessge);
                    break;
                case false:
                    this.KeyID = Tools.getGuidString(model.uUsers_ID);
                    if (!string.IsNullOrEmpty(tuser.cUsers_LoginPwd))
                        tuser.cUsers_LoginPwd = Tools.MD5Encrypt(model.cUsers_LoginPwd);
                    if (!db.Edit(tuser, ref li))
                        throw new MessageBox(db.ErrorMessge);
                    //用户角色
                    tuserrole.uUsersRoles_UsersID = tuser.uUsers_ID;
                    if (!db.Delete(tuserrole, ref li))
                        throw new MessageBox(db.ErrorMessge);
                    tuserrole = new T_UsersRoles();
                    tuserrole.uUsersRoles_UsersID = tuser.uUsers_ID;
                    tuserrole.uUsersRoles_RoleID = Tools.getGuid(uRoles_ID);
                    if (Tools.getGuid(db.Add(tuserrole, ref li)).Equals(Guid.Empty))
                        throw new MessageBox(db.ErrorMessge);
                    break;
                default:
                    break;
            }
            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);
            return Json(new { status = 1, ID = KeyID }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Del(string ID)
        {
            switch (!Tools.getGuid(ID).Equals(Guid.Empty))
            {
                case true://单个删除
                    tuser.uUsers_ID = Tools.getGuid(ID);
                    if (!db.Delete(tuser, ref li))
                        throw new MessageBox(db.ErrorMessge);
                    break;
                case false://多个删除
                    if (ID.Contains("[]") || ID.Contains("[null]"))
                        throw new MessageBox("删除失败");
                    var list = db.JsonToList<string>(ID);
                    foreach (var item in list)
                    {
                        tuser.uUsers_ID = Tools.getGuid(item);
                        if (!db.Delete(tuser, ref li))
                            throw new MessageBox(db.ErrorMessge);
                    }
                    break;
                default:
                    break;
            }
            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);
            return Json(new { status = 1 }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 查询根据ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Find(string ID)
        {
            tuser = new T_Users();
            tuser.uUsers_ID = Tools.getGuid(ID);
            tuser = db.Find(tuser);
            tuserrole = new T_UsersRoles();
            tuserrole.uUsersRoles_UsersID = tuser.uUsers_ID;
            tuserrole = db.Find(tuserrole);
            troles = new T_Roles();
            troles.uRoles_ID = tuserrole.uUsersRoles_RoleID;
            troles = db.Find(troles);

            tuser.cUsers_LoginPwd = "";
            var di = new ToJson().GetDictionary(new Dictionary<string, object>()
            {
                {"tuser",tuser},
                {"troles",troles},
                {"status",1}
            });
            di["dUsers_CreateTime"] = Tools.getDateTimeString(di["dUsers_CreateTime"], "yyyy-MM-dd");
            return Json(di, JsonRequestBehavior.DenyGet);
        }
        #endregion  基本操作，增删改查

    }
}
