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
            var Type = fc["ClassType"];
            var Url = (fc["Url"] == null ? Server.MapPath("/Content/CreateFile") : fc["Url"]);
            var Str = fc["Str"];
            var Table = fc["Table"];
            var isall = Tools.getBool(fc["isall"]);
            var template = Server.MapPath("/Content/Template/");

            if (Type == "Model")
            {
                Url = (Url + "\\Model");
                template = template + "Model\\Model.txt";
                Str = string.IsNullOrEmpty(Tools.getString(Str)) ? "M" : Tools.getString(Str);
            }
            else if (Type == "BLL")
            {
                Url = Url + "\\BLL";
                template = template + "Bll\\BLL.txt";
                Str = string.IsNullOrEmpty(Tools.getString(Str)) ? "BL" : Tools.getString(Str);
            }
            else if (Type == "DAL")
            {
                Url = Url + "\\DAL";
                template = template + "DAL\\DAL.txt";
                Str = string.IsNullOrEmpty(Tools.getString(Str)) ? "DA" : Tools.getString(Str);
            }

            if (System.IO.Directory.Exists(Url + "\\"))
            {
                var dir = new System.IO.DirectoryInfo(Url + "\\");
                var fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (var i in fileinfo)
                {
                    if (i is System.IO.DirectoryInfo)            //判断是否文件夹
                    {
                        var subdir = new System.IO.DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        System.IO.File.Delete(i.FullName);      //删除指定文件
                    }
                }
                //System.IO.Directory.Delete(Url + "\\");
            }
            System.IO.Directory.CreateDirectory(Url);

            if (!System.IO.File.Exists(template))
                throw new MessageBox("模板文件不存在");

            var Content = System.IO.File.ReadAllText(template);

            if (isall)
            {
                var list = createcodebl.GetAllTable();
                foreach (var item in list)
                {
                    Table = item["TABLE_NAME"] == null ? "" : item["TABLE_NAME"].ToString();
                    this.CreateFileLogic(Content, Table, Str, Type, Url);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Table))
                    throw new MessageBox("请选择表");
                this.CreateFileLogic(Content, Table, Str, Type, Url);
            }

            return Json(new { status = 1 }, JsonRequestBehavior.DenyGet);
        }

        [NonAction]
        public void CreateFileLogic(string Content, string Table, string Str, string Type, string Url)
        {
            var ClassName = Table + Str;

            Content = Content.Replace("<#ClassName#>", ClassName);
            Content = Content.Replace("<#TableName#>", Table);
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
                        filds += "\t\t[Filed(DisplayName = \"" + colname + "\", IsPrimaryKey = true)]" + "\r\n";
                    }
                    else
                    {
                        filds += "\t\t[Filed(DisplayName = \"" + colname + "\")]" + "\r\n";
                    }
                    filds += "\t\tpublic " + type + " " + colname + "\r\n";
                    filds += "\t\t{\r\n";
                    //filds += "\t\t\tset { SetValue(\"" + colname + "\", value); }\r\n";
                    //filds += "\t\t\tget { return GetValue<" + type + ">(\"" + colname + "\"); }\r\n";
                    filds += "\t\t\tset { SetValue(MethodBase.GetCurrentMethod().Name, value); }\r\n";//MethodBase.GetCurrentMethod().Name
                    filds += "\t\t\tget { return GetValue<" + type + ">(MethodBase.GetCurrentMethod().Name); }\r\n";
                    filds += "\t\t}\r\n\r\n";
                }
                Content = Content.Replace("<#Filds#>", filds);
            }
            System.IO.File.WriteAllText(Url + "\\" + ClassName + ".cs", Content);
        }


    }
}
