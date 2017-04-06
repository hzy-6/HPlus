using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//
using Utility;
using Application;

namespace Aop
{
    /// <summary>
    /// 程序异常过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ExceptionFilter : HandleErrorAttribute
    {
        /// <summary>
        /// 100 表示不执行里面的操作，1是后端，2是WebApi
        /// </summary>
        private bool IsExecute { get; set; }
        public ExceptionFilter(bool isExecute)
        {
            IsExecute = isExecute;
        }
        public override void OnException(ExceptionContext filterContext)
        {
            //判断该异常处理是用于后端的，还是WebApi的
            if (IsExecute)
                ExceptionWeb(filterContext);
            base.OnException(filterContext);
        }

        /// <summary>
        /// 后端异常处理
        /// </summary>
        private void ExceptionWeb(ExceptionContext filterContext)
        {
            //判断是否是自定义异常类型
            if (filterContext.Exception is MessageBox)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                    //返回错误信息
                    filterContext.Result = new JsonResult() { Data = MessageBox.cem };
                else
                {
                    CustomErrorModel cem = new CustomErrorModel(filterContext);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script src=\"/Scripts/Jquery/jquery-2.1.4.min.js\"></script>");
                    //sb.Append("<script src=\"/Scripts/Bootstrap/bootstrap.min.js\"></script>");
                    sb.Append("<script src=\"/Scripts/Layer/layer-v3.0.1/layer/layer.js\"></script>");
                    sb.Append("<script src=\"/Scripts/SysFrameWork/FrameWork.js\"></script>");
                    sb.Append("<script type='text/javascript'>");
                    sb.Append("$(function(){ $.ModalMsg('" + cem.ErrorMessage.Trim().Replace("'", "“").Replace("\"", "”") + "','" + (string.IsNullOrEmpty(cem.JumpUrl) ? "" : cem.JumpUrl) + "','warning'); });");
                    sb.Append("</script>");
                    filterContext.Result = new ContentResult() { Content = sb.ToString(), ContentType = "text/html", ContentEncoding = System.Text.Encoding.UTF8 };
                }
                filterContext.HttpContext.Response.StatusCode = 200;
            }
            else
            {
                ErrorWrite(filterContext);
                CustomErrorModel cem = new CustomErrorModel(filterContext);
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    //返回错误信息
                    filterContext.Result = new JsonResult() { Data = cem };
                    filterContext.HttpContext.Response.StatusCode = 200;
                }
                else
                {
                    filterContext.Result = new ViewResult() { ViewName = "~/Areas/Admin/Views/Error/Index.cshtml", ViewData = new ViewDataDictionary<CustomErrorModel>(cem) };
                }
            }
            //表示异常已处理
            filterContext.ExceptionHandled = true;
        }

        /// <summary>
        /// 写入错误
        /// </summary>
        /// <param name="filterContext"></param>
        public void ErrorWrite(ExceptionContext filterContext)
        {
            string errorinfo = string.Empty;
            string errorsource = string.Empty;
            string errortrace = string.Empty;

            errorinfo = "异常信息: " + filterContext.Exception.Message;
            errorsource = "错误源:" + filterContext.Exception.Source;
            errortrace = "堆栈信息:" + filterContext.Exception.StackTrace;
            filterContext.HttpContext.Server.ClearError();

            string line = "-----------------------------------------------------";

            string log = line + "\r\n" + filterContext.HttpContext.Request.UserHostAddress.ToString() + "\r\n" + errorinfo + "\r\n" + errorsource + "\r\n" + errortrace + "\r\n";
            LogHelper.WriteLog(log);
            LogHelper.WriteLog("应用程序错误");
        }

    }
}
