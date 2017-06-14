using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Collections;
using System.Data;
using Utility;
using DBAccess;
using DBAccess.Entity;
using DAL;
using Model;

namespace BLL
{
    public class T_MenuBL
    {
        DBContext db = new DBContext();
        T_Menu t_menu = new T_Menu();

        /// <summary>
        /// 获取所有的菜单
        /// </summary>
        /// <returns></returns>
        public DataTable GetMenuByRoleID()
        {
            return new T_MenuDA().GetMenuByRoleID();
        }

        #region  H+  左侧菜单

        /// <summary>
        /// 创建系统菜单
        /// </summary>
        int i = 0;
        public string CreateSysMenu()
        {
            DataTable dt = GetMenuByRoleID();
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                var listRow = dt.AsEnumerable().ToList();
                var parentList = listRow.Where(item => (item.Field<object>("uMenu_ParentID") == null ? item.Field<object>("uMenu_ParentID") == null : item.Field<object>("uMenu_ParentID").Equals(Guid.Empty))).ToList();
                foreach (var item in parentList)
                {
                    i = 0;
                    var childList = listRow.Where(m => m.Field<object>("uMenu_ParentID") != null && m.Field<object>("uMenu_ParentID").Equals(Guid.Parse(item["uMenu_ID"].ToString()))).ToList();
                    if (childList.Count() > 0)
                    {
                        sb.Append("<li>");
                        sb.Append(string.Format("<a href='javascript:void(0)'><i class='{0}'></i><span class='nav-label'>{1}</span><span class='fa arrow'></span></a>", item["cMenu_ICON"], item["cMenu_Name"]));
                        FindChildMenu(dt, Tools.getGuid(item["uMenu_ID"]), ref sb);
                        sb.Append("</li>");
                    }
                    else
                    {
                        sb.Append("<li>");
                        string url = Tools.getString(item["cMenu_Url"]);
                        sb.Append(string.Format("<a href='{0}' data-id='{0}' class='J_menuItem'><i class='{2}'></i><span class='nav-label'>{3}</span></a>", url, item["uMenu_ID"], item["cMenu_ICON"], item["cMenu_Name"]));
                        sb.Append("</li>");
                    }
                }
            }
            else
            {
                sb.Append(@"<li>");
                sb.Append(@"<a href='#'>");
                sb.Append(@"<i class='fa fa-ban'></i>");
                sb.Append(@"<span class='nav-label'>无权限</span>");
                sb.Append(@"</a>");
                sb.Append(@"</li>");
            }
            return sb.ToString();
        }

        private void FindChildMenu(DataTable dt, Guid id, ref StringBuilder sb)
        {
            if (dt.Rows.Count > 0)
            {
                i++;
                var listRow = dt.AsEnumerable().ToList();
                var List = listRow.Where(item => (item.Field<object>("uMenu_ParentID") == null ? item.Field<object>("uMenu_ParentID") != null : item.Field<object>("uMenu_ParentID").Equals(id))).ToList();
                foreach (var item in List)
                {
                    var childList = listRow.Where(m => m.Field<object>("uMenu_ParentID") != null && m.Field<object>("uMenu_ParentID").Equals(Guid.Parse(item["uMenu_ID"].ToString()))).ToList();
                    if (childList.Count() > 0)
                    {
                        if (List.IndexOf(item) == 0)
                            sb.Append("<ul class='nav nav-second-level'>");

                        sb.Append(string.Format("<li uMenu_ParentId='{0}'>", item["uMenu_ParentID"]));
                        sb.Append(string.Format("<a href='javascript:void(0)'><i class='{0}'></i> <span class='nav-label'>{1}</span><span class='fa arrow'></span></a>", item["cMenu_ICON"], item["cMenu_Name"]));
                        FindChildMenu(dt, Tools.getGuid(item["uMenu_ID"]), ref sb);
                        sb.Append("</li>");
                    }
                    else
                    {
                        if (List.IndexOf(item) == 0)
                        {
                            if (i == 1)
                                sb.Append("<ul class='nav nav-second-level'>");
                            else
                                sb.Append("<ul class='nav nav-third-level'>");
                        }

                        sb.Append(string.Format("<li uMenu_ParentId='{0}'>", item["uMenu_ParentID"]));
                        string url = Tools.getString(item["cMenu_Url"]);
                        sb.Append(string.Format("<a href='{0}' data-id='{0}' class='J_menuItem'><i class='{2}'></i><span class='nav-label'>{3}</span></a>", url, item["uMenu_ID"], item["cMenu_ICON"], item["cMenu_Name"]));
                        sb.Append("</li>");
                    }
                }
                sb.Append("</ul>");
            }
        }

        #endregion H+  左侧菜单

        #region  系统管理》菜单功能，角色功能  树的json处理

        /// <summary>
        /// 获取菜单和功能树
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetMenuAndFunctionTree()
        {
            var di = new Dictionary<string, object>();
            T_Function tf = new T_Function();
            T_MenuFunction tmf = new T_MenuFunction();
            var tf_list = db.FindToList(tf, " [iFunction_Number] asc");
            var list = new T_MenuDA().GetMenuAndFunctionTree();
            var tmf_list = db.FindToList(tmf);
            for (int i = 0; i < list.Count; i++)
            {
                string url = Tools.getString(list[i]["ur"]);
                string id = Tools.getGuidString(list[i]["id"]);
                if (!string.IsNullOrEmpty(url))
                {
                    tf_list.ForEach(x =>
                    {
                        di = new Dictionary<string, object>();
                        di.Add("name", x.cFunction_Name);
                        di.Add("id", x.uFunction_ID);
                        di.Add("pId", id);
                        di.Add("num", x.iFunction_Number);
                        di.Add("ur", "");
                        di.Add("tag", "fun");
                        //判断该功能是否选中
                        var ischecked = tmf_list.Where(item =>
                            item.uMenuFunction_FunctionID == x.uFunction_ID && item.uMenuFunction_MenuID == Tools.getGuid(id)
                            ).FirstOrDefault();
                        if (ischecked == null)
                            di.Add("checked", false);
                        else
                            di.Add("checked", true);
                        list.Add(di);
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取角色对应的功能树
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetRoleMenuFunctionTree(string roleid)
        {
            var dic = new Dictionary<string, object>();
            T_Function tf = new T_Function();
            T_MenuFunction tmf = new T_MenuFunction();
            T_RoleMenuFunction trmf = new T_RoleMenuFunction();
            var menu_list = db.FindToList<T_Menu>(db.Find(@"select a.uMenu_ID,a.cMenu_Name,a.cMenu_Url,a.cMenu_Icon,a.uMenu_ParentId,a.cMenu_Number  from (select * from T_Menu 
                             where (cMenu_Url is  null or cMenu_Url='') )a
                             join                                    
     (select cMenu_Number,uMenu_ParentId
                     from T_RoleMenuFunction join T_Menu on uMenu_ID=uRoleMenuFunction_MenuID
                group by uRoleMenuFunction_MenuID,uRoleMenuFunction_RoleID,cMenu_Number
                       ) b on instr(b.cMenu_Number,a.cMenu_Number)>0 and b.uMenu_ParentId=a.uMenu_ID
                   union select uMenu_ID,cMenu_Name,cMenu_Url,cMenu_Icon,uMenu_ParentId,cMenu_Number 
                    from T_Menu
               join (select uRoleMenuFunction_MenuID,uRoleMenuFunction_RoleID 
               from T_RoleMenuFunction 
               group by uRoleMenuFunction_MenuID,uRoleMenuFunction_RoleID) a
                on uMenu_ID=a.uRoleMenuFunction_MenuID
                  order by cMenu_Number asc"));//this.GetMenuByRoleID()
            trmf.uRoleMenuFunction_RoleID = Tools.getGuid(roleid);
            var trmf_list = db.FindToList(trmf);//角色菜单功能
            var tf_list = db.FindToList(tf, " [iFunction_Number] asc");//功能
            var tmf_list = db.FindToList(tmf);//菜单功能

            var list = new List<Dictionary<string, object>>();

            var _paret_menu_list = menu_list.FindAll(item => item.uMenu_ParentID == null || item.uMenu_ParentID.Equals(Guid.Empty));

            _paret_menu_list.ForEach(item =>
            {
                var _child_menu_list = menu_list.FindAll(x => x.uMenu_ParentID != null && x.uMenu_ParentID.Equals(item.uMenu_ID));
                //判断是否有子集
                if (_child_menu_list.Count() > 0)
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("name", item.cMenu_Name + "(" + item.cMenu_Number + ")");
                    dic.Add("id", item.uMenu_ID);
                    dic.Add("pId", item.uMenu_ParentID);
                    dic.Add("num", item.cMenu_Number);
                    dic.Add("ur", item.cMenu_Url);
                    dic.Add("tag", null);
                    dic.Add("checked", false);
                    list.Add(dic);
                    this.FindChildMenu(menu_list, trmf_list, tf_list, tmf_list, item, Tools.getGuid(roleid), list);
                }
                else
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("name", item.cMenu_Name + "(" + item.cMenu_Number + ")");
                    dic.Add("id", item.uMenu_ID);
                    dic.Add("pId", item.uMenu_ParentID);
                    dic.Add("num", item.cMenu_Number);
                    dic.Add("ur", item.cMenu_Url);
                    dic.Add("tag", null);
                    dic.Add("checked", false);
                    list.Add(dic);

                    //找出该菜单下的功能和选中的功能
                    tf_list.ForEach(a =>
                    {
                        if (tmf_list.FindAll(val => val.uMenuFunction_FunctionID.Equals(a.uFunction_ID)
                            && val.uMenuFunction_MenuID.Equals(item.uMenu_ID)).Count() > 0)
                        {
                            dic = new Dictionary<string, object>();
                            dic.Add("name", a.cFunction_Name);
                            dic.Add("id", a.uFunction_ID);
                            dic.Add("pId", item.uMenu_ID);
                            dic.Add("num", a.iFunction_Number);
                            dic.Add("ur", null);
                            dic.Add("tag", "fun");
                            //判断该功能是否选中
                            var ischecked = trmf_list.FindAll(x => x.uRoleMenuFunction_FunctionID.Equals(a.uFunction_ID) && x.uRoleMenuFunction_MenuID.Equals(item.uMenu_ID) && x.uRoleMenuFunction_RoleID.Equals(Tools.getGuid(roleid))).FirstOrDefault();
                            if (ischecked == null)
                                dic.Add("checked", false);
                            else
                                dic.Add("checked", true);
                            list.Add(dic);
                        }
                    });
                }
            });
            return list;
        }

        private void FindChildMenu(List<T_Menu> menu_list, List<T_RoleMenuFunction> trmf_list, List<T_Function> tf_list, List<T_MenuFunction> tmf_list, T_Menu menu, Guid roleid, List<Dictionary<string, object>> list)
        {
            var dic = new Dictionary<string, object>();

            var _paret_menu_list = menu_list.FindAll(item => item.uMenu_ParentID != null && item.uMenu_ParentID.Equals(menu.uMenu_ID));

            _paret_menu_list.ForEach(item =>
            {
                var _child_menu_list = menu_list.FindAll(x => x.uMenu_ParentID != null && x.uMenu_ParentID.Equals(item.uMenu_ID));
                //判断是否有子集
                if (_child_menu_list.Count() > 0)
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("name", item.cMenu_Name + "(" + item.cMenu_Number + ")");
                    dic.Add("id", item.uMenu_ID);
                    dic.Add("pId", item.uMenu_ParentID);
                    dic.Add("num", item.cMenu_Number);
                    dic.Add("ur", item.cMenu_Url);
                    dic.Add("tag", null);
                    dic.Add("checked", false);
                    list.Add(dic);
                    this.FindChildMenu(menu_list, trmf_list, tf_list, tmf_list, item, Tools.getGuid(roleid), list);
                }
                else
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("name", item.cMenu_Name + "(" + item.cMenu_Number + ")");
                    dic.Add("id", item.uMenu_ID);
                    dic.Add("pId", item.uMenu_ParentID);
                    dic.Add("num", item.cMenu_Number);
                    dic.Add("ur", item.cMenu_Url);
                    dic.Add("tag", null);
                    dic.Add("checked", false);
                    list.Add(dic);


                    //找出该菜单下的功能和选中的功能
                    tf_list.ForEach(a =>
                    {
                        if (tmf_list.FindAll(val => val.uMenuFunction_FunctionID.Equals(a.uFunction_ID)
                            && val.uMenuFunction_MenuID.Equals(item.uMenu_ID)).Count() > 0)
                        {
                            dic = new Dictionary<string, object>();
                            dic.Add("name", a.cFunction_Name);
                            dic.Add("id", a.uFunction_ID);
                            dic.Add("pId", item.uMenu_ID);
                            dic.Add("num", a.iFunction_Number);
                            dic.Add("ur", null);
                            dic.Add("tag", "fun");
                            //判断该功能是否选中
                            var ischecked = trmf_list.FindAll(x => x.uRoleMenuFunction_FunctionID.Equals(a.uFunction_ID) && x.uRoleMenuFunction_MenuID.Equals(item.uMenu_ID) && x.uRoleMenuFunction_RoleID.Equals(Tools.getGuid(roleid))).FirstOrDefault();
                            if (ischecked == null)
                                dic.Add("checked", false);
                            else
                                dic.Add("checked", true);
                            list.Add(dic);
                        }
                    });
                }
            });
        }

        #endregion 系统管理》菜单功能，角色功能  树的json处理


    }
}
