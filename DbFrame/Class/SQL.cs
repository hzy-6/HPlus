using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.Class
{
    public class SQL
    {
        public string Sql { get; set; }
        public string Sql_Parameter { get; set; }
        public Dictionary<string, object> Parameter = new Dictionary<string, object>();
        public SQL(string _Sql_Parameter, Dictionary<string, object> _Parameter)
        {
            this.Sql_Parameter = _Sql_Parameter;
            this.Parameter = _Parameter;
            this.Sql = _Sql_Parameter;

            foreach (var item in Parameter)
            {
                Sql = Sql.Replace("@" + item.Key, item.Value == null ? null : "'" + item.Value.ToString() + "' ");
            }
        }
    }
}
