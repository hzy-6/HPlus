using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;

namespace Model
{
    [ObjectRemarks.Table("T_MenuFunction")]
    public class T_MenuFunction : BaseEntity
    {
        //uMenuFunction_ID, uMenuFunction_MenuID, uMenuFunction_FunctionID, dMenuFunction_CreateTime
        public T_MenuFunction()
        {
            this.AddNoDbField(item => new { item.dMenuFunction_CreateTime });
        }

        [ObjectRemarks.Field("ID", FieldType = typeof(Guid?), IsPrimaryKey = true)]
        public Guid? uMenuFunction_ID { get; set; }

        [ObjectRemarks.Field("菜单ID")]
        public Guid? uMenuFunction_MenuID { get; set; }

        [ObjectRemarks.Field("功能ID")]
        public Guid? uMenuFunction_FunctionID { get; set; }

        [ObjectRemarks.Field("创建时间")]
        public DateTime? dMenuFunction_CreateTime { get; set; }


    }
}
