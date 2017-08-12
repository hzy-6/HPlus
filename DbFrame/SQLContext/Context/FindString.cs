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
    public class FindString : WhereString
    {
        public FindString()
        {

        }

        public virtual SQL GetSql<T>(string[] From, Expression<Func<T, bool>> Where, string OrderBy) where T : BaseEntity, new()
        {
            return this.SqlString<T>(From == null ? new string[] { " * " } : From, this.GetWhereString<T>(Where), OrderBy);
        }

        private SQL SqlString<T>(string[] From, string Where, string OrderBy) where T : BaseEntity, new()
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            var di = new Dictionary<string, object>();
            string TabName = Model.GetTabelName();
            var from = new List<string>();
            foreach (var item in From.ToList())
            {
                var Name = item;
                from.Add(Name);
            }
            OrderBy = string.IsNullOrEmpty(OrderBy) ? "" : " ORDER BY " + OrderBy;
            return new SQL(string.Format(" SELECT {0} FROM {1} \r\n  WHERE 1=1 {2} {3} ", string.Join(",", from), TabName, string.Join(" ", Where), OrderBy), di);
        }



    }
}
