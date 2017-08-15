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
            var Model = (T)Activator.CreateInstance(typeof(T));
            string TabName = Model.GetTabelName() + (Where == null ? "" : " " + Where.Parameters[0].Name);
            if (Where != null)
            {
                ParserArgs pa = new ParserArgs();
                this.GetWhereString<T>(Where, pa);
                return this.SqlString(From == null ? new string[] { " * " } : From, TabName, pa.Builder.ToString(), pa.SqlParameters, OrderBy);
            }
            return this.SqlString(From == null ? new string[] { " * " } : From, TabName, "", new Dictionary<string, object>(), OrderBy);
        }

        private SQL SqlString(string[] From, string TabName, string Where, Dictionary<string, object> SqlPar, string OrderBy)
        {
            var from = new List<string>();
            foreach (var item in From.ToList())
            {
                var Name = item;
                from.Add(Name);
            }
            OrderBy = string.IsNullOrEmpty(OrderBy) ? "" : " ORDER BY " + OrderBy;
            return new SQL(string.Format(" SELECT {0} FROM {1} \r\n  WHERE 1=1 {2} {3} ", string.Join(",", from), TabName, Where, OrderBy), SqlPar);
        }



    }
}
