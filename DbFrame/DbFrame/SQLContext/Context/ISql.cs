using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;
using System.Data;
using System.Linq.Expressions;

namespace DbFrame.SQLContext.Context
{
    public interface ISql
    {
        object Add<T>(T Model) where T : BaseEntity, new();
        object Add<T>(Expression<Func<T>> Func) where T : BaseEntity, new();
        object Add<T>(T Model, List<SQL> li) where T : BaseEntity, new();
        object Add<T>(Expression<Func<T>> Func, List<SQL> li) where T : BaseEntity, new();

        bool Edit<T>(T Model, Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        bool Edit<T>(T Model, Expression<Func<T, bool>> Where, List<SQL> li) where T : BaseEntity, new();
        bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where, List<SQL> li) where T : BaseEntity, new();

        bool Delete<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        bool Delete<T>(Expression<Func<T, bool>> Where, List<SQL> li) where T : BaseEntity, new();







    }
}
