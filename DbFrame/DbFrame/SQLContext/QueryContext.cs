using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Web.Script.Serialization;
using System.Linq.Expressions;
using System.Data;
using DbFrame.SQLContext.ExpressionTree;
using DbFrame.AdoDotNet;
using DbFrame.SQLContext.Context;
using DbFrame.Class;

namespace DbFrame.SQLContext
{
    public class QueryContext : AbstractQuery
    {
        private string _ConnectionString { get; set; }
        private DbHelper dbhelper = null;
        public QueryContext(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
            dbhelper = new DbHelper(ConnectionString);
        }

        public override IQuery Query<T1, T2>(Expression<Func<T1, T2, object>> Column)
        {
            this.CodeSelect<T1>(Column);
            return this;
        }

        public override IQuery Query<T1, T2, T3>(Expression<Func<T1, T2, T3, object>> Column)
        {
            this.CodeSelect<T1>(Column);
            return this;
        }

        public override IQuery Query<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object>> Column)
        {
            this.CodeSelect<T1>(Column);
            return this;
        }

        public override IQuery Query<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object>> Column)
        {
            this.CodeSelect<T1>(Column);
            return this;
        }

        public override IQuery Query<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column)
        {
            this.CodeSelect<T1>(Column);
            return this;
        }

        public override IQuery Query<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column)
        {
            this.CodeSelect<T1>(Column);
            return this;
        }

        public override IQuery Query<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column)
        {
            this.CodeSelect<T1>(Column);
            return this;
        }

        public override IQuery Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column)
        {
            this.CodeSelect<T1>(Column);
            return this;
        }

        public override IQuery Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column)
        {
            this.CodeSelect<T1>(Column);
            return this;
        }

        public override IQuery Where<T>(Expression<Func<T, bool>> Where)
        {
            ParserArgs pa = new ParserArgs();
            this.GetWhereString<T>(Where, pa);
            foreach (var item in pa.SqlParameters)
            {
                this.SqlParameters.Add(item.Key, item.Value);
            }
            Builder.Append(" WHERE 1=1 " + pa.Builder.To_String());
            return this;
        }

        public override IQuery InnerJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
        {
            CodeJoin<T1, T2>("INNER JOIN", ON, Code);
            return this;
        }

        public override IQuery LeftJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
        {
            CodeJoin<T1, T2>("LEFT JOIN", ON, Code);
            return this;
        }

        public override IQuery LeftOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
        {
            CodeJoin<T1, T2>("LEFT OUTER JOIN", ON, Code);
            return this;
        }

        public override IQuery RightJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
        {
            CodeJoin<T1, T2>("RIGHT JOIN", ON, Code);
            return this;
        }

        public override IQuery RightOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
        {
            CodeJoin<T1, T2>("RIGHT OUTER JOIN", ON, Code);
            return this;
        }

        public override IQuery FullJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
        {
            CodeJoin<T1, T2>("FULL JOIN", ON, Code);
            return this;
        }

        public override IQuery FullOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
        {
            CodeJoin<T1, T2>("FULL OUTER JOIN", ON, Code);
            return this;
        }

        public override IQuery CrossJoin<T1, T2>(Expression<Func<T1, T2, bool>> Where, string Code = null)
        {
            CodeJoin<T1, T2>("CROSS JOIN", Where, Code);
            return this;
        }

        public override IQuery OrderBy<T>(Expression<Func<T, object>> OrderBy)
        {
            CodeOrderBy<T>("ASC", OrderBy);
            return this;
        }

        public override IQuery OrderByDesc<T>(Expression<Func<T, object>> OrderByDesc)
        {
            CodeOrderBy<T>("DESC", OrderByDesc);
            return this;
        }

        public override IList<KeyValuePair<string, object>> ToList()
        {
            return DbHelper.ConvertDataTableToList<KeyValuePair<string, object>>(this.ToDataTable());
        }

        public override DataTable ToDataTable()
        {
            return dbhelper.ExecuteDataset(this.ToSQL());
        }

        public override string ToSQL()
        {
            return this.Builder.To_String();
        }




    }
}
