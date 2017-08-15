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
    public interface IQuery
    {

        /***********多表表查询*************/
        IQuery Query<T1, T2>(Expression<Func<T1, T2, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        IQuery Query<T1, T2, T3>(Expression<Func<T1, T2, T3, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity, new();
        IQuery Query<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity, new();
        IQuery Query<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity, new();
        IQuery Query<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity, new();
        IQuery Query<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity, new();
        IQuery Query<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity
            where T8 : BaseEntity, new();
        IQuery Query<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity
            where T8 : BaseEntity
            where T9 : BaseEntity, new();
        IQuery Query<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column)
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

        IQuery Where<T>(Expression<Func<T, bool>> Where)
            where T : BaseEntity, new();

        /// <summary>
        /// 内连接（INNER JOIN）
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        IQuery InnerJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 左连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        IQuery LeftJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 左外连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        IQuery LeftOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 右连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        IQuery RightJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 右外连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        IQuery RightOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 全连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        IQuery FullJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 全外连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ON"></param>
        /// <returns></returns>
        IQuery FullOuterJoin<T1, T2>(Expression<Func<T1, T2, bool>> ON, string Code = null)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        /// <summary>
        /// 交叉连接
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="Where"></param>
        /// <returns></returns>
        IQuery CrossJoin<T1, T2>(Expression<Func<T1, T2, bool>> Where, string Code = null)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();


        IQuery OrderBy<T>(Expression<Func<T, object>> OrderBy)
            where T : BaseEntity, new();
        IQuery OrderByDesc<T>(Expression<Func<T, object>> OrderByDesc)
            where T : BaseEntity, new();

        IList<KeyValuePair<string, object>> ToList();
        DataTable ToDataTable();
        string ToSQL();

    }
}
