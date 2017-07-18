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
    public class T_RolesDA
    {
        DBContext db = new DBContext();
        T_Roles troles = new T_Roles();

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
            where += string.IsNullOrEmpty(Tools.getString(query[Tools.getAttrName(() => troles.cRoles_Name)])) ? "" : " and " + Tools.getAttrName(() => troles.cRoles_Name) + " like '%" + Tools.getString(query[Tools.getAttrName(() => troles.cRoles_Name)]) + "%' ";

            var pe = db.Find(@"select uRoles_ID _ukid, cRoles_Number, cRoles_Name, cRoles_Remark, dRoles_CreateTime from T_Roles where 1=1 " + where + " order by cRoles_Number ", pageindex, pagesize);
            return new ToJson().GetPagingEntity(pe, new List<BaseModel>()
            {
                new T_Roles()
            });
        }


    }
}
