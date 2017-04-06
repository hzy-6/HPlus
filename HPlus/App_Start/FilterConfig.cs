using System.Web;
using System.Web.Mvc;
//
using Aop;

namespace HPlus
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ActionFilter(true));
            filters.Add(new ExceptionFilter(true));
            filters.Add(new HandleErrorAttribute());
        }
    }
}