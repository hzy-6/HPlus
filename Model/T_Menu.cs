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
        public Guid? uMenu_ID
        {
            set { SetValue("uMenu_ID", value); }
            get { return GetValue<Guid?>("uMenu_ID"); }
        }

        [CRequired(ErrorMessage = "{name}不能为空")]
        [CRepeat(ErrorMessage = "{name}已存在")]
        [Filed(DisplayName = "菜单名称")]
        public string cMenu_Name
        {
            set { SetValue("cMenu_Name", value); }
            get { return GetValue<string>("cMenu_Name"); }
        }

        [Filed(DisplayName = "菜单地址")]
        public string cMenu_Url
        {
            set { SetValue("cMenu_Url", value); }
            get { return GetValue<string>("cMenu_Url"); }
        }

        [Filed(DisplayName = "菜单父级ID")]
        public Guid? uMenu_ParentID
        {
            set { SetValue("uMenu_ParentID", value); }
            get { return GetValue<Guid?>("uMenu_ParentID"); }
        }

        [CRepeat(ErrorMessage = "{name}已存在")]
        [Filed(DisplayName = "菜单编号")]
        public string cMenu_Number
        {
            set { SetValue("cMenu_Number", value); }
            get { return GetValue<string>("cMenu_Number"); }
        }

        [Filed(DisplayName = "菜单ICON")]
        public string cMenu_ICON
        {
            set { SetValue("cMenu_ICON", value); }
            get { return GetValue<string>("cMenu_ICON"); }
        }

        [Filed(DisplayName = "创建时间")]
        public DateTime? dMenu_CreateTime
        {
            set { SetValue("dMenu_CreateTime", value); }
            get { return GetValue<DateTime?>("dMenu_CreateTime"); }
        }

    }
}
