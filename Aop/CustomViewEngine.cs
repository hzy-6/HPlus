using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Aop
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new string[] 
            { 
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            //区域
            AreaViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                "~/Areas/{2}/Views/Sys/{1}/{0}.cshtml",//系统管理
            };//{2}：区域名称，{1}：控制器名称，{0}：Action名称

            PartialViewLocationFormats = ViewLocationFormats;
            AreaPartialViewLocationFormats = AreaViewLocationFormats;//区域配置
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }
    }
}
