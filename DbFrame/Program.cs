using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DbFrame
{
    class Program
    {
        static void Main(string[] args)
        {
            DBContext db = new DBContext();
            List<SQL> li = new List<SQL>();

            Stopwatch s = new Stopwatch();
            s.Start();

            if (db.Delete<TestMember>(item => item.Member_CreateTime < DateTime.Now))
                Console.WriteLine("删除成功");
            else
                Console.WriteLine("删除失败");

            //1000 ：6 s
            //10000:55.602 s
            /*   for (int i = 0; i < 10000; i++)
               {
                   var id = db.Add<Member>(() => new Member
                   {
                       Member_Name = "我是一个表达树形式哦" + i,
                       Member_Sex = "男"
                   });
                   if (string.IsNullOrEmpty(id)) { Console.WriteLine("ID" + i + ":错误\r\n"); }
                   Console.WriteLine("ID" + i + ":" + id + "\r\n");
               }
               */

            /*   for (int i = 0; i < 1000; i++)
               {
                   var model = new Member();
                   model.Member_Sex = "男";
                   model.Member_Name = "我是一个实体传入操作的，我需要被验证";
                   var id = db.Add(model);
                   if (string.IsNullOrEmpty(id)) { Console.WriteLine("“" + db.ErrorMessge + "”\r\n"); }
                   Console.WriteLine("ID" + i + ":" + id + "\r\n");
               }
               */


            /*    for (int i = 0; i < 1; i++)
                {
                    var model = new Member();
                    //model.Member_ID = Guid.NewGuid();
                    model.Member_Name = "我是被一个实体对象传递进来的" + i;
                    model.Member_Sex = "男";
                    var id = db.Add(model);
                    if (string.IsNullOrEmpty(id)) { Console.WriteLine("ID" + i + ":错误\r\n"); }
                    Console.WriteLine("ID" + i + ":" + id + "\r\n");
                }
                */

            /*    for (int i = 0; i < 10000; i++)
                {
                    var id = db.Add<Member>(item => new Member()
                    {
                        Member_Name = "我是一个表达树形式哦" + i,
                        Member_Sex = "男"
                    }, ref li);
                    if (string.IsNullOrEmpty(id)) { Console.WriteLine("ID" + i + ":错误" + db.ErrorMessge + "\r\n"); }
                    Console.WriteLine("ID" + i + ":" + id + "\r\n");
                }

                if (!db.Commit(li))
                {
                    Console.WriteLine("错误：" + db.ErrorMessge + "\r\n");
                }
                */

            //for (int i = 0; i < 1000; i++)
            //{
            //    db.Add<Member>(() => new Member() { Member_ID = Guid.NewGuid(), Member_Name = "hzy", Member_Sex = "男" });
            //}

            //UserId uid = new UserId();
            //uid.Where(userId => (userId.Id == "8" && userId.LoginCount > 5) || userId.Pws != null || userId.Id.Like("%aa") && userId.LoginCount.In(new int?[] { 4, 6, 8, 9 }) && userId.Id.NotIn(new string[] { "a", "b", "c", "d" }));

            //Member member = new Member();

            ////member.GetTabelName();

            //var list = db.FindToList<Member>(item => item.Member_Sex.In(new string[] { "男" }), "");

            ////获取字段名称
            ////var filedname = member.GetFiledName(m => m.Member_Name);
            //var filedAlias = member.GetAlias(f => f.Member_Name);

            //member.Member_Name.To_String();

            Console.WriteLine(" 耗时：" + (s.ElapsedMilliseconds * 0.001) + " s");
            Console.ReadKey();

        }
    }
}
