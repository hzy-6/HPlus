using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data.SqlClient;
using System.Linq.Expressions;
using DBAccess.Entity;
using DBAccess.ExpressionTree;
using System.Dynamic;

namespace DBAccess.SQLContext
{
    public abstract class AbstractSqlContext<T> : ISqlContext<T> where T : Entity.BaseModel, new()
    {
        public abstract SQL_Container GetSqlString(T entity);

        /// <summary>
        /// 得到where语句
        /// </summary>
        /// <param name="model">需要拼接成字符串的实体</param>
        /// <returns></returns>
        public string GetWhereString<M>(M entity, ref List<dynamic> list_sqlpar) where M : BaseModel, new()
        {
            var where = string.Empty;
            var list = entity.fileds.ToList();
            foreach (var item in list)
            {
                where += " AND " + item.Key + "=@" + item.Key + " ";
                dynamic dy = new ExpandoObject();
                dy.Key = item.Key;
                dy.Value = item.Value;
                list_sqlpar.Add(dy);
            }
            return where;
        }

        /// <summary>
        /// 得到where语句
        /// </summary>
        /// <returns></returns>
        public string GetWhereString(string where)
        {
            return where;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public string GetWhereString<M>(Expression<Func<M, bool>> where, ref List<dynamic> list_sqlpar) where M : BaseModel, new()
        {
            string _where = string.Empty;
            _where = ExpressionHelper.DealExpress(where.Body);
            //if (where.Body is BinaryExpression)
            //{
            //    _where = ExpressionHelper.DealExpress(where.Body);
            //}
            //else
            //    throw new Exception(" where 条件语法错误! ");
            return _where;
        }


    }
}
