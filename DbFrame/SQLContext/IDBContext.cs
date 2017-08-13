using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;
using System.Data;
using System.Linq.Expressions;

namespace DbFrame.SQLContext
{
    interface IDBContext
    {

        string Add<T>(T Model, bool IsCheck) where T : BaseEntity, new();
        string Add<T>(Expression<Func<T>> Func, bool IsCheck) where T : BaseEntity, new();
        string Add<T>(T Model, bool IsCheck, ref List<SQL> li) where T : BaseEntity, new();      
        string Add<T>(Expression<Func<T>> Func, bool IsCheck, ref List<SQL> li) where T : BaseEntity, new();

        bool Edit<T>(T Model, Expression<Func<T, bool>> Where, bool IsCheck) where T : BaseEntity, new();
        bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where, bool IsCheck) where T : BaseEntity, new();
        bool Edit<T>(T Model, Expression<Func<T, bool>> Where, bool IsCheck, ref List<SQL> li) where T : BaseEntity, new();
        bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where, bool IsCheck, ref List<SQL> li) where T : BaseEntity, new();

        bool Delete<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        bool Delete<T>(Expression<Func<T, bool>> Where, ref List<SQL> li) where T : BaseEntity, new();

        /*************单表操作******************/
        T Find<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        T Find<T>(Expression<Func<T>> SelectField, Expression<Func<T, bool>> Where) where T : BaseEntity, new();
        DataTable FindTable<T>(Expression<Func<T>> Where, string OrderBy) where T : BaseEntity, new();
        IList<T> FindList<T>(Expression<Func<T>> Where, string OrderBy) where T : BaseEntity, new();

        /***********多表表查询*************/
        IQuery FindList<T1, T2>(Expression<Func<T1, T2, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        IQuery FindList<T1, T2, T3>(Expression<Func<T1, T2, T3, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity, new();
        IQuery FindList<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity, new();
        IQuery FindList<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity, new();
        IQuery FindList<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity, new();
        IQuery FindList<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity, new();
        IQuery FindList<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity
            where T8 : BaseEntity, new();
        IQuery FindList<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity
            where T8 : BaseEntity
            where T9 : BaseEntity, new();
        IQuery FindList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity
            where T8 : BaseEntity
            where T9 : BaseEntity
            where T10 : BaseEntity, new();


    }
}
