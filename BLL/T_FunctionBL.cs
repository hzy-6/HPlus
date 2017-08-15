using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Collections;
using System.Data;
using Utility;
using DbFrame;
using DbFrame.Class;
using DAL;
using Model;
using Application;

namespace BLL
{
    public class T_FunctionBL
    {
        DBContext db = new DBContext();
        List<SQL> li = new List<SQL>();
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
            return new T_FunctionDA().GetDataSource(query, pageindex, pagesize);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SQL> Save(T_Function model)
        {
            tf = model;
            if (Tools.getGuid(model.uFunction_ID).Equals(Guid.Empty))
            {
                model.uFunction_ID = Tools.getGuid(db.Add(tf, li));
                if (Tools.getGuid(model.uFunction_ID).Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                if (!db.Edit<T_Function>(tf, w => w.uFunction_ID == tf.uFunction_ID, li))
                    throw new MessageBox(db.ErrorMessge);
            }
            return li;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<SQL> Delete(string ID)
        {
            db.JsonToList<string>(ID).ForEach(item =>
            {
                if (!db.Delete<T_Function>(w => w.uFunction_ID == item.To_Guid(), li))
                    throw new MessageBox(db.ErrorMessge);
            });
            return li;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Dictionary<string, object> Find(Guid ID)
        {
            tf = db.Find<T_Function>(w => w.uFunction_ID == ID.To_Guid());
            var di = new ToJson().GetDictionary(new Dictionary<string, object>()
            {
                {"tf",tf},
                {"status",1}
            });
            return di;
        }


    }
}
