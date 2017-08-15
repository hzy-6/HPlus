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
    public abstract class AbstractSql : ISql
    {

        public AbstractSql()
        {

        }

        public abstract object Add<T>(T Model) where T : BaseEntity, new();
        public abstract object Add<T>(Expression<Func<T>> Func) where T : BaseEntity, new();
        public abstract object Add<T>(T Model, List<SQL> li) where T : BaseEntity, new();
        public abstract object Add<T>(Expression<Func<T>> Func, List<SQL> li) where T : BaseEntity, new();
        public abstract bool Edit<T>(T Set, Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        public abstract bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        public abstract bool Edit<T>(T Set, Expression<Func<T, bool>> Where, List<SQL> li) where T : BaseEntity, new();
        public abstract bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where, List<SQL> li) where T : BaseEntity, new();
        public abstract bool Delete<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        public abstract bool Delete<T>(Expression<Func<T, bool>> Where, List<SQL> li) where T : BaseEntity, new();


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



        object ISql.Add<T>(T Model)
        {
            return Add<T>(Model);
        }

        object ISql.Add<T>(Expression<Func<T>> Func)
        {
            return Add<T>(Func);
        }

        object ISql.Add<T>(T Model, List<Class.SQL> li)
        {
            return Add<T>(Model, li);
        }

        object ISql.Add<T>(Expression<Func<T>> Func, List<Class.SQL> li)
        {
            return Add<T>(Func, li);
        }

        bool ISql.Edit<T>(T Set, Expression<Func<T, bool>> Where)
        {
            return Edit<T>(Set, Where);
        }

        bool ISql.Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where)
        {
            return Edit<T>(Set, Where);
        }

        bool ISql.Edit<T>(T Set, Expression<Func<T, bool>> Where, List<Class.SQL> li)
        {
            return Edit<T>(Set, Where, li);
        }

        bool ISql.Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where, List<Class.SQL> li)
        {
            return Edit<T>(Set, Where, li);
        }

        bool ISql.Delete<T>(Expression<Func<T, bool>> Where)
        {
            return Delete<T>(Where);
        }

        bool ISql.Delete<T>(Expression<Func<T, bool>> Where, List<Class.SQL> li)
        {
            return Delete<T>(Where, li);
        }









    }
}
