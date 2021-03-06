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
using Apps.IDAL.Sys;
using Apps.Models.Sys;
using Apps.IBLL.Sys;

namespace Apps.BLL.Sys
{
	public partial class SysModuleBLL: Virtual_SysModuleBLL,ISysModuleBLL
	{
        

	}
	public class Virtual_SysModuleBLL
	{
        [Dependency]
        public ISysModuleRepository m_Rep { get; set; }
		
		[Dependency]
        public ISysUserRepository m_userRep { get; set; }

		[Dependency]
        public ISysStructRepository m_structRep { get; set; }

		public virtual async Task<Tuple<GridPager,List<SysModuleModel>>> GetListAsync(GridPager pager, string queryStr)
        {

            IQueryable<SysModule> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.Name.Contains(queryStr)
								|| a.EnglishName.Contains(queryStr)
								|| a.ParentId.Contains(queryStr)
								|| a.Url.Contains(queryStr)
								|| a.Iconic.Contains(queryStr)
								
								|| a.Remark.Contains(queryStr)
								
								|| a.CreatePerson.Contains(queryStr)
								
								
								
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
			  
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<SysModuleModel> list = await CreateModelListAsync(queryData);
            return  new Tuple<GridPager,List<SysModuleModel>>(pager,list);
        }
		public virtual async Task<List<SysModuleModel>> CreateModelListAsync(IQueryable<SysModule> queryData)
        {

            List<SysModuleModel> modelList = await (from r in queryData
                                              select new SysModuleModel
                                              {
                                                 													Id = r.Id,
													Name = r.Name,
													EnglishName = r.EnglishName,
													ParentId = r.ParentId,
													Url = r.Url,
													Iconic = r.Iconic,
													Sort = r.Sort,
													Remark = r.Remark,
													Enable = r.Enable,
													CreatePerson = r.CreatePerson,
													CreateTime = r.CreateTime,
													IsLast = r.IsLast,
													Version = r.Version,
          

                                              }).ToListAsync();

            return modelList;
        }

		public virtual List<SysModuleModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysModule> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.Name.Contains(queryStr)
								|| a.EnglishName.Contains(queryStr)
								|| a.ParentId.Contains(queryStr)
								|| a.Url.Contains(queryStr)
								|| a.Iconic.Contains(queryStr)
								
								|| a.Remark.Contains(queryStr)
								
								|| a.CreatePerson.Contains(queryStr)
								
								
								
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
			public virtual List<SysModuleModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<SysModuleModel>();
		}
		public virtual List<SysModuleModel> GetListByDepId(ref GridPager pager, string depId,string queryStr)
		{
			return new List<SysModuleModel>();
		}
		
		public virtual List<SysModuleModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<SysModuleModel>();
		}
		public virtual List<SysModuleModel> GetList(string id, string parentId)
        {
			return new List<SysModuleModel>();
		}
        public virtual List<SysModuleModel> CreateModelList(ref IQueryable<SysModule> queryData)
        {

            List<SysModuleModel> modelList = (from r in queryData
                                              select new SysModuleModel
                                              {
													Id = r.Id,
													Name = r.Name,
													EnglishName = r.EnglishName,
													ParentId = r.ParentId,
													Url = r.Url,
													Iconic = r.Iconic,
													Sort = r.Sort,
													Remark = r.Remark,
													Enable = r.Enable,
													CreatePerson = r.CreatePerson,
													CreateTime = r.CreateTime,
													IsLast = r.IsLast,
													Version = r.Version,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, SysModuleModel model)
        {
            try
            {
                SysModule entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new SysModule();
               				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.EnglishName = model.EnglishName;
				entity.ParentId = model.ParentId;
				entity.Url = model.Url;
				entity.Iconic = model.Iconic;
				entity.Sort = model.Sort;
				entity.Remark = model.Remark;
				entity.Enable = model.Enable;
				entity.CreatePerson = model.CreatePerson;
				entity.CreateTime = model.CreateTime;
				entity.IsLast = model.IsLast;
				entity.Version = model.Version;
  

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

		 public virtual async Task<Tuple<ValidationErrors, bool>> CreateAsync(SysModuleModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                SysModule entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                entity = new SysModule();
               				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.EnglishName = model.EnglishName;
				entity.ParentId = model.ParentId;
				entity.Url = model.Url;
				entity.Iconic = model.Iconic;
				entity.Sort = model.Sort;
				entity.Remark = model.Remark;
				entity.Enable = model.Enable;
				entity.CreatePerson = model.CreatePerson;
				entity.CreateTime = model.CreateTime;
				entity.IsLast = model.IsLast;
				entity.Version = model.Version;
  

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
       

        public virtual bool Edit(ref ValidationErrors errors, SysModuleModel model)
        {
            try
            {
                SysModule entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.EnglishName = model.EnglishName;
				entity.ParentId = model.ParentId;
				entity.Url = model.Url;
				entity.Iconic = model.Iconic;
				entity.Sort = model.Sort;
				entity.Remark = model.Remark;
				entity.Enable = model.Enable;
				entity.CreatePerson = model.CreatePerson;
				entity.CreateTime = model.CreateTime;
				entity.IsLast = model.IsLast;
				entity.Version = model.Version;
 


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

      	 public virtual async Task<Tuple<ValidationErrors, bool>> EditAsync(SysModuleModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                SysModule entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity == null)
                {
                     errors.Add(Resource.Disable);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.EnglishName = model.EnglishName;
				entity.ParentId = model.ParentId;
				entity.Url = model.Url;
				entity.Iconic = model.Iconic;
				entity.Sort = model.Sort;
				entity.Remark = model.Remark;
				entity.Enable = model.Enable;
				entity.CreatePerson = model.CreatePerson;
				entity.CreateTime = model.CreateTime;
				entity.IsLast = model.IsLast;
				entity.Version = model.Version;
 

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

        public virtual SysModuleModel GetById(object id)
        {
            if (IsExists(id))
            {
                SysModule entity = m_Rep.GetById(id);
                SysModuleModel model = new SysModuleModel();
                              				model.Id = entity.Id;
				model.Name = entity.Name;
				model.EnglishName = entity.EnglishName;
				model.ParentId = entity.ParentId;
				model.Url = entity.Url;
				model.Iconic = entity.Iconic;
				model.Sort = entity.Sort;
				model.Remark = entity.Remark;
				model.Enable = entity.Enable;
				model.CreatePerson = entity.CreatePerson;
				model.CreateTime = entity.CreateTime;
				model.IsLast = entity.IsLast;
				model.Version = entity.Version;
 
                return model;
            }
            else
            {
                return null;
            }
        }

		 public virtual async Task<SysModuleModel> GetByIdAsync(object id)
        {
            if (IsExists(id))
            {
                SysModule entity = await m_Rep.GetByIdAsync(id);
                SysModuleModel model = new SysModuleModel();
                              				model.Id = entity.Id;
				model.Name = entity.Name;
				model.EnglishName = entity.EnglishName;
				model.ParentId = entity.ParentId;
				model.Url = entity.Url;
				model.Iconic = entity.Iconic;
				model.Sort = entity.Sort;
				model.Remark = entity.Remark;
				model.Enable = entity.Enable;
				model.CreatePerson = entity.CreatePerson;
				model.CreateTime = entity.CreateTime;
				model.IsLast = entity.IsLast;
				model.Version = entity.Version;
 
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
        public virtual bool CheckImportData(string fileName, List<SysModuleModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<SysModuleModel>(x => x.Name, "Name");
				 excelFile.AddMapping<SysModuleModel>(x => x.EnglishName, "EnglishName");
				 excelFile.AddMapping<SysModuleModel>(x => x.ParentId, "ParentId");
				 excelFile.AddMapping<SysModuleModel>(x => x.Url, "Url");
				 excelFile.AddMapping<SysModuleModel>(x => x.Iconic, "Iconic");
				 excelFile.AddMapping<SysModuleModel>(x => x.Sort, "Sort");
				 excelFile.AddMapping<SysModuleModel>(x => x.Remark, "Remark");
				 excelFile.AddMapping<SysModuleModel>(x => x.Enable, "Enable");
				 excelFile.AddMapping<SysModuleModel>(x => x.CreatePerson, "CreatePerson");
				 excelFile.AddMapping<SysModuleModel>(x => x.CreateTime, "CreateTime");
				 excelFile.AddMapping<SysModuleModel>(x => x.IsLast, "IsLast");
				 excelFile.AddMapping<SysModuleModel>(x => x.Version, "Version");
 
            //SheetName
            var excelContent = excelFile.Worksheet<SysModuleModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new SysModuleModel();
						 				  entity.Id = row.Id;
				  entity.Name = row.Name;
				  entity.EnglishName = row.EnglishName;
				  entity.ParentId = row.ParentId;
				  entity.Url = row.Url;
				  entity.Iconic = row.Iconic;
				  entity.Sort = row.Sort;
				  entity.Remark = row.Remark;
				  entity.Enable = row.Enable;
				  entity.CreatePerson = row.CreatePerson;
				  entity.CreateTime = row.CreateTime;
				  entity.IsLast = row.IsLast;
				  entity.Version = row.Version;
 
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
        public virtual void SaveImportData(IEnumerable<SysModuleModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        SysModule entity = new SysModule();
                       						entity.Id = ResultHelper.NewId;
						entity.Name = model.Name;
						entity.EnglishName = model.EnglishName;
						entity.ParentId = model.ParentId;
						entity.Url = model.Url;
						entity.Iconic = model.Iconic;
						entity.Sort = model.Sort;
						entity.Remark = model.Remark;
						entity.Enable = model.Enable;
						entity.CreatePerson = model.CreatePerson;
						entity.CreateTime = ResultHelper.NowTime;
						entity.IsLast = model.IsLast;
						entity.Version = model.Version;
 
                        db.SysModule.Add(entity);
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
