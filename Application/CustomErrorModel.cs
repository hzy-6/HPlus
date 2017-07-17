using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Application
{
    public class CustomErrorModel
    {
        public CustomErrorModel()
        {

        }

        /// <summary>
        /// 异常模型
        /// </summary>
        /// <param name="ex">异常</param>
        public CustomErrorModel(Exception ex)
        {
            Success = true;
            ErrorMessage = ex.Message;
            status = "01";
            ErrorData = ex.Data;
            ErrorSource = ex.Source;
            ErrorStackTrace = ex.StackTrace;
            ErrorTargetSite = ex.TargetSite != null ? ex.TargetSite.Name : null;
            this.JumpUrl = string.Empty;
        }

        /// <summary>
        /// 异常模型
        /// </summary>
        /// <param name="ex">异常</param>
        public CustomErrorModel(Exception ex, int errorCode)
        {
            Success = true;
            ErrorMessage = ex.Message;
            status = errorCode.ToString();
            ErrorData = ex.Data;
            ErrorSource = ex.Source;
            ErrorStackTrace = ex.StackTrace;
            ErrorTargetSite = ex.TargetSite != null ? ex.TargetSite.Name : null;
            this.JumpUrl = string.Empty;
        }

        /// <summary>
        /// 异常模型
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="JumpUrl">跳转地址</param>
        public CustomErrorModel(Exception ex, string JumpUrl)
        {
            if (string.IsNullOrEmpty(JumpUrl))
                throw new Exception("缺少JumpUrl参数");
            Success = true;
            ErrorMessage = ex.Message;
            status = "01";
            ErrorData = ex.Data;
            ErrorSource = ex.Source;
            ErrorStackTrace = ex.StackTrace;
            ErrorTargetSite = ex.TargetSite != null ? ex.TargetSite.Name : null;
            this.JumpUrl = JumpUrl;
        }

        /// <summary>
        /// 违禁操作
        /// </summary>
        /// <param name="ex">异常(上下文)</param>
        public CustomErrorModel(string JumpUrl, string errorCode = "02", string errorMessage = "系统监测到违法操作,系统将计时后退出")
        {
            if (string.IsNullOrEmpty(JumpUrl))
                throw new Exception("缺少JumpUrl参数");
            Success = true;
            ErrorMessage = errorMessage;
            status = errorCode;
            ErrorData = "";
            ErrorSource = "";
            ErrorStackTrace = "";
            ErrorTargetSite = "";
            this.JumpUrl = JumpUrl;
        }

        /// <summary>
        /// 异常模型
        /// </summary>
        /// <param name="ex">异常(上下文)</param>
        public CustomErrorModel(ExceptionContext ex)
        {
            Success = true;
            ErrorMessage = ex.Exception.Message;
            status = ex.HttpContext.Response.StatusCode.ToString();
            ErrorData = ex.Exception.Data;
            ErrorSource = ex.Exception.Source;
            ErrorStackTrace = ex.Exception.StackTrace;
            ErrorTargetSite = ex.Exception.TargetSite != null ? ex.Exception.TargetSite.Name : null;
            this.JumpUrl = string.Empty;
        }

        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool Success { set; get; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { set; get; }

        /// <summary>
        /// 错误状态码[01:消息提示][02:登陆超时][03:打开窗口]
        /// </summary>
        public string status { set; get; }

        /// <summary>
        /// 异常信息键值对集合
        /// </summary>
        public object ErrorData { set; get; }

        /// <summary>
        /// 异常对象名或应用程序
        /// </summary>
        public string ErrorSource { set; get; }

        /// <summary>
        /// 堆栈形式
        /// </summary>
        public string ErrorStackTrace { set; get; }

        /// <summary>
        /// 当前引发异常的方法
        /// </summary>
        public string ErrorTargetSite { set; get; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string JumpUrl { set; get; }
    }
}
