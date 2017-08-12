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
    public class DeleteString : WhereString
    {
        public DeleteString()
        {

        }

        public virtual SQL GetSql<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            return this.SqlString<T>(this.GetWhereString<T>(Where));
        }

        private SQL SqlString<T>(string Where) where T : BaseEntity, new()
        {
            var Model = (T)Activator.CreateInstance(typeof(T));
            var di = new Dictionary<string, object>();
            string TabName = Model.GetTabelName();
            return new SQL(string.Format(" DELETE FROM {0} WHERE 1=1 {1} ", TabName, string.Join(" ", Where)), di);
        }














    }
}
