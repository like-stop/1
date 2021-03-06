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
using Apps.IDAL.WC;
using Apps.Models.WC;
using Apps.IBLL.WC;
using Apps.IDAL.Sys;
namespace Apps.BLL.WC
{
	public partial class WC_ResponseLogBLL: Virtual_WC_ResponseLogBLL,IWC_ResponseLogBLL
	{
        

	}
	public class Virtual_WC_ResponseLogBLL
	{
        [Dependency]
        public IWC_ResponseLogRepository m_Rep { get; set; }
		
		[Dependency]
        public ISysUserRepository m_userRep { get; set; }

		[Dependency]
        public ISysStructRepository m_structRep { get; set; }

		public virtual async Task<Tuple<GridPager,List<WC_ResponseLogModel>>> GetListAsync(GridPager pager, string queryStr)
        {

            IQueryable<WC_ResponseLog> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.OfficalAccountId.Contains(queryStr)
								|| a.OpenId.Contains(queryStr)
								
								|| a.RequestContent.Contains(queryStr)
								
								|| a.ResponseContent.Contains(queryStr)
								
								|| a.CreateBy.Contains(queryStr)
								
								|| a.ModifyBy.Contains(queryStr)
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
			  
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<WC_ResponseLogModel> list = await CreateModelListAsync(queryData);
            return  new Tuple<GridPager,List<WC_ResponseLogModel>>(pager,list);
        }
		public virtual async Task<List<WC_ResponseLogModel>> CreateModelListAsync(IQueryable<WC_ResponseLog> queryData)
        {

            List<WC_ResponseLogModel> modelList = await (from r in queryData
                                              select new WC_ResponseLogModel
                                              {
                                                 													Id = r.Id,
													OfficalAccountId = r.OfficalAccountId,
													OpenId = r.OpenId,
													RequestType = r.RequestType,
													RequestContent = r.RequestContent,
													ResponseType = r.ResponseType,
													ResponseContent = r.ResponseContent,
													CreateTime = r.CreateTime,
													CreateBy = r.CreateBy,
													ModifyTime = r.ModifyTime,
													ModifyBy = r.ModifyBy,
          

                                              }).ToListAsync();

            return modelList;
        }

		public virtual List<WC_ResponseLogModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<WC_ResponseLog> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.OfficalAccountId.Contains(queryStr)
								|| a.OpenId.Contains(queryStr)
								
								|| a.RequestContent.Contains(queryStr)
								
								|| a.ResponseContent.Contains(queryStr)
								
								|| a.CreateBy.Contains(queryStr)
								
								|| a.ModifyBy.Contains(queryStr)
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
			public virtual List<WC_ResponseLogModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<WC_ResponseLogModel>();
		}
		public virtual List<WC_ResponseLogModel> GetListByDepId(ref GridPager pager, string depId,string queryStr)
		{
			return new List<WC_ResponseLogModel>();
		}
		
		public virtual List<WC_ResponseLogModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<WC_ResponseLogModel>();
		}
		public virtual List<WC_ResponseLogModel> GetList(string id, string parentId)
        {
			return new List<WC_ResponseLogModel>();
		}
        public virtual List<WC_ResponseLogModel> CreateModelList(ref IQueryable<WC_ResponseLog> queryData)
        {

            List<WC_ResponseLogModel> modelList = (from r in queryData
                                              select new WC_ResponseLogModel
                                              {
													Id = r.Id,
													OfficalAccountId = r.OfficalAccountId,
													OpenId = r.OpenId,
													RequestType = r.RequestType,
													RequestContent = r.RequestContent,
													ResponseType = r.ResponseType,
													ResponseContent = r.ResponseContent,
													CreateTime = r.CreateTime,
													CreateBy = r.CreateBy,
													ModifyTime = r.ModifyTime,
													ModifyBy = r.ModifyBy,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, WC_ResponseLogModel model)
        {
            try
            {
                WC_ResponseLog entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new WC_ResponseLog();
               				entity.Id = model.Id;
				entity.OfficalAccountId = model.OfficalAccountId;
				entity.OpenId = model.OpenId;
				entity.RequestType = model.RequestType;
				entity.RequestContent = model.RequestContent;
				entity.ResponseType = model.ResponseType;
				entity.ResponseContent = model.ResponseContent;
				entity.CreateTime = model.CreateTime;
				entity.CreateBy = model.CreateBy;
				entity.ModifyTime = model.ModifyTime;
				entity.ModifyBy = model.ModifyBy;
  

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

		 public virtual async Task<Tuple<ValidationErrors, bool>> CreateAsync(WC_ResponseLogModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                WC_ResponseLog entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                entity = new WC_ResponseLog();
               				entity.Id = model.Id;
				entity.OfficalAccountId = model.OfficalAccountId;
				entity.OpenId = model.OpenId;
				entity.RequestType = model.RequestType;
				entity.RequestContent = model.RequestContent;
				entity.ResponseType = model.ResponseType;
				entity.ResponseContent = model.ResponseContent;
				entity.CreateTime = model.CreateTime;
				entity.CreateBy = model.CreateBy;
				entity.ModifyTime = model.ModifyTime;
				entity.ModifyBy = model.ModifyBy;
  

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
       

        public virtual bool Edit(ref ValidationErrors errors, WC_ResponseLogModel model)
        {
            try
            {
                WC_ResponseLog entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.OfficalAccountId = model.OfficalAccountId;
				entity.OpenId = model.OpenId;
				entity.RequestType = model.RequestType;
				entity.RequestContent = model.RequestContent;
				entity.ResponseType = model.ResponseType;
				entity.ResponseContent = model.ResponseContent;
				entity.CreateTime = model.CreateTime;
				entity.CreateBy = model.CreateBy;
				entity.ModifyTime = model.ModifyTime;
				entity.ModifyBy = model.ModifyBy;
 


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

      	 public virtual async Task<Tuple<ValidationErrors, bool>> EditAsync(WC_ResponseLogModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                WC_ResponseLog entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity == null)
                {
                     errors.Add(Resource.Disable);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                				entity.Id = model.Id;
				entity.OfficalAccountId = model.OfficalAccountId;
				entity.OpenId = model.OpenId;
				entity.RequestType = model.RequestType;
				entity.RequestContent = model.RequestContent;
				entity.ResponseType = model.ResponseType;
				entity.ResponseContent = model.ResponseContent;
				entity.CreateTime = model.CreateTime;
				entity.CreateBy = model.CreateBy;
				entity.ModifyTime = model.ModifyTime;
				entity.ModifyBy = model.ModifyBy;
 

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

        public virtual WC_ResponseLogModel GetById(object id)
        {
            if (IsExists(id))
            {
                WC_ResponseLog entity = m_Rep.GetById(id);
                WC_ResponseLogModel model = new WC_ResponseLogModel();
                              				model.Id = entity.Id;
				model.OfficalAccountId = entity.OfficalAccountId;
				model.OpenId = entity.OpenId;
				model.RequestType = entity.RequestType;
				model.RequestContent = entity.RequestContent;
				model.ResponseType = entity.ResponseType;
				model.ResponseContent = entity.ResponseContent;
				model.CreateTime = entity.CreateTime;
				model.CreateBy = entity.CreateBy;
				model.ModifyTime = entity.ModifyTime;
				model.ModifyBy = entity.ModifyBy;
 
                return model;
            }
            else
            {
                return null;
            }
        }

		 public virtual async Task<WC_ResponseLogModel> GetByIdAsync(object id)
        {
            if (IsExists(id))
            {
                WC_ResponseLog entity = await m_Rep.GetByIdAsync(id);
                WC_ResponseLogModel model = new WC_ResponseLogModel();
                              				model.Id = entity.Id;
				model.OfficalAccountId = entity.OfficalAccountId;
				model.OpenId = entity.OpenId;
				model.RequestType = entity.RequestType;
				model.RequestContent = entity.RequestContent;
				model.ResponseType = entity.ResponseType;
				model.ResponseContent = entity.ResponseContent;
				model.CreateTime = entity.CreateTime;
				model.CreateBy = entity.CreateBy;
				model.ModifyTime = entity.ModifyTime;
				model.ModifyBy = entity.ModifyBy;
 
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
        public virtual bool CheckImportData(string fileName, List<WC_ResponseLogModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<WC_ResponseLogModel>(x => x.OfficalAccountId, "OfficalAccountId");
				 excelFile.AddMapping<WC_ResponseLogModel>(x => x.OpenId, "OpenId");
				 excelFile.AddMapping<WC_ResponseLogModel>(x => x.RequestType, "RequestType");
				 excelFile.AddMapping<WC_ResponseLogModel>(x => x.RequestContent, "RequestContent");
				 excelFile.AddMapping<WC_ResponseLogModel>(x => x.ResponseType, "ResponseType");
				 excelFile.AddMapping<WC_ResponseLogModel>(x => x.ResponseContent, "ResponseContent");
				 excelFile.AddMapping<WC_ResponseLogModel>(x => x.CreateTime, "CreateTime");
				 excelFile.AddMapping<WC_ResponseLogModel>(x => x.CreateBy, "CreateBy");
				 excelFile.AddMapping<WC_ResponseLogModel>(x => x.ModifyTime, "ModifyTime");
				 excelFile.AddMapping<WC_ResponseLogModel>(x => x.ModifyBy, "ModifyBy");
 
            //SheetName
            var excelContent = excelFile.Worksheet<WC_ResponseLogModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new WC_ResponseLogModel();
						 				  entity.Id = row.Id;
				  entity.OfficalAccountId = row.OfficalAccountId;
				  entity.OpenId = row.OpenId;
				  entity.RequestType = row.RequestType;
				  entity.RequestContent = row.RequestContent;
				  entity.ResponseType = row.ResponseType;
				  entity.ResponseContent = row.ResponseContent;
				  entity.CreateTime = row.CreateTime;
				  entity.CreateBy = row.CreateBy;
				  entity.ModifyTime = row.ModifyTime;
				  entity.ModifyBy = row.ModifyBy;
 
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
        public virtual void SaveImportData(IEnumerable<WC_ResponseLogModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        WC_ResponseLog entity = new WC_ResponseLog();
                       						entity.Id = ResultHelper.NewId;
						entity.OfficalAccountId = model.OfficalAccountId;
						entity.OpenId = model.OpenId;
						entity.RequestType = model.RequestType;
						entity.RequestContent = model.RequestContent;
						entity.ResponseType = model.ResponseType;
						entity.ResponseContent = model.ResponseContent;
						entity.CreateTime = ResultHelper.NowTime;
						entity.CreateBy = model.CreateBy;
						entity.ModifyTime = model.ModifyTime;
						entity.ModifyBy = model.ModifyBy;
 
                        db.WC_ResponseLog.Add(entity);
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
