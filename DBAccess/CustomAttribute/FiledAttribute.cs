using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.CustomAttribute
{
    /// <summary>
    /// 属性的描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class FiledAttribute : Attribute
    {
        public FiledAttribute()
        {
            this.DisplayName = string.Empty;
            this.IsPrimaryKey = false;
            this.IsShowColumn = true;
        }

        /// <summary>
        /// 字段显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 是否 是主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 是否 显示在列表中
        /// </summary>
        public bool IsShowColumn { get; set; }

    }
}
