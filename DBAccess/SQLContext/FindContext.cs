using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using DBAccess.Reflection;
using DBAccess.Entity;
using System.Dynamic;
using System.Data;
using System.Web.Script.Serialization;

namespace DBAccess.SQLContext
{
    public class FindContext<T> where T : BaseModel, new()
    {
        Context.FindSqlString<T> sqlstring;
        JavaScriptSerializer jss;
        SelectContext select;
        private FindContext() { }

        private string _ConnectionString { get; set; }

        public FindContext(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
            select = new SelectContext(_ConnectionString);
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
            var dt = select.ExecuteDataset(sql);
            if (dt.Rows.Count == 0)
                return (M)Activator.CreateInstance(entity.GetType());
            return ToModel(dt.Rows[0], (M)Activator.CreateInstance(entity.GetType()));
        }

        public M Find<M>(string where) where M : BaseModel, new()
        {
            var sql = this.GetSql<M>(where);
            var dt = select.ExecuteDataset(sql);
            if (dt.Rows.Count == 0)
                return (M)Activator.CreateInstance(typeof(M));
            return ToModel(dt.Rows[0], (M)Activator.CreateInstance(typeof(M)));
        }

        public M Find<M>(Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            var sql = this.GetSql<M>(where);
            var dt = select.ExecuteDataset(sql);
            if (dt.Rows.Count == 0)
                return (M)Activator.CreateInstance(typeof(M));
            return ToModel(dt.Rows[0], (M)Activator.CreateInstance(typeof(M)));
        }

        public DataTable Find<M>(M entity, string OrderBy) where M : BaseModel, new()
        {
            sqlstring.OrderBy = OrderBy;
            var sql = this.GetSql(entity);
            return select.ExecuteDataset(sql);
        }

        public List<M> FindToList<M>(M entity, string OrderBy) where M : BaseModel, new()
        {
            sqlstring.OrderBy = OrderBy;
            var sql = this.GetSql(entity);
            var dt = select.ExecuteDataset(sql);
            return this.FindToList<M>(dt);
        }

        public List<M> FindToList<M>(DataTable dt) where M : BaseModel, new()
        {
            return this.ConvertDataTableToList<M>(dt);
        }

        public DataTable Find(string SQL)
        {
            return select.ExecuteDataset(SQL);
        }

        public object FINDToObj(string SQL)
        {
            return select.ExecuteScalar(SQL.ToString());
        }

        public DataTable Find(string SQL, int PageIndex, int PageSize, out int PageCount, out int Counts)
        {
            return select.SysPageList(SQL, PageIndex, PageSize, out PageCount, out Counts);
        }

        public PagingEntity Find(string SQL, int PageIndex, int PageSize)
        {
            int PageCount = 0, Counts = 0;
            var list = new List<Dictionary<string, object>>();
            var dt = this.Find(SQL, PageIndex, PageSize, out PageCount, out Counts);
            var di = new Dictionary<string, object>();
            foreach (DataRow dr in dt.Rows)
            {
                di = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    di.Add(dc.ColumnName, Convert.ChangeType(dr[dc.ColumnName], dc.DataType));
                }
                list.Add(di);
            }
            return new PagingEntity() { List = list.Count > 0 ? list : new List<Dictionary<string, object>>(), dt = dt, PageCount = PageCount, Counts = Counts };
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
            foreach (DataColumn item in r.Table.Columns) model.Add(item.ColumnName, r[item.ColumnName]);
            return jss.Deserialize<T>(jss.Serialize(model));
        }

        /// <summary>
        /// 将datatable转换为list<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<T> ConvertDataTableToList<T>(DataTable table)
        {
            var list = new List<T>();
            foreach (DataRow dr in table.Rows)
            {
                var model = new Dictionary<string, object>();
                foreach (DataColumn dc in table.Columns)
                    model.Add(dc.ColumnName, dr[dc.ColumnName]);
                list.Add(jss.Deserialize<T>(jss.Serialize(model)));
            }
            return list;
        }

    }
}
