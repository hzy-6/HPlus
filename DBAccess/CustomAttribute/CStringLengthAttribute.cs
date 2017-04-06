using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.CustomAttribute
{
    /// <summary>
    /// 验证字符串长度
    /// </summary>
    public class CStringLengthAttribute : BaseAttribute
    {
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public CStringLengthAttribute(int minLength)
        {
            this.MinLength = minLength;
        }

    }
}
