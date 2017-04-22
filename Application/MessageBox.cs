using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web;
using System.Web.Mvc;

namespace Application
{
    /// <summary>
    /// 配合Web页面使用纯ajax调用函数，后端弹出提示框的 类
    /// </summary>
    [Serializable]
    public class MessageBox : Exception
    {
        /// <summary>
        /// 异常模型
        /// </summary>
        public static CustomErrorModel cem { set; get; }

        /// <summary>
        /// 初始化 WebException 类的新实例。
        /// </summary>
        public MessageBox()
        {
            cem = new CustomErrorModel();
        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 WebException 类的新实例。
        /// </summary>
        /// <param name="Messager">解释异常原因的错误消息。</param>
        public MessageBox(string Messager)
            : base(Messager)
        {
            cem = new CustomErrorModel(this);
        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 WebException 类的新实例。
        /// </summary>
        /// <param name="Messager">解释异常原因的错误消息。</param>
        /// <param name="JumpUrl">跳转地址</param>
        public MessageBox(string Messager, string JumpUrl)
            : base(Messager)
        {
            cem = new CustomErrorModel(this, JumpUrl);
        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 WebException 类的新实例。
        /// </summary>
        /// <param name="Messager">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用</param>
        public MessageBox(string Messager, Exception InnerException)
            : base(Messager, InnerException)
        {
            cem = new CustomErrorModel(this);
        }

    }
}
