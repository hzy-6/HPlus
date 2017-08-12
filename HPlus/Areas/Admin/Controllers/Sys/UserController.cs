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
    public class UserController : BaseController
    {
        // 用户管理
        // GET: /ManageSys/User/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.MenuID = "Z-100";
        }

        T_UsersBL tuserbl = new T_UsersBL();

        /// <summary>
        /// 页面绘制
        /// </summary>
        /// <param name="indexpage"></param>
        public override void DrawIndex(WebControl.PageCode.PageIndex page)
        {
            base.DrawIndex(page);
            page.AddSearch(PageControl.AddInput("用户名：", "cUsers_Name", "请输入用户名"));
            page.AddSearch(PageControl.AddInput("登录名：", "cUsers_LoginName", "请输入登录名"));
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
            li = tuserbl.Save(model, uRoles_ID);
            this.KeyID = Tools.getGuidString(model.uUsers_ID);
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
            if (!db.Commit(tuserbl.Delete(ID)))
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
            return Json(tuserbl.Find(Tools.getGuid(ID)), JsonRequestBehavior.DenyGet);
        }
        #endregion  基本操作，增删改查

    }
}
