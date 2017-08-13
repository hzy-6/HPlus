using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;
using System.Linq.Expressions;

namespace DbFrame.SQLContext
{
    public abstract class Query : IQuery
    {
        public Query()
        {

        }

        public string SqlStr { get; set; }

        /***********多表表查询*************/
        public abstract IQuery FindList<T1, T2>(Expression<Func<T1, T2, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity, new();
        public abstract IQuery FindList<T1, T2, T3>(Expression<Func<T1, T2, T3, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity, new();
        public abstract IQuery FindList<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity, new();
        public abstract IQuery FindList<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity, new();
        public abstract IQuery FindList<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity, new();
        public abstract IQuery FindList<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity, new();
        public abstract IQuery FindList<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity
            where T8 : BaseEntity, new();
        public abstract IQuery FindList<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column)
            where T1 : BaseEntity
            where T2 : BaseEntity
            where T3 : BaseEntity
            where T4 : BaseEntity
            where T5 : BaseEntity
            where T6 : BaseEntity
            where T7 : BaseEntity
            where T8 : BaseEntity
            where T9 : BaseEntity, new();
        public abstract IQuery FindList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column)
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

        public abstract IQuery Where<E1>(Expression<Func<E1, bool>> Where)
            where E1 : BaseEntity, new();
        public abstract IQuery Where<E1, E2>(Expression<Func<E1, E2, bool>> Where)
            where E1 : BaseEntity
            where E2 : BaseEntity, new();
        public abstract IQuery Where<E1, E2, E3>(Expression<Func<E1, E2, E3, bool>> Where)
            where E1 : BaseEntity
            where E2 : BaseEntity
            where E3 : BaseEntity, new();
        public abstract IQuery Where<E1, E2, E3, E4>(Expression<Func<E1, E2, E3, E4, bool>> Where)
            where E1 : BaseEntity
            where E2 : BaseEntity
            where E3 : BaseEntity
            where E4 : BaseEntity, new();
        public abstract IQuery Where<E1, E2, E3, E4, E5>(Expression<Func<E1, E2, E3, E4, E5, bool>> Where)
            where E1 : BaseEntity
            where E2 : BaseEntity
            where E3 : BaseEntity
            where E4 : BaseEntity
            where E5 : BaseEntity, new();
        public abstract IQuery Where<E1, E2, E3, E4, E5, E6>(Expression<Func<E1, E2, E3, E4, E5, E6, bool>> Where)
            where E1 : BaseEntity
            where E2 : BaseEntity
            where E3 : BaseEntity
            where E4 : BaseEntity
            where E5 : BaseEntity
            where E6 : BaseEntity, new();
        public abstract IQuery Where<E1, E2, E3, E4, E5, E6, E7>(Expression<Func<E1, E2, E3, E4, E5, E6, E7, bool>> Where)
            where E1 : BaseEntity
            where E2 : BaseEntity
            where E3 : BaseEntity
            where E4 : BaseEntity
            where E5 : BaseEntity
            where E6 : BaseEntity
            where E7 : BaseEntity, new();
        public abstract IQuery Where<E1, E2, E3, E4, E5, E6, E7, E8>(Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, bool>> Where)
            where E1 : BaseEntity
            where E2 : BaseEntity
            where E3 : BaseEntity
            where E4 : BaseEntity
            where E5 : BaseEntity
            where E6 : BaseEntity
            where E7 : BaseEntity
            where E8 : BaseEntity, new();
        public abstract IQuery Where<E1, E2, E3, E4, E5, E6, E7, E8, E9>(Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, E9, bool>> Where)
            where E1 : BaseEntity
            where E2 : BaseEntity
            where E3 : BaseEntity
            where E4 : BaseEntity
            where E5 : BaseEntity
            where E6 : BaseEntity
            where E7 : BaseEntity
            where E8 : BaseEntity
            where E9 : BaseEntity, new();
        public abstract IQuery Where<E1, E2, E3, E4, E5, E6, E7, E8, E9, E10>(Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, E9, E10, bool>> Where)
            where E1 : BaseEntity
            where E2 : BaseEntity
            where E3 : BaseEntity
            where E4 : BaseEntity
            where E5 : BaseEntity
            where E6 : BaseEntity
            where E7 : BaseEntity
            where E8 : BaseEntity
            where E9 : BaseEntity
            where E10 : BaseEntity, new();

        /// <summary>
        /// 内连接（INNER JOIN）
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="IQuery"></param>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery InnerJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
            where E1 : BaseEntity
            where E2 : BaseEntity, new();
        /// <summary>
        /// 左连接
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="IQuery"></param>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery LeftJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
            where E1 : BaseEntity
            where E2 : BaseEntity, new();
        /// <summary>
        /// 左外连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="IQuery"></param>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery LeftOuterJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
            where E1 : BaseEntity
            where E2 : BaseEntity, new();
        /// <summary>
        /// 右连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="IQuery"></param>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery RightJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
            where E1 : BaseEntity
            where E2 : BaseEntity, new();
        /// <summary>
        /// 右外连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="IQuery"></param>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery RightOuterJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
            where E1 : BaseEntity
            where E2 : BaseEntity, new();
        /// <summary>
        /// 全连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="IQuery"></param>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery FullJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
            where E1 : BaseEntity
            where E2 : BaseEntity, new();
        /// <summary>
        /// 全外连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="IQuery"></param>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery FullOuterJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
            where E1 : BaseEntity
            where E2 : BaseEntity, new();
        /// <summary>
        /// 交叉连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="IQuery"></param>
        /// <param name="ON"></param>
        /// <returns></returns>
        public abstract IQuery CrossJoin<E1, E2>(Expression<Func<E1, E2, bool>> Where)
            where E1 : BaseEntity
            where E2 : BaseEntity, new();

        public abstract IQuery OrderBy<E>(Expression<Func<E, object>> OrderBy)
            where E : BaseEntity, new();
        public abstract IQuery OrderByDesc<E>(Expression<Func<E, object>> OrderByDesc)
            where E : BaseEntity, new();







        IQuery IQuery.FindList<T1, T2>(Expression<Func<T1, T2, object>> Column)
        {
            return FindList<T1, T2>(Column);
        }

        IQuery IQuery.FindList<T1, T2, T3>(Expression<Func<T1, T2, T3, object>> Column)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.FindList<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object>> Column)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.FindList<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object>> Column)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.FindList<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object>> Column)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.FindList<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, object>> Column)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.FindList<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, object>> Column)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.FindList<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>> Column)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.FindList<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> Column)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.Where<E1>(Expression<Func<E1, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.Where<E1, E2>(Expression<Func<E1, E2, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.Where<E1, E2, E3>(Expression<Func<E1, E2, E3, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.Where<E1, E2, E3, E4>(Expression<Func<E1, E2, E3, E4, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.Where<E1, E2, E3, E4, E5>(Expression<Func<E1, E2, E3, E4, E5, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.Where<E1, E2, E3, E4, E5, E6>(Expression<Func<E1, E2, E3, E4, E5, E6, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.Where<E1, E2, E3, E4, E5, E6, E7>(Expression<Func<E1, E2, E3, E4, E5, E6, E7, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.Where<E1, E2, E3, E4, E5, E6, E7, E8>(Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.Where<E1, E2, E3, E4, E5, E6, E7, E8, E9>(Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, E9, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.Where<E1, E2, E3, E4, E5, E6, E7, E8, E9, E10>(Expression<Func<E1, E2, E3, E4, E5, E6, E7, E8, E9, E10, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.InnerJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
        {
            return InnerJoin<E1, E2>(ON);
        }

        IQuery IQuery.LeftJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.LeftOuterJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.RightJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.RightOuterJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.FullJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.FullOuterJoin<E1, E2>(Expression<Func<E1, E2, bool>> ON)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.CrossJoin<E1, E2>(Expression<Func<E1, E2, bool>> Where)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.OrderBy<E>(Expression<Func<E, object>> OrderBy)
        {
            throw new NotImplementedException();
        }

        IQuery IQuery.OrderByDesc<E>(Expression<Func<E, object>> OrderByDesc)
        {
            throw new NotImplementedException();
        }
    }
}
