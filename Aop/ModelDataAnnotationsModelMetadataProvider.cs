using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Aop
{
    /// <summary>
    /// 其主要作用是用来将前端传递的model string 类型字段 赋值为 "" 而不是 null
    /// </summary>
    public class ModelDataAnnotationsModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var md = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            DataTypeAttribute dataTypeAttribute = attributes.OfType<DataTypeAttribute>().FirstOrDefault();
            DisplayFormatAttribute displayFormatAttribute = attributes.OfType<DisplayFormatAttribute>().FirstOrDefault();
            if (displayFormatAttribute == null && dataTypeAttribute != null)
            {
                displayFormatAttribute = dataTypeAttribute.DisplayFormat;
            }
            if (displayFormatAttribute == null)
            {
                md.ConvertEmptyStringToNull = false;
            }

            //如果是Null，那么改为空字符串
            if (modelType.Equals(typeof(string)) && (md.Model == null || md.Model.Equals("null")))
            {
                md.Model = null;
            }

            return md;
        }
    }
}
