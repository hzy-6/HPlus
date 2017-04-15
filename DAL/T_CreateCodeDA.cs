using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Collections;
using DBAccess;
using DBAccess.Entity;
using Utility;
using Model;

namespace DAL
{

    public class T_CreateCodeDA
    {
        DBContext db = new DBContext();

        /// <summary>
        /// 获取数据库中的所有表和字段
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetDatabaseAllTable()
        {
            string sql = @"select TABLE_NAME+' [表]' name,TABLE_NAME id,null pId from INFORMATION_SCHEMA.TABLES
union all
select case when CHARACTER_MAXIMUM_LENGTH is null then COLUMN_NAME+' [字段类型:'+DATA_TYPE+']'
when CHARACTER_MAXIMUM_LENGTH is not null then COLUMN_NAME+' [字段类型:'+DATA_TYPE+'('+CONVERT(varchar(10),CHARACTER_MAXIMUM_LENGTH)+')]' end
 name,TABLE_NAME+'$~'+COLUMN_NAME id,TABLE_NAME from INFORMATION_SCHEMA.COLUMNS";
            return db.GetList(db.Find(sql));
        }

        /// <summary>
        /// 根据表获取列
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetColByTable(string table)
        {
            string sql = @"select a.COLUMN_NAME colname,case when a.COLUMN_NAME=b.COLUMN_NAME then '主键' end iskey,a.DATA_TYPE type from INFORMATION_SCHEMA.COLUMNS a 
left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE b on a.TABLE_NAME=b.TABLE_NAME where a.TABLE_NAME='" + table + "' ";
            return db.GetList(db.Find(sql));
        }


    }
}
