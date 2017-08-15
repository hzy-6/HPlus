using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using System.Linq.Expressions;
using DbFrame.Class;

namespace DbFrame.SQLContext.Context
{
    public abstract class AbstractFind : IFind
    {


        /*************单表操作******************/
        public abstract T Find<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        public abstract DataTable FindTable<T>(Expression<Func<T, bool>> Where, string OrderBy) where T : BaseEntity, new();
        public abstract IList<T> FindList<T>(Expression<Func<T, bool>> Where, string OrderBy) where T : BaseEntity, new();


        T IFind.Find<T>(Expression<Func<T, bool>> Where)
        {
            return Find<T>(Where);
        }

        DataTable IFind.FindTable<T>(Expression<Func<T, bool>> Where, string OrderBy)
        {
            return FindTable<T>(Where, OrderBy);
        }

        IList<T> IFind.FindList<T>(Expression<Func<T, bool>> Where, string OrderBy)
        {
            return FindList<T>(Where, OrderBy);
        }


    }
}
