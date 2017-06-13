using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Entity
{
    public class PagingEntity
    {
        /// <summary>
        /// JqGird  ColModel 页面表头
        /// </summary>
        public List<BootStrapTableColModel> ColModel = new List<BootStrapTableColModel>();

        /// <summary>
        /// DataTable
        /// </summary>
        public DataTable dt = new DataTable();

        /// <summary>
        /// list 列表
        /// </summary>
        public List<Dictionary<string, object>> List { get; set; }

        /// <summary>
        /// 分页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Counts { get; set; }
    }
}
