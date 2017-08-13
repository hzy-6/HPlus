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


            SQLContext.IQuery query = new SQLContext.Select();
            query.FindList<TestT_Users, TestMember>((user, member) => new { _ukid = user.uUsers_ID, member.Member_Name }).InnerJoin<TestT_Users, TestMember>((user, member) => user.uUsers_ID == member.Member_ID);



            /*
            Expression<Func<TestMember, bool>> expr = u => u.Member_Sex == "男";//实例化表达式树
            DbFrame.SQLContext.ExpressionTree.ParserArgs arg = new DbFrame.SQLContext.ExpressionTree.ParserArgs();//实例化解析用参数
            DbFrame.SQLContext.ExpressionTree.Parser.Where(expr.Body, arg);     //调用解析方法,因为所有的Expression<T>都是继承LamdaExpression,所以解析的时候需要调用.Body获得真正的表达式树对象

            FindList<TestMember, TestMember>((A, B) => new { _ukid = A.Member_ID, A.Member_Name, A.Member_Sex, B.Member_CreateTime });
            */

            Console.WriteLine(" 耗时：" + (s.ElapsedMilliseconds * 0.001) + " s");
            Console.ReadKey();

        }

        public static void FindList<T1, T2>(Expression<Func<T1, T2, object>> Func)
            where T1 : BaseEntity
            where T2 : BaseEntity, new()
        {

        }



    }
}
