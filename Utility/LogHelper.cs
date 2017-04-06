using System;
using System.IO;
using System.Web;

namespace Utility
{
    /**/
    /// <summary>   
    /// LogHelper的摘要说明。   
    /// </summary>   
    public class LogHelper
    {
        private LogHelper() { }

        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");   //选择<logger name="loginfo">的配置 

        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");   //选择<logger name="logerror">的配置 

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static void WriteLog(string info)
        {
            LogHelper.WriteLog(info, "");
        }

        public static void WriteLog(string info, string filename)
        {
            if (loginfo.IsInfoEnabled)
            {
                //log4net.Core.LogImpl logImpl = loginfo as log4net.Core.LogImpl;
                //if (logImpl != null)
                //{
                //    log4net.Appender.AppenderCollection ac = ((log4net.Repository.Hierarchy.Logger)logImpl.Logger).Appenders;
                //    for (int i = 0; i < ac.Count; i++)
                //    {     // 这里我只对RollingFileAppender类型做修改 
                //        log4net.Appender.RollingFileAppender rfa = ac[i] as log4net.Appender.RollingFileAppender;
                //        if (rfa != null)
                //        {
                //            if (filename == "")
                //            {
                //                rfa.File = HttpContext.Current.Server.MapPath("Logs\\LogInfo\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                //                if (!System.IO.File.Exists(rfa.File))
                //                {
                //                    System.IO.File.Create(rfa.File);
                //                }
                //            }
                //            else
                //            {
                //                rfa.File = HttpContext.Current.Server.MapPath("Logs\\LogInfo\\" + DateTime.Now.ToString("yyyyMMdd"));
                //                if (!System.IO.Directory.Exists(rfa.File))
                //                {
                //                    System.IO.Directory.CreateDirectory(rfa.File);
                //                }
                //                rfa.File += "\\" + filename + ".txt";
                //                if (!System.IO.File.Exists(rfa.File))
                //                {
                //                    System.IO.File.Create(rfa.File);
                //                }
                //            }
                //            rfa.ActivateOptions();
                //        }
                //    }
                //}

                loginfo.Info(info);
            }
        }

        public static void WriteLog(string info, Exception se)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }
    }
}