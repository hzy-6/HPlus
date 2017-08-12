using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Collections;
using DbFrame;
using DbFrame.Class;
using Utility;
using Model;

namespace DAL
{
    public class T_FunctionDA
    {
        DBContext db = new DBContext();
        T_Function tf = new T_Function();

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
            PagingEntity pe = db.Find(@"select uFunction_ID _ukid, iFunction_Number, cFunction_Name, cFunction_ByName, dFunction_CreateTime from [dbo].[T_Function]
                                                where 1=1 " + where + " order by  iFunction_Number ", pageindex, pagesize);
            return new ToJson().GetPagingEntity(pe, new List<BaseEntity>()
            {
                new T_Function()
            });
        }


    }
}
