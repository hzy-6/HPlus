using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using System.Reflection;

namespace DbFrame.SQLContext.ExpressionTree.Parsers
{

    /*打印
SELECT * FROM [User] u WHERE u.[Name] LIKE 'bl' + '%'
SELECT * FROM [User] u WHERE u.[Name] LIKE '%' + 'bl' + '%'
SELECT * FROM [User] u WHERE u.[Name] LIKE '%' + 'bl'
*/


    /// <summary>
    /// db.Where<User>(u => u.Name.StartsWith("bl"));   //u.Name like 'bl%'
    /// MethodCallExpression 方法调用表达式

    //Method 表示调用的方法　　

    ///Arguments 表示方法中用到的参数

    ///Object 表示调用方法的实例对象

    /// </summary>
    class MethodCallExpressionParser : ExpressionParser<MethodCallExpression>
    {
        public override void Where(MethodCallExpression expr, ParserArgs args)
        {
            Action<MethodCallExpression, ParserArgs> act;

            var key = expr.Method;
            if (key.IsGenericMethod)
            {
                key = key.GetGenericMethodDefinition();
            }

            if (_Methods.TryGetValue(expr.Method, out act))
            {
                act(expr, args);
                return;
            }
            throw new NotImplementedException("无法解释方法" + expr.Method);
        }

        public override void GroupBy(MethodCallExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Having(MethodCallExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Object(MethodCallExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OrderBy(MethodCallExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Select(MethodCallExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }



        static Dictionary<MethodInfo, Action<MethodCallExpression, ParserArgs>> _Methods = MethodDitcInit();

        private static Dictionary<MethodInfo, Action<MethodCallExpression, ParserArgs>> MethodDitcInit()
        {
            Dictionary<MethodInfo, Action<MethodCallExpression, ParserArgs>> dict = new Dictionary<MethodInfo, Action<MethodCallExpression, ParserArgs>>();
            var type = typeof(string);
            foreach (var met in type.GetMethods())
            {
                switch (met.Name)
                {
                    case "StartsWith":
                        dict.Add(met, String_StartsWith);
                        break;
                    case "Contains":
                        dict.Add(met, String_Contains);
                        break;
                    case "EndsWith":
                        dict.Add(met, String_EndsWith);
                        break;
                    default:
                        break;
                }
            }
            type = typeof(Enumerable);
            foreach (var met in type.GetMethods())
            {
                switch (met.Name)
                {
                    case "Contains":
                        dict.Add(met, Enumerable_Contains);
                        break;
                    default:
                        break;
                }
            }
            return dict;
        }


        public static void String_StartsWith(MethodCallExpression expr, ParserArgs args)
        {
            Parser.Where(expr.Object, args);
            args.Builder.Append(" LIKE");
            Parser.Where(expr.Arguments[0], args);
            args.Builder.Append(" + '%'");
        }

        public static void String_Contains(MethodCallExpression expr, ParserArgs args)
        {
            Parser.Where(expr.Object, args);
            args.Builder.Append(" LIKE '%' +");
            Parser.Where(expr.Arguments[0], args);
            args.Builder.Append(" + '%'");
        }

        public static void String_EndsWith(MethodCallExpression expr, ParserArgs args)
        {
            Parser.Where(expr.Object, args);
            args.Builder.Append(" LIKE '%' +");
            Parser.Where(expr.Arguments[0], args);
        }

        /// <summary>
        /// int[] arr = { 13, 15, 17, 19, 21 };
        ///db.Where<User>(u => arr.Contains(u.Age)); 
        /// </summary>
        /// <param name="expr"></param>
        /// <param name="args"></param>
        public static void Enumerable_Contains(MethodCallExpression expr, ParserArgs args)
        {
            Parser.Where(expr.Arguments[1], args);
            args.Builder.Append(" IN");
            Parser.Where(expr.Arguments[0], args);
        }



    }
}
