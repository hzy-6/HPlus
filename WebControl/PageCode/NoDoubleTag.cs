using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web.Mvc;

namespace WebControl.PageCode
{
    /// <summary>
    /// 非成对标签类
    /// </summary>
    public class NoDoubleTag
    {

        private string Tag;

        private Dictionary<string, string> Attr;

        private StringBuilder sb = new StringBuilder();

        private NoDoubleTag() { }

        /// <summary>
        /// 标签参数
        /// </summary>
        /// <param name="attr">属性</param>
        public NoDoubleTag(string tag, Dictionary<string, string> attr)
        {
            this.Tag = tag;
            this.Attr = attr;
            this.GetHTML();
        }

        public virtual void GetHTML()
        {
            sb.Append(string.Format("<{0} ", this.Tag));
            if (Attr != null)
            {
                foreach (var item in Attr)
                    sb.Append(item.Key + "=\"" + item.Value + "\"");
            }
        }

        /// <summary>
        /// 控件参数配置完后 执行此函数即可得到控件
        /// </summary>
        /// <returns></returns>
        public virtual MvcHtmlString Create()
        {
            sb.Append(" />");
            return MvcHtmlString.Create(sb.ToString());
        }

    }
}
