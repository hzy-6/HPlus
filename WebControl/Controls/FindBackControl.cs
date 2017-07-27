using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.Text;
using System.Web.Mvc;
using WebControl.PageCode;

namespace WebControl.Controls
{
    public class FindBackControl
    {
        /// <summary>
        /// 查找带回
        /// </summary>
        /// <param name="options"> new { Text="",ID="",FindClick="",RemoveClick="" } </param>
        /// <param name="Readonly">【是否设置 文本框为 非只读】</param>
        /// <param name="KO">是否采用 KO 双向绑定值</param>
        /// <returns></returns>
        public MvcHtmlString Html(object Options, bool Readonly = true, bool KO = true)
        {
            var di = new Dictionary<string, object>();

            Type ty = Options.GetType();

            var fields = ty.GetProperties().ToList();

            foreach (var item in fields)
            {
                di.Add(item.Name, item.GetValue(Options));
            }

            if (!di.ContainsKey("Text"))
                throw new Exception("查找带回控件缺少 Text 属性");
            if (!di.ContainsKey("ID"))
                throw new Exception("查找带回控件缺少 ID 属性");
            if (!di.ContainsKey("FindClick"))
                throw new Exception("查找带回控件缺少 FindClick 属性");
            if (!di.ContainsKey("RemoveClick"))
                throw new Exception("查找带回控件缺少 RemoveClick 属性");
            if (!di.ContainsKey("Placeholder"))
                throw new Exception("查找带回控件缺少 Placeholder 属性");

            /*     var Html = "<div class=\"input-group\">" +
                                 "<input type=\"text\" class=\"form-control\">" +
                                 "<span class=\"input-group-btn\">" +
                                     "<button type=\"button\" class=\"btn btn-outline btn-default\">" +
                                         "<i class=\"fa fa-search\"></i>" +
                                     "</button>" +
                                     "<button type=\"button\" class=\"btn btn-outline btn-default\">" +
                                         "<i class=\"fa fa-close\"></i>" +
                                     "</button>" +
                                 "</span>" +
                             "</div>";
             */

            var input_attr = new Dictionary<string, string>() { 
                {"type","text"},{ "class", "form-control" },{ "name", di["Text"].ToString() },{ "data-bind", "value:"+di["Text"].ToString() }
            };

            //判断是否有 placeholder 属性
            if (di.ContainsKey("Placeholder"))
            {
                input_attr.Add("placeholder", di["Placeholder"].ToString());
            }

            //判断是否要设置 文本框的 readonly 属性
            if (Readonly)
            {
                input_attr.Add("readonly", "readonly");
            }

            //Text 属性文本框
            var input = new NoDoubleTag("input", input_attr).Create().ToHtmlString();
            //ID 属性文本框
            var input_attr_id = new Dictionary<string, string>() { 
                {"type","text"},{ "class", "form-control" },{ "style", "display:none" },{ "name", di["ID"].ToString() },{ "data-bind", "value:"+di["ID"].ToString() }
            };
            var input_id = new NoDoubleTag("input", input_attr_id).Create().ToHtmlString();

            //判断绑定值是否使用 KO 插件
            if (!KO)
            {
                input_attr.Remove("data-bind");
                input_attr.Add("value", "");
                input_attr_id.Remove("data-bind");
                input_attr_id.Add("value", "");
            }

            //打开窗口按钮
            var find = new DoubleTag("button", new Dictionary<string, string>() { 
                {"type","button"},{"class","btn btn-outline btn-default"},{"onclick",di["FindClick"].ToString()}
            }).Append(new DoubleTag("i", new Dictionary<string, string>() { { "class", "fa fa-search" } }).Create().ToHtmlString()).Create().ToHtmlString();
            //清空信息按钮
            var close = new DoubleTag("button", new Dictionary<string, string>() { 
                {"type","button"},{"class","btn btn-outline btn-default"},{"onclick",di["RemoveClick"].ToString()}
            }).Append(new DoubleTag("i", new Dictionary<string, string>() { { "class", "fa fa-close" } }).Create().ToHtmlString()).Create().ToHtmlString();
            //按钮组div
            var span = new DoubleTag("span", new Dictionary<string, string>() { 
                {"class","input-group-btn"}
            }).Append(find + close).Create().ToHtmlString();

            //创建 查找带回Html
            var CreateHtml = new DoubleTag("div", new Dictionary<string, string>(){
                {"class","input-group"},
            }).Append(input + input_id + span).Create();

            return CreateHtml;
        }

    }
}