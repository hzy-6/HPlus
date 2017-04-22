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
    public class T_Menu : BaseModel
    {
        public T_Menu()
        {
            this.TableName = "T_Menu";
            this.NotFiled.Add("dMenu_CreateTime");
        }

        [Filed(DisplayName = "菜单ID", IsPrimaryKey = true)]
        public Guid? uMenu_ID { get; set; }

        [CRequired(ErrorMessage = "{name}不能为空")]
        [CRepeat(ErrorMessage = "{name}已存在")]
        [Filed(DisplayName = "菜单名称")]
        public string cMenu_Name { get; set; }

        [Filed(DisplayName = "菜单地址")]
        public string cMenu_Url { get; set; }

        [Filed(DisplayName = "菜单父级ID")]
        public Guid? uMenu_ParentID { get; set; }

        [CRepeat(ErrorMessage = "{name}已存在")]
        [Filed(DisplayName = "菜单编号")]
        public string cMenu_Number { get; set; }

        [Filed(DisplayName = "菜单ICON")]
        public string cMenu_ICON { get; set; }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dMenu_CreateTime { get; set; }

    }
}
