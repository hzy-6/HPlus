using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using System.Data;
using DbFrame.SQLContext.ExpressionTree;
using DbFrame.Class;

namespace DbFrame.SQLContext.Context
{
    public abstract class AbstractQuery : IQuery
    {
        public StringBuilder Builder { get; private set; }
        //将别名 和表名存起来 别名是 Key
        public Dictionary<string, string> Alias { get; set; }
        public Dictionary<string, object> SqlParameters { get; set; }
        public AbstractQuery()
        {
            Builder = new StringBuilder();
            Alias = new Dictionary<string, string>();
            SqlParameters = new Dictionary<string, object>();
        }


        /***********多表表查询*************/
        public abstract IQuery Query<T1, T2>(Expression<Func<T1, T2, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        public abstract IQuery Query<T1, T2, T3>(Expression<Func<T1, T2, T3, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity, new();
        public abstract IQuery Query<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity, new();
        public abstract IQuery Query<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity, new();
        public abstract IQuery Query<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity, new();
        public abstract IQuery Query<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity, new();
        public abstract IQuery Query<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity
            where T8 : BaseEntity, new();
        public abstract IQuery Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity
            where T8 : BaseEntity
            where T9 : BaseEntity, new();
        public abstract IQuery Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity
            where T8 : BaseEntity
            where T9 : BaseEntity
            where T10 : BaseEntity, new();

        public abstract IQuery Where<T>(Expression<Func<T, bool>> Where)
            where T : BaseEntity, new();

        /// <summary>
        /// 内连接（INNER JOIN）
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery InnerJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 左连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery LeftJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 左外连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery LeftOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 右连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery RightJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 右外连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery RightOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 全连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery FullJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 全外连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery FullOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 交叉连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="Where"></param>
        /// <returns></returns>
        public abstract IQuery CrossJoin<T1, T2>(Expression<Func<T1, T2, bool>> Where, string Code)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();


        public abstract IQuery OrderBy<T>(Expression<Func<T, object>> OrderBy)
            where T : BaseEntity, new();
        public abstract IQuery OrderByDesc<T>(Expression<Func<T, object>> OrderByDesc)
            where T : BaseEntity, new();

        public abstract IList<KeyValuePair<string, object>> ToList();
        public abstract DataTable ToDataTable();
        public abstract string ToSQL();









        IQuery IQuery.Query<T1, T2>(Expression<Func<T1, T2, object>> Column)
        {
            return Query<T1, T2>(Column);
        }

        IQuery IQuery.Query<T1, T2, T3>(Expression<Func<T1, T2, T3, object>> Column)
        {
            return Query<T1, T2, T3>(Column);
        }

        IQuery IQuery.Query<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object>> Column)
        {
            return Query<T1, T2, T3, T4>(Column);
        }

        IQuery IQuery.Query<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object>> Column)
        {
            return Query<T1, T2, T3, T4, T5>(Column);
        }

        IQuery IQuery.Query<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column)
        {
            return Query<T1, T2, T3, T4, T5, T6>(Column);
        }

        IQuery IQuery.Query<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column)
        {
            return Query<T1, T2, T3, T4, T5, T6, T7>(Column);
        }

        IQuery IQuery.Query<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column)
        {
            return Query<T1, T2, T3, T4, T5, T6, T7, T8>(Column);
        }

        IQuery IQuery.Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column)
        {
            return Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Column);
        }

        IQuery IQuery.Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column)
        {
            return Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Column);
        }

        IQuery IQuery.Where<T>(Expression<Func<T, bool>> Where)
        {
            return Where<T>(Where);
        }

        IQuery IQuery.InnerJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
        {
            return InnerJoin<T1, T2>(ON, Code);
        }

        IQuery IQuery.LeftJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
        {
            return LeftJoin<T1, T2>(ON, Code);
        }

        IQuery IQuery.LeftOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
        {
            return LeftOuterJoin<T1, T2>(ON, Code);
        }

        IQuery IQuery.RightJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
        {
            return RightJoin<T1, T2>(ON, Code);
        }

        IQuery IQuery.RightOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
        {
            return RightOuterJoin<T1, T2>(ON, Code);
        }

        IQuery IQuery.FullJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
        {
            return FullJoin<T1, T2>(ON, Code);
        }

        IQuery IQuery.FullOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code)
        {
            return FullOuterJoin<T1, T2>(ON, Code);
        }

        IQuery IQuery.CrossJoin<T1, T2>(Expression<Func<T1, T2, bool>> Where, string Code)
        {
            return CrossJoin<T1, T2>(Where, Code);
        }

        IQuery IQuery.OrderBy<T>(Expression<Func<T, object>> OrderBy)
        {
            return OrderBy<T>(OrderBy);
        }

        IQuery IQuery.OrderByDesc<T>(Expression<Func<T, object>> OrderByDesc)
        {
            return OrderByDesc<T>(OrderByDesc);
        }

        IList<KeyValuePair<string, object>> IQuery.ToList()
        {
            return ToList();
        }

        System.Data.DataTable IQuery.ToDataTable()
        {
            return ToDataTable();
        }

        string IQuery.ToSQL()
        {
            return ToSQL();
        }

        /// <summary>
        /// 表达式树 条件拼接
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        protected void GetWhereString<M>(Expression<Func<M, bool>> where, ParserArgs pa) where M : BaseEntity, new()
        {
            var body = where.Body;
            pa.Builder.Append(" AND ");
            Parser.Where(body, pa);
        }

        /// <summary>
        /// 开始组装查询语句
        /// </summary>
        /// <param name="Func"></param>
        protected void CodeSelect<BaseEntity>(dynamic Func)
        {
            var Lambda = (Func as LambdaExpression);
            var body = (Lambda.Body as NewExpression);
            if (body == null) throw new Exception("语法错误 这里用过使用 new {  } 匿名实例化语法！");
            var values = body.Arguments;
            var member = body.Members;
            var column = new List<string>();
            foreach (var item in Lambda.Parameters)
            {
                dynamic dy = Activator.CreateInstance(item.Type);
                var TName = dy.EH.GetTabName(item.Type);
                var Displame = item.Name;
                Alias.Add(Displame, TName);
            }
            Builder.Append(" SELECT ");
            var list_member = member.ToList();
            foreach (MemberExpression item in values)
            {
                //检查是否有别名
                var DisplayName = list_member[values.IndexOf(item)].Name;
                if (DisplayName == item.Member.Name)
                    column.Add((item.Expression as ParameterExpression).Name + "." + item.Member.Name);
                else
                    column.Add((item.Expression as ParameterExpression).Name + "." + item.Member.Name + " AS " + DisplayName);
            }
            Builder.Append(string.Join(",", column));
            var ByName = Lambda.Parameters[0].Name;
            var TabName = Alias[ByName] + " " + ByName;
            Builder.Append(" FROM " + TabName);
        }

        /// <summary>
        /// 链接辅助函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JoinStr"></param>
        /// <param name="ON"></param>
        protected void CodeJoin<T1, T2>(string JoinStr, Expression<Func<T1, T2, bool>> ON, string Code)
            where T1 : BaseEntity
            where T2 : BaseEntity
        {
            var Lambda = (ON as LambdaExpression);
            var body = (Lambda.Body as BinaryExpression);
            Builder.Append(" " + JoinStr + " ");
            var ByName = Lambda.Parameters[1].Name;
            var TabName = Alias[ByName] + " " + ByName;
            Builder.Append(" " + TabName + " ON ");
            var Left = (body.Left as MemberExpression);
            var Type = body.NodeType;//ExpressionType.Equal
            if (Type != ExpressionType.Equal) throw new Exception("请使用 == 运算符，如要其他运算 请使用第二个参数 Code ");
            var Right = (body.Right as MemberExpression);
            var on = (Left.Expression as ParameterExpression).Name + "." + Left.Member.Name + "=" + (Right.Expression as ParameterExpression).Name + "." + Right.Member.Name + " " + Code;
            Builder.Append(on);
        }

        /// <summary>
        /// 排序辅助函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TypeStr"></param>
        /// <param name="OrderBy"></param>
        protected void CodeOrderBy<T>(string TypeStr, Expression<Func<T, object>> OrderBy) where T : BaseEntity, new()
        {
            var Lambda = (OrderBy as LambdaExpression);
            var body = (Lambda.Body as NewExpression);
            var values = body.Arguments;
            if (!Builder.ToString().Contains("ORDER BY"))
                Builder.Append(" ORDER BY ");
            var column = new List<string>();
            foreach (MemberExpression item in values)
            {
                var Alias = (item.Expression as ParameterExpression).Name;
                column.Add(Alias + "." + item.Member.Name);
            }
            Builder.Append(string.Join(",", column));
            Builder.Append(" " + TypeStr + " ");
        }






    }
}
