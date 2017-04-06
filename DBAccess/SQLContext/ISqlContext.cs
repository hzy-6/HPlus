using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using DBAccess.Entity;

namespace DBAccess.SQLContext
{
    /// <summary>
    /// 获取sql字符串接口
    /// </summary>
    public interface ISqlContext<T> where T : Entity.BaseModel, new()
    {
        SQL_Container GetSqlString(T entity);
    }
}
