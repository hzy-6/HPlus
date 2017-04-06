using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Reflection
{
    using System.Reflection;
    public class BaseHelper
    {
        public BaseHelper() { }

        /// <summary>
        /// 获取类中所有的公共属性 PropertyInfo集合
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetAllPropertyInfo(Type t)
        {
            return t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
        }

        /// <summary>
        /// 获取指定的公共属性 PropertyInfo
        /// </summary>
        /// <param name="t"></param>
        /// <param name="filed"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo(Type t, string filed)
        {
            return t.GetProperty(filed);
        }

        /// <summary>
        /// 设置给定字段的值
        /// </summary>
        /// <param name="t"></param>
        /// <param name="filed"></param>
        /// <param name="value"></param>
        public static void SetValue<T>(T entity, string filed, object value) where T : class,new()
        {
            BaseHelper.GetPropertyInfo(entity.GetType(), filed).SetValue(entity, value);
        }

        /// <summary>
        /// 获取给定字段的值
        /// </summary>
        /// <param name="t"></param>
        /// <param name="filed"></param>
        public static object GetValue<T>(T entity, string filed) where T : class,new()
        {
            return BaseHelper.GetPropertyInfo(typeof(T), filed).GetValue(entity);
        }

    }
}
