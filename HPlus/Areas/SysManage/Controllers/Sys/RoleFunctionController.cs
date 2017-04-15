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
    public class RoleFunctionController : BaseController
    {
        // 角色功能
        // GET: /ManageSys/RoleFunction/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            base.MenuID = "Z-140";
        }

        T_Roles troles = new T_Roles();
        T_Menu tmenu = new T_Menu();
        T_MenuBL tmenubl = new T_MenuBL();
        T_MenuFunction tmenufunction = new T_MenuFunction();
        T_Function tfunction = new T_Function();
        T_RoleMenuFunction trolemenufunction = new T_RoleMenuFunction();

        #region  基本操作，增删改查

        /// <summary>
        /// 获取角色菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleMenuFunctionTree(string roleid)
        {
            return Json(new { status = 1, value = tmenubl.GetRoleMenuFunctionTree(roleid) }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 保存角色功能
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(string json, string roleid)
        {
            if (Tools.getGuid(roleid).Equals(Guid.Empty))
                throw new MessageBox("请选择角色");
            var list = ((object[])jss.DeserializeObject(json)).ToList();
            if (list.Count > 0)
            {
                var guid_list = new List<Guid>();
                list.ForEach(item =>
                {
                    var func = (Dictionary<string, object>)item;
                    if (Tools.getString(func["tag"]).Equals("fun"))
                    {
                        var menuid = guid_list.Find(x => x.Equals(Tools.getGuid(func["pId"])));
                        if (Tools.getGuid(menuid).Equals(Guid.Empty))
                        {
                            trolemenufunction = new T_RoleMenuFunction();
                            trolemenufunction.uRoleMenuFunction_MenuID = Tools.getGuid(func["pId"]);
                            trolemenufunction.uRoleMenuFunction_RoleID = Tools.getGuid(roleid);
                            if (!db.Delete(trolemenufunction, ref li))
                                throw new MessageBox(db.ErrorMessge);
                        }
                        trolemenufunction = new T_RoleMenuFunction();
                        trolemenufunction.uRoleMenuFunction_MenuID = Tools.getGuid(func["pId"]);
                        trolemenufunction.uRoleMenuFunction_FunctionID = Tools.getGuid(func["id"]);
                        trolemenufunction.uRoleMenuFunction_RoleID = Tools.getGuid(roleid);
                        if (Tools.getGuid(db.Add(trolemenufunction, ref li)).Equals(Guid.Empty))
                            throw new MessageBox(db.ErrorMessge);
                        guid_list.Add(Tools.getGuid(func["pId"]));
                    }
                });
            }
            else
            {
                trolemenufunction = new T_RoleMenuFunction();
                trolemenufunction.uRoleMenuFunction_RoleID = Tools.getGuid(roleid);
                if (!db.Delete(trolemenufunction, ref li))
                    throw new MessageBox(db.ErrorMessge);
            }
            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);
            return Json(new { status = 1 }, JsonRequestBehavior.DenyGet);
        }


        #endregion 基本操作，增删改查
    }
}
