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
        /// <param name="model">需要拼接成字符串的实体</param>
        /// <returns></returns>
        protected string GetWhereString<M>(M Class, ref List<SQL> li) where M : BaseEntity, new()
        {
            var where = string.Empty;
            //var list = Class.fileds.ToList();
            //foreach (var item in list)
            //{
            //    where += " AND " + item.Key + "=@" + item.Key + " ";
            //    dynamic dy = new ExpandoObject();
            //    dy.Key = item.Key;
            //    dy.Value = item.Value;
            //    list_sqlpar.Add(dy);
            //}
            return where;
        }

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
        protected string GetWhereString<M>(Expression<Func<M, bool>> where) where M : BaseEntity, new()
        {
            if (where == null)
                return string.Empty;
            return " AND " + Helper.DealExpress(where.Body);
        }








    }
}
