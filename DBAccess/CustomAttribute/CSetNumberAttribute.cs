using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.CustomAttribute
{
    /// <summary>
    /// 编号 标记
    /// </summary>
    public class CSetNumberAttribute : BaseAttribute
    {
        /// <summary>
        /// 编号长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 字符串长度
        /// </summary>
        public char Str { get; set; }

        /// <summary>
        /// 编号标记
        /// </summary>
        /// <param name="length">编号长度</param>
        public CSetNumberAttribute(int length, char str = '0')
        {
            this.Length = length;
        }

    }
}
