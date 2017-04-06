using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//
using Aop;
using Utility;

namespace HPlus
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //注册自定义视图
            ViewEngines.Engines.Clear();                    // 清除原MVC视图引擎规则
            ViewEngines.Engines.Add(new CustomViewEngine());  // 使用自定义视图引擎

            // 在应用程序启动时运行的代码
            LogHelper.SetConfig();
            LogHelper.WriteLog("应用程序启动");
        }

        protected void Application_Error()
        {
            string code = Context.Response.StatusCode.ToString();
            // 在出现未处理的错误时运行的代码
            Exception objErr = (Exception)Server.GetLastError().GetBaseException();
            string errorinfo = string.Empty;
            string errorsource = string.Empty;
            string errortrace = string.Empty;

            errorinfo = "异常信息: " + objErr.Message;
            errorsource = "错误源:" + objErr.Source;
            errortrace = "堆栈信息:" + objErr.StackTrace;
            Server.ClearError();

            string line = "-----------------------------------------------------";

            string log = line + "\r\n" + Request.UserHostAddress.ToString() + "\r\n" + errorinfo + "\r\n" + errorsource + "\r\n" + errortrace + "\r\n";
            LogHelper.WriteLog(log);
            LogHelper.WriteLog("应用程序错误");
        }

        protected void Application_End()
        {
            //  在应用程序关闭时运行的代码
            LogHelper.WriteLog("应用程序已结束");
        }

    }
}