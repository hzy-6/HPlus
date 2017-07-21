using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using WebControl.BaseControl;

namespace WebControl.PageCode
{
    public class PageControl
    {
        /// <summary>
        /// 文本框
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static string Input(Dictionary<string, string> attr)
        {
            return new NoDoubleTag("INPUT", attr).Create().ToHtmlString();
        }

        /// <summary>
        /// textarea
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static string Textarea(Dictionary<string, string> attr)
        {
            return new DoubleTag("TEXTAREA", attr).Create().ToHtmlString();
        }

        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="Options"></param>
        /// <returns></returns>
        public static string Select(Dictionary<string, string> attr, string Options)
        {
            return new DoubleTag("SELECT", new Dictionary<string, string>()
             {
             }).Append(Options).Create().ToHtmlString();
        }

        /// <summary>
        /// 下拉框元素
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string Option(string Text, string Value)
        {
            return new DoubleTag("OPTION", new Dictionary<string, string>()
            {
                {"value",Value}
            }).Append(Text).Create().ToHtmlString();
        }

        /// <summary>
        /// 创建 文本框
        /// </summary>
        /// <param name="Title">控件标题</param>
        /// <param name="Name">name</param>
        /// <param name="Placeholder">placeholder</param>
        /// <param name="col">几列 bootstrap 里面的 col-sm-12  这里填写数字 ：默认3</param>
        /// <returns></returns>
        public static string AddInput(string Title, string Name, string Placeholder, int col = 3)
        {
            var Html = new DoubleTag("DIV", new Dictionary<string, string>() { { "class", "col-sm-" + col } }).Append(
                     new DoubleTag("DIV", new Dictionary<string, string>() { { "class", "form-group" } }).Append(
                         new DoubleTag("LABEL", new Dictionary<string, string>() { }).Append(Title).Create().ToHtmlString() +
                         Input(new Dictionary<string, string>() { 
                        { "class", "form-control input-sm" },
                        { "name", Name}, 
                        { "placeholder", Placeholder } })
                     ).Create().ToHtmlString()
                 );
            return Html.Create().ToString();
        }

        /// <summary>
        /// 创建 Textarea
        /// </summary>
        /// <param name="Title">控件标题</param>
        /// <param name="Name">name</param>
        /// <param name="Placeholder">placeholder</param>
        /// <param name="col">几列 bootstrap 里面的 col-sm-12  这里填写数字 ：默认3</param>
        /// <returns></returns>
        public static string AddTextArea(string Title, string Name, string Placeholder, int col = 3)
        {
            var Html = new DoubleTag("DIV", new Dictionary<string, string>() { { "class", "col-sm-" + col } }).Append(
                     new DoubleTag("DIV", new Dictionary<string, string>() { { "class", "form-group" } }).Append(
                         new DoubleTag("LABEL", new Dictionary<string, string>() { }).Append(Title).Create().ToHtmlString() +
                         Textarea(new Dictionary<string, string>(){
                             { "class", "form-control" },
                             { "name", Name},
                             { "placeholder", Placeholder }
                         })
                     ).Create().ToHtmlString()
                 );
            return Html.Create().ToString();
        }

        /// <summary>
        /// 创建 下拉框
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Name"></param>
        /// <param name="Placeholder"></param>
        /// <param name="Options"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string AddSelect(string Title, string Name, string Placeholder, string Options, int col = 3)
        {
            var Html = new DoubleTag("DIV", new Dictionary<string, string>() { { "class", "col-sm-" + col } }).Append(
                     new DoubleTag("DIV", new Dictionary<string, string>() { { "class", "form-group" } }).Append(
                         new DoubleTag("LABEL", new Dictionary<string, string>() { }).Append(Title).Create().ToHtmlString() +
                         new DoubleTag("SELECT", new Dictionary<string, string>(){
                             { "class", "form-control" },
                             { "name", Name}, 
                         }).Append(Options).Create().ToHtmlString()
                     ).Create().ToHtmlString()
                 );
            return Html.Create().ToString();
        }


    }
}
