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
    public class FunctionController : BaseController
    {
        // 功能管理
        // GET: /SysManage/Function/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.MenuID = "Z-120";
        }

        T_FunctionBL tfbl = new T_FunctionBL();

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
            return tfbl.GetDataSource(query, page, rows);
        }
        #endregion  查询数据列表

        #region  基本操作，增删改查
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(T_Function model)
        {
            li = tfbl.Save(model);
            this.KeyID = Tools.getGuidString(model.uFunction_ID);
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
            if (!db.Commit(tfbl.Delete(ID)))
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
            return Json(tfbl.Find(Tools.getGuid(ID)), JsonRequestBehavior.DenyGet);
        }

        #endregion  基本操作，增删改查

    }
}
