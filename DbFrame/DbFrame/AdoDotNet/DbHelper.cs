using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Data.SqlClient;
using DbFrame.Class;
using System.Web.Script.Serialization;

namespace DbFrame.AdoDotNet
{
    public class DbHelper
    {
        private string _ConnectionString { get; set; }

        public DbHelper(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
        }

        public DataTable ExecuteDataset(string SQL)
        {
            return SqlHelper.ExecuteDataset(_ConnectionString, CommandType.Text, SQL).Tables[0];
        }

        public DataTable ExecuteDataset(SQL SQL)
        {
            var list_sql = new List<SqlParameter>();
            foreach (var item in SQL.Parameter)
            {
                list_sql.Add(new SqlParameter() { ParameterName = item.Key, Value = item.Value == null ? DBNull.Value : item.Value });
            }
            return SqlHelper.ExecuteDataset(_ConnectionString, CommandType.Text, SQL.Sql_Parameter, list_sql.ToArray()).Tables[0];
        }

        public int ExecuteNonQuery(string SQL)
        {
            return SqlHelper.ExecuteNonQuery(_ConnectionString, CommandType.Text, SQL);
        }

        public int ExecuteNonQuery(SQL SQL)
        {
            var list_sql = new List<SqlParameter>();
            foreach (var item in SQL.Parameter)
            {
                list_sql.Add(new SqlParameter() { ParameterName = item.Key, Value = item.Value == null ? DBNull.Value : item.Value });
            }
            return SqlHelper.ExecuteNonQuery(_ConnectionString, CommandType.Text, SQL.Sql_Parameter, list_sql.ToArray());
        }

        public object ExecuteScalar(string SQL)
        {
            return SqlHelper.ExecuteScalar(_ConnectionString, CommandType.Text, SQL);
        }

        public PagingEntity PagingList(string SQL, int PageIndex, int PageSize)
        {
            var pe = new PagingEntity();
            int PageCount = 0, Counts = 0;
            var dt = new DataTable();
            dt = SqlHelper.SysPageList(SQL, PageIndex, PageSize, out PageCount, out Counts);
            pe.dt = dt;
            pe.Counts = Counts;
            pe.PageCount = PageCount;
            pe.List = ConvertDataTableToList<Dictionary<string, object>>(dt);
            return pe;
        }

        public bool Commit(List<SQL> li)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    li.ForEach(item =>
                    {
                        cmd.Parameters.Clear();
                        //执行sql
                        cmd.CommandText = item.Sql_Parameter;
                        foreach (var par in item.Parameter)
                        {
                            cmd.Parameters.Add(new SqlParameter() { ParameterName = par.Key, Value = par.Value == null ? DBNull.Value : par.Value });
                        }
                        cmd.ExecuteNonQuery();
                    });
                    //提交事务
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //失败则回滚事务
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 将datatable转换为list<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToList<T>(DataTable table)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var list = new List<T>();
            foreach (DataRow dr in table.Rows)
            {
                var model = new Dictionary<string, object>();
                foreach (DataColumn dc in table.Columns)
                {
                    if (dc.DataType.Equals(typeof(DateTime)))
                        model.Add(dc.ColumnName, (dr[dc.ColumnName] == DBNull.Value || dr[dc.ColumnName] == null ? "" : Convert.ToDateTime(dr[dc.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss")));
                    else
                        model.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                var json = jss.Serialize(model);
                json = System.Text.RegularExpressions.Regex.Replace(json, @"\\/Date\((\d+)\)\\/", match =>
                {
                    DateTime dt = new DateTime(1970, 1, 1);
                    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    dt = dt.ToLocalTime();
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                });
                list.Add(jss.Deserialize<T>(json));
            }
            return list;
        }


    }
}
