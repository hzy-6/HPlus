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
    public class T_UsersBL
    {
        DBContext db = new DBContext();
        List<SQL> li = new List<SQL>();
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
        public List<SQL> Save(T_Users model, string uRoles_ID)
        {
            tuser = model;
            if (model.uUsers_ID.To_Guid().Equals(Guid.Empty))
            {
                if (string.IsNullOrEmpty(tuser.cUsers_LoginPwd))
                    tuser.cUsers_LoginPwd = "123456"; //Tools.MD5Encrypt("123456");
                else
                    tuser.cUsers_LoginPwd = model.cUsers_LoginPwd;//Tools.MD5Encrypt(model.cUsers_LoginPwd);
                model.uUsers_ID = db.Add(tuser, ref li).To_Guid();
                if (model.uUsers_ID.To_Guid().Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
                //用户角色
                tuserrole.uUsersRoles_UsersID = tuser.uUsers_ID;
                tuserrole.uUsersRoles_RoleID = Tools.getGuid(uRoles_ID);
                if (db.Add(tuserrole, ref li).To_Guid().Equals(Guid.Empty))
                    throw new MessageBox(db.ErrorMessge);
            }
            else
            {
                //如果 密码字段为空，则设置忽略字段
                if (string.IsNullOrEmpty(tuser.cUsers_LoginPwd))
                    tuser.AddNoDbField(f => new { f.cUsers_LoginPwd });
                if (!db.Edit<T_Users>(tuser, w => w.uUsers_ID == tuser.uUsers_ID, ref li))
                    throw new MessageBox(db.ErrorMessge);

                //用户角色
                if (!db.Delete<T_UsersRoles>(w => w.uUsersRoles_UsersID == tuser.uUsers_ID, ref li))
                    throw new MessageBox(db.ErrorMessge);
                if (db.Add<T_UsersRoles>(() => new T_UsersRoles()
                {
                    uUsersRoles_UsersID = tuser.uUsers_ID,
                    uUsersRoles_RoleID = uRoles_ID.To_Guid()
                }, ref li).To_Guid().Equals(Guid.Empty))
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
                if (!db.Delete<T_UsersRoles>(w => w.uUsersRoles_UsersID == item.To_Guid(), ref li))
                    throw new MessageBox(db.ErrorMessge);
                if (!db.Delete<T_Users>(w => w.uUsers_ID == item.To_Guid(), ref li))
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
            tuser = db.Find<T_Users>(w => w.uUsers_ID == ID);
            tuserrole = db.Find<T_UsersRoles>(w => w.uUsersRoles_UsersID == tuser.uUsers_ID);
            troles = db.Find<T_Roles>(w => w.uRoles_ID == tuserrole.uUsersRoles_RoleID);

            tuser.cUsers_LoginPwd = "";
            var di = new ToJson().GetDictionary(new Dictionary<string, object>()
            {
                {"tuser",tuser},
                {"troles",troles},
                {"status",1}
            });
            di["dUsers_CreateTime"] = di["dUsers_CreateTime"].To_DateTimeString();
            return di;
        }

    }
}
