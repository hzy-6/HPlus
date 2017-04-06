using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DBAccess.Entity;
using DBAccess.CustomAttribute;
using System.Reflection;

namespace DBAccess.Model
{
    public class T_Users : BaseModel
    {
        //uUsers_ID, cUsers_Name, cUsers_LoginName, cUsers_LoginPwd, cUsers_Email, dUsers_CreateTime
        public T_Users()
        {
            this.TableName = "T_Users";
            this.NotFiled.Add("dUsers_CreateTime");
        }

        [Filed(DisplayName = "ID", IsPrimaryKey = true)]
        public Guid? uUsers_ID { get; set; }
        [Filed(DisplayName = "真实姓名")]
        [CRequired(ErrorMessage = "请输入{name}")]
        public string cUsers_Name { get; set; }
        public string cUsers_LoginName { get; set; }
        public string cUsers_LoginPwd { get; set; }
        public string cUsers_Email { get; set; }
        public DateTime? dUsers_CreateTime { get; set; }


        //[Filed(DisplayName = "ID", IsPrimaryKey = true)]
        //public Guid? uUsers_ID
        //{
        //    set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
        //    get { return GetValue<Guid?>(MethodBase.GetCurrentMethod().Name); }
        //}
        //[Filed(DisplayName = "真实姓名")]
        //[CRequired("请输入{name}")]
        //public string cUsers_Name
        //{
        //    set
        //    {
        //        SetValue(MethodBase.GetCurrentMethod().Name, value);
        //        cUsers_Name = value;
        //    }
        //    get;
        //}
        //public string cUsers_LoginName
        //{
        //    set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
        //    get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        //}
        //public string cUsers_LoginPwd
        //{
        //    set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
        //    get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        //}
        //public string cUsers_Email
        //{
        //    set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
        //    get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        //}
        //public DateTime? dUsers_CreateTime
        //{
        //    set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
        //    get { return GetValue<DateTime?>(MethodBase.GetCurrentMethod().Name); }
        //}

    }
}
