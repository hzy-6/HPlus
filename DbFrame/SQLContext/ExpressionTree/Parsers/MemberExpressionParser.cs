using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using System.Reflection;
using System.Collections;

namespace DbFrame.SQLContext.ExpressionTree.Parsers
{
    class MemberExpressionParser : ExpressionParser<MemberExpression>
    {

        public override void Where(MemberExpression expr, ParserArgs args)
        {
            if (expr.Expression is ParameterExpression)
            {
                if (args.TabIsAlias)//在字段上是否加上表的别名 
                    Parser.Where(expr.Expression, args);
                args.Builder.Append(expr.Member.Name);
                args.Builder.Append(' ');
            }
            else
            {
                object val = Helper.GetValue(expr);
                args.Builder.Append(' ');
                IEnumerator array = val as IEnumerator;
                if (array != null)
                {
                    AppendArray(args, array);
                }
                else if (val is Array)//else if (val is IEnumerable)
                {
                    AppendArray(args, ((IEnumerable)val).GetEnumerator());
                }
                else
                {
                    args.AddParameter(val);
                }
            }





        }

        public override void GroupBy(MemberExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Having(MemberExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Object(MemberExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OrderBy(MemberExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Select(MemberExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }


        /// <summary> 追加可遍历对象(数组或集合或简单迭代器)
        /// </summary>
        private static void AppendArray(ParserArgs args, IEnumerator array)
        {
            if (array.MoveNext())
            {
                args.Builder.Append('(');
                args.AddParameter(array.Current);
                while (array.MoveNext())
                {
                    args.Builder.Append(',');
                    args.AddParameter(array.Current);
                }
                args.Builder.Append(')');
            }
            else
            {
                args.Builder.Append("NULL");
            }
        }


    }
}
