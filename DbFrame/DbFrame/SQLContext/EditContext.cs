using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using DbFrame.SQLContext.ExpressionTree;
using DbFrame.AdoDotNet;
using DbFrame.SQLContext.Context;
using DbFrame.Class;

namespace DbFrame.SQLContext
{
    public class EditContext : AbstractSql
    {
        private string _ConnectionString { get; set; }
        private DbHelper dbhelper = null;
        public EditContext(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
            dbhelper = new DbHelper(ConnectionString);
        }

        private bool ExecuteSQL<T>(ref MemberInitExpression Set, Expression<Func<T, bool>> Where, List<SQL> li = null) where T : BaseEntity, new()
        {
            var sql = this.GetSQL(Set, Where);
            if (li == null)
            {
                if (!dbhelper.Commit(new List<SQL>() { sql }))
                    return false;
            }
            else
            {
                li.Add(sql);
            }
            return true;
        }

        private SQL GetSQL<T>(MemberInitExpression Set, Expression<Func<T, bool>> FuncWhere) where T : BaseEntity, new()
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            string TabName = Model.GetTabelName();
            var SqlPar = new Dictionary<string, object>();
            var set = new List<string>();

            //获取 Where 语句
            var pa = new ParserArgs();
            pa.TabIsAlias = false;
            this.GetWhereString<T>(FuncWhere, pa);
            var Where = pa.Builder.To_String();

            foreach (MemberAssignment item in Set.Bindings)
            {
                //检测有无忽略字段
                if (!string.IsNullOrEmpty(Model.GetNoDbField().Find(f => f == item.Member.Name)))
                    continue;
                var Value = Helper.Eval_1(item.Expression);
                var Name = item.Member.Name;
                set.Add(Name + "=@" + Name + "");
                pa.SqlParameters.Add(Name, Value);
            }

            string SqlStr = string.Format(" UPDATE {0} SET {1} WHERE 1=1 {2} ", TabName, string.Join(",", set), Where);
            return new SQL(SqlStr, pa.SqlParameters);

        }

        public override bool Edit<T>(T Model, Expression<Func<T, bool>> Where)
        {
            var list = new List<MemberBinding>();
            var fileds = Model.EH.GetAllPropertyInfo(Model);
            foreach (var item in fileds)
            {
                //判断如果是 主键 不做为 Set 对象
                if (item.Name == Model.GetKey().FieldName)
                    continue;
                //检测有无忽略字段
                if (!string.IsNullOrEmpty(Model.GetNoDbField().Find(f => f == item.Name)))
                    continue;
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Model), item.PropertyType)));
            }
            var Set = Expression.MemberInit(Expression.New(typeof(T)), list);

            return ExecuteSQL<T>(ref Set, Where);
        }

        public override bool Edit<T>(Expression<Func<T>> Func, Expression<Func<T, bool>> Where)
        {
            var Set = Func.Body as MemberInitExpression;
            return ExecuteSQL<T>(ref Set, Where);
        }

        public override bool Edit<T>(T Model, Expression<Func<T, bool>> Where, List<SQL> li)
        {
            var list = new List<MemberBinding>();
            var fileds = Model.EH.GetAllPropertyInfo(Model);
            foreach (var item in fileds)
            {
                //判断如果是 主键 不做为 Set 对象
                if (item.Name == Model.GetKey().FieldName)
                    continue;
                //检测有无忽略字段
                if (!string.IsNullOrEmpty(Model.GetNoDbField().Find(f => f == item.Name)))
                    continue;
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Model), item.PropertyType)));
            }
            var Set = Expression.MemberInit(Expression.New(typeof(T)), list);

            return ExecuteSQL<T>(ref Set, Where, li);
        }

        public override bool Edit<T>(Expression<Func<T>> Func, Expression<Func<T, bool>> Where, List<SQL> li)
        {
            var Set = Func.Body as MemberInitExpression;
            return ExecuteSQL<T>(ref Set, Where, li);
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

        public override bool Delete<T>(Expression<Func<T, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override bool Delete<T>(Expression<Func<T, bool>> Where, List<SQL> li)
        {
            throw new NotImplementedException();
        }
    }
}
