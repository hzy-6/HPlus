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
    public class T_UsersBL
    {
        DBContext db = new DBContext();
        List<SQL_Container> li = new List<SQL_Container>();
        T_Users tuser = new T_Users();
        T_Roles troles = new T_Roles();
        T_UsersRoles tuserrole = new T_UsersRoles();


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="QuickConditions"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public PagingEntity GetDataSource(Hashtable query, int pageindex, int pagesize)
        {
            return new T_UsersDA().GetDataSource(query, pageindex, pagesize);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SQL_Container> Save(T_Users model, string uRoles_ID)
        {
            tuser = model;
            if (Tools.getGuid(model.uUsers_ID).Equals(Guid.Empty))
            {
                if (string.IsNullOrEmpty(tuser.cUsers_LoginPwd))
                    tuser.cUsers_LoginPwd = "123456"; //Tools.MD5Encrypt("123456");
                else
                    tuser.cUsers_LoginPwd = model.cUsers_LoginPwd;//Tools.MD5Encrypt(model.cUsers_LoginPwd);
                model.uUsers_ID = Tools.getGuid(db.Add(tuser, ref li));
                if (Tools.getGuid(model.uUsers_ID).Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
                //用户角色
                tuserrole.uUsersRoles_UsersID = tuser.uUsers_ID;
                tuserrole.uUsersRoles_RoleID = Tools.getGuid(uRoles_ID);
                if (Tools.getGuid(db.Add(tuserrole, ref li)).Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                if (!string.IsNullOrEmpty(tuser.cUsers_LoginPwd))
                    tuser.cUsers_LoginPwd = model.cUsers_LoginPwd;//Tools.MD5Encrypt(model.cUsers_LoginPwd);
                else
                    tuser.fileds.Remove("cUsers_LoginPwd");
                if (!db.Edit(tuser, ref li))
                    throw new MessageBox(db.ErrorMessge);
                //用户角色
                tuserrole.uUsersRoles_UsersID = tuser.uUsers_ID;
                if (!db.Delete(tuserrole, ref li))
                    throw new MessageBox(db.ErrorMessge);
                tuserrole = new T_UsersRoles();
                tuserrole.uUsersRoles_UsersID = tuser.uUsers_ID;
                tuserrole.uUsersRoles_RoleID = Tools.getGuid(uRoles_ID);
                if (Tools.getGuid(db.Add(tuserrole, ref li)).Equals(Guid.Empty))
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
                tuserrole = new T_UsersRoles();
                tuserrole.uUsersRoles_UsersID = tuser.uUsers_ID;
                if (!db.Delete(tuserrole, ref li))
                    throw new MessageBox(db.ErrorMessge);
                tuser = new T_Users();
                tuser.uUsers_ID = Tools.getGuid(item);
                if (!db.Delete(tuser, ref li))
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
            tuser = new T_Users();
            tuser.uUsers_ID = ID;
            tuser = db.Find(tuser);
            tuserrole = new T_UsersRoles();
            tuserrole.uUsersRoles_UsersID = tuser.uUsers_ID;
            tuserrole = db.Find(tuserrole);
            troles = new T_Roles();
            troles.uRoles_ID = tuserrole.uUsersRoles_RoleID;
            troles = db.Find(troles);

            tuser.cUsers_LoginPwd = "";
            var di = new ToJson().GetDictionary(new Dictionary<string, object>()
            {
                {"tuser",tuser},
                {"troles",troles},
                {"status",1}
            });
            di["dUsers_CreateTime"] = Tools.getDateTimeString(di["dUsers_CreateTime"], "yyyy-MM-dd");
            return di;
        }

    }
}
