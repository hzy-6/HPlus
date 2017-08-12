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
            var SqlPar = new Dictionary<string, object>();
            return this.SqlString<T>(body, SqlPar);
        }

        private SQL SqlString<T>(MemberInitExpression body, Dictionary<string, object> SqlPar) where T : BaseEntity, new()
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            string TabName = Model.GetTabelName();
            var col = new List<string>();
            var val = new List<string>();
            foreach (MemberAssignment item in body.Bindings)
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



    }
}
