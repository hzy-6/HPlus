using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Word;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using System.Reflection;
//
using Utility;

namespace Utility.OfficeHelper
{
    public class WordToHtml
    {
        /// <summary>  
        /// word转成html  
        /// </summary>  
        /// <param name="wordFileName">绝对地址</param> 
        public static string wordToHtml(object wordFileName)
        {
            try
            {
                //判断文件夹是否存在（不存在创建一个）
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("/WordToHtml")))
                    new DirectoryInfo(HttpContext.Current.Server.MapPath("/WordToHtml")).Create();
                //在此处放置用户代码以初始化页面 
                Microsoft.Office.Interop.Word.ApplicationClass word = new Microsoft.Office.Interop.Word.ApplicationClass();
                Type wordType = word.GetType();
                Microsoft.Office.Interop.Word.Documents docs = word.Documents;
                //打开文件 
                Type docsType = docs.GetType();
                Microsoft.Office.Interop.Word.Document doc = (Microsoft.Office.Interop.Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { wordFileName, true, true });
                //转换格式，另存为 
                Type docType = doc.GetType();
                string wordSaveFileName = wordFileName.ToString();
                string strSaveFileName = "";
                strSaveFileName = wordSaveFileName.Substring(0, wordSaveFileName.Length - 3) + "html";
                object saveFileName = (object)strSaveFileName;
                docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatFilteredHTML });
                docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, null);
                //退出 Word 
                wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);
                return saveFileName.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(Convert.ToString("Word错误:" + ex.Message + "------------------------------------" + ex.StackTrace));
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
