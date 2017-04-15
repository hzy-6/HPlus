using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Aop;
using Application;
using DBAccess;
using DBAccess.Entity;
using Utility;
using BLL;
using Model;
using System.Collections;

namespace HPlus.Areas.SysManage.Controllers.Sys
{
    public class CreateCodeController : BaseController
    {
        // 根据表 生成 代码
        // GET: /SysManage/CreateCode/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.MenuID = "Z-160";
        }
        T_CreateCodeBL createcodebl = new T_CreateCodeBL();

        /// <summary>
        /// 获取数据库中所有的表和字段
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDatabaseAllTable()
        {
            return Json(new { status = 1, value = createcodebl.GetDatabaseAllTable() }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(FormCollection fc)
        {
            var Type = fc["Type"];
            var Url = (fc["Url"] == null ? Server.MapPath("/Content/CreateFile/") : fc["Url"]);
            var Str = fc["Str"];
            var Table = fc["Table"];
            var isall = Tools.getBool(fc["isall"]);
            var template = Server.MapPath("/Content/Template/");

            if (Type == "Model")
            {
                Url = Url + "\\Model";
                template = template + "\\Model\\Model.txt";
                Str = (Str == null ? "M" : Str);
            }
            else if (Type == "BLL")
            {
                Url = Url + "\\BLL";
                template = template + "\\Bll\\BLL.txt";
                Str = (Str == null ? "BL" : Str);
            }
            else if (Type == "DAL")
            {
                Url = Url + "\\DAL";
                template = template + "\\DAL\\DAL.txt";
                Str = (Str == null ? "DA" : Str);
            }

            if (System.IO.Directory.Exists(Url))
            {
                System.IO.Directory.Delete(Url);
            }
            else
            {
                System.IO.Directory.CreateDirectory(Url);
            }

            if (System.IO.File.Exists(template))
                throw new MessageBox("模板文件不存在");

            var Content = System.IO.File.ReadAllText(template);

            if (isall)
            {
                var list = createcodebl.GetDatabaseAllTable();
                list = list.FindAll(item => item["pId"] == null);
                foreach (var item in list)
                {
                    Table = item["name"] == null ? "" : item["name"].ToString();
                    this.CreateFileLogic(Content, Table, Str, Type);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Table))
                    throw new MessageBox("请选择表");
                this.CreateFileLogic(Content, Table, Str, Type);
            }

            return Json(new { status = 1 }, JsonRequestBehavior.DenyGet);
        }

        [NonAction]
        public void CreateFileLogic(string Content, string Table, string Str, string Type)
        {
            var ClassName = Table + Str;

            Content.Replace("<#ClassName#>", ClassName);
            Content.Replace("<#TableName#>", Table);
            string filds = string.Empty;

            if (Type == "Model")
            {
                var col = createcodebl.GetColByTable(Table);
                foreach (var item in col)
                {
                    var key = item["iskey"] == null ? "" : item["iskey"].ToString();
                    var colname = item["colname"] == null ? "" : item["colname"].ToString();
                    var type = item["type"] == null ? "" : item["type"].ToString();

                    switch (type)
                    {
                        case "uniqueidentifier":
                            type = "Guid?";
                            break;
                        case "bit":
                        case "int":
                            type = "int?";
                            break;
                        case "datetime":
                            type = "DateTime?";
                            break;
                        case "float":
                            type = "float?";
                            break;
                        case "money":
                            type = "double?";
                            break;
                        case "decimal":
                            type = "decimal?";
                            break;
                        default:
                            type = "string";
                            break;
                    }

                    if (!string.IsNullOrEmpty(key))
                    {
                        filds += "[Filed(DisplayName = \"" + colname + "\", IsPrimaryKey = true)]";
                        filds += "public " + type + " " + colname + " { get; set; }";
                    }
                    else
                    {
                        filds += "[Filed(DisplayName = \"" + colname + "\")]";
                        filds += "public " + type + " " + colname + " { get; set; }";
                    }
                }
                Content.Replace("<#Filds#>", filds);
            }
            System.IO.File.WriteAllText(Url + "\\" + ClassName + ".cs", Content);
        }


    }
}
