using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using System.Data.SqlClient;
using DBAccess.Entity;
using DBAccess.ExpressionTree;
using System.Dynamic;

namespace DBAccess.SQLContext.Context
{
    /// <summary>
    /// 删除语句
    /// </summary>
    public class DeleteSqlString<T> : AbstractSqlContext<T> where T : BaseModel, new()
    {
        List<dynamic> list_sqlpar;

        public DeleteSqlString()
        {
            list_sqlpar = new List<dynamic>();
        }

        /// <summary>
        /// 获取sql语句
        /// </summary>
        /// <param name="mie"></param>
        /// <returns></returns>
        public override SQL_Container GetSqlString(T where)
        {
            list_sqlpar = new List<dynamic>();
            return this.GetSQL(where);
        }

        public SQL_Container GetSqlString<M>(Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            list_sqlpar = new List<dynamic>();
            return this.GetSQL<M>(" AND " + this.GetWhereString(where, ref list_sqlpar));
        }

        //public SQL_Container GetSqlString<M>(M where) where M : BaseModel, new()
        //{
        //    return this.GetSQL<M>(this.GetWhereString(where, ref list_sqlpar));
        //}

        public SQL_Container GetSqlString<M>(string where) where M : BaseModel, new()
        {
            list_sqlpar = new List<dynamic>();
            return this.GetSQL<M>(where);
        }

        private SQL_Container GetSQL(T entity)
        {
            var TableName = entity.TableName;
            var list = entity.fileds.ToList();
            var where = new List<string>();
            foreach (var item in list)
            {
                var value = item.Value;
                var key = item.Key;
                where.Add(" AND " + key + "=@" + key + "");
                dynamic dy = new ExpandoObject();
                dy.Key = key;
                dy.Value = value;
                list_sqlpar.Add(dy);
            }
            string sql = string.Format(" DELETE FROM {0} WHERE 1=1 {1} ", TableName, string.Join(" ", where));
            return new SQL_Container(sql, list_sqlpar);
        }

        private SQL_Container GetSQL<M>(string where) where M : BaseModel, new()
        {
            M m = default(M);
            var TableName = m.TableName;
            string sql = string.Format(" DELETE FROM {0} WHERE 1=1 {1} ", TableName, where);
            return new SQL_Container(sql, list_sqlpar);
        }

    }
}
