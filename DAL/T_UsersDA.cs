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
    public class T_UsersDA
    {
        DBContext db = new DBContext();
        T_Users tusers = new T_Users();

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
            where += string.IsNullOrEmpty(Tools.getString(query[Tools.getAttrName(() => tusers.cUsers_Name)])) ? "" : " and " + Tools.getAttrName(() => tusers.cUsers_Name) + " like '%" + Tools.getString(query[Tools.getAttrName(() => tusers.cUsers_Name)]) + "%' ";
            where += string.IsNullOrEmpty(Tools.getString(query[Tools.getAttrName(() => tusers.cUsers_LoginName)])) ? "" : " and " + Tools.getAttrName(() => tusers.cUsers_LoginName) + " like '%" + Tools.getString(query[Tools.getAttrName(() => tusers.cUsers_LoginName)]) + "%' ";

            PagingEntity pe = db.Find(@"select cUsers_Name, cUsers_LoginName,cRoles_Name ,dUsers_CreateTime,uUsers_ID _ukid
				            from T_Users a
				            left join dbo.T_UsersRoles b on a.uUsers_ID=b.uUsersRoles_UsersID
				            left join dbo.T_Roles c on b.uUsersRoles_RoleID=c.uRoles_ID
                                                where 1=1 " + where + " ", pageindex, pagesize);
            return new ToJson().GetPagingEntity(pe, new List<BaseEntity>()
            {
                new T_Users(),
                new T_Roles()
            });
        }


    }
}
