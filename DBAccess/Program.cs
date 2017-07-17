using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DBAccess;
using DBAccess.Entity;
using DBAccess.Model;
using System.Data;

namespace DBAccess
{
    public class Program
    {
        static void Main(string[] args)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            //申明一个操作实体的对象类
            DBContext db = new DBContext();
            var li = new List<SQL_Container>();
            var id = Guid.NewGuid();
            var user = new T_Users();
            user.uUsers_ID = id;
            user.cUsers_Email = "xxxxxx@qq.com";
            user.cUsers_LoginName = "admin_test";
            user.cUsers_LoginPwd = "123";
            user.cUsers_Name = "测试管理员";
            var userid = db.Add(user);
            if (id == Guid.Parse(userid))
            {
                var res = db.Edit<T_Users>(user, item => item.cUsers_Name == "123123");
                if (res)
                {
                    Console.WriteLine("修改成功！");
                }
            }
            //创建一个实体类
            /*       var user = new T_Users();

                   //---------------------------新增
                   user.uUsers_ID = Guid.NewGuid();
                   user.cUsers_Email = "xxxxxx@qq.com";
                   user.cUsers_LoginName = "admin_test";
                   user.cUsers_LoginPwd = "123";
                   user.cUsers_Name = "测试管理员";
                   var userid = db.Add(user);
                   userid = db.Add(user, false);//第二个参数表示不验证实体
                   //新增  配合  提交事务
                   userid = db.Add(user, ref li);
                   userid = db.Add(user, ref li, false);//第三个参数表示不验证实体
                   //---------------------------修改
                   user = new T_Users();
                   user.uUsers_ID = Guid.NewGuid();
                   user.cUsers_Name = "测试管理员_修改过";
                   //这里会自动找到实体中的主键 作为条件修改
                   if (db.Edit(user))
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");

                   if (db.Edit(user, false))
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");

                   if (db.Edit(user, ref li))
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");

                   if (db.Edit(user, new T_Users() { cUsers_Name = "测试管理员_修改过" }))//表示  cUsers_Name=测试管理员_修改过 作为条件进行修改
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");

                   if (db.Edit(user, new T_Users() { cUsers_Name = "测试管理员_修改过" }, false))//表示  cUsers_Name=测试管理员_修改过 作为条件进行修改
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");

                   if (db.Edit(user, new T_Users() { cUsers_Name = "测试管理员_修改过" }, ref li))//表示  cUsers_Name=测试管理员_修改过 作为条件进行修改
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");

                   if (db.Edit(user, new T_Users() { cUsers_Name = "测试管理员_修改过" }, ref li, false))//表示  cUsers_Name=测试管理员_修改过 作为条件进行修改
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");

                   if (db.Edit(user, " and cUsers_Name = '测试管理员_修改过' ", ref li))//表示  cUsers_Name=测试管理员_修改过 作为条件进行修改
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");

                   if (db.Edit(user, " and cUsers_Name = '测试管理员_修改过' "))//表示  cUsers_Name=测试管理员_修改过 作为条件进行修改
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");

                   if (db.Edit(user, " and cUsers_Name = '测试管理员_修改过' ", false))//表示  cUsers_Name=测试管理员_修改过 作为条件进行修改
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");

                   if (db.Edit<T_Users>(user, x => x.cUsers_Name == "测试管理员_修改过"))//表示  cUsers_Name=测试管理员_修改过 作为条件进行修改
                       Console.WriteLine("成功！");
                   else
                       Console.WriteLine("失败！");







                   //提交事务
                   if (db.Commit(li))
                       Console.WriteLine("事务提交成功！");
                   else
                       Console.WriteLine("事务提交失败！");*/

            //1s=1000ms   1ms=0.001m
            Console.WriteLine(" 耗时：" + (s.ElapsedMilliseconds * 0.001) + " s");
            Console.ReadKey();
        }
    }
}
