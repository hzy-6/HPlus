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
        public Guid? uUsersRoles_ID
        {
            set { SetValue("uUsersRoles_ID", value); }
            get { return GetValue<Guid?>("uUsersRoles_ID"); }
        }

        [Filed(DisplayName = "用户ID")]
        public Guid? uUsersRoles_UsersID
        {
            set { SetValue("uUsersRoles_UsersID", value); }
            get { return GetValue<Guid?>("uUsersRoles_UsersID"); }
        }

        [Filed(DisplayName = "角色ID")]
        public Guid? uUsersRoles_RoleID
        {
            set { SetValue("uUsersRoles_RoleID", value); }
            get { return GetValue<Guid?>("uUsersRoles_RoleID"); }
        }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dUsersRoles_CreateTime
        {
            set { SetValue("dUsersRoles_CreateTime", value); }
            get { return GetValue<DateTime?>("dUsersRoles_CreateTime"); }
        }
    }
}
