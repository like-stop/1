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
	public partial class SysExceptionBLL: Virtual_SysExceptionBLL,ISysExceptionBLL
	{
        

	}
	public class Virtual_SysExceptionBLL
	{
        [Dependency]
        public ISysExceptionRepository m_Rep { get; set; }
		
		[Dependency]
        public ISysUserRepository m_userRep { get; set; }

		[Dependency]
        public ISysStructRepository m_structRep { get; set; }

		public virtual async Task<Tuple<GridPager,List<SysExceptionModel>>> GetListAsync(GridPager pager, string queryStr)
        {

            IQueryable<SysException> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.HelpLink.Contains(queryStr)
								|| a.Message.Contains(queryStr)
								|| a.Source.Contains(queryStr)
								|| a.StackTrace.Contains(queryStr)
								|| a.TargetSite.Contains(queryStr)
								|| a.Data.Contains(queryStr)
								
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
			  
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<SysExceptionModel> list = await CreateModelListAsync(queryData);
            return  new Tuple<GridPager,List<SysExceptionModel>>(pager,list);
        }
		public virtual async Task<List<SysExceptionModel>> CreateModelListAsync(IQueryable<SysException> queryData)
        {

            List<SysExceptionModel> modelList = await (from r in queryData
                                              select new SysExceptionModel
                                              {
                                                 													Id = r.Id,
													HelpLink = r.HelpLink,
													Message = r.Message,
													Source = r.Source,
													StackTrace = r.StackTrace,
													TargetSite = r.TargetSite,
													Data = r.Data,
													CreateTime = r.CreateTime,
          

                                              }).ToListAsync();

            return modelList;
        }

		public virtual List<SysExceptionModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysException> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.HelpLink.Contains(queryStr)
								|| a.Message.Contains(queryStr)
								|| a.Source.Contains(queryStr)
								|| a.StackTrace.Contains(queryStr)
								|| a.TargetSite.Contains(queryStr)
								|| a.Data.Contains(queryStr)
								
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
			public virtual List<SysExceptionModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<SysExceptionModel>();
		}
		public virtual List<SysExceptionModel> GetListByDepId(ref GridPager pager, string depId,string queryStr)
		{
			return new List<SysExceptionModel>();
		}
		
		public virtual List<SysExceptionModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<SysExceptionModel>();
		}
		public virtual List<SysExceptionModel> GetList(string id, string parentId)
        {
			return new List<SysExceptionModel>();
		}
        public virtual List<SysExceptionModel> CreateModelList(ref IQueryable<SysException> queryData)
        {

            List<SysExceptionModel> modelList = (from r in queryData
                                              select new SysExceptionModel
                                              {
													Id = r.Id,
													HelpLink = r.HelpLink,
													Message = r.Message,
													Source = r.Source,
													StackTrace = r.StackTrace,
													TargetSite = r.TargetSite,
													Data = r.Data,
													CreateTime = r.CreateTime,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, SysExceptionModel model)
        {
            try
            {
                SysException entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new SysException();
               				entity.Id = model.Id;
				entity.HelpLink = model.HelpLink;
				entity.Message = model.Message;
				entity.Source = model.Source;
				entity.StackTrace = model.StackTrace;
				entity.TargetSite = model.TargetSite;
				entity.Data = model.Data;
				entity.CreateTime = model.CreateTime;
  

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

		 public virtual async Task<Tuple<ValidationErrors, bool>> CreateAsync(SysExceptionModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                SysException entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                entity = new SysException();
               				entity.Id = model.Id;
				entity.HelpLink = model.HelpLink;
				entity.Message = model.Message;
				entity.Source = model.Source;
				entity.StackTrace = model.StackTrace;
				entity.TargetSite = model.TargetSite;
				entity.Data = model.Data;
				entity.CreateTime = model.CreateTime;
  

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
       

        public virtual bool Edit(ref ValidationErrors errors, SysExceptionModel model)
        {
            try
            {
                SysException entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.HelpLink = model.HelpLink;
				entity.Message = model.Message;
				entity.Source = model.Source;
				entity.StackTrace = model.StackTrace;
				entity.TargetSite = model.TargetSite;
				entity.Data = model.Data;
				entity.CreateTime = model.CreateTime;
 


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

      	 public virtual async Task<Tuple<ValidationErrors, bool>> EditAsync(SysExceptionModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                SysException entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity == null)
                {
                     errors.Add(Resource.Disable);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                				entity.Id = model.Id;
				entity.HelpLink = model.HelpLink;
				entity.Message = model.Message;
				entity.Source = model.Source;
				entity.StackTrace = model.StackTrace;
				entity.TargetSite = model.TargetSite;
				entity.Data = model.Data;
				entity.CreateTime = model.CreateTime;
 

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

        public virtual SysExceptionModel GetById(object id)
        {
            if (IsExists(id))
            {
                SysException entity = m_Rep.GetById(id);
                SysExceptionModel model = new SysExceptionModel();
                              				model.Id = entity.Id;
				model.HelpLink = entity.HelpLink;
				model.Message = entity.Message;
				model.Source = entity.Source;
				model.StackTrace = entity.StackTrace;
				model.TargetSite = entity.TargetSite;
				model.Data = entity.Data;
				model.CreateTime = entity.CreateTime;
 
                return model;
            }
            else
            {
                return null;
            }
        }

		 public virtual async Task<SysExceptionModel> GetByIdAsync(object id)
        {
            if (IsExists(id))
            {
                SysException entity = await m_Rep.GetByIdAsync(id);
                SysExceptionModel model = new SysExceptionModel();
                              				model.Id = entity.Id;
				model.HelpLink = entity.HelpLink;
				model.Message = entity.Message;
				model.Source = entity.Source;
				model.StackTrace = entity.StackTrace;
				model.TargetSite = entity.TargetSite;
				model.Data = entity.Data;
				model.CreateTime = entity.CreateTime;
 
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
        public virtual bool CheckImportData(string fileName, List<SysExceptionModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<SysExceptionModel>(x => x.HelpLink, "HelpLink");
				 excelFile.AddMapping<SysExceptionModel>(x => x.Message, "Message");
				 excelFile.AddMapping<SysExceptionModel>(x => x.Source, "Source");
				 excelFile.AddMapping<SysExceptionModel>(x => x.StackTrace, "StackTrace");
				 excelFile.AddMapping<SysExceptionModel>(x => x.TargetSite, "TargetSite");
				 excelFile.AddMapping<SysExceptionModel>(x => x.Data, "Data");
				 excelFile.AddMapping<SysExceptionModel>(x => x.CreateTime, "CreateTime");
 
            //SheetName
            var excelContent = excelFile.Worksheet<SysExceptionModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new SysExceptionModel();
						 				  entity.Id = row.Id;
				  entity.HelpLink = row.HelpLink;
				  entity.Message = row.Message;
				  entity.Source = row.Source;
				  entity.StackTrace = row.StackTrace;
				  entity.TargetSite = row.TargetSite;
				  entity.Data = row.Data;
				  entity.CreateTime = row.CreateTime;
 
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
        public virtual void SaveImportData(IEnumerable<SysExceptionModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        SysException entity = new SysException();
                       						entity.Id = ResultHelper.NewId;
						entity.HelpLink = model.HelpLink;
						entity.Message = model.Message;
						entity.Source = model.Source;
						entity.StackTrace = model.StackTrace;
						entity.TargetSite = model.TargetSite;
						entity.Data = model.Data;
						entity.CreateTime = ResultHelper.NowTime;
 
                        db.SysException.Add(entity);
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
