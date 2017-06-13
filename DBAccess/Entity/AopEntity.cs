using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Runtime.Remoting.Proxies;

namespace DBAccess.Entity
{
    /// <summary>
    /// AOP 拦截器 这里用来拦截实体的属性 Set
    /// </summary>
    public class AopEntity : ProxyAttribute
    {
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            return new AopProxy(serverType, base.CreateInstance(serverType)).GetTransparentProxy() as MarshalByRefObject;
        }
    }
}
