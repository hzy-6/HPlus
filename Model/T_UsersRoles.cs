using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;

namespace Model
{
    [ObjectRemarks.Table("T_UsersRoles")]
    public class T_UsersRoles : BaseEntity
    {
        //uUsersRoles_ID, uUsersRoles_UsersID, uUsersRoles_RoleID, dUsersRoles_CreateTime
        public T_UsersRoles()
        {
            this.AddNoDbField(item => new { item.dUsersRoles_CreateTime });
        }

        [ObjectRemarks.Field("ID", FieldType = typeof(Guid?), IsPrimaryKey = true)]
        public Guid? uUsersRoles_ID { get; set; }

        [ObjectRemarks.Field("用户ID")]
        public Guid? uUsersRoles_UsersID { get; set; }

        [ObjectRemarks.Field("角色ID")]
        public Guid? uUsersRoles_RoleID { get; set; }

        [ObjectRemarks.Field("创建时间")]
        public DateTime? dUsersRoles_CreateTime { get; set; }

    }
}
