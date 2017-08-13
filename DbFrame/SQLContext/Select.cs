using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.SQLContext
{
    public class Select : Query
    {


        public override IQuery FindList<T1, T2>(System.Linq.Expressions.Expression<Func<T1, T2, object>> Column)
        {
            this.SqlStr += "我进入了 FindList 函数";
            return this;
        }

        public override IQuery FindList<T1, T2, T3>(System.Linq.Expressions.Expression<Func<T1, T2, T3, object>> Column)
        {
            throw new NotImplementedException();
        }

        public override IQuery FindList<T1, T2, T3, T4>(System.Linq.Expressions.Expression<Func<T1, T2, T3, T4, object>> Column)
        {
            throw new NotImplementedException();
        }

        public override IQuery FindList<T1, T2, T3, T4, T5>(System.Linq.Expressions.Expression<Func<T1, T2, T3, T4, T5, object>> Column)
        {
            throw new NotImplementedException();
        }

        public override IQuery FindList<T1, T2, T3, T4, T5, T6>(System.Linq.Expressions.Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column)
        {
            throw new NotImplementedException();
        }

        public override IQuery FindList<T1, T2, T3, T4, T5, T6, T7>(System.Linq.Expressions.Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column)
        {
            throw new NotImplementedException();
        }

        public override IQuery FindList<T1, T2, T3, T4, T5, T6, T7, T8>(System.Linq.Expressions.Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column)
        {
            throw new NotImplementedException();
        }

        public override IQuery FindList<T1, T2, T3, T4, T5, T6, T7, T8, T9>(System.Linq.Expressions.Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column)
        {
            throw new NotImplementedException();
        }

        public override IQuery FindList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(System.Linq.Expressions.Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column)
        {
            throw new NotImplementedException();
        }

        public override IQuery Where<E1>(System.Linq.Expressions.Expression<Func<E1, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery Where<E1, E2>(System.Linq.Expressions.Expression<Func<E1, E2, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery Where<E1, E2, E3>(System.Linq.Expressions.Expression<Func<E1, E2, E3, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery Where<E1, E2, E3, E4>(System.Linq.Expressions.Expression<Func<E1, E2, E3, E4, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery Where<E1, E2, E3, E4, E5>(System.Linq.Expressions.Expression<Func<E1, E2, E3, E4, E5, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery Where<E1, E2, E3, E4, E5, E6>(System.Linq.Expressions.Expression<Func<E1, E2, E3, E4, E5, E6, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery Where<E1, E2, E3, E4, E5, E6, E7>(System.Linq.Expressions.Expression<Func<E1, E2, E3, E4, E5, E6, E7, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery Where<E1, E2, E3, E4, E5, E6, E7, E8>(System.Linq.Expressions.Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery Where<E1, E2, E3, E4, E5, E6, E7, E8, E9>(System.Linq.Expressions.Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, E9, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery Where<E1, E2, E3, E4, E5, E6, E7, E8, E9, E10>(System.Linq.Expressions.Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, E9, E10, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery InnerJoin<E1, E2>(System.Linq.Expressions.Expression<Func<E1, E2, bool>> ON)
        {
            this.SqlStr += "我进入了 FindList 函数";
            return this;
        }

        public override IQuery LeftJoin<E1, E2>(System.Linq.Expressions.Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        public override IQuery LeftOuterJoin<E1, E2>(System.Linq.Expressions.Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        public override IQuery RightJoin<E1, E2>(System.Linq.Expressions.Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        public override IQuery RightOuterJoin<E1, E2>(System.Linq.Expressions.Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        public override IQuery FullJoin<E1, E2>(System.Linq.Expressions.Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        public override IQuery FullOuterJoin<E1, E2>(System.Linq.Expressions.Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        public override IQuery CrossJoin<E1, E2>(System.Linq.Expressions.Expression<Func<E1, E2, bool>> Where)
        {
            throw new NotImplementedException();
        }

        public override IQuery OrderBy<E>(System.Linq.Expressions.Expression<Func<E, object>> OrderBy)
        {
            throw new NotImplementedException();
        }

        public override IQuery OrderByDesc<E>(System.Linq.Expressions.Expression<Func<E, object>> OrderByDesc)
        {
            throw new NotImplementedException();
        }
    }
}
