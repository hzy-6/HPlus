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
    public class AddString
    {
        public AddString()
        {

        }

        public virtual SQL GetSql<T>(MemberInitExpression body) where T : BaseEntity, new()
        {
            return this.SqlString<T>(body);
        }

        private SQL SqlString<T>(MemberInitExpression body) where T : BaseEntity, new()
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            var di = new Dictionary<string, object>();
            List<string> col = new List<string>(), val = new List<string>();
            string TabName = Model.GetTabelName();
            foreach (MemberAssignment item in body.Bindings)
            {
                //检测有无忽略字段
                if (!string.IsNullOrEmpty(Model.GetNoDbField().Find(f => f == item.Member.Name)))
                    continue;
                var Value = Helper.Eval_1(item.Expression);
                var Name = item.Member.Name;
                col.Add(Name); val.Add("@" + Name + "");
                if (di.ContainsKey(Name))
                    di[Name] = Value;
                else
                    di.Add(Name, Value);
            }
            return new SQL(string.Format(" INSERT INTO {0} ({1}) VALUES ({2}) ", TabName, string.Join(",", col), string.Join(",", val)), di);
        }



    }
}
