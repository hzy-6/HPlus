using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebControl.BaseControl
{
    public abstract class AbstractBaseClass : IBaseClass
    {
        protected JavaScriptSerializer jss = new JavaScriptSerializer();
        //public abstract void GetHTML();
    }
}
