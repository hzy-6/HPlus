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
        public Guid? uRoleMenuFunction_ID
        {
            set { SetValue("uRoleMenuFunction_ID", value); }
            get { return GetValue<Guid?>("uRoleMenuFunction_ID"); }
        }

        [Filed(DisplayName = "角色ID")]
        public Guid? uRoleMenuFunction_RoleID
        {
            set { SetValue("uRoleMenuFunction_RoleID", value); }
            get { return GetValue<Guid?>("uRoleMenuFunction_RoleID"); }
        }

        [Filed(DisplayName = "功能ID")]
        public Guid? uRoleMenuFunction_FunctionID
        {
            set { SetValue("uRoleMenuFunction_FunctionID", value); }
            get { return GetValue<Guid?>("uRoleMenuFunction_FunctionID"); }
        }

        [Filed(DisplayName = "菜单ID")]
        public Guid? uRoleMenuFunction_MenuID
        {
            set { SetValue("uRoleMenuFunction_MenuID", value); }
            get { return GetValue<Guid?>("uRoleMenuFunction_MenuID"); }
        }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dRoleMenuFunction_CreateTime
        {
            set { SetValue("dRoleMenuFunction_CreateTime", value); }
            get { return GetValue<DateTime?>("dRoleMenuFunction_CreateTime"); }
        }
    }
}
