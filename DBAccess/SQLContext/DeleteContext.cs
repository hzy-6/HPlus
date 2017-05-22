using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using DBAccess.Reflection;
using DBAccess.Entity;
using DBAccess.AdoDotNet;
using System.Dynamic;

namespace DBAccess.SQLContext
{
    public class DeleteContext<T> where T : BaseModel, new()
    {
        Context.DeleteSqlString<T> sqlstring;
        DBHelper dbhelper;
        private DeleteContext() { }

        private string _ConnectionString { get; set; }

        public DeleteContext(string ConnectionString, DBType DBType)
        {
            _ConnectionString = ConnectionString;
            dbhelper = new DBHelper(_ConnectionString, DBType);
            sqlstring = new Context.DeleteSqlString<T>();
        }

        private SQL_Container GetSql(T entity)
        {
            return sqlstring.GetSqlString(entity);
        }

        private SQL_Container GetSql<M>(string where) where M : BaseModel, new()
        {
            return sqlstring.GetSqlString<M>(where);
        }

        private SQL_Container GetSql<M>(Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            return sqlstring.GetSqlString<M>(where);
        }

        public bool Delete(T entity)
        {
            var sql = this.GetSql(entity);
            if (dbhelper.Commit(new List<SQL_Container>() { sql }))
                return true;
            else
                return false;
        }

        public bool Delete<M>(string where) where M : BaseModel, new()
        {
            var sql = this.GetSql<M>(where);
            if (dbhelper.Commit(new List<SQL_Container>() { sql }))
                return true;
            else
                return false;
        }


        public bool Delete<M>(Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            var sql = sqlstring.GetSqlString(where);
            if (dbhelper.Commit(new List<SQL_Container>() { sql }))
                return true;
            else
                return false;
        }

        public bool Delete(T entity, ref List<SQL_Container> li)
        {
            var sql = this.GetSql(entity);
            li.Add(sql);
            return true;
        }

        public bool Delete<M>(string where, ref List<SQL_Container> li) where M : BaseModel, new()
        {
            var sql = this.GetSql<M>(where);
            li.Add(sql);
            return true;
        }

        public bool Delete<M>(Expression<Func<M, bool>> where, ref List<SQL_Container> li) where M : BaseModel, new()
        {
            var sql = sqlstring.GetSqlString(where);
            li.Add(sql);
            return true;
        }

    }
}
