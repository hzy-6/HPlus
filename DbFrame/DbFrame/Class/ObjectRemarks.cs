using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.Class
{
    /// <summary>
    /// 对象 备注
    /// </summary>
    public class ObjectRemarks
    {

        /// <summary>
        /// 实体描述 表名
        /// </summary>
        public class TableAttribute : Attribute
        {
            public string TableName = string.Empty;

            public TableAttribute(string _TableName)
            {
                this.TableName = _TableName;
            }
        }

        /// <summary>
        /// 字段描述
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public class FieldAttribute : Attribute
        {
            public string Alias = string.Empty;
            /// <summary>
            /// 是否主键
            /// </summary>
            public bool IsPrimaryKey = false;
            /// <summary>
            /// 是否自增
            /// </summary>
            public bool IsIdentity = false;
            /// <summary>
            /// 属性类型
            /// </summary>
            public Type FieldType;
            /// <summary>
            /// 字段名称
            /// </summary>
            public string FieldName = string.Empty;
            /// <summary>
            /// 主键值
            /// </summary>
            public object Value = string.Empty;
            /// <summary>
            /// 字段描述
            /// </summary>
            /// <param name="_Alias">别名</param>
            public FieldAttribute(string _Alias)
            {
                this.Alias = _Alias;
            }
        }

        /***********验证 特性标记************/

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public class BaseAttribute : Attribute
        {
            public BaseAttribute() { }

            /// <summary>
            /// 错误信息
            /// </summary>
            public string ErrorMessage { get; set; }

        }

        /// <summary>
        /// 比较两字段是否相等  如果你要使用 DisplayName 请在 ErrorMessage 的文本中加上 {name} 这样的标记即可
        /// </summary>
        public class CCompareAttribute : BaseAttribute
        {
            /// <summary>
            /// 要比较的属性
            /// </summary>
            public string OtherProperty { get; set; }
            public CCompareAttribute(string otherProperty)
            {
                this.OtherProperty = otherProperty;
            }
        }

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

        public class CRepeatAttribute : BaseAttribute
        {
            /// <summary>
            /// 要追加的 Where 条件 例如： and 1=1  and filed1='{filed1}'
            /// </summary>
            public string Where { get; set; }
            public CRepeatAttribute()
            {

            }


        }

        /// <summary>
        /// 非空验证 标记  如果你要使用 DisplayName 请在 ErrorMessage 的文本中加上 {name} 这样的标记即可
        /// </summary>
        public class CRequiredAttribute : BaseAttribute
        {
            public CRequiredAttribute()
            {
            }

        }

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
                this.Str = str;
            }

        }

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
}
