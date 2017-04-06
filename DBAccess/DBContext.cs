using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using DBAccess.SQLContext;
using DBAccess.CheckClass;
using DBAccess.Entity;
using System.Data;
using System.Web.Script.Serialization;

namespace DBAccess
{
    public class DBContext
    {
        public string ErrorMessge = string.Empty;
        public JavaScriptSerializer jss;
        protected AddContext<BaseModel> add;
        protected EditContext<BaseModel> edit;
        protected DeleteContext<BaseModel> delete;
        protected FindContext<BaseModel> find;
        protected CommitContext commit;
        protected CheckContext<BaseModel> check;

        private string _ConnectionString { get; set; }

        /// <summary>
        /// 默认连接
        /// </summary>
        private DBContext() { }

        /// <summary>
        /// 数据库操作类
        /// </summary>
        /// <param name="ConnectionString">链接串 不传入默认为 ConnectionString </param>
        public DBContext(string ConnectionString = null)
        {
            if (string.IsNullOrEmpty(ConnectionString))
                _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            else
                _ConnectionString = ConnectionString;
            add = new AddContext<BaseModel>(_ConnectionString);
            edit = new EditContext<BaseModel>(_ConnectionString);
            delete = new DeleteContext<BaseModel>(_ConnectionString);
            find = new FindContext<BaseModel>(_ConnectionString);
            jss = new JavaScriptSerializer();
            commit = new CommitContext(_ConnectionString);
            check = new CheckContext<BaseModel>(_ConnectionString);
        }

        public string Add(BaseModel entity, bool IsCheck = true)
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                return add.Add(entity);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return null;
            }

        }

        public string Add(BaseModel entity, ref List<SQL_Container> li, bool IsCheck = true)
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                return add.Add(entity, ref li);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return null;
            }
        }

        public bool Edit(BaseModel entity, bool IsCheck = true)
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                return edit.Edit(entity);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Edit(BaseModel entity, string where, bool IsCheck = true)
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                return edit.Edit(entity, where);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Edit(BaseModel entity, BaseModel where, bool IsCheck = true)
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                return edit.Edit(entity, where);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Edit<M>(BaseModel entity, Expression<Func<M, bool>> where, bool IsCheck = true) where M : BaseModel, new()
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                return edit.Edit(entity, where);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Edit(BaseModel entity, ref List<SQL_Container> li, bool IsCheck = true)
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                return edit.Edit(entity, ref li);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Edit(BaseModel entity, string where, ref List<SQL_Container> li, bool IsCheck = true)
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                return edit.Edit(entity, where, ref li);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Edit(BaseModel entity, BaseModel where, ref List<SQL_Container> li, bool IsCheck = true)
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                return edit.Edit(entity, where, ref li);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Edit<M>(BaseModel entity, Expression<Func<M, bool>> where, ref List<SQL_Container> li, bool IsCheck = true) where M : BaseModel, new()
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                return edit.Edit(entity, where, ref li);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Delete(BaseModel entity)
        {
            try
            {
                return delete.Delete(entity);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Delete<M>(string where) where M : BaseModel, new()
        {
            try
            {
                return delete.Delete<M>(where);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Delete<M>(Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            try
            {
                return delete.Delete(where);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Delete(BaseModel entity, ref List<SQL_Container> li)
        {
            try
            {
                return delete.Delete(entity, ref li);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Delete<M>(string where, ref List<SQL_Container> li) where M : BaseModel, new()
        {
            try
            {
                return delete.Delete<M>(where, ref li);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public bool Delete<M>(Expression<Func<M, bool>> where, ref List<SQL_Container> li) where M : BaseModel, new()
        {
            try
            {
                return delete.Delete(where, ref li);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public M Find<M>(M entity) where M : BaseModel, new()
        {
            return find.Find(entity);
        }

        public M Find<M>(string where) where M : BaseModel, new()
        {
            return find.Find<M>(where);
        }

        public M Find<M>(Expression<Func<M, bool>> where) where M : BaseModel, new()
        {
            return find.Find<M>(where);
        }

        public DataTable Find<M>(M entity, string OrderBy = null) where M : BaseModel, new()
        {
            return find.Find<M>(entity, OrderBy);
        }

        public List<M> FindToList<M>(M entity, string OrderBy = null) where M : BaseModel, new()
        {
            return find.FindToList<M>(entity, OrderBy);
        }

        public List<M> FindToList<M>(DataTable dt) where M : BaseModel, new()
        {
            return find.FindToList<M>(dt);
        }

        public DataTable Find(string SQL)
        {
            return find.Find(SQL);
        }

        public object FINDToObj(string SQL)
        {
            return find.FINDToObj(SQL);
        }

        public DataTable Find(string SQL, int PageIndex, int PageSize, out int PageCount, out int Counts)
        {
            return find.Find(SQL, PageIndex, PageSize, out PageCount, out Counts);
        }

        public PagingEntity Find(string SQL, int PageIndex, int PageSize)
        {
            return find.Find(SQL, PageIndex, PageSize);
        }

        /// <summary>
        /// 将datarow 转换为Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public T DataRowToModel<T>(DataRow dr) where T : BaseModel
        {
            return find.ToModel(dr, (T)Activator.CreateInstance(typeof(T)));
        }

        /// <summary>
        /// 根据 DataTable 获取 List<Dictionary<string,object>> 类型
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetList(DataTable table)
        {
            return GetDataTableToList(table);
        }

        /// <summary>
        /// Json 转换为 List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public List<T> JsonToList<T>(string Json)
        {
            T[] str = jss.Deserialize(Json, typeof(T[])) as T[];
            return new List<T>(str);
        }

        /// <summary>
        /// 根据datatable获取List
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<Dictionary<string, object>> GetDataTableToList(DataTable table)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            Dictionary<string, object> di = new Dictionary<string, object>();
            foreach (DataRow rowItem in table.Rows)
            {
                di = new Dictionary<string, object>();
                //给objT的所有属性赋值
                foreach (DataColumn columnItem in table.Columns)
                {
                    if (columnItem.DataType == typeof(DateTime))
                        di.Add(columnItem.ColumnName, Convert.ToDateTime(rowItem[columnItem.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss"));
                    else
                        di.Add(columnItem.ColumnName, rowItem[columnItem.ColumnName]);
                }
                list.Add(di);
            }
            return list;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public bool Commit(List<SQL_Container> li)
        {
            try
            {
                if (commit.COMMIT(li))
                    return true;
                SetError("操作失败");
                return false;
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 验证实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckModel(BaseModel model)
        {
            try
            {
                if (!check.Check(model))
                    throw new Exception(check.ErrorMessage);
                return true;
            }
            catch (Exception ex)
            {
                this.SetError(check.ErrorMessage);
                return false;
            }
        }

        /// <summary>
        /// 验证实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private void Check(BaseModel model)
        {
            if (!check.Check(model))
                throw new Exception(check.ErrorMessage);
        }

        /// <summary>
        /// 设置错误消息
        /// </summary>
        /// <param name="Error"></param>
        private void SetError(string Error)
        {
            ErrorMessge = string.Empty;
            ErrorMessge = Error.Replace("\r\n", "<br />");
        }

    }
}
