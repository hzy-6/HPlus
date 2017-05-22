using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using DBAccess.Entity;

namespace DBAccess.AdoDotNet
{
    public sealed class MySqlHelper
    {
        public static DataSet ExecuteDataset(string connectionString, string Sql)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                connection.Open();
                MySqlDataAdapter command = new MySqlDataAdapter(Sql, connection);
                command.Fill(ds, "ds");
                return ds;
            }
        }

        public static DataSet ExecuteDataset(string connectionString, string Sql, params MySqlParameter[] commandParameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, Sql, commandParameters);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                    return ds;
                }
            }
        }

        public static int ExecuteNonQuery(string connectionString, string Sql)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(Sql, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public static int ExecuteNonQuery(string connectionString, string Sql, params MySqlParameter[] commandParameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    PrepareCommand(cmd, connection, null, Sql, commandParameters);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
            }
        }

        public static object ExecuteScalar(string connectionString, string SQL)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(SQL, connection);
            connection.Open();
            MySqlDataReader myReader = cmd.ExecuteReader();
            return myReader;
        }

        public static DataTable PagingList(string connectionString, string SQL, int PageIndex, int PageSize, out int PageCount, out int Counts)
        {
            Counts = ExecuteDataset(connectionString, SQL).Tables[0].Rows.Count;
            if (Counts % PageSize == 0)
                PageCount = Counts / PageSize;
            else
                PageCount = Counts / PageSize + 1;
            SQL = SQL + " limit " + ((PageIndex - 1) * PageSize) + "," + PageSize + " ";
            return ExecuteDataset(connectionString, SQL).Tables[0];
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="li"></param>
        /// <returns></returns>
        public static bool COMMIT(string connectionString, List<SQL_Container> li)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                var list_sqlpar = new List<MySqlParameter>();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    li.ForEach(item =>
                    {
                        cmd.Parameters.Clear();
                        //执行sql
                        cmd.CommandText = item._SQL;
                        foreach (var par in item._SQL_Parameter)
                        {
                            cmd.Parameters.Add(new MySqlParameter() { ParameterName = par.Key, Value = par.Value == null ? DBNull.Value : par.Value });
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
        /// 执行ADO.NET SQL时要用的参数添加
        /// <returns>SqlDataReader</returns>
        /// 
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

    }
}
