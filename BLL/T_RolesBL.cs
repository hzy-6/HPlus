using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Collections;
using System.Data;
using Utility;
using DBAccess;
using DBAccess.Entity;
using DAL;
using Model;
using Application;

namespace BLL
{
    public class T_RolesBL
    {
        DBContext db = new DBContext();
        List<SQL_Container> li = new List<SQL_Container>();
        T_Roles troles = new T_Roles();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="QuickConditions"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public PagingEntity GetDataSource(Hashtable query, int pageindex, int pagesize)
        {
            return new T_RolesDA().GetDataSource(query, pageindex, pagesize);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SQL_Container> Save(T_Roles model)
        {
            troles = model;
            if (Tools.getGuid(model.uRoles_ID).Equals(Guid.Empty))
            {
                model.uRoles_ID = Tools.getGuid(db.Add(troles, ref li));
                if (model.uRoles_ID.Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                if (!db.Edit(troles, ref li))
                    throw new MessageBox(db.ErrorMessge);
            }
            return li;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<SQL_Container> Delete(string ID)
        {
            db.JsonToList<string>(ID).ForEach(item =>
            {
                troles.uRoles_ID = Tools.getGuid(item);
                if (!db.Delete(troles, ref li))
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
            troles.uRoles_ID = Tools.getGuid(ID);
            troles = db.Find(troles);
            var di = new ToJson().GetDictionary(new Dictionary<string, object>()
            {
                {"troles",troles},
                {"status",1}
            });
            return di;
        }


    }
}
