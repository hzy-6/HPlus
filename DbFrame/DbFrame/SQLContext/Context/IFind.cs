using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using System.Data;
using DbFrame.Class;

namespace DbFrame.SQLContext.Context
{
    public interface IFind
    {
        /*************单表操作******************/
        T Find<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        DataTable FindTable<T>(Expression<Func<T, bool>> Where, string OrderBy) where T : BaseEntity, new();
        IList<T> FindList<T>(Expression<Func<T, bool>> Where, string OrderBy) where T : BaseEntity, new();





    }
}
