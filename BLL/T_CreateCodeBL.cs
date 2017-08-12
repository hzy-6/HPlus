using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Collections;
using System.Data;
using Utility;
using DbFrame;
using DbFrame.Class;
using DAL;
using Model;

namespace BLL
{
    public class T_CreateCodeBL
    {
        DBContext db = new DBContext();

        /// <summary>
        /// 获取数据库中所有的表
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetDatabaseAllTable()
        {
            return new T_CreateCodeDA().GetDatabaseAllTable();
        }

        /// <summary>
        /// 根据表获取列
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetColByTable(string table)
        {
            return new T_CreateCodeDA().GetColByTable(table);
        }

        /// <summary>
        /// 获取所有的table
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetAllTable()
        {
            return new T_CreateCodeDA().GetAllTable();
        }


    }
}
