using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Activation;
using System.Reflection;

namespace DBAccess.Entity
{
    /// <summary>
    /// 属性拦截器
    /// </summary>
    public class AopProxy : RealProxy
    {
        Type ServerType { get; set; }
        public AopProxy(Type serverType)
            : base(serverType)
        {
            ServerType = serverType;
        }

        public override IMessage Invoke(IMessage msg)
        {
            //消息拦截之后，就会执行这里的方法。
            if (msg is IConstructionCallMessage) // 如果是构造函数，按原来的方式返回即可。
            {
                IConstructionCallMessage constructCallMsg = msg as IConstructionCallMessage;
                IConstructionReturnMessage constructionReturnMessage = this.InitializeServerObject((IConstructionCallMessage)msg);
                RealProxy.SetStubData(this, constructionReturnMessage.ReturnValue);
                return constructionReturnMessage;
            }
            else if (msg is IMethodCallMessage) //如果是方法调用（属性也是方法调用的一种）
            {
                var callMsg = msg as IMethodCallMessage;
                var args = callMsg.Args;
                IMessage message;
                try
                {
                    if (callMsg.MethodName.StartsWith("set_") && args.Length == 1)
                    {
                        var method = ServerType.BaseType.GetMethod("Set", BindingFlags.NonPublic | BindingFlags.Instance);
                        method.Invoke(GetUnwrappedServer(), new object[] { callMsg.MethodName.Substring(4), args[0] });//对属性进行调用
                    }
                    object o = callMsg.MethodBase.Invoke(GetUnwrappedServer(), args);
                    message = new ReturnMessage(o, args, args.Length, callMsg.LogicalCallContext, callMsg);
                }
                catch (Exception e)
                {
                    message = new ReturnMessage(e, callMsg);
                }
                return message;
            }
            return msg;
        }



    }
}
