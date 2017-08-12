using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;

namespace Model
{
    [ObjectRemarks.Table("T_Roles")]
    public class T_Roles : BaseEntity
    {
        //uRoles_ID, cRoles_Number, cRoles_Name, cRoles_Remark, dRoles_CreateTime
        public T_Roles()
        {
            this.AddNoDbField(item => new { item.dRoles_CreateTime });
        }

        [ObjectRemarks.Field("ID", FieldType = typeof(Guid?), IsPrimaryKey = true)]
        public Guid? uRoles_ID { get; set; }

        [ObjectRemarks.Field("角色编号")]
        public string cRoles_Number { get; set; }

        [ObjectRemarks.CRequired(ErrorMessage = "{name}不能为空")]
        [ObjectRemarks.Field("角色名称")]
        public string cRoles_Name { get; set; }

        [ObjectRemarks.Field("角色备注")]
        public string cRoles_Remark { get; set; }

        [ObjectRemarks.Field("创建时间")]
        public DateTime? dRoles_CreateTime { get; set; }

    }
}
