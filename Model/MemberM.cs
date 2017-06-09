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
        }

		[Filed(DisplayName = "Member_ID", IsPrimaryKey = true)]
		public Guid? Member_ID
		{
			set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
			get { return GetValue<Guid?>(MethodBase.GetCurrentMethod().Name); }
		}

		[Filed(DisplayName = "Member_Name")]
		public string Member_Name
		{
			set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
			get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
		}

		[Filed(DisplayName = "Member_Sex")]
		public string Member_Sex
		{
			set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
			get { return GetValue<string>(MethodBase.GetCurrentMethod().Name); }
		}

		[Filed(DisplayName = "Member_CreateTime")]
		public DateTime? Member_CreateTime
		{
			set { SetValue(MethodBase.GetCurrentMethod().Name, value); }
			get { return GetValue<DateTime?>(MethodBase.GetCurrentMethod().Name); }
		}


    }
}
