using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Reflection;

namespace DbFrame.Class
{
    public class BaseEntity
    {
        /// <summary>
        /// 存储不会操作数据的字段
        /// </summary>
        public List<string> NoDbField = new List<string>();

        /// <summary>
        /// 实体反射操作Helper 
        /// </summary>
        public readonly EntityHelper<BaseEntity> EH = new EntityHelper<BaseEntity>();


    }
}
