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
using DBAccess.HelperClass;
using DBAccess.AdoDotNet;
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
        protected DBHelper dbhelper;
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
        /// <param name="DBType">数据库类型 默认 sqlserver </param>
        public DBContext(string ConnectionString = null, DBType DBType = DBType.SqlServer)
        {
            if (string.IsNullOrEmpty(ConnectionString))
                _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            else
                _ConnectionString = ConnectionString;
            add = new AddContext<BaseModel>(_ConnectionString, DBType);
            edit = new EditContext<BaseModel>(_ConnectionString, DBType);
            delete = new DeleteContext<BaseModel>(_ConnectionString, DBType);
            find = new FindContext<BaseModel>(_ConnectionString, DBType);
            dbhelper = new DBHelper(_ConnectionString, DBType);
            check = new CheckContext<BaseModel>(_ConnectionString, DBType);
            jss = new JavaScriptSerializer();
        }

        public string Add(BaseModel entity, bool IsCheck = true)
        {
            try
            {
                if (IsCheck)
                    this.Check(entity);
                var key = add.Add(entity);
                if (string.IsNullOrEmpty(key))
                    throw new Exception("操作失败");
                return key;
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
                var key = add.Add(entity, ref li);
                if (string.IsNullOrEmpty(key))
                    throw new Exception("操作失败");
                return key;
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
                if (edit.Edit(entity))
                    return true;
                throw new Exception("操作失败");
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
                if (edit.Edit(entity, where))
                    return true;
                throw new Exception("操作失败");
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
                if (edit.Edit(entity, where))
                    return true;
                throw new Exception("操作失败");
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
                if (edit.Edit(entity, where))
                    return true;
                throw new Exception("操作失败");
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
                if (edit.Edit(entity, ref li))
                    return true;
                throw new Exception("操作失败");
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
                if (edit.Edit(entity, where, ref li))
                    return true;
                throw new Exception("操作失败");
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
                if (edit.Edit(entity, where, ref li))
                    return true;
                throw new Exception("操作失败");
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
                if (edit.Edit(entity, where, ref li))
                    return true;
                throw new Exception("操作失败");
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
                if (delete.Delete(entity))
                    return true;
                throw new Exception("操作失败");
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
                if (delete.Delete<M>(where))
                    return true;
                throw new Exception("操作失败");
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
                if (delete.Delete(where))
                    return true;
                throw new Exception("操作失败");
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
                if (delete.Delete(entity, ref li))
                    return true;
                throw new Exception("操作失败");
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
                if (delete.Delete<M>(where, ref li))
                    return true;
                throw new Exception("操作失败");
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
                if (delete.Delete(where, ref li))
                    return true;
                throw new Exception("操作失败");
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
            return Tool.ConvertDataTableToList<Dictionary<string, object>>(table);
        }

        /// <summary>
        /// 根据单 表获取 list 实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<T> GetList<T>(DataTable table) where T : BaseModel
        {
            return Tool.ConvertDataTableToList<T>(table);
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
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public bool Commit(List<SQL_Container> li)
        {
            try
            {
                if (dbhelper.Commit(li))
                    return true;
                throw new Exception("操作失败");
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
