using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Entity
{
    public class BootStrapTableColModel
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 标题名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool visible { get; set; }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public string align { get; set; }
    }
}
