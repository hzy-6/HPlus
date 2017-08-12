using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;

namespace Model
{
    [ObjectRemarks.Table("T_Function")]
    public class T_Function : BaseEntity
    {
        //uFunction_ID, iFunction_Number, cFunction_Name, dFunction_CreateTime210
        public T_Function()
        {
            this.AddNoDbField(item => new { item.dFunction_CreateTime });
        }

        [ObjectRemarks.Field("ID", FieldType = typeof(Guid?), IsPrimaryKey = true)]
        public Guid? uFunction_ID { get; set; }

        [ObjectRemarks.Field("会员名称")]
        public int? iFunction_Number { get; set; }

        [ObjectRemarks.CRequired(ErrorMessage = "{name}不能为空")]
        [ObjectRemarks.CRepeat(ErrorMessage = "{name}已存在")]
        [ObjectRemarks.Field("功能名称")]
        public string cFunction_Name { get; set; }

        [ObjectRemarks.CRequired(ErrorMessage = "{name}不能为空")]
        [ObjectRemarks.CRepeat(ErrorMessage = "{name}已存在")]
        [ObjectRemarks.Field("功能英文名")]
        public string cFunction_ByName { get; set; }

        [ObjectRemarks.Field("创建时间")]
        public DateTime? dFunction_CreateTime { get; set; }

    }
}
