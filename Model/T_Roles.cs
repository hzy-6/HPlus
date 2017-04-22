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
        public Guid? uRoles_ID { get; set; }

        [Filed(DisplayName = "角色编号")]
        public string cRoles_Number { get; set; }

        [Filed(DisplayName = "角色名称")]
        public string cRoles_Name { get; set; }

        [Filed(DisplayName = "角色备注")]
        public string cRoles_Remark { get; set; }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dRoles_CreateTime { get; set; }
    }
}
