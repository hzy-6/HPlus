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
    public class DeleteContext
    {
        private string _ConnectionString { get; set; }
        private DeleteString delete = new DeleteString();
        private DbHelper dbhelper = null;
        public DeleteContext(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
            if (delete != null)
                delete = new DeleteString();
            dbhelper = new DbHelper(ConnectionString);
        }

        private bool ExecuteSQL<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            var sql = delete.GetSql<T>(Where);
            if (!dbhelper.Commit(new List<SQL>() { sql }))
                return false;
            return true;
        }

        private bool ExecuteSQL<T>(Expression<Func<T, bool>> Where, ref List<SQL> li) where T : BaseEntity, new()
        {
            var sql = delete.GetSql<T>(Where);
            li.Add(sql);
            return true;
        }

        public virtual bool Delete<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            return ExecuteSQL<T>(Where);
        }

        public virtual bool Delete<T>(Expression<Func<T, bool>> Where, ref List<SQL> li) where T : BaseEntity, new()
        {
            return ExecuteSQL<T>(Where, ref li);
        }


    }
}
