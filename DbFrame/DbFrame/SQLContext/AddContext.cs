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
    public class AddContext : AbstractSql
    {

        private string _ConnectionString { get; set; }
        private DbHelper dbhelper = null;
        public AddContext(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
            dbhelper = new DbHelper(ConnectionString);
        }

        /// <summary>
        /// 创建主键
        /// </summary>
        /// <returns></returns>
        private string CreatePrimaryKey<T>(ref MemberInitExpression body, string FiledName, Type type, bool IsIdentity) where T : BaseEntity, new()
        {
            var id = string.Empty;

            if (type == typeof(Guid?))
            {
                var list = new List<MemberBinding>();
                var Model = (T)Activator.CreateInstance(typeof(T));
                //检测 用户是否自己设置了主键
                var member = (body.Bindings.Where(item => item.Member.Name == FiledName).FirstOrDefault() as MemberAssignment);
                if (member == null)
                {
                    id = Guid.NewGuid().ToString();
                    var memberinfo = Model.GetType().GetProperty(FiledName);
                    list.Add(Expression.Bind(memberinfo, Expression.Constant(Guid.Parse(id), typeof(Guid?))));
                }
                else
                {
                    if (Helper.Eval_1(member.Expression) == null)
                        id = Guid.NewGuid().ToString();
                    else
                        id = Helper.Eval_1(member.Expression).ToString();
                }

                foreach (MemberAssignment item in body.Bindings)
                {
                    if (item.Member.Name == FiledName)
                        list.Add(Expression.Bind(item.Member, Expression.Constant(Guid.Parse(id), item.Expression.Type)));
                    else
                        list.Add(Expression.Bind(item.Member, Expression.Constant(Helper.Eval_1(item.Expression), item.Expression.Type)));
                }

                body = Expression.MemberInit(Expression.New(typeof(T)), list);
                return id.ToString();
            }
            else if (type == typeof(int?) && IsIdentity)
            {
                return @" /*请将这句代码 传递给 引用他的外键*/
                 SELECT SCOPE_IDENTITY()  ";
            }
            else
            {
                return Helper.Eval_1(((body.Bindings.Where(item => item.Member.Name == FiledName).FirstOrDefault()) as MemberAssignment).Expression).To_String();
            }
        }

        private string ExecuteSQL<T>(ref MemberInitExpression body, T Model, List<SQL> li = null) where T : BaseEntity, new()
        {
            var ID = this.CreatePrimaryKey<T>(ref body, Model.GetKey().FieldName, Model.GetKey().FieldType, Model.GetKey().IsIdentity);
            var Sql = this.GetSQL<T>(body);

            if (ID.Contains("SELECT SCOPE_IDENTITY()"))
            {
                var obj = dbhelper.ExecuteScalar(ID);
                ID = obj != null ? obj.ToString() : string.Empty;
            };

            if (li == null)
            {
                if (!dbhelper.Commit(new List<SQL>() { Sql }))
                {
                    return string.Empty;
                }
            }
            else
            {
                li.Add(Sql);
            }

            //if (dbhelper.ExecuteNonQuery(Sql) == 0)
            //{
            //    return string.Empty;
            //}

            return ID;
        }


        private SQL GetSQL<T>(MemberInitExpression Body) where T : BaseEntity, new()
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            string TabName = Model.GetTabelName();
            var SqlPar = new Dictionary<string, object>();
            var col = new List<string>();
            var val = new List<string>();
            foreach (MemberAssignment item in Body.Bindings)
            {
                //检测有无忽略字段
                if (!string.IsNullOrEmpty(Model.GetNoDbField().Find(f => f == item.Member.Name)))
                    continue;
                var Value = Helper.Eval_1(item.Expression);
                var Name = item.Member.Name;
                var len = SqlPar.Count;
                col.Add(Name); val.Add("@" + Name + len + "");
                SqlPar.Add(Name + len, Value);
            }
            return new SQL(string.Format(" INSERT INTO {0} ({1}) VALUES ({2}) ", TabName, string.Join(",", col), string.Join(",", val)), SqlPar);

        }

        public override object Add<T>(T Model)
        {
            var list = new List<MemberBinding>();
            var fileds = Model.EH.GetAllPropertyInfo(Model);
            foreach (var item in fileds)
            {
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Model), item.PropertyType)));
            }
            var body = Expression.MemberInit(Expression.New(typeof(T)), list);

            return this.ExecuteSQL(ref body, Model);
        }

        public override object Add<T>(Expression<Func<T>> Func)
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            var body = Func.Body as MemberInitExpression;

            return this.ExecuteSQL(ref body, Model);
        }

        public override object Add<T>(T Model, List<SQL> li)
        {
            var list = new List<MemberBinding>();
            var fileds = Model.EH.GetAllPropertyInfo(Model);
            foreach (var item in fileds)
            {
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Model), item.PropertyType)));
            }
            var body = Expression.MemberInit(Expression.New(typeof(T)), list);

            return this.ExecuteSQL(ref body, Model, li);
        }

        public override object Add<T>(Expression<Func<T>> Func, List<SQL> li)
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            var body = Func.Body as MemberInitExpression;

            return this.ExecuteSQL(ref body, Model, li);
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
