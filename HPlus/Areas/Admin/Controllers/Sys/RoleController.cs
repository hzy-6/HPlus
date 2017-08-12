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
using WebControl.PageCode;

namespace HPlus.Areas.Admin.Controllers.Sys
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

        /// <summary>
        /// 页面绘制
        /// </summary>
        /// <param name="indexpage"></param>
        public override void DrawIndex(WebControl.PageCode.PageIndex page)
        {
            base.DrawIndex(page);
            page.AddSearch(PageControl.AddInput("角色名称：", "cRoles_Name", "请输入角色名称"));
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
            li = trolesbl.Save(model);
            this.KeyID = Tools.getGuidString(model.uRoles_ID);
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
            if (!db.Commit(trolesbl.Delete(ID)))
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
            return Json(trolesbl.Find(Tools.getGuid(ID)), JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// 获取编号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Num()
        {
            return Json(new { status = 1, num = Tools.GetOrderNumber(troles.GetTabelName(), Tools.getAttrName(() => troles.cRoles_Number)) });
        }

        #endregion  基本操作，增删改查

    }
}
