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
using System.Runtime.Remoting;
using System.Runtime.InteropServices;

namespace DBAccess.Entity
{
    /// <summary>
    /// 属性拦截器
    /// </summary>
    [ComVisible(false)]
    public class AopProxy : RealProxy
    {
        MethodInfo method;
        MarshalByRefObject _target = null;
        public AopProxy(Type serverType, MarshalByRefObject target)
            : base(serverType)
        {
            _target = target;
            method = serverType.BaseType.GetMethod("Set", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override IMessage Invoke(IMessage msg)
        {
            if (msg != null)
            {
                if (msg is IConstructionCallMessage)
                {
                    IConstructionCallMessage constructCallMsg = msg as IConstructionCallMessage;
                    RealProxy defaultProxy = RemotingServices.GetRealProxy(_target);
                    //如果不做下面这一步，_target还是一个没有直正实例化被代理对象的透明代理，
                    //这样的话，会导致没有直正构建对象。
                    defaultProxy.InitializeServerObject(constructCallMsg);
                    //本类是一个RealProxy，它可通过GetTransparentProxy函数得到透明代理
                    return System.Runtime.Remoting.Services.EnterpriseServicesHelper.CreateConstructionReturnMessage(constructCallMsg, (MarshalByRefObject)GetTransparentProxy());
                }
                else if (msg is IMethodCallMessage)
                {
                    IMethodCallMessage callMsg = msg as IMethodCallMessage;
                    object[] args = callMsg.Args;
                    if (callMsg.MethodName.StartsWith("set_") && args.Length == 1)
                    {
                        method.Invoke(_target, new object[] { callMsg.MethodName.Substring(4), args[0] });//对属性进行调用
                    }
                    return RemotingServices.ExecuteMessage(_target, callMsg);
                }
            }
            return msg;
        }
    }
}
