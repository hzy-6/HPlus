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
    public class MemberM : BaseModel
    {
        public MemberM()
        {
            this.TableName = "Member";
            this.NotFiled.Add("Member_CreateTime");
        }

        [Filed(DisplayName = "Member_ID", IsPrimaryKey = true)]
        public Guid? Member_ID
        {
            set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
            get { return GetValue<Guid?>(MethodBase.GetCurrentMethod().Name); }
        }

        [Filed(DisplayName = "会员名称")]
        public string Member_Name
        {
            set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
            get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        }

        [Filed(DisplayName = "性别")]
        public string Member_Sex
        {
            set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
            get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
        }

        [Filed(DisplayName = "创建时间")]
        public DateTime? Member_CreateTime
        {
            set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
            get { return GetValue<DateTime?>(MethodBase.GetCurrentMethod().Name); }
        }


    }
}
