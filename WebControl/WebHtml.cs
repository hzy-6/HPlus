using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web.Mvc;
using WebControl.Controls;

namespace WebControl
{
    public class WebHtml
    {
        protected static FindBackControl fbc = new FindBackControl();
        public WebHtml()
        {
            fbc = new FindBackControl();
        }

        /// <summary>
        /// 常用
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ID"></param>
        /// <param name="BtnFindClick"></param>
        /// <param name="BtnRemoveClick"></param>
        /// <returns></returns>
        public static MvcHtmlString FindBackControl(string Name, string ID, string BtnFindClick, string BtnRemoveClick)
        {
            return fbc.Html(Name, ID, BtnFindClick, BtnRemoveClick);
        }

        /// <summary>
        /// 可控制input框是否只读
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ID"></param>
        /// <param name="BtnFindClick"></param>
        /// <param name="BtnRemoveClick"></param>
        /// <param name="IsReadonlyInput"></param>
        /// <returns></returns>
        public static MvcHtmlString FindBackControl(string Name, string ID, string BtnFindClick, string BtnRemoveClick, bool IsReadonlyInput)
        {
            return fbc.Html(Name, ID, BtnFindClick, BtnRemoveClick, IsReadonlyInput);
        }

        /// <summary>
        /// 可让查找按钮自定义ko属性
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ID"></param>
        /// <param name="BtnFindClick"></param>
        /// <param name="BtnRemoveClick"></param>
        /// <param name="databind"></param>
        /// <returns></returns>
        public static MvcHtmlString FindBackControl(string Name, string ID, string BtnFindClick, string BtnRemoveClick, string databind)
        {
            return fbc.Html(Name, ID, BtnFindClick, BtnRemoveClick, databind);
        }

        /// <summary>
        /// 即可控制input是否只读又可给查找按钮自定义ko属性
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ID"></param>
        /// <param name="BtnFindClick"></param>
        /// <param name="BtnRemoveClick"></param>
        /// <param name="IsReadonlyInput"></param>
        /// <param name="databind"></param>
        /// <returns></returns>
        public static MvcHtmlString FindBackControl(string Name, string ID, string BtnFindClick, string BtnRemoveClick, bool IsReadonlyInput, string databind)
        {
            return fbc.Html(Name, ID, BtnFindClick, BtnRemoveClick, IsReadonlyInput, databind);
        }
    }
}
