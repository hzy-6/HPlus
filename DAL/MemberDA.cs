using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Collections;
using DBAccess;
using DBAccess.Entity;
using Utility;
using Model;

namespace DAL
{
    public class MemberDA
    {
        DBContext db = new DBContext();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="QuickConditions"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public PagingEntity GetDataSource(Hashtable query, int pageindex, int pagesize)
        {
            string where = "";
            var pe = db.Find(@"select * from member", pageindex, pagesize);
            return new ToJson().GetPagingEntity(pe, new List<BaseModel>()
            { new MemberM()
            });
        }


    }
}
