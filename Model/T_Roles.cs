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
    public class T_Roles : BaseModel
    {
        //uRoles_ID, cRoles_Number, cRoles_Name, cRoles_Remark, dRoles_CreateTime
        public T_Roles()
        {
            this.TableName = "T_Roles";
            this.NotFiled.Add("dRoles_CreateTime");
        }

        [Filed(DisplayName = "角色ID", IsPrimaryKey = true)]
        public Guid? uRoles_ID
        {
            set { SetValue("uRoles_ID", value); }
            get { return GetValue<Guid?>("uRoles_ID"); }
        }

        [Filed(DisplayName = "角色编号")]
        public string cRoles_Number
        {
            set { SetValue("cRoles_Number", value); }
            get { return GetValue<string>("cRoles_Number"); }
        }

        [Filed(DisplayName = "角色名称")]
        public string cRoles_Name
        {
            set { SetValue("cRoles_Name", value); }
            get { return GetValue<string>("cRoles_Name"); }
        }

        [Filed(DisplayName = "角色备注")]
        public string cRoles_Remark
        {
            set { SetValue("cRoles_Remark", value); }
            get { return GetValue<string>("cRoles_Remark"); }
        }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dRoles_CreateTime
        {
            set { SetValue("dRoles_CreateTime", value); }
            get { return GetValue<DateTime?>("dRoles_CreateTime"); }
        }
    }
}
