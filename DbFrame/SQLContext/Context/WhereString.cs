using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using DbFrame.Class;
using DbFrame.SQLContext.ExpressionTree;

namespace DbFrame.SQLContext.Context
{
    public class WhereString
    {

        /// <summary>
        /// 得到where语句
        /// </summary>
        /// <returns></returns>
        protected string GetWhereString(string where)
        {
            return where;
        }

        /// <summary>
        /// 表达式树 条件拼接
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        protected void GetWhereString<M>(Expression<Func<M, bool>> where, ParserArgs pa) where M : BaseEntity, new()
        {
            var body = where.Body;
            pa.Builder.Append(" AND ");
            Parser.Where(body, pa);
        }








    }
}
