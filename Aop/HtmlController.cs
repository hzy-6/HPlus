using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebControl.PageCode;
using DbFrame.Class;
using Utility;

namespace Aop
{
    public class HtmlController : Controller
    {
        protected JavaScriptSerializer jss = new JavaScriptSerializer();
        private PageIndex pageIndex = null;

        [HttpGet]
        public virtual ActionResult Index()
        {
            pageIndex = new PageIndex();
            pageIndex.Btn_Delete_ApiUrl = Url.Action("Del");
            pageIndex.Btn_ExportExcel_ApiUrl = Url.Action("ExportExcel");
            this.DrawIndex(pageIndex);
            ViewBag.ColModel = jss.Serialize(this.GetPagingEntity(null, 1, 1).ColModel);
            ViewBag.Html_List = MvcHtmlString.Create(pageIndex.GetHtml);
            return View();
        }

        [HttpGet]
        public virtual ActionResult Info()
        {
            this.DrawInfo();
            return View();
        }

        /// <summary>
        /// 列表页
        /// </summary>
        public virtual void DrawIndex(PageIndex page)
        {

        }

        /// <summary>
        /// 详情页
        /// </summary>
        public virtual void DrawInfo()
        {

        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [NonAction]
        public virtual PagingEntity GetPagingEntity(FormCollection fc, int page = 1, int rows = 20)
        {
            return new PagingEntity();
        }

    }
}
