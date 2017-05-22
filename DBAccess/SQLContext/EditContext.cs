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
    public class EditContext<T> where T : BaseModel, new()
    {
        Context.EditSqlString<T> sqlstring;
        DBHelper dbhelper;
        private EditContext() { }

        private string _ConnectionString { get; set; }

        public EditContext(string ConnectionString, DBType DBType)
        {
            _ConnectionString = ConnectionString;
            dbhelper = new DBHelper(_ConnectionString, DBType);
            sqlstring = new Context.EditSqlString<T>();
        }

        private SQL_Container GetSql(T entity)
        {
            return sqlstring.GetSqlString(entity);
        }

        private SQL_Container GetSql(T entity, string where)
        {
            return sqlstring.GetSqlString(entity, where);
        }

        private SQL_Container GetSql<M>(T entity, M where) where M : BaseModel, new()
        {
            return sqlstring.GetSqlString(entity, where);
        }

        private SQL_Container GetSql<M>(T entity, Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            return sqlstring.GetSqlString(entity, where);
        }




        public bool Edit(T entity)
        {
            var sql = this.GetSql(entity);
            //if (select.ExecuteNonQuery(sql) > 0)
            if (dbhelper.Commit(new List<SQL_Container>() { sql }))
                return true;
            return false;
        }

        public bool Edit(T entity, string where)
        {
            var sql = this.GetSql(entity, where);
            //if (select.ExecuteNonQuery(sql) > 0)
            if (dbhelper.Commit(new List<SQL_Container>() { sql }))
                return true;
            return false;
        }

        public bool Edit(T entity, T where)
        {
            var sql = this.GetSql(entity, where);
            //if (select.ExecuteNonQuery(sql) > 0)
            if (dbhelper.Commit(new List<SQL_Container>() { sql }))
                return true;
            return false;
        }

        public bool Edit<M>(T entity, Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            var sql = sqlstring.GetSqlString(entity, where);
            //if (select.ExecuteNonQuery(sql) > 0)
            if (dbhelper.Commit(new List<SQL_Container>() { sql }))
                return true;
            return false;
        }

        public bool Edit(T entity, ref List<SQL_Container> li)
        {
            var sql = this.GetSql(entity);
            li.Add(sql);
            return true;
        }

        public bool Edit(T entity, string where, ref List<SQL_Container> li)
        {
            var sql = this.GetSql(entity, where);
            li.Add(sql);
            return true;
        }

        public bool Edit(T entity, T where, ref List<SQL_Container> li)
        {
            var sql = this.GetSql(entity, where);
            li.Add(sql);
            return true;
        }

        public bool Edit<M>(T entity, Expression<Func<M, bool>> where, ref List<SQL_Container> li) where M : BaseModel, new()
        {
            var sql = sqlstring.GetSqlString(entity, where);
            li.Add(sql);
            return true;
        }

    }
}
