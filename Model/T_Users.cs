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
            set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
            get { return GetValue<Guid?>(MethodBase.GetCurrentMethod().Name); }
        }

        [CRequired(ErrorMessage = "{name}不能为空")]
        [Filed(DisplayName = "用户名")]
        public string cUsers_Name
        {
            set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
            get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        }

        [CRequired(ErrorMessage = "{name}不能为空")]
        [Filed(DisplayName = "登陆名称")]
        public string cUsers_LoginName
        {
            set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
            get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        }

        [Filed(DisplayName = "登陆密码")]
        public string cUsers_LoginPwd
        {
            set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
            get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        }

        [Filed(DisplayName = "邮箱")]
        public string cUsers_Email
        {
            set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
            get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dUsers_CreateTime
        {
            set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
            get { return GetValue<DateTime?>(MethodBase.GetCurrentMethod().Name); }
        }
    }
}
