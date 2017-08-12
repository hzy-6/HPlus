using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Linq.Expressions;
using DbFrame.SQLContext;
using DbFrame.Class;
using DbFrame.AdoDotNet;
using System.Data;
using System.Web.Script.Serialization;

namespace DbFrame
{
    public class DBContext
    {
        public string ErrorMessge = string.Empty;
        public JavaScriptSerializer jss;
        protected AddContext add;
        protected EditContext edit;
        protected DeleteContext delete;
        protected FindContext find;
        protected CheckContext<BaseEntity> check;
        public DbHelper dbhelper;

        private string _ConnectionString { get; set; }

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
            add = new AddContext(_ConnectionString);
            edit = new EditContext(_ConnectionString);
            delete = new DeleteContext(_ConnectionString);
            find = new FindContext(_ConnectionString);
            dbhelper = new DbHelper(_ConnectionString);
            check = new CheckContext<BaseEntity>(_ConnectionString);
            jss = new JavaScriptSerializer();
        }



        /***********************添加********************/



        public virtual string Add<T>(T Model, bool IsCheck = false) where T : BaseEntity, new()
        {
            try
            {
                if (IsCheck)
                    this.Check(Model);
                var key = add.Add<T>(Model);
                if (string.IsNullOrEmpty(key))
                    throw new Exception("操作失败");
                return key;
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return string.Empty;
            }
        }

        public virtual string Add<T>(Expression<Func<T>> Func) where T : BaseEntity, new()
        {
            try
            {
                var key = add.Add<T>(Func);
                if (string.IsNullOrEmpty(key))
                    throw new Exception("操作失败");
                return key;
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return string.Empty;
            }
        }

        public virtual string Add<T>(T Model, ref List<SQL> li, bool IsCheck = false) where T : BaseEntity, new()
        {
            try
            {
                if (IsCheck)
                    this.Check(Model);
                var key = add.Add<T>(Model, ref li);
                if (string.IsNullOrEmpty(key))
                    throw new Exception("操作失败");
                return key;
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return string.Empty;
            }
        }

        public virtual string Add<T>(Expression<Func<T>> Func, ref List<SQL> li) where T : BaseEntity, new()
        {
            try
            {
                var key = add.Add<T>(Func, ref li);
                if (string.IsNullOrEmpty(key))
                    throw new Exception("操作失败");
                return key;
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return string.Empty;
            }
        }


        /**********************修该**********************/


        public virtual bool Edit<T>(T Model, Expression<Func<T, bool>> Where, bool IsCheck = false) where T : BaseEntity, new()
        {
            try
            {
                if (IsCheck)
                    this.Check(Model);
                if (edit.Edit(Model, Where))
                    return true;
                throw new Exception("操作失败");
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public virtual bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            try
            {
                if (edit.Edit(Set, Where))
                    return true;
                throw new Exception("操作失败");
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public virtual bool Edit<T>(T Model, Expression<Func<T, bool>> Where, ref List<SQL> li, bool IsCheck = false) where T : BaseEntity, new()
        {
            try
            {
                if (IsCheck)
                    this.Check(Model);
                if (edit.Edit(Model, Where, ref li))
                    return true;
                throw new Exception("操作失败");
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public virtual bool Edit<T>(Expression<Func<T>> Set, Expression<Func<T, bool>> Where, ref List<SQL> li) where T : BaseEntity, new()
        {
            try
            {
                if (edit.Edit(Set, Where, ref li))
                    return true;
                throw new Exception("操作失败");
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }



        /**********************删除**********************/

        public virtual bool Delete<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            try
            {
                if (delete.Delete<T>(Where))
                    return true;
                throw new Exception("操作失败");
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }

        public virtual bool Delete<T>(Expression<Func<T, bool>> Where, ref List<SQL> li) where T : BaseEntity, new()
        {
            try
            {
                if (delete.Delete<T>(Where, ref li))
                    return true;
                throw new Exception("操作失败");
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
                return false;
            }
        }


        /**********************查询**********************/
        public virtual T Find<T>(Expression<Func<T, bool>> Where) where T : BaseEntity, new()
        {
            return find.Find<T>(Where);
        }

        public virtual DataTable Find<T>(Expression<Func<T, bool>> Where, string OrderBy = null) where T : BaseEntity, new()
        {
            return find.Find<T>(Where, OrderBy);
        }

        public virtual DataTable Find<T>(string[] From, Expression<Func<T, bool>> Where, string OrderBy = null) where T : BaseEntity, new()
        {
            if (From.Length == 0)
                throw new Exception(" 参数 From 不能为空！ ");
            return find.Find<T>(From, Where, OrderBy);
        }

        public virtual List<T> FindToList<T>(Expression<Func<T, bool>> Where, string OrderBy = null) where T : BaseEntity, new()
        {
            return find.FindToList<T>(Where, OrderBy);
        }

        public virtual List<Dictionary<string, object>> FindToList(DataTable dt)
        {
            return find.FindToList(dt);
        }

        public virtual List<Dictionary<string, object>> FindToList(string SQL)
        {
            return find.FindToList(this.Find(SQL));
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
        public T DataRowToModel<T>(DataRow dr) where T : BaseEntity, new()
        {
            return find.ToModel(dr, (T)Activator.CreateInstance(typeof(T)));
        }

        /// <summary>
        /// Json 转换为 List <T>
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
        public bool Commit(List<SQL> li)
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
        public bool CheckModel<T>(T model) where T : BaseEntity, new()
        {
            try
            {
                if (!check.Check(model))
                    throw new Exception(check.ErrorMessage);
                return true;
            }
            catch (Exception ex)
            {
                this.SetError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 验证实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private void Check<T>(T model) where T : BaseEntity, new()
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
