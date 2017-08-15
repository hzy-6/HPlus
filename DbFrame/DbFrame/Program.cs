using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;
using DbFrame.SQLContext;
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

            QueryContext qc = new QueryContext("");

            Stopwatch s = new Stopwatch();
            s.Start();

            for (int i = 0; i < 1000; i++)
            {
                SQLContext.Context.IQuery iquery = db.Find().
                Query<TestT_Users, TestMember>((A, B) => new { _ukid = A.uUsers_ID, B.Member_Name })
                .InnerJoin<TestT_Users, TestMember>((A, B) => A.uUsers_ID == B.Member_ID)
                .Where<TestT_Users>(A => A.uUsers_ID == Guid.Empty);

                Console.WriteLine("SQL:" + iquery.ToSQL() + "\r\n");
            }


            Console.WriteLine(" 耗时：" + (s.ElapsedMilliseconds * 0.001) + " s");
            Console.ReadKey();

        }

    }
}
