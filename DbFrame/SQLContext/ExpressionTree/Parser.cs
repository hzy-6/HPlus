using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;

namespace DbFrame.SQLContext.ExpressionTree
{
    public static class Parser
    {

        private static readonly IExpressionParser[] Parsers = InitParsers();

        static IExpressionParser[] InitParsers()
        {
            var codes = Enum.GetValues(typeof(ExpressionTypeCode));
            var parsers = new IExpressionParser[codes.Length];

            foreach (ExpressionTypeCode code in codes)
            {
                if (code.ToString().EndsWith("Expression"))
                {
                    //通过反射 找到该命名空间下的 指定的表达式树种类 并实例化
                    var type = Type.GetType(typeof(Parser).Namespace + ".Parsers." + code.ToString() + "Parser");
                    if (type != null)
                    {
                        parsers[(int)code] = (IExpressionParser)Activator.CreateInstance(type);
                    }
                }
            }
            return parsers;
        }

        /// <summary> 得到表达式类型的枚举对象 </summary>
        /// <param name="expr"> 扩展对象:Expression </param>
        /// <returns> </returns>
        public static ExpressionTypeCode GetCodeType(Expression expr)
        {
            if (expr == null)
            {
                return ExpressionTypeCode.Null;
            }
            if (expr is BinaryExpression)
            {
                return ExpressionTypeCode.BinaryExpression;
            }
            if (expr is BlockExpression)
            {
                return ExpressionTypeCode.BlockExpression;
            }
            if (expr is ConditionalExpression)
            {
                return ExpressionTypeCode.ConditionalExpression;
            }
            if (expr is ConstantExpression)
            {
                return ExpressionTypeCode.ConstantExpression;
            }
            if (expr is DebugInfoExpression)
            {
                return ExpressionTypeCode.DebugInfoExpression;
            }
            if (expr is DefaultExpression)
            {
                return ExpressionTypeCode.DefaultExpression;
            }
            if (expr is DynamicExpression)
            {
                return ExpressionTypeCode.DynamicExpression;
            }
            if (expr is GotoExpression)
            {
                return ExpressionTypeCode.GotoExpression;
            }
            if (expr is IndexExpression)
            {
                return ExpressionTypeCode.IndexExpression;
            }
            if (expr is InvocationExpression)
            {
                return ExpressionTypeCode.InvocationExpression;
            }
            if (expr is LabelExpression)
            {
                return ExpressionTypeCode.LabelExpression;
            }
            if (expr is LambdaExpression)
            {
                return ExpressionTypeCode.LambdaExpression;
            }
            if (expr is ListInitExpression)
            {
                return ExpressionTypeCode.ListInitExpression;
            }
            if (expr is LoopExpression)
            {
                return ExpressionTypeCode.LoopExpression;
            }
            if (expr is MemberExpression)
            {
                return ExpressionTypeCode.MemberExpression;
            }
            if (expr is MemberInitExpression)
            {
                return ExpressionTypeCode.MemberInitExpression;
            }
            if (expr is MethodCallExpression)
            {
                return ExpressionTypeCode.MethodCallExpression;
            }
            if (expr is NewArrayExpression)
            {
                return ExpressionTypeCode.NewArrayExpression;
            }
            if (expr is NewExpression)
            {
                return ExpressionTypeCode.NewArrayExpression;
            }
            if (expr is ParameterExpression)
            {
                return ExpressionTypeCode.ParameterExpression;
            }
            if (expr is RuntimeVariablesExpression)
            {
                return ExpressionTypeCode.RuntimeVariablesExpression;
            }
            if (expr is SwitchExpression)
            {
                return ExpressionTypeCode.SwitchExpression;
            }
            if (expr is TryExpression)
            {
                return ExpressionTypeCode.TryExpression;
            }
            if (expr is TypeBinaryExpression)
            {
                return ExpressionTypeCode.TypeBinaryExpression;
            }
            if (expr is UnaryExpression)
            {
                return ExpressionTypeCode.UnaryExpression;
            }
            return ExpressionTypeCode.Unknown;
        }

        /// <summary> 得到当前表达式对象的解析组件 </summary>
        /// <param name="expr"> 扩展对象:Expression </param>
        /// <returns> </returns>
        public static IExpressionParser GetParser(Expression expr)
        {
            var codetype = GetCodeType(expr);
            var parser = Parsers[(int)codetype];
            if (parser == null)
            {
                switch (codetype)
                {
                    case ExpressionTypeCode.Unknown:
                        throw new ArgumentException("未知的表达式类型", "expr");
                    case ExpressionTypeCode.Null:
                        throw new ArgumentNullException("expr", "表达式为空");
                    default:
                        throw new NotImplementedException("尚未实现" + codetype + "的解析");
                }
            }
            return parser;
        }

        public static void Select(Expression expr, ParserArgs args)
        {
            GetParser(expr).Select(expr, args);
        }

        public static void Where(Expression expr, ParserArgs args)
        {
            GetParser(expr).Where(expr, args);
        }

        public static void GroupBy(Expression expr, ParserArgs args)
        {
            GetParser(expr).GroupBy(expr, args);
        }

        public static void Having(Expression expr, ParserArgs args)
        {
            GetParser(expr).Having(expr, args);
        }

        public static void OrderBy(Expression expr, ParserArgs args)
        {
            GetParser(expr).OrderBy(expr, args);
        }

        public static void Object(Expression expr, ParserArgs args)
        {
            GetParser(expr).Object(expr, args);
        }




    }
}
