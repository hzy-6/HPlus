using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Data.SqlClient;
using DBAccess.Entity;
using DBAccess.HelperClass;
using MySql.Data.MySqlClient;

namespace DBAccess.AdoDotNet
{
    public sealed class DBHelper
    {
        private string _ConnectionString { get; set; }
        private DBType _DBType { get; set; }

        /// <summary>
        /// 数据库帮助类 
        /// </summary>
        /// <param name="ConnectionString">链接字符串</param>
        /// <param name="DBType">数据库类型[SqlServer，MySql，Oracle]</param>
        public DBHelper(string ConnectionString, DBType DBType)
        {
            this._ConnectionString = ConnectionString;// = DBType.SqlServer
            this._DBType = DBType;
        }

        public DataTable ExecuteDataset(string SQL)
        {
            var dt = new DataTable();
            switch (_DBType)
            {
                case DBType.MySql:
                    dt = MySqlHelper.ExecuteDataset(_ConnectionString, SQL).Tables[0];
                    break;
                case DBType.SqlServer:
                default:
                    dt = SqlHelper.ExecuteDataset(_ConnectionString, CommandType.Text, SQL).Tables[0];
                    break;
            }
            return dt;
        }

        public DataTable ExecuteDataset(SQL_Container SQL)
        {
            var dt = new DataTable();
            switch (_DBType)
            {
                case DBType.MySql:
                    var list_mysql = new List<MySqlParameter>();
                    foreach (var item in SQL._SQL_Parameter)
                    {
                        list_mysql.Add(new MySqlParameter() { ParameterName = item.Key, Value = item.Value == null ? DBNull.Value : item.Value });
                    }
                    dt = MySqlHelper.ExecuteDataset(_ConnectionString, SQL._SQL, list_mysql.ToArray()).Tables[0];
                    break;
                case DBType.SqlServer:
                default:
                    var list_sql = new List<SqlParameter>();
                    foreach (var item in SQL._SQL_Parameter)
                    {
                        list_sql.Add(new SqlParameter() { ParameterName = item.Key, Value = item.Value == null ? DBNull.Value : item.Value });
                    }
                    dt = SqlHelper.ExecuteDataset(_ConnectionString, CommandType.Text, SQL._SQL, list_sql.ToArray()).Tables[0];
                    break;
            }
            return dt;
        }

        public int ExecuteNonQuery(string SQL)
        {
            var count = 0;
            switch (_DBType)
            {
                case DBType.MySql:
                    count = MySqlHelper.ExecuteNonQuery(_ConnectionString, SQL);
                    break;
                case DBType.SqlServer:
                default:
                    count = SqlHelper.ExecuteNonQuery(_ConnectionString, CommandType.Text, SQL);
                    break;
            }
            return count;
        }

        public int ExecuteNonQuery(SQL_Container SQL)
        {
            var count = 0;
            switch (_DBType)
            {
                case DBType.MySql:
                    var list_mysql = new List<MySqlParameter>();
                    foreach (var item in SQL._SQL_Parameter)
                    {
                        list_mysql.Add(new MySqlParameter() { ParameterName = item.Key, Value = item.Value == null ? DBNull.Value : item.Value });
                    }
                    count = MySqlHelper.ExecuteNonQuery(_ConnectionString, SQL._SQL, list_mysql.ToArray());
                    break;
                case DBType.SqlServer:
                default:
                    var list_sql = new List<SqlParameter>();
                    foreach (var item in SQL._SQL_Parameter)
                    {
                        list_sql.Add(new SqlParameter() { ParameterName = item.Key, Value = item.Value == null ? DBNull.Value : item.Value });
                    }
                    count = SqlHelper.ExecuteNonQuery(_ConnectionString, CommandType.Text, SQL._SQL, list_sql.ToArray());
                    break;
            }
            return count;
        }

        public object ExecuteScalar(string SQL)
        {
            object rel = null;
            switch (_DBType)
            {
                case DBType.MySql:
                    rel = MySqlHelper.ExecuteScalar(_ConnectionString, SQL);
                    break;
                case DBType.SqlServer:
                default:
                    rel = SqlHelper.ExecuteScalar(_ConnectionString, CommandType.Text, SQL);
                    break;
            }
            return rel;
        }

        public PagingEntity PagingList(string SQL, int PageIndex, int PageSize)
        {
            var pe = new PagingEntity();
            int PageCount = 0, Counts = 0;
            var dt = new DataTable();
            switch (_DBType)
            {
                case DBType.MySql:
                    dt = MySqlHelper.PagingList(_ConnectionString, SQL, PageIndex, PageSize, out PageCount, out Counts);
                    break;
                case DBType.SqlServer:
                default:
                    dt = SqlHelper.SysPageList(SQL, PageIndex, PageSize, out PageCount, out Counts);
                    break;
            }
            pe.dt = dt;
            pe.Counts = Counts;
            pe.PageCount = PageCount;
            pe.List = Tool.ConvertDataTableToList<Dictionary<string, object>>(dt);
            return pe;
        }

        public bool Commit(List<SQL_Container> li)
        {
            var rel = false;
            switch (_DBType)
            {
                case DBType.MySql:
                    rel = MySqlHelper.COMMIT(_ConnectionString, li);
                    break;
                case DBType.SqlServer:
                default:
                    rel = SqlHelper.COMMIT(_ConnectionString, li);
                    break;
            }
            return rel;
        }

    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DBType
    {
        SqlServer,
        MySql,
        //Oracle
    }
}
