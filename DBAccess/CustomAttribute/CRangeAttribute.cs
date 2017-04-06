using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.CustomAttribute
{
    /// <summary>
    /// 范围验证
    /// </summary>
    public class CRangeAttribute : BaseAttribute
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public object MinLength { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public object MaxLength { get; set; }
        /// <summary>
        /// 验证数据类型
        /// </summary>
        public Type type { get; set; }
        public CRangeAttribute(int minLength, int maxLength)
        {
            this.MinLength = minLength;
            this.MaxLength = MaxLength;
        }

    }
}
