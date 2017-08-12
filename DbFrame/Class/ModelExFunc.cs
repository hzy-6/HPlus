using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using System.Reflection;

namespace DbFrame.Class
{
    /*扩展方法被定义为静态方法，但它们是通过实例方法语法进行调用的。 它们的第一个参数指定该方法作用于哪个类型，并且该参数以 this 修饰符为前缀。 扩展方法当然不能破坏面向对象封装的概念，所以只能是访问所扩展类的public成员。
  扩展方法使您能够向现有类型“添加”方法，而无需创建新的派生类型、重新编译或以其他方式修改原始类型。扩展方法是一种特殊的静态方法，但可以像扩展类型上的实例方法一样进行调用。
C#扩展方法第一个参数指定该方法作用于哪个类型，并且该参数以 this 修饰符为前缀。
扩展方法的目的就是为一个现有类型添加一个方法，现有类型既可以是int，string等数据类型，也可以是自定义的数据类型。
为数据类型的添加一个方法的理解：一般来说，int数据类型有个Tostring的方法，就是把int 数据转换为字符串的类型，比如现在我们想在转换成字符串的时候还添加一点东西，比如增加一个字符 a .那么之前的Tostring就不好使了，因为它只是它我们的int数据转换为string类型的，却并不能添加一个字母 a.所以这就要用到所谓的扩展方法了。
首先我们看一个给现有的类型增加一个扩展方法。
我们想给string 类型增加一个Add方法，该方法的作用是给字符串增加一个字母a.*/


    /// <summary>
    /// 扩展方法  http://blog.csdn.net/zyh_1988/article/details/51103612
    /// </summary>
    public static class ModelExFunc
    {
        /// <summary>
        /// 标记 不是数据字段 【忽略字段】
        /// </summary>
        /// <param name="Fields">字段</param>
        public static void AddNoDbField<T>(this T Model, Expression<Func<T, object>> func) where T : BaseEntity, new()
        {
            var body = (func.Body as NewExpression);
            if (body == null) throw new Exception("语法错误 这里用过使用 new {  } 匿名实例化语法！");
            if (body.Arguments.Count > 0)
            {
                var list = new List<string>();
                var values = body.Arguments;
                foreach (MemberExpression item in values)
                {
                    list.Add(item.Member.Name);
                }
            }

            //Model.NoDbField
        }

        /// <summary>
        /// 获取 不是数据字段集合 【获取忽略字段集合】
        /// </summary>
        /// <returns></returns>
        public static List<string> GetNoDbField<T>(this T Model) where T : BaseEntity, new()
        {
            return Model.NoDbField;
        }

        /// <summary>
        /// 获取主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static ObjectRemarks.FieldAttribute GetKey<T>(this T Model) where T : BaseEntity, new()
        {
            var list = Model.EH.GetAllPropertyInfo(Model);
            foreach (var item in list)
            {
                var attr = (Attribute.GetCustomAttribute(item, typeof(ObjectRemarks.FieldAttribute)) as ObjectRemarks.FieldAttribute);
                if (attr.IsPrimaryKey != true)
                    continue;
                attr.FieldName = item.Name;
                attr.Value = item.GetValue(Model);
                return attr;
            }
            return null;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static string GetTabelName<T>(this T Model) where T : BaseEntity, new()
        {
            var type = Model.GetType();
            var attrs = Attribute.GetCustomAttributes(type, true);
            if (attrs.Length == 0)
                throw new Exception("实体:" + type.Name + "未描述实体对应表名！");
            foreach (var item in attrs)
            {
                if (item is ObjectRemarks.TableAttribute)
                {
                    return (item as ObjectRemarks.TableAttribute).TableName;
                }
                else
                {
                    continue;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取属性字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static string GetFiledName<T>(this T Model, Expression<Func<T, object>> func) where T : BaseEntity, new()
        {
            var body = func.Body;
            MemberExpression member = body as MemberExpression;
            if (member != null)
            {
                return member.Member.Name;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取属性字段 的 中文名称（别名）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static string GetAlias<T>(this T Model, Expression<Func<T, object>> func) where T : BaseEntity, new()
        {
            var body = func.Body;
            MemberExpression member = body as MemberExpression;
            if (member != null)
            {
                var attrs = member.Member.GetCustomAttributes(typeof(ObjectRemarks.FieldAttribute), true);
                if (attrs.Count() > 0)
                {
                    return (attrs[0] as ObjectRemarks.FieldAttribute).Alias;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 在 拉姆达表达式 where 表达式中使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool In<T>(this T obj, T[] array)
        {
            return true;
        }

        /// <summary>
        /// 在 拉姆达表达式 where 表达式中使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool NotIn<T>(this T obj, T[] array)
        {
            return true;
        }


        #region 由Object 转换值
        /// <summary>
        /// 转换 String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string To_String<T>(this T obj)
        {
            try
            {
                return obj.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 转换 Int
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int To_Int<T>(this T obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 转换 Float
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float To_Float<T>(this T obj)
        {
            try
            {
                return float.Parse(obj.To_String());
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 转换 Double
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double To_Double<T>(this T obj)
        {
            try
            {
                return double.Parse(obj.To_String());
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 转换 Decimal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal To_Decimal<T>(this T obj)
        {
            try
            {
                return decimal.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 转换为 Guid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid To_Guid<T>(this T obj)
        {
            try
            {
                return Guid.Parse(obj.To_String());
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
        /// <summary>
        /// 转换为 GuidString
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string To_GuidString<T>(this T obj)
        {
            return obj.To_Guid().To_String();
        }
        /// <summary>
        /// 转换为 DateTime
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime To_DateTime<T>(this T obj)
        {
            try
            {
                return DateTime.Parse(obj.ToString());
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// 转换为  知道 格式的 时间 字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="FormatStr"></param>
        /// <returns></returns>
        public static string To_DateTimeString<T>(this T obj, string FormatStr = "yyyy-MM-dd")
        {
            DateTime datetime = (obj).To_DateTime();
            if (datetime.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                return String.Empty;
            else
                return datetime.ToString(FormatStr);
        }
        /// <summary>
        /// 转换为 Bool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool To_Bool<T>(this T obj)
        {
            try
            {
                if (obj.ToString() == "0" || obj.ToString() == "" || obj.ToString().ToLower() == "false" || obj == null)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion



    }
}
