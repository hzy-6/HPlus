using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;

namespace Model
{
    [ObjectRemarks.Table("T_Menu")]
    public class T_Menu : BaseEntity
    {
        public T_Menu()
        {
            this.AddNoDbField(item => new { item.dMenu_CreateTime });
        }

        [ObjectRemarks.Field("ID", FieldType = typeof(Guid?), IsPrimaryKey = true)]
        public Guid? uMenu_ID { get; set; }

        [ObjectRemarks.CRequired(ErrorMessage = "{name}不能为空")]
        [ObjectRemarks.CRepeat(ErrorMessage = "{name}已存在")]
        [ObjectRemarks.Field("菜单名称")]
        public string cMenu_Name { get; set; }

        [ObjectRemarks.Field("菜单地址")]
        public string cMenu_Url { get; set; }

        [ObjectRemarks.Field("菜单父级ID")]
        public Guid? uMenu_ParentID { get; set; }

        [ObjectRemarks.CRepeat(ErrorMessage = "{name}已存在")]
        [ObjectRemarks.Field("菜单编号")]
        public string cMenu_Number { get; set; }

        [ObjectRemarks.Field("菜单ICON")]
        public string cMenu_ICON { get; set; }

        [ObjectRemarks.Field("创建时间")]
        public DateTime? dMenu_CreateTime { get; set; }

    }
}
