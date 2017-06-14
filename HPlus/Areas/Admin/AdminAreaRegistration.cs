using System.Web.Mvc;

namespace HPlus.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { 
                "HPlus.Areas."+AreaName+".Controllers",
                "HPlus.Areas."+AreaName+".Controllers.Sys",
                "HPlus.Areas."+AreaName+".Controllers.Base",
                }
            );
        }
    }
}
