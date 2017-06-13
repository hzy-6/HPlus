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

        /// <summary>
        /// 根据菜单ID 和功能ID 获取 菜单权限
        /// </summary>
        /// <param name="MenuID"></param>
        /// <param name="FunID"></param>
        /// <returns></returns>
        public DataTable GetMenuPowerByMenuIDAndFunID(Guid MenuID, Guid FunID)
        {
            return db.Find(@"select distinct * 
                                    from dbo.T_MenuFunction a
                                    left join dbo.T_Function b on a.uMenuFunction_FunctionID=b.uFunction_ID
                                    left join dbo.T_Menu c on a.uMenuFunction_MenuID=c.uMenu_ID
                                    where 1=1 
                                    and c.uMenu_ID='" + MenuID + @"' 
                                    and c.uFunction_ID='" + FunID + @"'");
        }

        /// <summary>
        /// 获取角色权限根据权限id和菜单id
        /// </summary>
        /// <returns></returns>
        public DataTable GetRolePowerByRoleIDAndMenuID(Guid RoleID, Guid MenuID, Guid FunID)
        {
            return db.Find(@"select distinct uFunction_ID,uRoleMenuFunction_ID,cFunction_Name,iFunction_Number,uRoleMenuFunction_RoleID 
                                                    from dbo.T_RoleMenuFunction a
                                                    left join dbo.T_MenuFunction b on a.uRoleMenuFunction_FunctionID=b.uMenuFunction_FunctionID
                                                    left join dbo.T_Function c on b.uMenuFunction_FunctionID=c.uFunction_ID
                                                    where uFunction_ID ='" + FunID + @"'
                                                    and uRoleMenuFunction_RoleID='" + RoleID + @"' and uRoleMenuFunction_MenuID='" + MenuID + @"'");
        }

    }
}
