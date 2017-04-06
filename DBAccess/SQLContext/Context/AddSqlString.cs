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
    /// <summary>
    /// 插入语句
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AddSqlString<T> : AbstractSqlContext<T> where T : BaseModel, new()
    {
        //INSERT INTO TAB COL VALUES () 
        List<SqlParameter> list_sqlpar = new List<SqlParameter>();

        public AddSqlString()
        {
            list_sqlpar = new List<SqlParameter>();
        }

        /// <summary>
        /// 获取sql语句
        /// </summary>
        /// <param name="mie"></param>
        /// <returns></returns>
        public override SQL_Container GetSqlString(T entity)
        {
            list_sqlpar = new List<SqlParameter>();
            return this.GetSQL(entity);
        }

        private SQL_Container GetSQL(T entity)
        {
            var TableName = entity.TableName;
            var col = new List<string>();
            var val = new List<string>();
            var list = entity.fileds.ToList();
            foreach (var item in list)
            {
                var value = item.Value;
                var key = item.Key;
                col.Add(key); val.Add("@" + key + "");
                list_sqlpar.Add(new SqlParameter() { ParameterName = key, Value = value });
            }
            string sql = string.Format(" INSERT INTO {0} ({1}) VALUES ({2}) ", TableName, string.Join(",", col), string.Join(",", val));
            return new SQL_Container(sql, list_sqlpar.ToArray());
        }

    }
}
