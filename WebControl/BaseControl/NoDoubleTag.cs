using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web.Mvc;

namespace WebControl.BaseControl
{
    /// <summary>
    /// 非成对标签类
    /// </summary>
    public class NoDoubleTag : AbstractBaseClass
    {

        private string Tag;

        private Dictionary<string, object> Attr;

        private StringBuilder sb = new StringBuilder();

        private NoDoubleTag() { }

        /// <summary>
        /// 标签参数
        /// </summary>
        /// <param name="attr">属性</param>
        public NoDoubleTag(string tag, object attr)
        {
            this.Tag = tag;
            var json = jss.Serialize(attr);//将匿名对象转换为json
            this.Attr = jss.Deserialize<Dictionary<string, object>>(json);//将json转换为字典
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
