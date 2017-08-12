using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using System.Data;
using System.Web.Script.Serialization;
using DbFrame.SQLContext.ExpressionTree;
using DbFrame.SQLContext.Context;
using DbFrame.Class;
using DbFrame.AdoDotNet;

namespace DbFrame.SQLContext
{
    public class FindContext
    {
        private string _ConnectionString { get; set; }
        private FindString find = new FindString();
        private DbHelper dbhelper = null;
        private JavaScriptSerializer jss;
        public FindContext(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
            if (find != null)
                find = new FindString();
            dbhelper = new DbHelper(ConnectionString);
            jss = new JavaScriptSerializer();
        }

        private DataTable ExecuteSQL<T>(string[] From, Expression<Func<T, bool>> Where, string OrderBy = "") where T :BaseEntity, new()
        {
            var sql = find.GetSql<T>(From, Where, OrderBy);
            return dbhelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 根据条件 获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Where"></param>
        /// <returns></returns>
        public virtual T Find<T>(Expression<Func<T, bool>> Where) where T :BaseEntity, new()
        {
            var dt = this.ExecuteSQL<T>(null, Where);
            if (dt.Rows.Count == 0)
                return (T)Activator.CreateInstance(typeof(T));
            return ToModel(dt.Rows[0], (T)Activator.CreateInstance(typeof(T)));
        }

        /// <summary>
        /// 根据条件 获取 DataTable 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public virtual DataTable Find<T>(Expression<Func<T, bool>> Where, string OrderBy) where T :BaseEntity, new()
        {
            return this.ExecuteSQL<T>(null, Where, OrderBy);
        }

        /// <summary>
        /// 根据条件 获取 DataTable 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="From"></param>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public virtual DataTable Find<T>(string[] From, Expression<Func<T, bool>> Where, string OrderBy) where T :BaseEntity, new()
        {
            return this.ExecuteSQL<T>(From, Where, OrderBy);
        }

        public virtual IEnumerable<T> FindToList<T>(Expression<Func<T, bool>> Where, string OrderBy) where T :BaseEntity, new()
        {
            return DbHelper.ConvertDataTableToList<T>(this.ExecuteSQL<T>(null, Where, OrderBy));
        }

        public virtual IEnumerable<Dictionary<string, object>> FindToList(DataTable dt)
        {
            return DbHelper.ConvertDataTableToList<Dictionary<string, object>>(dt);
        }





        public DataTable Find(string SQL)
        {
            return dbhelper.ExecuteDataset(SQL);
        }

        public object FINDToObj(string SQL)
        {
            return dbhelper.ExecuteScalar(SQL.ToString());
        }

        public PagingEntity Find(string SQL, int PageIndex, int PageSize)
        {
            return dbhelper.PagingList(SQL, PageIndex, PageSize);
        }

        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="r"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public T ToModel<T>(DataRow r, T Class) where T :BaseEntity, new()
        {
            var model = new Dictionary<string, object>();
            foreach (DataColumn item in r.Table.Columns) model.Add(item.ColumnName, r[item.ColumnName] == DBNull.Value ? null : r[item.ColumnName]);
            var json = jss.Serialize(model);
            json = System.Text.RegularExpressions.Regex.Replace(json, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            });
            return jss.Deserialize<T>(json);
        }



    }
}
