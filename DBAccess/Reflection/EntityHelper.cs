using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Reflection;
using DBAccess.CustomAttribute;

namespace DBAccess.Reflection
{
    public sealed class EntityHelper<T> where T : class,new()
    {
        public static readonly EntityHelper<T> EH = new EntityHelper<T>();

        public EntityHelper() { }


        /// <summary>
        /// 获取实体中的主键 名字
        /// </summary>
        /// <returns></returns>
        public string GetKeyName(T model)
        {
            var list = BaseHelper.GetAllPropertyInfo(model.GetType());
            var result = string.Empty;
            foreach (var item in list)
            {
                var attr = item.GetCustomAttribute(typeof(FiledAttribute));
                if (attr == null)
                    continue;
                if (!(attr as FiledAttribute).IsPrimaryKey)
                    continue;
                result = item.Name;
                break;
            }
            return result;
        }

        /// <summary>
        /// 获取实体中的主键 值
        /// </summary>
        /// <returns></returns>
        public string GetKeyValue(T model)
        {
            var list = BaseHelper.GetAllPropertyInfo(model.GetType());
            var result = string.Empty;
            foreach (var item in list)
            {
                var attr = item.GetCustomAttribute(typeof(FiledAttribute));
                if (attr == null)
                    continue;
                if (!(attr as FiledAttribute).IsPrimaryKey)
                    continue;
                result = item.GetValue(model, null) == null ? "" : item.GetValue(model, null).ToString();
                break;
            }
            return result;
        }

        /// <summary>
        /// 获取字段的值
        /// </summary>
        /// <param name="filed"></param>
        /// <returns></returns>
        public object GetValue(T model, string filed)
        {
            return BaseHelper.GetValue<T>(model, filed);
        }

        /// <summary>
        /// 设置字段的值
        /// </summary>
        /// <param name="filed"></param>
        /// <param name="value"></param>
        public void SetValue(T model, string filed, object value)
        {
            BaseHelper.SetValue<T>(model, filed, value);
        }

        /// <summary>
        /// 获取自定字段的别名
        /// </summary>
        /// <param name="filed">字段</param>
        /// <returns></returns>
        public string GetDisplayName(T model, string filed)
        {
            var val = (BaseHelper.GetPropertyInfo(model.GetType(), filed).GetCustomAttribute(typeof(FiledAttribute)) as FiledAttribute);
            return val == null ? string.Empty : val.DisplayName;
        }

        /// <summary>
        /// 获取 属性 PropertyInfo 对象 【 发现属性 (Property) 的属性 (Attribute) 并提供对属性 (Property) 元数据的访问。】
        /// </summary>
        /// <param name="filed">字段</param>
        /// <returns></returns>
        public PropertyInfo GetPropertyInfo(T model, string filed)
        {
            return BaseHelper.GetPropertyInfo(model.GetType(), filed);
        }

        /// <summary>
        /// 获取 属性 List<PropertyInfo> 对象 【 发现属性 (Property) 的属性 (Attribute) 并提供对属性 (Property) 元数据的访问。】
        /// </summary>
        /// <returns></returns>
        public List<PropertyInfo> GetAllPropertyInfo(T model)
        {
            return BaseHelper.GetAllPropertyInfo(model.GetType());
        }

        /// <summary>
        /// 获取字段的标记的集合
        /// </summary>
        /// <param name="filed">字段</param>
        /// <returns></returns>
        public List<CustomAttributeData> GetAttrTag(T model, string filed)
        {
            return BaseHelper.GetPropertyInfo(model.GetType(), filed).CustomAttributes.ToList();
        }

        /// <summary>
        /// 获取字段的自定标记 信息
        /// </summary>
        /// <typeparam name="BaseSign">返回标记</typeparam>
        /// <param name="filed">字段</param>
        /// <param name="bs">传入标记</param>
        /// <returns></returns>
        public BaseSign GetAttrTag<BaseSign>(T model, string filed) where BaseSign : Attribute
        {
            return (BaseHelper.GetPropertyInfo(model.GetType(), filed).GetCustomAttribute(typeof(BaseSign))) as BaseSign;
        }

    }
}
