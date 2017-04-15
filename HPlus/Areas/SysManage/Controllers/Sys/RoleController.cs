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
    public class RoleController : BaseController
    {
        //角色管理
        // GET: /ManageSys/Role/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            base.MenuID = "Z-110";
        }

        T_Roles troles = new T_Roles();
        T_RolesBL trolesbl = new T_RolesBL();

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
            return trolesbl.GetDataSource(query, page, rows);
        }
        #endregion  查询数据列表

        #region  基本操作，增删改查
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(T_Roles model)
        {
            troles = model;
            if (Tools.getGuid(model.uRoles_ID).Equals(Guid.Empty))
            {
                KeyID = db.Add(troles, ref li);
                if (Tools.getGuid(KeyID).Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                KeyID = Tools.getGuidString(model.uRoles_ID);
                if (!db.Edit(troles, ref li))
                    throw new MessageBox(db.ErrorMessge);
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
            if (!Tools.getGuid(ID).Equals(Guid.Empty))
            {
                troles.uRoles_ID = Tools.getGuid(ID);
                if (!db.Delete(troles, ref li))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                if (ID.Contains("[]") || ID.Contains("[null]"))
                    throw new MessageBox("删除失败");
                var list = db.JsonToList<string>(ID);
                foreach (var item in list)
                {
                    troles.uRoles_ID = Tools.getGuid(item);
                    if (!db.Delete(troles, ref li))
                        throw new MessageBox(db.ErrorMessge);
                }
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
            troles.uRoles_ID = Tools.getGuid(ID);
            troles = db.Find(troles);
            return Json(new ToJson().GetDictionary(new Dictionary<string, object>()
            {
                {"troles",troles},
                {"status",1}
            }), JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Num()
        {
            return Json(new { status = 1, num = Tools.GetOrderNumber(troles.TableName, Tools.getAttrName(() => troles.cRoles_Number)) });
        }

        #endregion  基本操作，增删改查

    }
}
