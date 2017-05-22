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
            T_Users user = new T_Users();
            Console.WriteLine(" \r\n :  \r\n ");
            /*   DBContext db = new DBContext();
               List<SQL_Container> li = new List<SQL_Container>();
               for (int i = 0; i < 10000; i++)
               {
                   //    T_Users user = new T_Users();//
                   //    user.cUsers_Email = "1396510655@qq.com";
                   //    user.cUsers_LoginName = "test";
                   //    user.cUsers_LoginPwd = "123456";
                   //    user.cUsers_Name = "haha";
                   //    user.uUsers_ID = Guid.Parse("306de9a2-920f-43a7-aed4-83e6ad7aca61");
                   //    user.dUsers_CreateTime = DateTime.Now;
                   //}
                   T_Users user = new T_Users();
                   user.cUsers_Email = "1396510655@qq.com";
                   user.cUsers_LoginName = "test&" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                   user.cUsers_LoginPwd = "123456";
                   user.cUsers_Name = "haha";
                   //user.AddNotChecks("cUsers_Name");
                   var keyid = db.Add(user);
                   user = new T_Users();
                   user.uUsers_ID = Guid.Parse(keyid.ToString());

                   var model = db.Find(user);


                   //user.uUsers_ID = Guid.Parse("306de9a2-920f-43a7-aed4-83e6ad7aca61");
                   //var di = user.fileds;
                   /*var keyid = db.Add(user);
                   if (keyid == null)
                   {
                       Console.WriteLine(" \r\n  添加失败:" + db.ErrorMessge + " \r\n ");
                   }
                   else
                   {
                       Console.WriteLine(" \r\n  添加成功:" + db.ErrorMessge + " \r\n ");
                   }*/

            /*

            var keyid = db.Add(user, ref li);
            if (keyid != null)
            {
                //if (db.Commit(li))
                //    Console.WriteLine(" \r\n  添加成功:" + db.ErrorMessge + " \r\n ");
                //else
                //    Console.WriteLine(" \r\n  添加失败:" + db.ErrorMessge + " \r\n ");
            }
            //user = new T_Users();
            //user.cUsers_Name = "我添加后被Edit修改了";
            //user.uUsers_ID = Guid.Parse(keyid.ToString());
            //var success = db.Edit(user);
            //if (success)
            //    Console.WriteLine(" \r\n  修改成功:" + db.ErrorMessge + " \r\n ");
            //else
            //    Console.WriteLine(" \r\n  修改失败:" + db.ErrorMessge + " \r\n ");

            user = new T_Users();
            user.cUsers_Name = "我添加后被Edit修改了123";
            user.uUsers_ID = Guid.Parse(keyid.ToString());
            var success = db.Edit(user, ref li);
            if (success)
            {
                if (db.Commit(li))
                    Console.WriteLine(" \r\n  修改成功:" + db.ErrorMessge + " \r\n ");
                else
                    Console.WriteLine(" \r\n  修改失败:" + db.ErrorMessge + " \r\n ");
            }
            else
                Console.WriteLine(" \r\n  修改失败:" + db.ErrorMessge + " \r\n ");

            */

            //var keyid = db.Add(user);
            //user = new T_Users();
            //if (keyid == null)
            //{
            //    Console.WriteLine(" \r\n  删除失败:" + db.ErrorMessge + " \r\n ");
            //}
            //else
            //{
            //    user.uUsers_ID = Guid.Parse(keyid.ToString());
            //    var success = db.Delete(user, ref li);
            //    if (success)
            //    {
            //        if (db.Commit(li))
            //            Console.WriteLine(" \r\n  删除成功:" + db.ErrorMessge + " \r\n ");
            //        else
            //            Console.WriteLine(" \r\n  删除失败:" + db.ErrorMessge + " \r\n ");
            //    }
            //    else
            //        Console.WriteLine(" \r\n  删除失败:" + db.ErrorMessge + " \r\n ");
            //}

            /*
            var model = db.Find(user, " cUsers_Email ");
            Console.WriteLine(" \r\n  查询结果:" + db.ErrorMessge + " \r\n ");
            foreach (DataRow item in model.Rows)
            {
                foreach (var c in model.Columns)
                {
                    Console.WriteLine(" \r\n  " + c + ":" + item[1] + " \r\n ");
                }

            }
            
                

            foreach (var item in model.EH.GetAllPropertyInfo(model))
            {
                Console.WriteLine(" \r\n  " + item.Name + ":" + item.GetValue(model) + " \r\n ");
            }
        } */
            Console.WriteLine(" 耗时：" + s.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
