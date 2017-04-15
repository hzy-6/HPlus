using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Data.SqlClient;
using DBAccess.AdoDotNet;
using DBAccess.Entity;

namespace DBAccess.SQLContext
{
    public class DBHelper
    {
        private DBHelper() { }

        private string _ConnectionString { get; set; }

        public DBHelper(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public DataTable ExecuteDataset(string SQL)
        {
            return SqlHelper.ExecuteDataset(_ConnectionString, CommandType.Text, SQL).Tables[0];
        }

        public DataTable ExecuteDataset(SQL_Container SQL)
        {
            return SqlHelper.ExecuteDataset(_ConnectionString, CommandType.Text, SQL._SQL, SQL._SQL_Parameter).Tables[0];
        }

        public int ExecuteNonQuery(string SQL)
        {
            return SqlHelper.ExecuteNonQuery(_ConnectionString, CommandType.Text, SQL);
        }

        public int ExecuteNonQuery(SQL_Container SQL)
        {
            return SqlHelper.ExecuteNonQuery(_ConnectionString, CommandType.Text, SQL._SQL, SQL._SQL_Parameter);
        }

        public object ExecuteScalar(string SQL)
        {
            return SqlHelper.ExecuteScalar(_ConnectionString, CommandType.Text, SQL);
        }

        public DataTable SysPageList(string SQL, int PageIndex, int PageSize, out int PageCount, out int Counts)
        {
            return SqlHelper.SysPageList(SQL, PageIndex, PageSize, out PageCount, out Counts);
        }


    }
}
