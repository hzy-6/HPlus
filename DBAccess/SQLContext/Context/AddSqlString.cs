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
    /// 插入语句
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AddSqlString<T> : AbstractSqlContext<T> where T : BaseModel, new()
    {
        //INSERT INTO TAB COL VALUES () 
        List<dynamic> list_sqlpar = new List<dynamic>();

        public AddSqlString()
        {
            list_sqlpar = new List<dynamic>();
        }

        /// <summary>
        /// 获取sql语句
        /// </summary>
        /// <param name="mie"></param>
        /// <returns></returns>
        public override SQL_Container GetSqlString(T entity)
        {
            list_sqlpar = new List<dynamic>();
            return this.GetSQL(entity);
        }

        private SQL_Container GetSQL(T entity)
        {
            var TableName = entity.TableName;
            var col = new List<string>();
            var val = new List<string>();
            var list = entity.fileds.ToList();
            list = list.FindAll(item => !entity.NotFiled.Contains(item.Key));
            foreach (var item in list)
            {
                var value = item.Value;
                var key = item.Key;
                col.Add(key); val.Add("@" + key + "");
                dynamic dy = new ExpandoObject();
                dy.Key = key;
                dy.Value = value;
                list_sqlpar.Add(dy);
            }
            string sql = string.Format(" INSERT INTO {0} ({1}) VALUES ({2}) ", TableName, string.Join(",", col), string.Join(",", val));
            return new SQL_Container(sql, list_sqlpar);
        }

    }
}
