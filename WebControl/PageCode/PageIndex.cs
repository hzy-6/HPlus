using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using WebControl.BaseControl;

namespace WebControl.PageCode
{
    public class PageIndex
    {
        /// <summary>
        /// 列表页面 工具栏 对象
        /// </summary>
        private string ToolBar { get; set; }

        /// <summary>
        /// 列表页面 主框架对象
        /// </summary>
        private string Framework { get; set; }

        /// <summary>
        /// 检索栏
        /// </summary>
        private string Search { get; set; }

        /// <summary>
        /// 修改按钮 文本
        /// </summary>
        public string Btn_Update_Text { get; set; }

        /// <summary>
        /// 删除按钮 接口地址
        /// </summary>
        public string Btn_Delete_ApiUrl { get; set; }

        /// <summary>
        /// 导出Excel按钮 接口地址
        /// </summary>
        public string Btn_ExportExcel_ApiUrl { get; set; }

        /// <summary>
        /// 得到 Html 代码
        /// </summary>
        public string GetHtml
        {
            get
            {
                if (string.IsNullOrEmpty(Btn_Delete_ApiUrl))
                {
                    throw new Exception("删除按钮未设置 接口地址！");
                }
                if (string.IsNullOrEmpty(Btn_ExportExcel_ApiUrl))
                {
                    throw new Exception("导出Excel按钮未设置 接口地址！");
                }
                return Framework.Replace("<#=ToolBar=#>", this.ToolBar).Replace("<#=Search=#>", this.Search);
            }
        }

        public PageIndex()
        {
            // <div class="wrapper wrapper-content">
            //    <!--检索区域-->
            //    <div class="ibox" id="Panel_Search" style="display:none;">
            //        <div class="ibox-content">
            //            <div class="row">
            //                <div class="col-sm-12" style="margin-bottom:5px;">

            //                    <div class="form-group">
            //                        <form id="form_search">
            //                            <div class="col-sm-3">
            //                                <div class="form-group">
            //                                    <label>用户名：</label>
            //                                    <input class="form-control input-sm" name="cUsers_Name" placeholder="请输入用户名" />
            //                                </div>
            //                            </div>
            //                            <div class="col-sm-3">
            //                                <div class="form-group">
            //                                    <label>登录名：</label>
            //                                    <input class="form-control input-sm" name="cUsers_LoginName" placeholder="请输入登录名" />
            //                                </div>
            //                            </div>
            //                        </form>
            //                    </div>

            //                </div>
            //                <div class="col-sm-12">
            //                    <button type="button" class="btn btn-info btn-sm pull-right" id="btn_search"><i class="fa fa-search"></i>&nbsp;检索</button>
            //                </div>
            //            </div>
            //        </div>
            //    </div>
            //    <!--列表区域-->
            //    <div class="ibox">
            //        <div class="ibox-content">
            //            <div class="row">
            //                <div class="col-sm-12" style="margin-bottom:5px;">
            //                    <div class="btn-group btn-group-sm">
            //                        @Html.Raw(ViewBag.SysBtn)
            //                        <a class="btn btn-white btn-sm" onclick="$List.ExportExcel('@Url.Action("ExportExcel")')" href="javascript:void(0)" id="Btn_Power_GetExcel"><i class="fa fa-file-excel-o"></i>&nbsp;导出 Excel</a>
            //                    </div>
            //                </div>
            //                <div class="col-sm-12">
            //                    <table id="btable"></table>
            //                </div>
            //            </div>
            //        </div>
            //    </div>

            //</div>
            this.Framework = @"<div class='wrapper wrapper-content'>
                        <!--检索区域-->
                        <div class='ibox' id='Panel_Search' style='display:none;'>
                            <div class='ibox-content'>
                                <div class='row'>
                                    <div class='col-sm-12' style='margin-bottom:5px;'>

                                        <div class='form-group'>
                                            <form id='form_search'>
                                                <#=Search=#>
                                            </form>
                                        </div>

                                    </div>
                                    <div class='col-sm-12'>
                                        <button type='button' class='btn btn-info btn-sm pull-right' id='btn_search'><i class='fa fa-search'></i>&nbsp;检索</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--列表区域-->
                        <div class='ibox'>
                            <div class='ibox-content'>
                                <div class='row'>
                                    <div class='col-sm-12' style='margin-bottom:5px;'>
                                        <div class='btn-group btn-group-sm'>
                                            <#=ToolBar=#>
                                        </div>
                                    </div>
                                    <div class='col-sm-12'>
                                        <table id='btable'></table>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>";
            this.Search = "";
            //<button type="button" class="btn btn-white btn-sm" onclick="window.location=''"><i class="fa fa-refresh"></i>&nbsp;刷新</button>
            //<button type="button" class="btn btn-white btn-sm" onclick="ShowSearch(this)" data-power="Search"><i class="fa fa-chevron-down"></i>&nbsp;检索</button>
            //<button type="button" class="btn btn-white btn-sm" onclick="Func.OpenInfoPage('add')" data-power="Add"><i class="fa fa-plus"></i>&nbsp;添加</button>
            //<button type="button" class="btn btn-white btn-sm" onclick="Func.OpenInfoPage('edit')" data-power="Edit" disabled="disabled"><i class="fa fa-pencil"></i>&nbsp;修改</button>
            //<button type="button" class="btn btn-white btn-sm" onclick="$List.Del({url:'/Admin/User/Del'});" data-power="Del" disabled="disabled"><i class="fa fa-trash"></i>&nbsp;删除</button>
            //<a class="btn btn-white btn-sm" onclick="$List.ExportExcel('/Admin/User/ExportExcel')" href="javascript:void(0)"id="Btn_Power_GetExcel"><i class="fa fa-file-excel-o"></i>&nbsp;导出 Excel</a>
            this.ToolBar += "<button type=\"button\" class=\"btn btn-white btn-sm\" onclick=\"location=location\"><i class=\"fa fa-refresh\"></i>&nbsp;刷新</button>";

            this.ToolBar += "<button type=\"button\" class=\"btn btn-white btn-sm\" onclick=\"ShowSearch(this)\" data-power=\"Search\"><i class=\"fa fa-chevron-down\"></i>&nbsp;检索</button>";

            this.ToolBar += "<button type=\"button\" class=\"btn btn-white btn-sm\" onclick=\"Func.OpenInfoPage('add')\" data-power=\"Add\"><i class=\"fa fa-plus\"></i>&nbsp;添加</button>";

            this.ToolBar += "<button type=\"button\" class=\"btn btn-white btn-sm\" onclick=\"Func.OpenInfoPage('edit')\" data-power=\"Edit\" disabled=\"disabled\"><i class=\"fa fa-pencil\"></i>&nbsp;" + (string.IsNullOrEmpty(this.Btn_Update_Text) ? "修改" : this.Btn_Update_Text) + "</button>";

            this.ToolBar += "<button type=\"button\" class=\"btn btn-white btn-sm\" onclick=\"$List.Del({url:'" + this.Btn_Delete_ApiUrl + "'});\" data-power=\"Del\" disabled=\"disabled\"><i class=\"fa fa-trash\"></i>&nbsp;删除</button>";

            this.ToolBar += "<a class=\"btn btn-white btn-sm\" onclick=\"$List.ExportExcel('" + this.Btn_ExportExcel_ApiUrl + "')\" href=\"javascript:void(0)\"id=\"Btn_Power_GetExcel\"><i class=\"fa fa-file-excel-o\"></i>&nbsp;导出 Excel</a>";
        }

        /// <summary>
        /// 添加工具栏
        /// </summary>
        /// <param name="html"></param>
        public void AddToolBar(string html)
        {
            this.ToolBar += html;
        }

        /// <summary>
        /// 添加检索框
        /// </summary>
        /// <param name="html"></param>
        public void AddSearch(string html)
        {
            this.Search += html;
        }

        /// <summary>
        /// 创建 检索文本框
        /// </summary>
        /// <returns></returns>
        //public string CreateText(string Title, string Name, string Placeholder)
        //{
        //    var Text = new DoubleTag("div", new Dictionary<string, string>() { { "class", "col-sm-3" } }).Append(
        //             new DoubleTag("div", new Dictionary<string, string>() { { "class", "form-group" } }).Append(
        //                 new DoubleTag("label", new Dictionary<string, string>() { }).Append(Title).Create().ToHtmlString() +
        //                 new NoDoubleTag("input", new Dictionary<string, string>() { 
        //                { "class", "form-control input-sm" },
        //                { "name", Name}, 
        //                { "placeholder", Placeholder } }).Create().ToHtmlString()
        //             ).Create().ToHtmlString()
        //         );
        //    return Text.Create().ToString();
        //}

    }
}
