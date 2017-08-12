using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DbFrame.Class;
using System.Data;
using System.Reflection;

namespace DbFrame
{
    /// <summary>
    /// 转json类
    /// </summary>
    public class ToJson
    {
        BootStrapTableColModel mjgcm = new BootStrapTableColModel();

        public ToJson()
        {

        }

        /// <summary>
        /// 将多个实体组合成为一个 字典类型
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetDictionary(Dictionary<string, object> di)
        {
            Dictionary<string, object> r = new Dictionary<string, object>();
            foreach (var item in di)
            {
                if (item.Value == null)
                {
                    r.Add(item.Key, item.Value);
                    continue;
                }
                if (!item.Value.GetType().BaseType.Equals(typeof(BaseEntity)))
                {
                    r.Add(item.Key, item.Value);
                    continue;
                }
                Type t = item.Value.GetType();
                var list = t.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToList();
                list.ForEach(pi =>
                {
                    if (pi.GetValue(item.Value, null) == null)
                        r.Add(pi.Name, null);
                    else
                    {
                        if (pi.PropertyType == typeof(DateTime))
                            r.Add(pi.Name, pi.GetValue(item.Value, null).To_DateTimeString("yyyy-MM-dd HH:mm:ss"));
                        else
                            r.Add(pi.Name, pi.GetValue(item.Value, null));
                    }
                });
            }
            return r;
        }

        /// <summary>
        /// 设置前台页面表头
        /// </summary>
        /// <param name="pe">Find查询完成后的PagingEntity对象</param>
        /// <param name="ArryEntity">查询涉及的表</param>
        /// <param name="isExecute">是否执行</param>
        public PagingEntity GetPagingEntity(PagingEntity pe, List<BaseEntity> ArryEntity, bool isExecute = true)
        {
            if (isExecute)
            {
                var list = new List<PropertyInfo>();
                ArryEntity.ForEach(item =>
                {
                    //将所有实体里面的属性放入list中
                    item.EH.GetAllPropertyInfo(item).ForEach(p =>
                    {
                        list.Add(p);
                    });
                });
                foreach (DataColumn dc in pe.dt.Columns)
                {
                    mjgcm = new BootStrapTableColModel();
                    var pro = list.Find(item => item.Name.Equals(dc.ColumnName));
                    if (pro == null)
                    {
                        mjgcm.title = dc.ColumnName;
                        mjgcm.field = dc.ColumnName;
                        mjgcm.visible = dc.ColumnName.Equals("_ukid") ? false : true;
                        mjgcm.align = "left";
                    }
                    else
                    {
                        //获取有特性标记的属性【获取字段别名（中文名称）】
                        var FiledConfig = pro.GetCustomAttribute(typeof(ObjectRemarks.FieldAttribute)) as ObjectRemarks.FieldAttribute;
                        mjgcm = new BootStrapTableColModel();
                        mjgcm.title = (FiledConfig.Alias == "" ? dc.ColumnName : FiledConfig.Alias);
                        mjgcm.field = dc.ColumnName;
                        mjgcm.visible = true;
                        mjgcm.align = "left";
                    }
                    pe.ColModel.Add(mjgcm);
                }
            }
            return pe;
        }


    }
}
