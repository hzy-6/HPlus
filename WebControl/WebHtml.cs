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
        /// 查找带回
        /// </summary>
        /// <param name="options"> new { Text="",ID="",FindClick="",RemoveClick="" } </param>
        /// <param name="Readonly">【是否设置 文本框为 非只读】</param>
        /// <param name="KO">是否采用 KO 双向绑定值</param>
        public static MvcHtmlString FindBackControl(object Options, bool Readonly = true, bool KO = true)
        {
            return fbc.Html(Options, Readonly, KO);
        }

    }
}
