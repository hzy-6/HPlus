using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;

namespace Model
{
    [ObjectRemarks.Table("T_RoleMenuFunction")]
    public class T_RoleMenuFunction : BaseEntity
    {
        //uRoleMenuFunction_ID, uRoleMenuFunction_RoleID, uRoleMenuFunction_MenuFunctionID, dRoleMenuFunction_CreateTime
        public T_RoleMenuFunction()
        {
            this.AddNoDbField(item => new { item.dRoleMenuFunction_CreateTime });
        }

        [ObjectRemarks.Field("ID", FieldType = typeof(Guid?), IsPrimaryKey = true)]
        public Guid? uRoleMenuFunction_ID { get; set; }

        [ObjectRemarks.Field("角色ID")]
        public Guid? uRoleMenuFunction_RoleID { get; set; }

        [ObjectRemarks.Field("功能ID")]
        public Guid? uRoleMenuFunction_FunctionID { get; set; }

        [ObjectRemarks.Field("菜单ID")]
        public Guid? uRoleMenuFunction_MenuID { get; set; }

        [ObjectRemarks.Field("创建时间")]
        public DateTime? dRoleMenuFunction_CreateTime { get; set; }

    }
}
