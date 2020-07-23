//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Apps.Models;
using Apps.Common;
using Unity.Attributes;
using System.Transactions;
using Apps.BLL.Core;
using Apps.Locale;
using LinqToExcel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Apps.IDAL.Spl;
using Apps.Models.Spl;
using Apps.IBLL.Spl;
using Apps.IDAL.Sys;
namespace Apps.BLL.Spl
{
	public partial class Spl_InOutCategoryBLL: Virtual_Spl_InOutCategoryBLL,ISpl_InOutCategoryBLL
	{
        

	}
	public class Virtual_Spl_InOutCategoryBLL
	{
        [Dependency]
        public ISpl_InOutCategoryRepository m_Rep { get; set; }
		
		[Dependency]
        public ISysUserRepository m_userRep { get; set; }

		[Dependency]
        public ISysStructRepository m_structRep { get; set; }

		public virtual async Task<Tuple<GridPager,List<Spl_InOutCategoryModel>>> GetListAsync(GridPager pager, string queryStr)
        {

            IQueryable<Spl_InOutCategory> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.Name.Contains(queryStr)
								
								|| a.Category.Contains(queryStr)
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
			  
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<Spl_InOutCategoryModel> list = await CreateModelListAsync(queryData);
            return  new Tuple<GridPager,List<Spl_InOutCategoryModel>>(pager,list);
        }
		public virtual async Task<List<Spl_InOutCategoryModel>> CreateModelListAsync(IQueryable<Spl_InOutCategory> queryData)
        {

            List<Spl_InOutCategoryModel> modelList = await (from r in queryData
                                              select new Spl_InOutCategoryModel
                                              {
                                                 													Id = r.Id,
													Name = r.Name,
													CreateTime = r.CreateTime,
													Category = r.Category,
          

                                              }).ToListAsync();

            return modelList;
        }

		public virtual List<Spl_InOutCategoryModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Spl_InOutCategory> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.Name.Contains(queryStr)
								
								|| a.Category.Contains(queryStr)
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }

			   
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
			public virtual List<Spl_InOutCategoryModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<Spl_InOutCategoryModel>();
		}
		public virtual List<Spl_InOutCategoryModel> GetListByDepId(ref GridPager pager, string depId,string queryStr)
		{
			return new List<Spl_InOutCategoryModel>();
		}
		
		public virtual List<Spl_InOutCategoryModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<Spl_InOutCategoryModel>();
		}
		public virtual List<Spl_InOutCategoryModel> GetList(string id, string parentId)
        {
			return new List<Spl_InOutCategoryModel>();
		}
        public virtual List<Spl_InOutCategoryModel> CreateModelList(ref IQueryable<Spl_InOutCategory> queryData)
        {

            List<Spl_InOutCategoryModel> modelList = (from r in queryData
                                              select new Spl_InOutCategoryModel
                                              {
													Id = r.Id,
													Name = r.Name,
													CreateTime = r.CreateTime,
													Category = r.Category,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, Spl_InOutCategoryModel model)
        {
            try
            {
                Spl_InOutCategory entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new Spl_InOutCategory();
               				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.CreateTime = model.CreateTime;
				entity.Category = model.Category;
  

                if (m_Rep.Create(entity))
                {
                    return true;
                }
                else
                {
                    errors.Add(Resource.InsertFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

		 public virtual async Task<Tuple<ValidationErrors, bool>> CreateAsync(Spl_InOutCategoryModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                Spl_InOutCategory entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                entity = new Spl_InOutCategory();
               				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.CreateTime = model.CreateTime;
				entity.Category = model.Category;
  

                if (await m_Rep.CreateAsync(entity))
                {
                    return new Tuple<ValidationErrors, bool>(errors, true);
                }
                else
                {
                    errors.Add(Resource.InsertFail);
                    return new Tuple<ValidationErrors, bool>(errors, false);
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return new Tuple<ValidationErrors, bool>(errors, false);
            }
        }

	

		public virtual bool Delete(ref ValidationErrors errors, object id)
        {
            try
            {
                					 if (m_Rep.Delete(id) == 1)
					{
						return true;
					}
					else
					{
						return false;
					}
				  
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public virtual bool Delete(ref ValidationErrors errors, object[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        if (m_Rep.Delete(deleteCollection) == deleteCollection.Length)
                        {
                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

		public virtual async Task<Tuple<ValidationErrors, bool>> DeleteAsync(object[] deleteCollection)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                if (deleteCollection != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        if (await m_Rep.DeleteAsync(deleteCollection) == deleteCollection.Length)
                        {
                            transactionScope.Complete();
                            return new Tuple<ValidationErrors, bool>(errors, true);
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                            errors.Add("其中有数据删除出错！");
                            return new Tuple<ValidationErrors, bool>(errors, false);
                        }
                    }
                }
                return new Tuple<ValidationErrors, bool>(errors, false);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return new Tuple<ValidationErrors, bool>(errors, false);
            }
        }
       

        public virtual bool Edit(ref ValidationErrors errors, Spl_InOutCategoryModel model)
        {
            try
            {
                Spl_InOutCategory entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.CreateTime = model.CreateTime;
				entity.Category = model.Category;
 


                if (m_Rep.Edit(entity))
                {
                    return true;
                }
                else
                {
                    errors.Add(Resource.NoDataChange);
                    return false;
                }

            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

      	 public virtual async Task<Tuple<ValidationErrors, bool>> EditAsync(Spl_InOutCategoryModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                Spl_InOutCategory entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity == null)
                {
                     errors.Add(Resource.Disable);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.CreateTime = model.CreateTime;
				entity.Category = model.Category;
 

                if (await m_Rep.EditAsync(entity))
                {
                    return new Tuple<ValidationErrors, bool>(errors, true);
                }
                else
                {
                    errors.Add(Resource.NoDataChange);
                    return new Tuple<ValidationErrors, bool>(errors, false);
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return new Tuple<ValidationErrors, bool>(errors, false);
            }
        }

        public virtual Spl_InOutCategoryModel GetById(object id)
        {
            if (IsExists(id))
            {
                Spl_InOutCategory entity = m_Rep.GetById(id);
                Spl_InOutCategoryModel model = new Spl_InOutCategoryModel();
                              				model.Id = entity.Id;
				model.Name = entity.Name;
				model.CreateTime = entity.CreateTime;
				model.Category = entity.Category;
 
                return model;
            }
            else
            {
                return null;
            }
        }

		 public virtual async Task<Spl_InOutCategoryModel> GetByIdAsync(object id)
        {
            if (IsExists(id))
            {
                Spl_InOutCategory entity = await m_Rep.GetByIdAsync(id);
                Spl_InOutCategoryModel model = new Spl_InOutCategoryModel();
                              				model.Id = entity.Id;
				model.Name = entity.Name;
				model.CreateTime = entity.CreateTime;
				model.Category = entity.Category;
 
                return model;
            }
            else
            {
                return null;
            }
        }

		 /// <summary>
        /// 校验Excel数据,这个方法一般用于重写校验逻辑
        /// </summary>
        public virtual bool CheckImportData(string fileName, List<Spl_InOutCategoryModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<Spl_InOutCategoryModel>(x => x.Name, "Name");
				 excelFile.AddMapping<Spl_InOutCategoryModel>(x => x.CreateTime, "CreateTime");
				 excelFile.AddMapping<Spl_InOutCategoryModel>(x => x.Category, "Category");
 
            //SheetName
            var excelContent = excelFile.Worksheet<Spl_InOutCategoryModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new Spl_InOutCategoryModel();
						 				  entity.Id = row.Id;
				  entity.Name = row.Name;
				  entity.CreateTime = row.CreateTime;
				  entity.Category = row.Category;
 
                //=============================================================================
                if (errorMessage.Length > 0)
                {
                    errors.Add(string.Format(
                        "第 {0} 列发现错误：{1}{2}",
                        rowIndex,
                        errorMessage,
                        "<br/>"));
                }
                list.Add(entity);
                rowIndex += 1;
            }
            if (errors.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public virtual void SaveImportData(IEnumerable<Spl_InOutCategoryModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        Spl_InOutCategory entity = new Spl_InOutCategory();
                       						entity.Id = ResultHelper.NewId;
						entity.Name = model.Name;
						entity.CreateTime = ResultHelper.NowTime;
						entity.Category = model.Category;
 
                        db.Spl_InOutCategory.Add(entity);
                    }
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
		public virtual bool Check(ref ValidationErrors errors, object id,int flag)
        {
			return true;
		}

        public virtual bool IsExists(object id)
        {
            return m_Rep.IsExist(id);
        }
		
		public void Dispose()
        { 
            
        }

	}
}
