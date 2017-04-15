using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DBAccess.Entity;
using DBAccess.CustomAttribute;
using System.Data;
using System.Reflection;

namespace DBAccess
{
    /// <summary>
    /// 转json类
    /// </summary>
    public class ToJson
    {
        M_JqGridColModel mjgcm = new M_JqGridColModel();

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
                if (item.Value.GetType().BaseType.Equals(typeof(BaseModel)))
                {
                    Type t = item.Value.GetType();
                    var list = t.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToList();
                    foreach (var pi in list)
                    {
                        if (pi.GetValue(item.Value) != null)
                        {
                            if (pi.PropertyType == typeof(DateTime))
                                r.Add(pi.Name, Convert.ToDateTime(pi.GetValue(item.Value)).ToString("yyyy-MM-dd HH:mm:ss"));
                            else
                                r.Add(pi.Name, pi.GetValue(item.Value));
                        }
                        else
                        {
                            r.Add(pi.Name, null);
                        }
                    }
                }
                else
                    r.Add(item.Key, item.Value);
            }
            return r;
        }

        /// <summary>
        /// 设置前台页面表头
        /// </summary>
        /// <param name="pe">Find查询完成后的PagingEntity对象</param>
        /// <param name="ArryEntity">查询涉及的表</param>
        /// <param name="isExecute">是否执行</param>
        public PagingEntity GetPagingEntity(PagingEntity pe, List<BaseModel> ArryEntity, bool isExecute = true)
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
                    mjgcm = new M_JqGridColModel();
                    var pro = list.Find(item => item.Name.Equals(dc.ColumnName));
                    if (pro == null)
                    {
                        mjgcm.label = dc.ColumnName;
                        mjgcm.name = dc.ColumnName;
                        mjgcm.hidden = dc.ColumnName.Equals("_ukid") ? true : false;
                        mjgcm.align = "left";
                    }
                    else
                    {
                        //获取有特性标记的属性【获取字段别名（中文名称）】
                        var FiledConfig = pro.GetCustomAttribute(typeof(FiledAttribute)) as FiledAttribute;
                        mjgcm = new M_JqGridColModel();
                        mjgcm.label = (FiledConfig.DisplayName == "" ? dc.ColumnName : FiledConfig.DisplayName);
                        mjgcm.name = dc.ColumnName;
                        mjgcm.hidden = !FiledConfig.IsShowColumn;
                        mjgcm.align = "left";
                    }
                    pe.JqGridColModel.Add(mjgcm);
                }
            }
            return pe;
        }


    }
}
