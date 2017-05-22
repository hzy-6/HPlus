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
        public Guid? uMenuFunction_ID
        {
            set { SetValue("uMenuFunction_ID", value); }
            get { return GetValue<Guid?>("uMenuFunction_ID"); }
        }

        [Filed(DisplayName = "菜单ID")]
        public Guid? uMenuFunction_MenuID
        {
            set { SetValue("uMenuFunction_MenuID", value); }
            get { return GetValue<Guid?>("uMenuFunction_MenuID"); }
        }

        [Filed(DisplayName = "功能ID")]
        public Guid? uMenuFunction_FunctionID
        {
            set { SetValue("uMenuFunction_FunctionID", value); }
            get { return GetValue<Guid?>("uMenuFunction_FunctionID"); }
        }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dMenuFunction_CreateTime
        {
            set { SetValue("dMenuFunction_CreateTime", value); }
            get { return GetValue<DateTime?>("dMenuFunction_CreateTime"); }
        }
    }
}
