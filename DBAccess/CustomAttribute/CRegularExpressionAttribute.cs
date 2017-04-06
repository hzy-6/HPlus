using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.CustomAttribute
{
    /// <summary>
    /// 正则表达式验证
    /// </summary>
    public class CRegularExpressionAttribute : BaseAttribute
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Pattern { get; set; }

        public CRegularExpressionAttribute(string pattern)
        {
            this.Pattern = pattern;
        }

    }
}
