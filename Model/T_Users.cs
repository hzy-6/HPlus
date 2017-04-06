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
        }

        [Filed(DisplayName = "用户ID", IsPrimaryKey = true)]
        public Guid? uUsers_ID { get; set; }

        [CRequired(ErrorMessage = "{name}不能为空")]
        [CRepeat(ErrorMessage = "{name}已存在")]
        [Filed(DisplayName = "真实名称")]
        public string cUsers_Name { get; set; }

        [CRequired(ErrorMessage = "用户登陆名不能为空")]
        [Filed(DisplayName = "登陆名称")]
        public string cUsers_LoginName { get; set; }

        [Filed(DisplayName = "登陆密码")]
        public string cUsers_LoginPwd { get; set; }

        [Filed(DisplayName = "邮箱")]
        public string cUsers_Email { get; set; }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dUsers_CreateTime { get; set; }
    }
}
