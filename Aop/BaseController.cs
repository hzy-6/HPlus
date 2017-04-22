using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Utility;
using Application;
using Model;
using BLL;
using DBAccess;
using DBAccess.Entity;
using System.Data;
using WebControl.BaseControl;
//
using NPOI;
using NPOI.XSSF;
using NPOI.HSSF;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace Aop
{
    public class BaseController : Controller
    {
        T_RoleMenuFunction trmf = new T_RoleMenuFunction();
        T_MenuFunction tmenufunction = new T_MenuFunction();
        T_Function tfunction = new T_Function();
        T_Roles troles = new T_Roles();
        T_RolesBL trolesbl = new T_RolesBL();
        T_Menu tmenu = new T_Menu();
        protected JavaScriptSerializer jss = new JavaScriptSerializer();
        protected DBContext db = new DBContext();
        protected List<SQL_Container> li = new List<SQL_Container>();

        /// <summary>
        /// 主键ID
        /// </summary>
        public string KeyID { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuID { get; set; }

        /// <summary>
        /// index 页面初始加载
        /// </summary>
        public void InitIndex()
        {
            ViewBag.SysBtn = this.GetBtn();
            //获取表格列头
            ViewBag.JqGridColModel = jss.Serialize(this.GetPagingEntity(null, 1, 1).JqGridColModel);
        }

        /// <summary>
        /// info 页面初始加载
        /// </summary>
        public void InitInfo()
        {

        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            this.InitIndex();
            return View();
        }

        [HttpGet]
        public virtual ActionResult Info()
        {
            this.InitInfo();
            return View();
        }

        /// <summary>
        /// 在行为方法执行后执行
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
                PowerLogic(filterContext);
        }

        private void PowerLogic(ActionExecutedContext filterContext)
        {
            string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string ActionName = filterContext.ActionDescriptor.ActionName;
            string Area = Tools.getString(filterContext.RouteData.DataTokens["area"]);
            string RoleID = Tools.getSession("RoleID");
            //dynamic model = new ExpandoObject();
            if (string.IsNullOrEmpty(MenuID))
            {
                throw new MessageBox("区域(" + Area + "),控制器(" + ControllerName + "):的程序中缺少菜单ID");
            }

            tmenu = new T_Menu();
            tmenu.cMenu_Number = MenuID;
            tmenu = db.Find(tmenu);
            MenuID = Tools.getGuidString(tmenu.uMenu_ID);
            if (!tmenu.cMenu_Url.StartsWith("/" + Area + "/" + ControllerName + "/"))
            {
                throw new MessageBox("区域(" + Area + "),控制器(" + ControllerName + "):的程序中缺少菜单ID与该页面不匹配");
            }

            //这里得判断一下是否是查找带回调用页面
            string fun = Tools.getString(filterContext.HttpContext.Request.QueryString["fun"]);
            var _func_list = db.FindToList(tfunction, " iFunction_Number ");
            var _role_menu_func_list = db.FindToList(trmf);
            var _power_list = new Dictionary<string, object>();

            if (string.IsNullOrEmpty(fun))
            {
                if (!RoleID.Equals("admin"))
                {
                    _power_list = new Dictionary<string, object>();
                    _func_list.ForEach(item =>
                    {
                        var ispower = _role_menu_func_list.FindAll(x => x.uRoleMenuFunction_RoleID.Equals(Tools.getGuid(RoleID)) && x.uRoleMenuFunction_MenuID.Equals(Tools.getGuid(MenuID)) && x.uRoleMenuFunction_FunctionID.Equals(item.uFunction_ID));
                        if (ispower.Count > 0)
                            _power_list.Add(item.cFunction_ByName, true);
                        else
                            _power_list.Add(item.cFunction_ByName, false);
                    });
                }
                else
                    _func_list.ForEach(item =>
                     {
                         _power_list.Add(item.cFunction_ByName, true);
                     });
            }
            else
            {
                _power_list = new Dictionary<string, object>();
                _func_list.ForEach(item =>
                {
                    _power_list.Add(item.cFunction_ByName, false);
                });
                _power_list["Have"] = true;
                _power_list["Search"] = true;
                _power_list["GetExcel"] = true;
            }
            filterContext.Controller.ViewData["PowerModel"] = jss.Serialize(_power_list); ;
        }


        /// <summary>
        /// 列表页接口
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GetDataSource(FormCollection fc, int page = 1, int rows = 20)
        {
            var hs = Tools.GetUrlQueryString(Request.UrlReferrer.Query);
            foreach (var item in hs.Keys)
            {
                if (!fc.AllKeys.Contains(item.ToString()))
                    fc.Add(item.ToString(), Tools.getString(hs[item.ToString()]));
            }
            PagingEntity pe = GetPagingEntity(fc, page, rows);
            return Json(new { status = 1, columnModel = pe.JqGridColModel, rows = pe.List, page = page, records = pe.Counts, total = pe.PageCount }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult ExportExcel(FormCollection fc, int page = 1, int rows = 100000)
        {
            foreach (var item in Request.QueryString.Keys)
            {
                if (!fc.AllKeys.Contains(item.ToString()))
                    fc.Add(item.ToString(), Tools.getString(Request.QueryString[item.ToString()]));
            }
            PagingEntity pe = GetPagingEntity(fc, page, rows);
            return File(DBToExcel(pe), Tools.GetFileContentType[".xls"], Guid.NewGuid().ToString() + ".xls");
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

        /// <summary>
        /// 表数据转换为EXCEL
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [NonAction]
        public virtual byte[] DBToExcel(PagingEntity pe)
        {
            DataTable dt = pe.dt;
            var list = pe.JqGridColModel;
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            //填充表头
            IRow dataRow = sheet.CreateRow(0);
            foreach (DataColumn column in dt.Columns)
            {
                foreach (var item in list)
                {
                    if (!column.ColumnName.Equals("_ukid") && column.ColumnName.Equals(item.name))
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(item.label);
                    }
                }

            }

            //填充内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!dt.Columns[j].ColumnName.Equals("_ukid"))
                        dataRow.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //保存
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 将  FormCollection  转换为  哈希表
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected System.Collections.Hashtable FormCollectionToHashtable(FormCollection fc)
        {
            System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
            if (fc != null)
                fc.AllKeys.ToList().ForEach(item =>
                {
                    hashtable.Add(item, fc[item]);
                });
            return hashtable;
        }

        [NonAction]
        public string GetBtn()
        {
            var Refresh = new DoubleTag("button", new Dictionary<string, string>(){
                {"type","button"},
                {"class","btn btn-white btn-sm"},
                {"onclick","window.location=''"},
            }).Append(
                new DoubleTag("i", new Dictionary<string, string>() { { "class", "fa fa-refresh" } }).Create() + "&nbsp;刷新"
            ).Create();

            var Search = new DoubleTag("button", new Dictionary<string, string>(){
                {"type","button"},
                {"class","btn btn-white btn-sm"},
                {"onclick","ShowSearch(this)"},
                {"data-power","Search"},
            }).Append(
                new DoubleTag("i", new Dictionary<string, string>() { { "class", "fa fa-chevron-down" } }).Create() + "&nbsp;检索"
            ).Create();

            var Add = new DoubleTag("button", new Dictionary<string, string>(){
                {"type","button"},
                {"class","btn btn-white btn-sm"},
                {"onclick","Func.OpenInfoPage('add')"},
                {"data-power","Add"},
            }).Append(
                new DoubleTag("i", new Dictionary<string, string>() { { "class", "fa fa-plus" } }).Create() + "&nbsp;添加"
            ).Create();

            var Edit = new DoubleTag("button", new Dictionary<string, string>(){
                {"type","button"},
                {"class","btn btn-white btn-sm"},
                {"onclick","Func.OpenInfoPage('edit')"},
                {"data-power","Edit"},
                {"disabled","disabled"}
            }).Append(
                new DoubleTag("i", new Dictionary<string, string>() { { "class", "fa fa-pencil" } }).Create() + "&nbsp;修改"
            ).Create();

            var Del = new DoubleTag("button", new Dictionary<string, string>(){
                {"type","button"},
                {"class","btn btn-white btn-sm"},
                {"onclick","$List.Del({url:'" + Url.Action("Del") + "'});"},
                {"data-power","Del"},
                {"disabled","disabled"}
            }).Append(
                new DoubleTag("i", new Dictionary<string, string>() { { "class", "fa fa-trash" } }).Create() + "&nbsp;删除"
            ).Create();

            return Refresh.ToHtmlString() + Search.ToHtmlString() + Add.ToHtmlString() + Edit.ToHtmlString() + Del.ToHtmlString();
        }

    }
}
