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
    public class T_Function : BaseModel
    {
        //uFunction_ID, iFunction_Number, cFunction_Name, dFunction_CreateTime
        public T_Function()
        {
            this.TableName = "T_Function";
            this.NotFiled.Add("dFunction_CreateTime");
        }

        [Filed(DisplayName = "功能ID", IsPrimaryKey = true)]
        public Guid? uFunction_ID { get; set; }

        [Filed(DisplayName = "功能编号")]
        public int? iFunction_Number { get; set; }

        [CRequired(ErrorMessage = "{name}不能为空")]
        [CRepeat(ErrorMessage = "{name}已存在")]
        [Filed(DisplayName = "功能名称")]
        public string cFunction_Name { get; set; }

        [CRequired(ErrorMessage = "{name}不能为空")]
        [CRepeat(ErrorMessage = "{name}已存在")]
        [Filed(DisplayName = "功能英文名")]
        public string cFunction_ByName { get; set; }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dFunction_CreateTime { get; set; }
    }
}
