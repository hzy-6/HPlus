using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;

namespace DbFrame.SQLContext.ExpressionTree
{
    public class Helper
    {
        public Helper()
        {

        }

        /// <summary>
        /// 表达式计算
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string DealExpress(Expression exp)
        {
            if (exp is LambdaExpression)
            {
                LambdaExpression l_exp = exp as LambdaExpression;
                return DealExpress(l_exp.Body);
            }
            if (exp is BinaryExpression)
            {
                return DealBinaryExpression(exp as BinaryExpression);
            }
            if (exp is MemberExpression)
            {
                var val = GetValue((exp as MemberExpression));
                return val == null ? null : val.ToString();//(exp as MemberExpression).Member.Name;
            }
            if (exp is ConstantExpression)
            {
                return DealConstantExpression(exp as ConstantExpression);
            }
            if (exp is UnaryExpression)
            {
                return DealUnaryExpression(exp as UnaryExpression);
            }
            if (exp is NewArrayExpression)
            {
                NewArrayExpression ae = ((NewArrayExpression)exp);
                StringBuilder tmpstr = new StringBuilder();
                foreach (Expression ex in ae.Expressions)
                {
                    tmpstr.Append("'" + DealExpress(ex) + "'");
                    tmpstr.Append(",");
                }
                return tmpstr.ToString(0, tmpstr.Length - 1);
            }
            if (exp is MethodCallExpression)
            {
                var member = exp as MethodCallExpression;
                if (member.Arguments.Count > 0)
                {
                    dynamic name = member.Object;
                    dynamic val = member.Arguments[0];

                    if (member.Method.Name == "StartsWith")
                    {
                        return name.Member.Name + " LIKE '" + Convert.ChangeType(val, val.Type) + "%' ";
                    }
                    else if (member.Method.Name == "Contains")
                    {
                        return name.Member.Name + " LIKE '%" + Convert.ChangeType(val.Value, val.Type) + "%' ";
                    }
                    else if (member.Method.Name == "EndsWith")
                    {
                        return name.Member.Name + " LIKE '%" + Convert.ChangeType(val.Value, val.Type) + "' ";
                    }
                    else if (member.Method.Name == "In")
                    {
                        string str = (member.Arguments[0] as MemberExpression).Member.Name + " IN (" + DealExpress(member.Arguments[1]) + ")";
                        return str;
                    }
                    else if (member.Method.Name == "NotIn")
                    {
                        return (member.Arguments[0] as MemberExpression).Member.Name + "  Not In (" + DealExpress(member.Arguments[1]) + ")";
                    }
                    else
                    {
                        return Eval_1(member).ToString();
                    }
                }
                else
                {
                    return Eval_1(member).ToString();
                }
            }
            return "";
        }

        public static string DealUnaryExpression(UnaryExpression exp)
        {
            return Eval_1(exp).ToString();
        }

        public static string DealBinaryExpression(BinaryExpression exp)
        {
            string left = (exp.Left as MemberExpression).Member.Name;//DealExpress(exp.Left);
            string oper = GetOperStr(exp.NodeType);
            string right = DealExpress(exp.Right);
            if (right == null)
            {
                if (oper == "=")
                    oper = " IS ";
                else
                    oper = " IS NOT ";
            }
            return left + oper + (right == null ? " NULL " : "'" + right + "'");
        }

        public static string DealConstantExpression(ConstantExpression exp)
        {
            object vaule = exp.Value;
            string v_str = string.Empty;
            if (vaule == null)
            {
                return "NULL";
            }
            if (vaule is string)
            {
                v_str = string.Format("{0}", vaule.ToString());
            }
            else if (vaule is DateTime)
            {
                DateTime time = (DateTime)vaule;
                v_str = string.Format("{0}", time.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                v_str = vaule.ToString();
            }
            return v_str;
        }

        public static string GetOperStr(ExpressionType e_type)
        {
            switch (e_type)
            {
                case ExpressionType.OrElse: return " OR ";
                case ExpressionType.Or: return "|";
                case ExpressionType.AndAlso: return " AND ";
                case ExpressionType.And: return "&";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Modulo: return "%";
                case ExpressionType.Equal: return "=";
            }
            return "";
        }

        /// <summary>
        /// 计算右边的值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string Eval(Expression expression)
        {
            UnaryExpression cast = Expression.Convert(expression, typeof(object));
            object obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
            return obj == null ? "NULL" : string.Format("'{0}'", obj.ToString());
        }

        public static object Eval_1(Expression expression)
        {
            UnaryExpression cast = Expression.Convert(expression, typeof(object));
            return Expression.Lambda<Func<object>>(cast).Compile().Invoke();
        }

        ///
        /// <summary> 获取成员表达式中的实际值
        /// </summary>
        private static object GetValue(MemberExpression expr)
        {
            object val;
            var field = expr.Member as System.Reflection.FieldInfo;
            if (field != null)
            {
                val = field.GetValue(((ConstantExpression)expr.Expression).Value);
            }
            else
            {
                val = Eval_1(expr);//((System.Reflection.PropertyInfo)expr.Member).GetValue(((ConstantExpression)expr.Expression).Value, null);
            }
            return val;
        }

    }
}
