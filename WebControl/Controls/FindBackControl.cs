using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.Text;
using System.Web.Mvc;

namespace WebControl.Controls
{
    public class FindBackControl
    {
        /// <summary>
        /// 查找带回控件
        /// </summary>
        /// <param name="Name"> 如 ： cRoles_Name</param>
        /// <param name="ID"> 如 ： uRoles_ID</param>
        /// <param name="BtnFindClick">
        /// Tools.FindBack.OpenFindBackPage('请选择角色','@Url.Action("Index", "Role", new { isUse = 1, fun = "BackFindRole" })')
        /// </param>
        /// <param name="BtnRemoveClick">RemoveFindRole();</param>
        /// <returns></returns>
        public MvcHtmlString Html(string Name, string ID, string BtnFindClick, string BtnRemoveClick)
        {
            /*if (!BtnRemoveClick.Contains("(") && !BtnRemoveClick.Contains(")"))
                BtnRemoveClick += "()";
            StringBuilder sb = new StringBuilder();
            sb.Append("<input type=\"text\" readonly=\"readonly\" class=\"form-control input-sm\" data-bind=\"value:" + Name + "\" style=\"width:74%;\" />");
            sb.Append("<input type=\"text\" name=\"" + ID + "\" class=\"form-control\" data-bind=\"value:" + ID + "\" style=\"display:none\" />");
            sb.Append("<div style=\"position: relative;margin-top: -30px;left: 75%;\">");
            sb.Append("<a href=\"javascript:void(0);\" onclick=\"" + BtnFindClick + "\" class=\"btn btn-outline btn-default btn-sm\" style=\"margin-left: 1px;\"><i class=\"fa fa-search\"></i></a>");
            sb.Append("<a href=\"javascript:void(0);\" onclick=\"" + BtnRemoveClick + "\" class=\"btn btn-outline btn-default btn-sm\" style=\"margin-left: 1px;\"><i class=\"fa fa-close\"></i></a>");
            sb.Append("</div>");*/

            return Html(Name, ID, BtnFindClick, BtnRemoveClick, "");
        }
        public MvcHtmlString Html(string Name, string ID, string BtnFindClick, string BtnRemoveClick, string Group = "")
        {
            if (Group == "")
            {
                Group = Name + "," + ID;
            }
            if (BtnRemoveClick == "")
            {
                BtnRemoveClick = "$.FindBack.Clear()";
            }
            else if (!BtnRemoveClick.Contains("(") && !BtnRemoveClick.Contains(")"))
                BtnRemoveClick += "()";
            StringBuilder sb = new StringBuilder();
            sb.Append("<input type=\"text\" lookGroup=\"" + Group + "\" readonly=\"readonly\" class=\"form-control input-sm\" data-bind=\"value:" + Name + "\" style=\"width:74%;\" />");
            sb.Append("<input type=\"text\" lookGroup=\"" + Group + "\" name=\"" + ID + "\" class=\"form-control\" data-bind=\"value:" + ID + "\" style=\"display:none\" />");
            sb.Append("<div style=\"position: relative;margin-top: -28px;left: 75%;\">");
            sb.Append("<a href=\"javascript:void(0);\" lookGroup=\"" + Group + "\" onclick=\"" + BtnFindClick + "\" class=\"btn btn-outline btn-default btn-sm\" style=\"margin-left: 1px;\"><i lookGroup=\"" + Group + "\" class=\"fa fa-search\"></i></a>");
            sb.Append("<a href=\"javascript:void(0);\" lookGroup=\"" + Group + "\" onclick=\"" + BtnRemoveClick + "\" class=\"btn btn-outline btn-default btn-sm\" style=\"margin-left: 1px;\"><i lookGroup=\"" + Group + "\" class=\"fa fa-close\"></i></a>");
            sb.Append("</div>");
            //sb.Append("<div class=\"row\">");
            //sb.Append("<div class=\"col-xs-9\">");
            //sb.Append("<input type=\"text\" readonly=\"readonly\" class=\"form-control input-sm\" data-bind=\"value:" + Name + "\" />");
            //sb.Append("<input type=\"text\" name=\"" + ID + "\" class=\"form-control\" data-bind=\"value:" + ID + "\" style=\"display:none\" />");
            //sb.Append("</div>");
            //sb.Append("<div class=\"pull-left\">");
            //sb.Append("<a href=\"javascript:void(0);\" onclick=\"" + BtnFindClick + "\" class=\"btn btn-outline btn-default btn-sm\"><i class=\"fa fa-search\"></i></a>&nbsp;");
            //sb.Append("<a href=\"javascript:void(0);\" onclick=\"" + BtnRemoveClick + "\" class=\"btn btn-outline btn-default btn-sm\"><i class=\"fa fa-close\"></i></a>");
            //sb.Append("</div>");
            //sb.Append("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }
        /// <summary>
        /// 查找带回控件
        /// </summary>
        /// <param name="Name"> 如 ： cRoles_Name</param>
        /// <param name="ID"> 如 ： uRoles_ID</param>
        /// <param name="BtnFindClick">
        /// Tools.FindBack.OpenFindBackPage('请选择角色','@Url.Action("Index", "Role", new { isUse = 1, fun = "BackFindRole" })')
        /// </param>
        /// <param name="BtnRemoveClick">RemoveFindRole();</param>
        /// <param name="IsReadonlyInput">是否关闭文本框的输入状态</param>
        /// <returns></returns>
        public MvcHtmlString Html(string Name, string ID, string BtnFindClick, string BtnRemoveClick, bool IsReadonlyInput)
        {
            if (!BtnRemoveClick.Contains("(") && !BtnRemoveClick.Contains(")"))
                BtnRemoveClick += "()";
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"row\">");
            sb.Append("<div class=\"col-xs-9\">");
            if (IsReadonlyInput)
            {
                sb.Append("<input type=\"text\" class=\"form-control input-sm\" data-bind=\"value:" + Name + "\" />");
                //sb.Append("<input type=\"text\" name=\"" + ID + "\" class=\"form-control\" data-bind=\"value:" + ID + "\" disabled=\"disabled\" />");
            }
            else
                sb.Append("<input type=\"text\" readonly=\"readonly\" class=\"form-control input-sm\" data-bind=\"value:" + Name + "\" />");
            sb.Append("<input type=\"text\" name=\"" + ID + "\" class=\"form-control\" data-bind=\"value:" + ID + "\" style=\"display:none\" />");
            sb.Append("</div>");
            //sb.Append("<div class=\"col-xs-4\">");
            sb.Append("<div class=\"pull-left\">");
            sb.Append("<a href=\"javascript:void(0);\" onclick=\"" + BtnFindClick + "\" class=\"btn btn-success btn-sm\"><i class=\"fa fa-search\"></i></a>");
            sb.Append("<a href=\"javascript:void(0);\" onclick=\"" + BtnRemoveClick + "\" class=\"btn btn-danger btn-sm\"><i class=\"fa fa-close\"></i></a>");
            //sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// 查找带回控件
        /// </summary>
        /// <param name="Name"> 如 ： cRoles_Name</param>
        /// <param name="ID"> 如 ： uRoles_ID</param>
        /// <param name="BtnFindClick">
        /// Tools.FindBack.OpenFindBackPage('请选择角色','@Url.Action("Index", "Role", new { isUse = 1, fun = "BackFindRole" })')
        /// </param>
        /// <param name="BtnRemoveClick">RemoveFindRole();</param>
        /// <param name="databind">给打卡窗口按钮绑定自己定义的ko事件</param>
        /// <returns></returns>
        public MvcHtmlString Html1(string Name, string ID, string BtnFindClick, string BtnRemoveClick, string databind)
        {
            if (!BtnRemoveClick.Contains("(") && !BtnRemoveClick.Contains(")"))
                BtnRemoveClick += "()";
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"row\">");
            sb.Append("<div class=\"col-xs-9\">");
            sb.Append("<input type=\"text\" readonly=\"readonly\" class=\"form-control input-sm\" data-bind=\"value:" + Name + "\" />");
            sb.Append("<input type=\"text\" name=\"" + ID + "\" class=\"form-control\" data-bind=\"value:" + ID + "\" style=\"display:none\" />");
            sb.Append("</div>");
            //sb.Append("<div class=\"col-xs-4\">");
            sb.Append("<div class=\"pull-left\">");
            if (string.IsNullOrEmpty(databind))
                sb.Append("<a href=\"javascript:void(0);\" onclick=\"" + BtnFindClick + "\" class=\"btn btn-success btn-sm\"><i class=\"fa fa-search\"></i></a>");
            else
                sb.Append("<a href=\"javascript:void(0);\" data-bind=" + databind + " class=\"btn btn-success btn-sm\"><i class=\"fa fa-search\"></i></a>");
            sb.Append("<a href=\"javascript:void(0);\" onclick=\"" + BtnRemoveClick + "\" class=\"btn btn-danger btn-sm\"><i class=\"fa fa-close\"></i></a>");
            //sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// 查找带回控件
        /// </summary>
        /// <param name="Name"> 如 ： cRoles_Name</param>
        /// <param name="ID"> 如 ： uRoles_ID</param>
        /// <param name="BtnFindClick">
        /// Tools.FindBack.OpenFindBackPage('请选择角色','@Url.Action("Index", "Role", new { isUse = 1, fun = "BackFindRole" })')
        /// </param>
        /// <param name="BtnRemoveClick">RemoveFindRole();</param>
        /// <param name="IsReadonlyInput">是否关闭文本框的输入状态</param>
        /// <param name="databind">给打卡窗口按钮绑定自己定义的ko事件</param>
        /// <returns></returns>
        public MvcHtmlString Html(string Name, string ID, string BtnFindClick, string BtnRemoveClick, bool IsReadonlyInput, string databind)
        {
            if (!BtnRemoveClick.Contains("(") && !BtnRemoveClick.Contains(")"))
                BtnRemoveClick += "()";
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"row\">");
            sb.Append("<div class=\"col-xs-9\">");
            if (IsReadonlyInput)
            {
                sb.Append("<input type=\"text\" class=\"form-control input-sm\" data-bind=\"value:" + Name + "\" />");
                //sb.Append("<input type=\"text\" name=\"" + ID + "\" class=\"form-control\" data-bind=\"value:" + ID + "\" disabled=\"disabled\" />");
            }
            else
                sb.Append("<input type=\"text\" readonly=\"readonly\" class=\"form-control input-sm\" data-bind=\"value:" + Name + "\" />");
            sb.Append("<input type=\"text\" name=\"" + ID + "\" class=\"form-control\" data-bind=\"value:" + ID + "\" style=\"display:none\" />");
            sb.Append("</div>");
            //sb.Append("<div class=\"col-xs-4\">");
            sb.Append("<div class=\"pull-left\">");
            if (string.IsNullOrEmpty(databind))
                sb.Append("<a href=\"javascript:void(0);\" onclick=\"" + BtnFindClick + "\" class=\"btn btn-success btn-sm\"><i class=\"fa fa-search\"></i></a>");
            else
                sb.Append("<a href=\"javascript:void(0);\" data-bind=" + databind + " class=\"btn btn-success btn-sm\"><i class=\"fa fa-search\"></i></a>");
            sb.Append("<a href=\"javascript:void(0);\" onclick=\"" + BtnRemoveClick + "\" class=\"btn btn-danger btn-sm\"><i class=\"fa fa-close\"></i></a>");
            //sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }



        //<div class="row">
        //                            <div class="col-sm-9">
        //                                <input type="text" readonly="readonly" class="form-control" data-bind="value:cRoles_Name" />
        //                                <input type="text" name="uRoles_ID" class="form-control" data-bind="value:uRoles_ID" style="display:none;" />
        //                            </div>
        //                            <div class="col-sm-3">
        //                                <div class="btn-group btn-group-sm pull-left">
        //                                    <a href="javascript:void(0)" onclick="Tools.FindBack.OpenFindBackPage('请选择角色','@Url.Action("Index", "Role", new { isUse = 1, fun = "BackFindRole" })')" class="btn btn-primary"><i class="fa fa-search"></i></a>
        //                                    <a href="javascript:void(0)" onclick="RemoveFindRole()" class="btn btn-danger"><i class="fa fa-close"></i></a>
        //                                </div>
        //                            </div>
        //                        </div>
    }
}