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
            var Model = (T)Activator.CreateInstance(typeof(T));
            string TabName = Model.GetTabelName();
            var pa = new ParserArgs();
            pa.TabIsAlias = false;
            this.GetWhereString<T>(Where, pa);
            return this.SqlString<T>(TabName, pa.Builder.ToString(), pa.SqlParameters);
        }

        private SQL SqlString<T>(string TabName, string Where, Dictionary<string, object> SqlPar) where T : BaseEntity, new()
        {
            return new SQL(string.Format(" DELETE FROM {0} WHERE 1=1 {1} ", TabName, Where), SqlPar);
        }

    }
}
