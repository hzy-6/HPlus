using System.Web.Mvc;

namespace HPlus.Areas.SysManage
{
    public class SysManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SysManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SysManage_default",
                "SysManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { 
                "HPlus.Areas."+AreaName+".Controllers.Sys"
                }
            );
        }
    }
}
