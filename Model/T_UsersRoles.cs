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
    public class T_UsersRoles : BaseModel
    {
        //uUsersRoles_ID, uUsersRoles_UsersID, uUsersRoles_RoleID, dUsersRoles_CreateTime
        public T_UsersRoles()
        {
            this.TableName = "T_UsersRoles";
            this.NotFiled.Add("dUsersRoles_CreateTime");
        }

        [Filed(DisplayName = "用户角色ID", IsPrimaryKey = true)]
        public Guid? uUsersRoles_ID { get; set; }

        [Filed(DisplayName = "用户ID")]
        public Guid? uUsersRoles_UsersID { get; set; }

        [Filed(DisplayName = "角色ID")]
        public Guid? uUsersRoles_RoleID { get; set; }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dUsersRoles_CreateTime { get; set; }
    }
}
