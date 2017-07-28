using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;

namespace DBAccess.ExpressionTree
{
    public class ExpressionHelper
    {
        //public static string ExpressionRouter(Expression exp)
        //{
        //    string sb = string.Empty;
        //    if (exp is BinaryExpression)
        //    {
        //        BinaryExpression be = ((BinaryExpression)exp);
        //        return BinarExpressionProvider(be.Left, be.Right, be.NodeType);
        //    }
        //    else if (exp is MemberExpression)
        //    {
        //        MemberExpression me = ((MemberExpression)exp);
        //        return me.Member.Name;
        //    }
        //    else if (exp is NewArrayExpression)
        //    {
        //        NewArrayExpression ae = ((NewArrayExpression)exp);
        //        StringBuilder tmpstr = new StringBuilder();
        //        foreach (Expression ex in ae.Expressions)
        //        {
        //            tmpstr.Append(ExpressionRouter(ex));
        //            tmpstr.Append(",");
        //        }
        //        return tmpstr.ToString(0, tmpstr.Length - 1);
        //    }
        //    else if (exp is MethodCallExpression)
        //    {
        //        var member = exp as MethodCallExpression;
        //        if (member.Arguments.Count > 0)
        //        {
        //            dynamic name = member.Object;
        //            dynamic val = member.Arguments[0];

        //            if (member.Method.Name == "StartsWith")
        //            {
        //                return name.Member.Name + " LIKE '" + Convert.ChangeType(val, val.Type) + "%' ";
        //            }
        //            else if (member.Method.Name == "Contains")
        //            {
        //                return name.Member.Name + " LIKE '%" + Convert.ChangeType(val.Value, val.Type) + "%' ";
        //            }
        //            else if (member.Method.Name == "EndsWith")
        //            {
        //                return name.Member.Name + " LIKE '%" + Convert.ChangeType(val.Value, val.Type) + "' ";
        //            }
        //            else
        //            {
        //                return "'" + val.Value + "' ";
        //            }
        //        }
        //        else
        //        {
        //            UnaryExpression cast = Expression.Convert(member, typeof(object));
        //            object obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
        //            return obj == null ? "" : obj.ToString();
        //        }
        //    }
        //    else if (exp is ConstantExpression)
        //    {
        //        ConstantExpression ce = ((ConstantExpression)exp);
        //        if (ce.Value == null)
        //            return "null";
        //        else if (ce.Value is ValueType)
        //            return ce.Value.ToString();
        //        else if (ce.Value is string || ce.Value is DateTime || ce.Value is char)
        //            return ce.Value.ToString();
        //    }
        //    else if (exp is UnaryExpression)
        //    {
        //        UnaryExpression ue = ((UnaryExpression)exp);
        //        return ExpressionRouter(ue.Operand);
        //    }
        //    return null;
        //}


        //public static string BinarExpressionProvider(Expression left, Expression right, ExpressionType type)
        //{
        //    string sb = "(";
        //    //先处理左边
        //    sb += ExpressionRouter(left);

        //    sb += ExpressionTypeCast(type);

        //    //再处理右边
        //    string tmpStr = ExpressionRouter(right);
        //    if (tmpStr == "null")
        //    {
        //        if (sb.EndsWith(" ="))
        //            sb = sb.Substring(0, sb.Length - 2) + " is null";
        //        else if (sb.EndsWith("<>"))
        //            sb = sb.Substring(0, sb.Length - 2) + " is not null";
        //    }
        //    else
        //        sb += tmpStr;
        //    return sb += ")";
        //}


        //public static string ExpressionTypeCast(ExpressionType type)
        //{
        //    switch (type)
        //    {
        //        case ExpressionType.And:
        //        case ExpressionType.AndAlso:
        //            return " AND ";
        //        case ExpressionType.Equal:
        //            return " =";
        //        case ExpressionType.GreaterThan:
        //            return " >";
        //        case ExpressionType.GreaterThanOrEqual:
        //            return ">=";
        //        case ExpressionType.LessThan:
        //            return "<";
        //        case ExpressionType.LessThanOrEqual:
        //            return "<=";
        //        case ExpressionType.NotEqual:
        //            return "<>";
        //        case ExpressionType.Or:
        //        case ExpressionType.OrElse:
        //            return " Or ";
        //        case ExpressionType.Add:
        //        case ExpressionType.AddChecked:
        //            return "+";
        //        case ExpressionType.Subtract:
        //        case ExpressionType.SubtractChecked:
        //            return "-";
        //        case ExpressionType.Divide:
        //            return "/";
        //        case ExpressionType.Multiply:
        //        case ExpressionType.MultiplyChecked:
        //            return "*";
        //        default:
        //            return null;
        //    }
        //}


        //public static void Main(string[] args)
        //{
        //    Expression<Func<Student, bool>> la = (n => n.id > 1 && n.id < 100 && n.name != "张三" && n.matn >= 60 && n.id != 50 && n.createTime != null);
        //    Console.WriteLine(DealExpress(la));
        //    Console.ReadLine();
        //}
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
                return DealMemberExpression(exp as MemberExpression);
            }
            if (exp is ConstantExpression)
            {
                return DealConstantExpression(exp as ConstantExpression);
            }
            if (exp is UnaryExpression)
            {
                return DealUnaryExpression(exp as UnaryExpression);
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
                    else
                    {
                        UnaryExpression cast = Expression.Convert(member, typeof(object));
                        object obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
                        return obj == null ? "NULL" : string.Format("'{0}'", obj.ToString());
                        //return "'" + val.Value + "' ";
                    }
                }
                else
                {
                    UnaryExpression cast = Expression.Convert(member, typeof(object));
                    object obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
                    return obj == null ? "NULL" : string.Format("'{0}'", obj.ToString());
                }
            }
            return "";
        }

        public static string DealUnaryExpression(UnaryExpression exp)
        {
            var member = exp as UnaryExpression;
            UnaryExpression cast = Expression.Convert(member, typeof(object));
            object obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
            return obj == null ? "NULL" : string.Format("'{0}'", obj.ToString());
            //return DealExpress(exp.Operand);
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
        public static string DealBinaryExpression(BinaryExpression exp)
        {
            string left = DealExpress(exp.Left);
            string oper = GetOperStr(exp.NodeType);
            string right = DealExpress(exp.Right);
            if (right == "NULL")
            {
                if (oper == "=")
                    oper = " IS ";
                else
                    oper = " IS NOT ";
            }
            return left + oper + right;
        }
        public static string DealMemberExpression(MemberExpression exp)
        {
            return exp.Member.Name;
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

        public static object Eval(MemberExpression member)
        {
            UnaryExpression cast = Expression.Convert(member, typeof(object));
            object obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
            return obj;
        }


    }
}
