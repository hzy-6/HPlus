using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;

namespace DbFrame.Class
{
    [ObjectRemarks.Table("Member")]
    public class Member : BaseEntity
    {
        public Member()
        {
            this.AddNoDbField(item => new { item.Member_CreateTime });
        }

        //Member_ID, Member_Name, Member_Sex, Member_CreateTime

        [ObjectRemarks.Field("ID", FieldType = typeof(Guid?), IsPrimaryKey = true)]
        public Guid? Member_ID { get; set; }
        [ObjectRemarks.CRequired(ErrorMessage = "{name}不能为空！")]
        [ObjectRemarks.Field("会员名称")]
        public string Member_Name { get; set; }
        [ObjectRemarks.Field("会员性别")]
        public string Member_Sex { get; set; }
        [ObjectRemarks.Field("创建时间")]
        public DateTime? Member_CreateTime { get; set; }

    }
}
