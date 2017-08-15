using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.SQLContext.ExpressionTree
{
    public class ParserArgs
    {

        public ParserArgs()
        {
            Builder = new StringBuilder();
            SqlParameters = new Dictionary<string, object>();
            TabIsAlias = true;
        }

        public StringBuilder Builder { get; private set; }

        public Dictionary<string, object> SqlParameters { get; set; }

        /// <summary>
        /// 创建语句时是否需要 加上表的 别名
        /// </summary>
        public bool TabIsAlias { get; set; }

        public IList<ParameterExpression> Forms { get; set; }

        //public readonly string[] FormsAlias = { "it", "A", "B", "C", "D", "E" };

        /// <summary> 
        /// 追加参数
        /// </summary>
        public void AddParameter(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                Builder.Append("NULL");
            }
            else
            {
                string name = "p" + SqlParameters.Count;
                SqlParameters.Add(name, obj);
                Builder.Append('@');
                Builder.Append(name);
            }
        }





    }
}
