using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;

namespace DbFrame.SQLContext.ExpressionTree.Parsers
{
    /// <summary>
    /// 其主要属性就是Left Right 和 NodeType

    ///现在需要进行进一步的解析

    ///Left和Right都是表达式树 Expression对象,所以将他们继续交给框架处理

    ///NodeType才是需要立即处理的,由于他是枚举,所以处理起来相对容易
    /// </summary>
    class BinaryExpressionParser : ExpressionParser<BinaryExpression>
    {


        public override void Where(BinaryExpression expr, ParserArgs args)
        {
            if (ExistsBracket(expr.Left))
            {
                args.Builder.Append(' ');
                args.Builder.Append('(');
                Parser.Where(expr.Left, args);
                args.Builder.Append(')');
            }
            else
            {
                Parser.Where(expr.Left, args);
            }
            var index = args.Builder.Length;
            if (ExistsBracket(expr.Right))
            {
                args.Builder.Append(' ');
                args.Builder.Append('(');
                Parser.Where(expr.Right, args);
                args.Builder.Append(')');
            }
            else
            {
                Parser.Where(expr.Right, args);
            }
            var length = args.Builder.Length;
            if (length - index == 5 &&
                 args.Builder[length - 5] == ' ' &&
                 args.Builder[length - 4] == 'N' &&
                 args.Builder[length - 3] == 'U' &&
                 args.Builder[length - 2] == 'L' &&
                 args.Builder[length - 1] == 'L')
            {
                Sign(expr.NodeType, index, args, true);
            }
            else
            {
                Sign(expr.NodeType, index, args);
            }
        }

        public override void Select(BinaryExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void GroupBy(BinaryExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Having(BinaryExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void Object(BinaryExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        public override void OrderBy(BinaryExpression expr, ParserArgs args)
        {
            throw new NotImplementedException();
        }

        /// <summary> 判断是否需要添加括号
        /// </summary>
        private static bool ExistsBracket(Expression expr)
        {
            var s = expr.ToString();
            return s != null && s.Length > 5 && s[0] == '(' && s[1] == '(';
        }

        /// <summary>
        /// 判断树节点的 类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        private static void Sign(ExpressionType type, int index, ParserArgs args, bool useis = false)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    args.Builder.Insert(index, " AND");
                    break;
                case ExpressionType.Equal:
                    if (useis)
                    {
                        args.Builder.Insert(index, " IS");
                    }
                    else
                    {
                        args.Builder.Insert(index, " =");
                    }
                    break;
                case ExpressionType.GreaterThan:
                    args.Builder.Insert(index, " >");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    args.Builder.Insert(index, " >=");
                    break;
                case ExpressionType.NotEqual:
                    if (useis)
                    {
                        args.Builder.Insert(index, " IS NOT");
                    }
                    else
                    {
                        args.Builder.Insert(index, " <>");
                    }
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    args.Builder.Insert(index, " OR");
                    break;
                case ExpressionType.LessThan:
                    args.Builder.Insert(index, " <");
                    break;
                case ExpressionType.LessThanOrEqual:
                    args.Builder.Insert(index, " <=");
                    break;
                default:
                    throw new NotImplementedException("无法解释节点类型" + type);
            }
        }


    }




}
