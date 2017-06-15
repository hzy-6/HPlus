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

        T_Function tf = new T_Function();
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
        public ActionResult Save(T_Function model, string uRoles_ID)
        {
            tf = new T_Function();
            tf = model;
            switch (Tools.getGuid(model.uFunction_ID).Equals(Guid.Empty))
            {
                case true:
                    this.KeyID = db.Add(tf, ref li);
                    if (Tools.getGuid(KeyID).Equals(Guid.Empty))
                        throw new MessageBox(db.ErrorMessge);
                    break;
                case false:
                    if (!db.Edit(tf, ref li))
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
                    tf.uFunction_ID = Tools.getGuid(ID);
                    if (!db.Delete(tf, ref li))
                        throw new MessageBox(db.ErrorMessge);
                    break;
                case false://多个删除
                    if (ID.Contains("[]") || ID.Contains("[null]"))
                        throw new MessageBox("删除失败");
                    var list = db.JsonToList<string>(ID);
                    foreach (var item in list)
                    {
                        tf.uFunction_ID = Tools.getGuid(item);
                        if (!db.Delete(tf, ref li))
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
            tf = new T_Function();
            tf.uFunction_ID = Tools.getGuid(ID);
            tf = db.Find(tf);

            return Json(new ToJson().GetDictionary(new Dictionary<string, object>()
            {
                {"tf",tf},
                {"status",1}
            }), JsonRequestBehavior.DenyGet);
        }

        #endregion  基本操作，增删改查

    }
}
