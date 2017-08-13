using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;

namespace DbFrame.Class
{
    [ObjectRemarks.Table("T_Users")]
    public class TestT_Users : BaseEntity
    {
        //uUsers_ID, cUsers_Name, cUsers_LoginName, cUsers_LoginPwd, cUsers_Email, dUsers_CreateTime
        public TestT_Users()
        {
            this.AddNoDbField(item => new { item.dUsers_CreateTime });
        }

        [ObjectRemarks.Field("ID", FieldType = typeof(Guid?), IsPrimaryKey = true)]
        public Guid? uUsers_ID { get; set; }

        [ObjectRemarks.CRequired(ErrorMessage = "{name}不能为空")]
        [ObjectRemarks.Field("用户名")]
        public string cUsers_Name { get; set; }

        [ObjectRemarks.CRequired(ErrorMessage = "{name}不能为空")]
        [ObjectRemarks.Field("登陆名称")]
        public string cUsers_LoginName { get; set; }

        [ObjectRemarks.Field("登陆密码")]
        public string cUsers_LoginPwd { get; set; }

        [ObjectRemarks.Field("邮箱")]
        public string cUsers_Email { get; set; }

        [ObjectRemarks.Field("创建时间")]
        public DateTime? dUsers_CreateTime { get; set; }

    }
}
