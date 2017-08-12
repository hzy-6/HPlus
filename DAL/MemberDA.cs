using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Collections;
using DbFrame;
using DbFrame.Class;
using Utility;
using Model;

namespace DAL
{
    public class MemberDA
    {
        DBContext db = new DBContext();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="QuickConditions"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public PagingEntity GetDataSource(Hashtable query, int pageindex, int pagesize)
        {
            string where = "";
            where += string.IsNullOrEmpty(Tools.getString(query["Member_Name"])) ? "" : " and Member_Name like '%" + Tools.getString(query["Member_Name"]) + "%'";

            var pe = db.Find(@"select Member_Name,Member_Sex,Member_CreateTime,Member_ID _ukid from member where 1=1 " + where + " ", pageindex, pagesize);
            return new ToJson().GetPagingEntity(pe, new List<BaseEntity>()
            {
                new MemberM()
            });
        }


    }
}
