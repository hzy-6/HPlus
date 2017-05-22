using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DBAccess.Entity;
using DBAccess.CustomAttribute;

namespace Model
{
    public class T_Users : BaseModel
    {
        //uUsers_ID, cUsers_Name, cUsers_LoginName, cUsers_LoginPwd, cUsers_Email, dUsers_CreateTime
        public T_Users()
        {
            this.TableName = "T_Users";
            this.NotFiled.Add("dUsers_CreateTime");
        }

        [Filed(DisplayName = "用户ID", IsPrimaryKey = true)]
        public Guid? uUsers_ID
        {
            set { SetValue("uUsers_ID", value); }
            get { return GetValue<Guid?>("uUsers_ID"); }
        }

        [CRequired(ErrorMessage = "{name}不能为空")]
        [Filed(DisplayName = "真实名称")]
        public string cUsers_Name
        {
            set { SetValue("cUsers_Name", value); }
            get { return GetValue<string>("cUsers_Name"); }
        }

        [CRequired(ErrorMessage = "用户登陆名不能为空")]
        [Filed(DisplayName = "登陆名称")]
        public string cUsers_LoginName
        {
            set { SetValue("cUsers_LoginName", value); }
            get { return GetValue<string>("cUsers_LoginName"); }
        }

        [Filed(DisplayName = "登陆密码")]
        public string cUsers_LoginPwd
        {
            set { SetValue("cUsers_LoginPwd", value); }
            get { return GetValue<string>("cUsers_LoginPwd"); }
        }

        [Filed(DisplayName = "邮箱")]
        public string cUsers_Email
        {
            set { SetValue("cUsers_Email", value); }
            get { return GetValue<string>("cUsers_Email"); }
        }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dUsers_CreateTime
        {
            set { SetValue("dUsers_CreateTime", value); }
            get { return GetValue<DateTime?>("dUsers_CreateTime"); }
        }
    }
}
