using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using DbFrame.SQLContext.ExpressionTree;
using DbFrame.Class;

namespace DbFrame.SQLContext.Context
{
    public class EditString : WhereString
    {
        public EditString()
        {

        }

        public virtual SQL GetSql<T>(MemberInitExpression Set, Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            return this.SqlString<T>(Set, this.GetWhereString<T>(Where));
        }

        private SQL SqlString<T>(MemberInitExpression Set, string Where) where T : BaseEntity, new()
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            var di = new Dictionary<string, object>();
            string TabName = Model.GetTabelName();
            var set = new List<string>();
            foreach (MemberAssignment item in Set.Bindings)
            {
                //检测有无忽略字段
                if (!string.IsNullOrEmpty(Model.GetNoDbField().Find(f => f == item.Member.Name)))
                    continue;
                var Value = Helper.Eval_1(item.Expression);
                var Name = item.Member.Name;
                set.Add(Name + "=@" + Name + "");
                if (di.ContainsKey(Name))
                    di[Name] = Value;
                else
                    di.Add(Name, Value);
            }
            return new SQL(string.Format(" UPDATE {0} SET {1} WHERE 1=1 {2} ", TabName, string.Join(",", set), string.IsNullOrEmpty(Where) ? Where : Where), di);
        }



    }
}
