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
    public class EditContext
    {
        private string _ConnectionString { get; set; }
        private EditString edit = new EditString();
        private DbHelper dbhelper = null;
        public EditContext(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
            if (edit != null)
                edit = new EditString();
            dbhelper = new DbHelper(ConnectionString);
        }

        private bool ExecuteSQL<T>(ref MemberInitExpression Set, Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            var sql = edit.GetSql<T>(Set, Where);
            if (!dbhelper.Commit(new List<SQL>() { sql }))
                return false;
            return true;
        }

        private bool ExecuteSQL<T>(ref MemberInitExpression Set, Expression<Func<T, bool>> Where, ref List<SQL> li) where T : BaseEntity, new()
        {
            var sql = edit.GetSql<T>(Set, Where);
            li.Add(sql);
            return true;
        }

        public virtual bool Edit<T>(Expression<Func<T>> Func, Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            var Set = Func.Body as MemberInitExpression;
            return ExecuteSQL<T>(ref Set, Where);
        }

        public virtual bool Edit<T>(T Model, Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            var list = new List<MemberBinding>();
            var fileds = Model.EH.GetAllPropertyInfo(Model);
            foreach (var item in fileds)
            {
                //判断如果是 主键 不做为 Set 对象
                if (item.Name == Model.GetKey().FieldName)
                    continue;
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Model), item.PropertyType)));
            }
            var Set = Expression.MemberInit(Expression.New(typeof(T)), list);

            return ExecuteSQL<T>(ref Set, Where);
        }

        public virtual bool Edit<T>(Expression<Func<T>> Func, Expression<Func<T, bool>> Where, ref List<SQL> li) where T : BaseEntity, new()
        {
            var Set = Func.Body as MemberInitExpression;
            return ExecuteSQL<T>(ref Set, Where, ref li);
        }

        public virtual bool Edit<T>(T Model, Expression<Func<T, bool>> Where, ref List<SQL> li) where T : BaseEntity, new()
        {
            var list = new List<MemberBinding>();
            var fileds = Model.EH.GetAllPropertyInfo(Model);
            foreach (var item in fileds)
            {
                //判断如果是 主键 不做为 Set 对象
                if (item.Name == Model.GetKey().FieldName)
                    continue;
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Model), item.PropertyType)));
            }
            var Set = Expression.MemberInit(Expression.New(typeof(T)), list);

            return ExecuteSQL<T>(ref Set, Where, ref li);
        }


    }
}
