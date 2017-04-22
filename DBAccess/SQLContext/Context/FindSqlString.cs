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

namespace DBAccess.SQLContext.Context
{
    public class FindSqlString<T> : AbstractSqlContext<T> where T : BaseModel, new()
    {
        List<SqlParameter> list_sqlpar;

        public FindSqlString()
        {
            list_sqlpar = new List<SqlParameter>();
        }

        public string OrderBy = string.Empty;

        /// <summary>
        /// 获取sql语句
        /// </summary>
        /// <param name="mie"></param>
        /// <returns></returns>
        public override SQL_Container GetSqlString(T entity)
        {
            throw new Exception();
        }

        public SQL_Container GetSqlString<M>(M entity) where M : BaseModel, new()
        {
            list_sqlpar = new List<SqlParameter>();
            return this.GetSQL(entity);
        }

        public SQL_Container GetSqlString<M>(Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            list_sqlpar = new List<SqlParameter>();
            return this.GetSQL<M>(" AND " + this.GetWhereString(where, ref list_sqlpar));
        }

        public SQL_Container GetSqlString<M>(string where) where M : BaseModel, new()
        {
            list_sqlpar = new List<SqlParameter>();
            return this.GetSQL<M>(where);
        }

        public SQL_Container GetSQL<M>(M entity) where M : BaseModel, new()
        {
            var TableName = entity.TableName;
            var list = entity.fileds.ToList();
            var where = new List<string>();
            foreach (var item in list)
            {
                var value = item.Value;
                var key = item.Key;
                where.Add(" AND " + key + "=@" + key + "");
                list_sqlpar.Add(new SqlParameter() { ParameterName = key, Value = value == null ? DBNull.Value : value });
            }
            OrderBy = string.IsNullOrEmpty(OrderBy) ? "" : " Order By " + OrderBy;
            string sql = string.Format(" SELECT {0} FROM {1} WHERE 1=1 {2} {3} ", "*", TableName, string.Join(" ", where), OrderBy);
            return new SQL_Container(sql, list_sqlpar.ToArray());
        }


        private SQL_Container GetSQL<M>(string where) where M : BaseModel, new()
        {
            M m = (M)Activator.CreateInstance(typeof(M));
            var TableName = m.TableName;
            OrderBy = string.IsNullOrEmpty(OrderBy) ? "" : " Order By " + OrderBy;
            string sql = string.Format(" SELECT {0} FROM {1} WHERE 1=1 {2} {3} ", "*", TableName, where, OrderBy);
            return new SQL_Container(sql, list_sqlpar.ToArray());
        }

    }
}
