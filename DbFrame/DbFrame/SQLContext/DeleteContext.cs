using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using DbFrame.SQLContext.ExpressionTree;
using DbFrame.SQLContext.Context;
using DbFrame.Class;
using DbFrame.AdoDotNet;

namespace DbFrame.SQLContext
{
    public class DeleteContext : AbstractSql
    {
        private string _ConnectionString { get; set; }
        private DbHelper dbhelper = null;
        public DeleteContext(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
            dbhelper = new DbHelper(ConnectionString);
        }


        public SQL GetSql<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            string TabName = Model.GetTabelName();
            var pa = new ParserArgs();
            pa.TabIsAlias = false;
            this.GetWhereString<T>(Where, pa);
            return new SQL(string.Format(" DELETE FROM {0} WHERE 1=1 {1} ", TabName, pa.Builder.To_String()), pa.SqlParameters);
        }

        private bool ExecuteSQL<T>(Expression<Func<T, bool>> Where, List<SQL> li = null) where T : BaseEntity, new()
        {
            var sql = this.GetSql<T>(Where);
            if (li == null)
            {
                if (!dbhelper.Commit(new List<SQL>() { sql }))
                    return false;
            }
            else
                li.Add(sql);
            return true;
        }

        public override bool Delete<T>(Expression<Func<T, bool>> Where)
        {
            return ExecuteSQL<T>(Where);
        }

        public override bool Delete<T>(Expression<Func<T, bool>> Where, List<SQL> li)
        {
            return ExecuteSQL<T>(Where, li);
        }







        public override object Add<T>(T Model)
        {
            throw new NotImplementedException();
        }

        public override object Add<T>(Expression<Func<T>> Func)
        {
            throw new NotImplementedException();
        }

        public override object Add<T>(T Model, List<SQL> li)
        {
            throw new NotImplementedException();
        }

        public override object Add<T>(Expression<Func<T>> Func, List<SQL> li)
        {
            throw new NotImplementedException();
        }

        public override bool Edit<T>(T Set, Expression<Func<T, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override bool Edit<T>(T Set, Expression<Func<T, bool>> Where, List<SQL> li)
        {
            throw new NotImplementedException();
        }

        public override bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where, List<SQL> li)
        {
            throw new NotImplementedException();
        }


    }
}
