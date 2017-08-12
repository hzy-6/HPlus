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
    public class AddContext
    {
        private string _ConnectionString { get; set; }
        private AddString add = new AddString();
        private DbHelper dbhelper = null;
        public AddContext(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
            if (add != null)
                add = new AddString();
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

        private string ExecuteSQL<T>(ref MemberInitExpression body, T Model) where T : BaseEntity, new()
        {
            var ID = this.CreatePrimaryKey<T>(ref body, Model.GetKey().FieldName, Model.GetKey().FieldType, Model.GetKey().IsIdentity);
            var Sql = add.GetSql<T>(body);


            if (ID.Contains("SELECT SCOPE_IDENTITY()"))
            {
                var obj = dbhelper.ExecuteScalar(ID);
                ID = obj != null ? obj.ToString() : string.Empty;
            };

            if (!dbhelper.Commit(new List<SQL>() { Sql }))
            {
                return string.Empty;
            }

            //if (dbhelper.ExecuteNonQuery(Sql) == 0)
            //{
            //    return string.Empty;
            //}

            return ID;
        }

        private string ExecuteSQL<T>(ref MemberInitExpression body, T Model, ref List<SQL> li) where T : BaseEntity, new()
        {
            var ID = this.CreatePrimaryKey<T>(ref body, Model.GetKey().FieldName, Model.GetKey().FieldType, Model.GetKey().IsIdentity);
            var Sql = add.GetSql<T>(body);

            if (ID.Contains("SELECT SCOPE_IDENTITY()"))
            {
                var obj = dbhelper.ExecuteScalar(ID);
                ID = obj != null ? obj.ToString() : string.Empty;
            };

            li.Add(Sql);

            return ID;
        }

        public string Add<T>(T Model) where T : BaseEntity, new()
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

        public string Add<T>(Expression<Func<T>> Func) where T : BaseEntity, new()
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            var body = Func.Body as MemberInitExpression;

            return this.ExecuteSQL(ref body, Model);
        }

        public string Add<T>(T Model, ref List<SQL> li) where T : BaseEntity, new()
        {
            var list = new List<MemberBinding>();
            var fileds = Model.EH.GetAllPropertyInfo(Model);
            foreach (var item in fileds)
            {
                list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Model), item.PropertyType)));
            }
            var body = Expression.MemberInit(Expression.New(typeof(T)), list);

            return this.ExecuteSQL(ref body, Model, ref li);
        }

        public string Add<T>(Expression<Func<T>> Func, ref List<SQL> li) where T : BaseEntity, new()
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            var body = Func.Body as MemberInitExpression;

            return this.ExecuteSQL(ref body, Model, ref li);
        }


    }
}
