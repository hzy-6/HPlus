using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Data.SqlClient;

namespace DBAccess.Entity
{
    public class SQL_Container
    {
        /// <summary>
        /// 参数化的 原生sql 语句
        /// </summary>
        public string _SQL { get; set; }
        /// <summary>
        /// 参数化的 值
        /// </summary>
        public List<dynamic> _SQL_Parameter { get; set; }
        /// <summary>
        /// 未参数化的 原生sql 语句
        /// </summary>
        public string _Not_SQL_Parameter { get; set; }

        public SQL_Container(string SQL, List<dynamic> SQL_Parameter)
        {
            this._SQL = SQL;
            this._SQL_Parameter = SQL_Parameter;
            SQL_Parameter.ToList().ForEach(item =>
            {
                SQL = SQL.Replace("@" + item.Key, item.Value == null ? null : "'" + item.Value.ToString() + "' ");
            });
            this._Not_SQL_Parameter = SQL;
        }


    }
}
