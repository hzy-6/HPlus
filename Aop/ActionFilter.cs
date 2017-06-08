using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//引用
using System.Web;
using System.Web.Mvc;
using Utility;
using Application;
using System.Data;
using System.Dynamic;
using System.Web.Script.Serialization;

namespace Aop
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ActionFilter : ActionFilterAttribute
    {
        private bool isExecute { set; get; }
        public ActionFilter(bool IsExecute)
        {
            isExecute = IsExecute;
        }
        /// <summary>
        /// 每次请求Action之前发生，，在行为方法执行前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (isExecute)
            {
                //登陆超时验证
                Judge(filterContext);
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 在行为方法执行后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 在行为方法返回前执行，判断session是否为空,重写这个方法即可实现
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        /// <summary>
        /// 在行为方法返回后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        /// <summary>
        /// 判断，验证
        /// </summary>
        /// <param name="filterContext"></param>
        private void Judge(ActionExecutingContext filterContext)
        {
            //获取当前的方法名
            string ActionName = filterContext.ActionDescriptor.ActionName;
            //获取当前的控制器名
            string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //获取当前的区域名
            string Area = Tools.getString(filterContext.RouteData.DataTokens["area"]);
            StringBuilder sb = new StringBuilder();
            switch (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                case true:
                    //排除登陆页面
                    if (Tools.getGuid(filterContext.HttpContext.Session["UserID"]).Equals(Guid.Empty))
                        filterContext.Result = new JsonResult() { Data = new CustomErrorModel("/Home/Index/", "02", "登陆超时,计时后退出") };
                    break;
                case false:
                    if (Tools.getGuid(filterContext.HttpContext.Session["UserID"]).Equals(Guid.Empty))
                    {
                        sb.Append("<html>");
                        sb.Append("<head>");
                        sb.Append("<script src=\"/Scripts/Jquery/jquery-2.1.4.min.js\"></script>");
                        //sb.Append("<script src=\"/Scripts/Bootstrap/bootstrap.min.js\"></script>");
                        sb.Append("<script src=\"/Scripts/Toastr/toastr.min.js\"></script>");
                        sb.Append("<script src=\"/Scripts/Layer/layer-v3.0.1/layer/layer.js\"></script>");
                        sb.Append("<script src=\"/Scripts/SysFrameWork/FrameWork.js\"></script>");
                        sb.Append("<script type='text/javascript'>");
                        sb.Append("$(function(){ FW.OutLogin('登陆超时,计时后退出','/Home/Index/'); });");
                        sb.Append("</script>");
                        sb.Append("</head><body></body>");
                        sb.Append("</html>");
                        filterContext.Result = new ContentResult() { Content = sb.ToString(), ContentType = "text/html", ContentEncoding = System.Text.Encoding.UTF8 };
                    }
                    break;
            }
        }
    }
}
