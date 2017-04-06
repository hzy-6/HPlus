using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Collections;
using System.Data;
using Utility;
using DBAccess;
using DBAccess.Entity;
using DAL;
using Model;

namespace BLL
{
    public class T_RolesBL
    {
        DBContext db = new DBContext();
        T_Roles troles = new T_Roles();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="QuickConditions"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="pagecount"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetDataSource(Hashtable query, int pageindex, int pagesize, out int pagecount, out int counts)
        {
            try
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                DataTable dt = new T_RolesDA().GetDataSource(query, pageindex, pagesize, out pagecount, out counts);
                foreach (DataRow dr in dt.Rows)
                {
                    Dictionary<string, object> di = new Dictionary<string, object>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.DataType.Equals(DateTime.MaxValue.GetType()))
                            di.Add(dc.ColumnName, Tools.getDateTimeString(dr[dc.ColumnName], "yyyy-MM-dd HH:mm:ss"));
                        else
                            di.Add(dc.ColumnName, Tools.getString(dr[dc.ColumnName]));
                    }
                    list.Add(di);
                }
                return list.Count > 0 ? list : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="QuickConditions"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public PagingEntity GetDataSource(Hashtable query, int pageindex, int pagesize)
        {
            return new T_RolesDA().GetDataSource(query, pageindex, pagesize);
        }

        /// <summary>
        /// 根据菜单ID 和功能ID 获取 菜单权限
        /// </summary>
        /// <param name="MenuID"></param>
        /// <param name="FunID"></param>
        /// <returns></returns>
        public DataTable GetMenuPowerByMenuIDAndFunID(Guid MenuID, Guid FunID)
        {
            return new T_RolesDA().GetMenuPowerByMenuIDAndFunID(MenuID, FunID);
        }

        /// <summary>
        /// 获取角色权限根据权限id和菜单id
        /// </summary>
        /// <returns></returns>
        public DataTable GetRolePowerByRoleIDAndMenuID(Guid RoleID, Guid MenuID, Guid FunID)
        {
            return new T_RolesDA().GetRolePowerByRoleIDAndMenuID(RoleID, MenuID, FunID);
        }


    }
}
