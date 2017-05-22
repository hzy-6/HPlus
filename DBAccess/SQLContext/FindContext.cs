using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using DBAccess.Reflection;
using DBAccess.Entity;
using DBAccess.HelperClass;
using DBAccess.AdoDotNet;
using System.Dynamic;
using System.Data;
using System.Web.Script.Serialization;


namespace DBAccess.SQLContext
{
    public class FindContext<T> where T : BaseModel, new()
    {
        Context.FindSqlString<T> sqlstring;
        JavaScriptSerializer jss;
        DBHelper dbhelper;
        private FindContext() { }

        private string _ConnectionString { get; set; }

        public FindContext(string ConnectionString, DBType DBType)
        {
            _ConnectionString = ConnectionString;
            dbhelper = new DBHelper(_ConnectionString, DBType);
            sqlstring = new Context.FindSqlString<T>();
            jss = new JavaScriptSerializer();
        }

        private SQL_Container GetSql<M>(M entity) where M : BaseModel, new()
        {
            return sqlstring.GetSqlString(entity);
        }

        private SQL_Container GetSql<M>(string where) where M : BaseModel, new()
        {
            return sqlstring.GetSqlString<M>(where);
        }

        private SQL_Container GetSql<M>(Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            return sqlstring.GetSqlString<M>(where);
        }



        public M Find<M>(M entity) where M : BaseModel, new()
        {
            var sql = this.GetSql(entity);
            var dt = dbhelper.ExecuteDataset(sql);
            if (dt.Rows.Count == 0)
                return (M)Activator.CreateInstance(entity.GetType());
            return ToModel(dt.Rows[0], (M)Activator.CreateInstance(entity.GetType()));
        }

        public M Find<M>(string where) where M : BaseModel, new()
        {
            var sql = this.GetSql<M>(where);
            var dt = dbhelper.ExecuteDataset(sql);
            if (dt.Rows.Count == 0)
                return (M)Activator.CreateInstance(typeof(M));
            return ToModel(dt.Rows[0], (M)Activator.CreateInstance(typeof(M)));
        }

        public M Find<M>(Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            var sql = this.GetSql<M>(where);
            var dt = dbhelper.ExecuteDataset(sql);
            if (dt.Rows.Count == 0)
                return (M)Activator.CreateInstance(typeof(M));
            return ToModel(dt.Rows[0], (M)Activator.CreateInstance(typeof(M)));
        }

        public DataTable Find<M>(M entity, string OrderBy) where M : BaseModel, new()
        {
            sqlstring = new Context.FindSqlString<T>();
            sqlstring.OrderBy = OrderBy;
            var sql = this.GetSql(entity);
            return dbhelper.ExecuteDataset(sql);
        }

        public List<M> FindToList<M>(M entity, string OrderBy) where M : BaseModel, new()
        {
            sqlstring = new Context.FindSqlString<T>();
            sqlstring.OrderBy = OrderBy;
            var sql = this.GetSql(entity);
            var dt = dbhelper.ExecuteDataset(sql);
            return this.FindToList<M>(dt);
        }

        public List<M> FindToList<M>(DataTable dt) where M : BaseModel, new()
        {
            return Tool.ConvertDataTableToList<M>(dt);
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
        public T ToModel<T>(DataRow r, T entity) where T : BaseModel
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
