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
    public class T_MenuFunction : BaseModel
    {
        //uMenuFunction_ID, uMenuFunction_MenuID, uMenuFunction_FunctionID, dMenuFunction_CreateTime
        public T_MenuFunction()
        {
            this.TableName = "T_MenuFunction";
            this.NotFiled.Add("dMenuFunction_CreateTime");
        }

        [Filed(DisplayName = "菜单功能ID", IsPrimaryKey = true)]
        public Guid? uMenuFunction_ID { get; set; }

        [Filed(DisplayName = "菜单ID")]
        public Guid? uMenuFunction_MenuID { get; set; }

        [Filed(DisplayName = "功能ID")]
        public Guid? uMenuFunction_FunctionID { get; set; }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dMenuFunction_CreateTime { get; set; }
    }
}
