using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DBAccess.Entity;
using DBAccess.CustomAttribute;
using System.Reflection;

namespace Model
{
    public class T_RoleMenuFunction : BaseModel
    {
        //uRoleMenuFunction_ID, uRoleMenuFunction_RoleID, uRoleMenuFunction_MenuFunctionID, dRoleMenuFunction_CreateTime
        public T_RoleMenuFunction()
        {
            this.TableName = "T_RoleMenuFunction";
            this.NotFiled.Add("dRoleMenuFunction_CreateTime");
        }

        [Filed(DisplayName = "角色菜单功能ID", IsPrimaryKey = true)]
        public Guid? uRoleMenuFunction_ID { get; set; }

        [Filed(DisplayName = "角色ID")]
        public Guid? uRoleMenuFunction_RoleID { get; set; }

        [Filed(DisplayName = "功能ID")]
        public Guid? uRoleMenuFunction_FunctionID { get; set; }

        [Filed(DisplayName = "菜单ID")]
        public Guid? uRoleMenuFunction_MenuID { get; set; }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dRoleMenuFunction_CreateTime { get; set; }
    }
}
