using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
namespace Apps.DAL
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        DBContainer db;
        public BaseRepository(DBContainer context)
        {
            this.db = context;
        }

        public DBContainer Context
        {
            get { return db; }
        }


        #region 创建记录（异步与同步方法）

        public virtual bool Create(T model, bool isCommit = true)
        {
            db.Set<T>().Add(model);
            if (isCommit)
            {
                return SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public virtual async Task<bool> CreateAsync(T model, bool isCommit = true)
        {
            db.Set<T>().Add(model);
            if (isCommit)
            {
                return await db.SaveChangesAsync() > 0;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 增加多条记录
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public bool CreateList<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return false;
            var tmp = db.ChangeTracker.Entries<T>().ToList();
            foreach (var x in tmp)
            {
                var properties = typeof(T).GetTypeInfo().GetProperties();
                foreach (var y in properties)
                {
                    var entry = x.Property(y.Name);
                    entry.CurrentValue = entry.OriginalValue;
                    entry.IsModified = false;
                    y.SetValue(x.Entity, entry.OriginalValue);
                }
                x.State = EntityState.Unchanged;
            }
            T.ToList().ForEach(item =>
            {
                db.Set<T1>().Add(item);
            });
            if (IsCommit)
                return db.SaveChanges() > 0;
            else
                return false;
        }


        /// <summary>
        /// 增加多条记录（异步）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public async Task<bool> CreateListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return await Task.Run(() => false);
            var tmp = db.ChangeTracker.Entries<T>().ToList();
            foreach (var x in tmp)
            {
                var properties = typeof(T).GetTypeInfo().GetProperties();
                foreach (var y in properties)
                {
                    var entry = x.Property(y.Name);
                    entry.CurrentValue = entry.OriginalValue;
                    entry.IsModified = false;
                    y.SetValue(x.Entity, entry.OriginalValue);
                }
                x.State = EntityState.Unchanged;
            }
            T.ToList().ForEach(item =>
            {
                db.Set<T1>().Add(item);
            });
            if (IsCommit)
                return await Task.Run(() => db.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        #endregion

        #region 修改记录（异步和同步方法）
        public virtual bool Edit(T model, bool isCommit = true)
        {
            db.Set<T>().Attach(model);
            db.Entry<T>(model).State = EntityState.Modified;

            if (isCommit)
            {
                return SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public virtual async Task<bool> EditAsync(T model, bool isCommit = true)
        {
            db.Set<T>().Attach(model);
            db.Entry<T>(model).State = EntityState.Modified;

            if (isCommit)
            {
                return await db.SaveChangesAsync() > 0;
            }
            else
            {
                return false;
            }
        }

        public virtual int BatchUpdate(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> updateLambda)
        {
            if (whereLambda == null)
            {
                return db.Set<T>().Update(updateLambda);
            }
            else
            {
                return db.Set<T>().Where(whereLambda).Update(updateLambda);
            }
        }

        public virtual async Task<int> BatchUpdateAsync(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> updateLambda)
        {
            if (whereLambda == null)
            {
                return await db.Set<T>().UpdateAsync(updateLambda);
            }
            else
            {
                return await db.Set<T>().Where(whereLambda).UpdateAsync(updateLambda);
            }
        }

        public bool EditList<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return false;
            var tmp = db.ChangeTracker.Entries<T>().ToList();
            foreach (var x in tmp)
            {
                var properties = typeof(T).GetTypeInfo().GetProperties();
                foreach (var y in properties)
                {
                    var entry = x.Property(y.Name);
                    entry.CurrentValue = entry.OriginalValue;
                    entry.IsModified = false;
                    y.SetValue(x.Entity, entry.OriginalValue);
                }
                x.State = EntityState.Unchanged;
            }
            T.ToList().ForEach(item =>
            {
                db.Set<T1>().Attach(item);
                db.Entry<T1>(item).State = EntityState.Modified;
            });
            if (IsCommit)
                return db.SaveChanges() > 0;
            else
                return false;
        }

        #endregion

        #region 删除记录 (同步和异步方法)

        public virtual bool Delete(T model, bool isCommit = true)
        {
            db.Set<T>().Remove(model);
            if (isCommit)
            {
                return SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public virtual async Task<bool> DeleteAsync(T model, bool isCommit = true)
        {
            db.Set<T>().Remove(model);
            if (isCommit)
            {
                return await db.SaveChangesAsync() > 0;
            }
            else
            {
                return false;
            }
        }

        public int Delete(params object[] keyValues)
        {
            foreach (var item in keyValues)
            {
                T model = GetById(item);
                if (model != null)
                {
                    db.Set<T>().Remove(model);
                }
            }
            return db.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(params object[] keyValues)
        {
            foreach (var item in keyValues)
            {
                T model = GetById(item);
                if (model != null)
                {
                    db.Set<T>().Remove(model);
                }
            }
            return await db.SaveChangesAsync();
        }

        // m_Rep.BatchDelete(a=>a.CreateTime>DateTime.Now);
        public int BatchDelete(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).Delete();
        }

        public async Task<int> BatchDeleteAsync(Expression<Func<T, bool>> whereLambda)
        {
            if (whereLambda == null)
            {

                return await db.Set<T>().Where(whereLambda).DeleteAsync();
            }
            else
            {
                return await db.Set<T>().DeleteAsync();
            }
        }


        #endregion

        #region 查询记录 (异步与同步方法)

        public T GetById(params object[] keyValues)
        {
            return db.Set<T>().Find(keyValues);
        }

        public async Task<T> GetByIdAsync(params object[] keyValues)
        {
            return await db.Set<T>().FindAsync(keyValues);
        }

        public T GetSingleWhere(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().FirstOrDefault(whereLambda);
        }
        public IQueryable<T> GetList()
        {
            return db.Set<T>();
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda);
        }






        public IQueryable<T> GetList<S>(int pageSize, int pageIndex, out int total
            , Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, bool>> orderByLambda)
        {
            var queryable = db.Set<T>().Where(whereLambda);
            total = queryable.Count();
            if (isAsc)
            {
                queryable = queryable.OrderBy(orderByLambda).Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
            }
            else
            {
                queryable = queryable.OrderByDescending(orderByLambda).Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
            }
            return queryable;
        }



        public int ExecuteSqlCommand(string sql)
        {
            return db.Database.ExecuteSqlCommand(sql);
        }

        public int ExecuteSqlCommand(string sql, params SqlParameter[] sp)
        {
            return db.Database.ExecuteSqlCommand(sql, sp);
        }

        /// <summary>
        /// 异步执行一条SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Task<int> ExecuteSqlCommandAsync(string sql)
        {
            return db.Database.ExecuteSqlCommandAsync(sql);
        }

        public IQueryable<T> SqlQuery(string sql)
        {
            return db.Database.SqlQuery<T>(sql).AsQueryable();
        }
        /// <summary>
        /// 查询一条语句返回结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IQueryable<T> SqlQuery(string sql, params object[] paras)
        {
            return db.Database.SqlQuery<T>(sql, paras).AsQueryable();
        }

        #endregion

        public bool IsExist(object id)
        {
            return GetById(id) != null;
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }


        //1、 Finalize只释放非托管资源；
        //2、 Dispose释放托管和非托管资源；
        //3、 重复调用Finalize和Dispose是没有问题的；
        //4、 Finalize和Dispose共享相同的资源释放策略，因此他们之间也是没有冲突的。
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {

            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
