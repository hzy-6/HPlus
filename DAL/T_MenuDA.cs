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

namespace DAL
{
    public class T_MenuDA
    {
        DBContext db = new DBContext();

        public DataTable GetMenuByRoleID()
        {
            if (Tools.getSession("RoleID").Equals("admin"))
            {
                return db.Find(@"select uMenu_ID,cMenu_Name,cMenu_Url,cMenu_Icon,uMenu_ParentId,cMenu_Number from T_Menu");
            }
            else
            {
                return db.Find(@"select a.uMenu_ID,a.cMenu_Name,a.cMenu_Url,a.cMenu_Icon,a.uMenu_ParentId,a.cMenu_Number  from (select * from T_Menu 
                             where (cMenu_Url is  null or cMenu_Url='') )a
                             join                                    
     (select cMenu_Number
                     from dbo.T_RoleMenuFunction join T_Menu on uMenu_ID=uRoleMenuFunction_MenuID
                        and uRoleMenuFunction_RoleID='" + Tools.getGuid(Tools.getSession("RoleID")) + @"'
                group by uRoleMenuFunction_MenuID,uRoleMenuFunction_RoleID,cMenu_Number
                       ) b on charindex(a.cMenu_Number,b.cMenu_Number)>0 and b.uMenu_ParentId=a.uMenu_ID
                   union select uMenu_ID,cMenu_Name,cMenu_Url,cMenu_Icon,uMenu_ParentId,cMenu_Number 
                    from T_Menu
               join (select uRoleMenuFunction_MenuID,uRoleMenuFunction_RoleID 
               from dbo.T_RoleMenuFunction 
               group by uRoleMenuFunction_MenuID,uRoleMenuFunction_RoleID)a
                on uMenu_ID=a.uRoleMenuFunction_MenuID
                  and a.uRoleMenuFunction_RoleID='" + Tools.getGuid(Tools.getSession("RoleID")) + @"'
                  order by cMenu_Number asc");
            }
        }

        /// <summary>
        /// 获取菜单和功能树
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetMenuAndFunctionTree(string RoleID = "")
        {
            //菜单功能查询
            string sql = @"WITH tab
        AS (SELECT * FROM [dbo].[T_Menu] WHERE [uMenu_ParentID] IS NULL OR uMenu_ParentID='" + Guid.Empty + @"'
			UNION ALL
            SELECT a.* FROM tab INNER JOIN [dbo].[T_Menu] a ON tab.[uMenu_ID]=a.[uMenu_ParentID]
			) 
        SELECT ([cMenu_Name]+'('+[cMenu_Number]+')') name,[uMenu_ID] id,[uMenu_ParentID] pId,[cMenu_Number] num,[cMenu_Url] ur,'false' checked,null tag FROM tab 
		ORDER BY [cMenu_Number]";

            if (!Tools.getGuid(RoleID).Equals(Guid.Empty))
            {
                //角色功能查询
                sql = @"WITH tab
        AS (SELECT * FROM [dbo].[T_Menu] WHERE [uMenu_ParentID] IS NULL OR uMenu_ParentID='" + Guid.Empty + @"'
			UNION ALL
            SELECT a.* FROM tab INNER JOIN [dbo].[T_Menu] a ON tab.[uMenu_ID]=a.[uMenu_ParentID]
			) 
        SELECT ([cMenu_Name]+'('+[cMenu_Number]+')') name,[uMenu_ID] id,[uMenu_ParentID] pId,[cMenu_Number] num,[cMenu_Url] ur,'false' checked,null tag FROM tab 
		LEFT JOIN [dbo].[T_RoleMenuFunction] A ON tab.[uMenu_ID]=A.[uRoleMenuFunction_MenuID]
		WHERE 1=1 AND [uRoleMenuFunction_RoleID]='" + Tools.getGuid(RoleID) + @"'
		ORDER BY [cMenu_Number]";
            }

            return db.GetList(db.Find(sql));
        }


    }
}
