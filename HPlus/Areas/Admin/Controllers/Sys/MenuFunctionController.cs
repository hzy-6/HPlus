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
using System.Collections;

namespace HPlus.Areas.Admin.Controllers.Sys
{
    public class MenuFunctionController : BaseController
    {
        // 菜单功能
        // GET: /ManageSys/MenuFunction/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            base.MenuID = "Z-130";
        }

        T_Menu tmenu = new T_Menu();
        T_MenuBL tmenubl = new T_MenuBL();
        T_Function tfunction = new T_Function();
        T_MenuFunction tmenufunction = new T_MenuFunction();

        /// <summary>
        /// 获取菜单和功能树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuAndFunctionTree()
        {
            return Json(new { status = 1, value = tmenubl.GetMenuAndFunctionTree() }, JsonRequestBehavior.DenyGet);
        }




        #region  基本操作，增删改查
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(T_Menu model)
        {
            tmenu = new T_Menu();
            tmenu = model;
            switch (Tools.getGuid(model.uMenu_ID).Equals(Guid.Empty))
            {
                case true:
                    this.KeyID = Tools.getGuidString(db.Add(tmenu, li));
                    if (Tools.getGuid(KeyID).Equals(Guid.Empty))
                        throw new MessageBox(db.ErrorMessge);
                    break;
                case false:
                    this.KeyID = Tools.getGuidString(model.uMenu_ID);
                    if (!db.Edit<T_Menu>(tmenu, w => w.uMenu_ID == KeyID.To_Guid(), li))
                        throw new MessageBox(db.ErrorMessge);
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
                    if (!db.Delete<T_Menu>(w => w.uMenu_ParentID == ID.To_Guid(), li))
                        throw new MessageBox(db.ErrorMessge);

                    //删除菜单的功能
                    if (!db.Delete<T_MenuFunction>(w => w.uMenuFunction_MenuID == ID.To_Guid(), li))
                        throw new MessageBox(db.ErrorMessge);

                    if (!db.Delete<T_Menu>(w => w.uMenu_ID == ID.To_Guid(), li))
                        throw new MessageBox(db.ErrorMessge);
                    break;
                case false://多个删除
                    if (ID.Contains("[]") || ID.Contains("[null]"))
                        throw new MessageBox("删除失败");
                    var list = db.JsonToList<string>(ID);
                    foreach (var item in list)
                    {
                        if (!db.Delete<T_Menu>(w => w.uMenu_ParentID == item.To_Guid(), li))
                            throw new MessageBox(db.ErrorMessge);

                        //删除菜单的功能
                        if (!db.Delete<T_MenuFunction>(w => w.uMenuFunction_MenuID == item.To_Guid(), li))
                            throw new MessageBox(db.ErrorMessge);

                        if (!db.Delete<T_Menu>(w => w.uMenu_ID == item.To_Guid(), li))
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
            tmenu = db.Find<T_Menu>(w => w.uMenu_ID == ID.To_Guid());

            if (Tools.getGuid(ID).Equals(Guid.Empty))
            {
                tmenu.dMenu_CreateTime = Tools.getDateTime(DateTime.Now);
            }

            var menu = db.Find<T_Menu>(w => w.uMenu_ID == tmenu.uMenu_ParentID);

            var di = new ToJson().GetDictionary(new Dictionary<string, object>()
            {
                {"tmenu",tmenu},
                {"pname",Tools.getString( menu.cMenu_Name)},
                {"status",1}
            });
            di["dMenu_CreateTime"] = di["dMenu_CreateTime"].To_DateTimeString("yyyy-MM-dd HH:mm:ss");
            return Json(di, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 保存菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveMenuFunction(string nodes)
        {
            var json = ((object[])jss.DeserializeObject(nodes)).ToList();
            var list = new List<Guid>();
            if (!db.Delete<T_MenuFunction>(null, li))
                throw new MessageBox(db.ErrorMessge);
            json.ForEach(item =>
            {
                var func = (Dictionary<string, object>)item;
                if (Tools.getString(func["tag"]).Equals("fun"))
                {
                    var menuid = list.Find(x => x.Equals(Tools.getGuid(func["pId"])));
                    tmenufunction = new T_MenuFunction();
                    tmenufunction.uMenuFunction_MenuID = Tools.getGuid(func["pId"]);
                    tmenufunction.uMenuFunction_FunctionID = Tools.getGuid(func["id"]);
                    if (Tools.getGuid(db.Add(tmenufunction, li)).Equals(Guid.Empty))
                        throw new MessageBox(db.ErrorMessge);
                    list.Add(Tools.getGuid(func["pId"]));
                }
            });
            if (!db.Commit(li))
                throw new MessageBox(db.ErrorMessge);
            return Json(new { status = 1 }, JsonRequestBehavior.DenyGet);
        }
        #endregion  基本操作，增删改查

    }
}
