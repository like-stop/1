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
	public partial class WC_OfficalAccountsBLL: Virtual_WC_OfficalAccountsBLL,IWC_OfficalAccountsBLL
	{
        

	}
	public class Virtual_WC_OfficalAccountsBLL
	{
        [Dependency]
        public IWC_OfficalAccountsRepository m_Rep { get; set; }
		
		[Dependency]
        public ISysUserRepository m_userRep { get; set; }

		[Dependency]
        public ISysStructRepository m_structRep { get; set; }

		public virtual async Task<Tuple<GridPager,List<WC_OfficalAccountsModel>>> GetListAsync(GridPager pager, string queryStr)
        {

            IQueryable<WC_OfficalAccounts> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.OfficalId.Contains(queryStr)
								|| a.OfficalName.Contains(queryStr)
								|| a.OfficalCode.Contains(queryStr)
								|| a.OfficalPhoto.Contains(queryStr)
								|| a.OfficalKey.Contains(queryStr)
								|| a.ApiUrl.Contains(queryStr)
								|| a.Token.Contains(queryStr)
								|| a.AppId.Contains(queryStr)
								|| a.AppSecret.Contains(queryStr)
								|| a.AccessToken.Contains(queryStr)
								|| a.Remark.Contains(queryStr)
								
								
								
								
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
            List<WC_OfficalAccountsModel> list = await CreateModelListAsync(queryData);
            return  new Tuple<GridPager,List<WC_OfficalAccountsModel>>(pager,list);
        }
		public virtual async Task<List<WC_OfficalAccountsModel>> CreateModelListAsync(IQueryable<WC_OfficalAccounts> queryData)
        {

            List<WC_OfficalAccountsModel> modelList = await (from r in queryData
                                              select new WC_OfficalAccountsModel
                                              {
                                                 													Id = r.Id,
													OfficalId = r.OfficalId,
													OfficalName = r.OfficalName,
													OfficalCode = r.OfficalCode,
													OfficalPhoto = r.OfficalPhoto,
													OfficalKey = r.OfficalKey,
													ApiUrl = r.ApiUrl,
													Token = r.Token,
													AppId = r.AppId,
													AppSecret = r.AppSecret,
													AccessToken = r.AccessToken,
													Remark = r.Remark,
													Enable = r.Enable,
													IsDefault = r.IsDefault,
													Category = r.Category,
													CreateTime = r.CreateTime,
													CreateBy = r.CreateBy,
													ModifyTime = r.ModifyTime,
													ModifyBy = r.ModifyBy,
          

                                              }).ToListAsync();

            return modelList;
        }

		public virtual List<WC_OfficalAccountsModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<WC_OfficalAccounts> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.OfficalId.Contains(queryStr)
								|| a.OfficalName.Contains(queryStr)
								|| a.OfficalCode.Contains(queryStr)
								|| a.OfficalPhoto.Contains(queryStr)
								|| a.OfficalKey.Contains(queryStr)
								|| a.ApiUrl.Contains(queryStr)
								|| a.Token.Contains(queryStr)
								|| a.AppId.Contains(queryStr)
								|| a.AppSecret.Contains(queryStr)
								|| a.AccessToken.Contains(queryStr)
								|| a.Remark.Contains(queryStr)
								
								
								
								
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
			public virtual List<WC_OfficalAccountsModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<WC_OfficalAccountsModel>();
		}
		public virtual List<WC_OfficalAccountsModel> GetListByDepId(ref GridPager pager, string depId,string queryStr)
		{
			return new List<WC_OfficalAccountsModel>();
		}
		
		public virtual List<WC_OfficalAccountsModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<WC_OfficalAccountsModel>();
		}
		public virtual List<WC_OfficalAccountsModel> GetList(string id, string parentId)
        {
			return new List<WC_OfficalAccountsModel>();
		}
        public virtual List<WC_OfficalAccountsModel> CreateModelList(ref IQueryable<WC_OfficalAccounts> queryData)
        {

            List<WC_OfficalAccountsModel> modelList = (from r in queryData
                                              select new WC_OfficalAccountsModel
                                              {
													Id = r.Id,
													OfficalId = r.OfficalId,
													OfficalName = r.OfficalName,
													OfficalCode = r.OfficalCode,
													OfficalPhoto = r.OfficalPhoto,
													OfficalKey = r.OfficalKey,
													ApiUrl = r.ApiUrl,
													Token = r.Token,
													AppId = r.AppId,
													AppSecret = r.AppSecret,
													AccessToken = r.AccessToken,
													Remark = r.Remark,
													Enable = r.Enable,
													IsDefault = r.IsDefault,
													Category = r.Category,
													CreateTime = r.CreateTime,
													CreateBy = r.CreateBy,
													ModifyTime = r.ModifyTime,
													ModifyBy = r.ModifyBy,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, WC_OfficalAccountsModel model)
        {
            try
            {
                WC_OfficalAccounts entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new WC_OfficalAccounts();
               				entity.Id = model.Id;
				entity.OfficalId = model.OfficalId;
				entity.OfficalName = model.OfficalName;
				entity.OfficalCode = model.OfficalCode;
				entity.OfficalPhoto = model.OfficalPhoto;
				entity.OfficalKey = model.OfficalKey;
				entity.ApiUrl = model.ApiUrl;
				entity.Token = model.Token;
				entity.AppId = model.AppId;
				entity.AppSecret = model.AppSecret;
				entity.AccessToken = model.AccessToken;
				entity.Remark = model.Remark;
				entity.Enable = model.Enable;
				entity.IsDefault = model.IsDefault;
				entity.Category = model.Category;
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

		 public virtual async Task<Tuple<ValidationErrors, bool>> CreateAsync(WC_OfficalAccountsModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                WC_OfficalAccounts entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                entity = new WC_OfficalAccounts();
               				entity.Id = model.Id;
				entity.OfficalId = model.OfficalId;
				entity.OfficalName = model.OfficalName;
				entity.OfficalCode = model.OfficalCode;
				entity.OfficalPhoto = model.OfficalPhoto;
				entity.OfficalKey = model.OfficalKey;
				entity.ApiUrl = model.ApiUrl;
				entity.Token = model.Token;
				entity.AppId = model.AppId;
				entity.AppSecret = model.AppSecret;
				entity.AccessToken = model.AccessToken;
				entity.Remark = model.Remark;
				entity.Enable = model.Enable;
				entity.IsDefault = model.IsDefault;
				entity.Category = model.Category;
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
       

        public virtual bool Edit(ref ValidationErrors errors, WC_OfficalAccountsModel model)
        {
            try
            {
                WC_OfficalAccounts entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.OfficalId = model.OfficalId;
				entity.OfficalName = model.OfficalName;
				entity.OfficalCode = model.OfficalCode;
				entity.OfficalPhoto = model.OfficalPhoto;
				entity.OfficalKey = model.OfficalKey;
				entity.ApiUrl = model.ApiUrl;
				entity.Token = model.Token;
				entity.AppId = model.AppId;
				entity.AppSecret = model.AppSecret;
				entity.AccessToken = model.AccessToken;
				entity.Remark = model.Remark;
				entity.Enable = model.Enable;
				entity.IsDefault = model.IsDefault;
				entity.Category = model.Category;
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

      	 public virtual async Task<Tuple<ValidationErrors, bool>> EditAsync(WC_OfficalAccountsModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                WC_OfficalAccounts entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity == null)
                {
                     errors.Add(Resource.Disable);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                				entity.Id = model.Id;
				entity.OfficalId = model.OfficalId;
				entity.OfficalName = model.OfficalName;
				entity.OfficalCode = model.OfficalCode;
				entity.OfficalPhoto = model.OfficalPhoto;
				entity.OfficalKey = model.OfficalKey;
				entity.ApiUrl = model.ApiUrl;
				entity.Token = model.Token;
				entity.AppId = model.AppId;
				entity.AppSecret = model.AppSecret;
				entity.AccessToken = model.AccessToken;
				entity.Remark = model.Remark;
				entity.Enable = model.Enable;
				entity.IsDefault = model.IsDefault;
				entity.Category = model.Category;
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

        public virtual WC_OfficalAccountsModel GetById(object id)
        {
            if (IsExists(id))
            {
                WC_OfficalAccounts entity = m_Rep.GetById(id);
                WC_OfficalAccountsModel model = new WC_OfficalAccountsModel();
                              				model.Id = entity.Id;
				model.OfficalId = entity.OfficalId;
				model.OfficalName = entity.OfficalName;
				model.OfficalCode = entity.OfficalCode;
				model.OfficalPhoto = entity.OfficalPhoto;
				model.OfficalKey = entity.OfficalKey;
				model.ApiUrl = entity.ApiUrl;
				model.Token = entity.Token;
				model.AppId = entity.AppId;
				model.AppSecret = entity.AppSecret;
				model.AccessToken = entity.AccessToken;
				model.Remark = entity.Remark;
				model.Enable = entity.Enable;
				model.IsDefault = entity.IsDefault;
				model.Category = entity.Category;
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

		 public virtual async Task<WC_OfficalAccountsModel> GetByIdAsync(object id)
        {
            if (IsExists(id))
            {
                WC_OfficalAccounts entity = await m_Rep.GetByIdAsync(id);
                WC_OfficalAccountsModel model = new WC_OfficalAccountsModel();
                              				model.Id = entity.Id;
				model.OfficalId = entity.OfficalId;
				model.OfficalName = entity.OfficalName;
				model.OfficalCode = entity.OfficalCode;
				model.OfficalPhoto = entity.OfficalPhoto;
				model.OfficalKey = entity.OfficalKey;
				model.ApiUrl = entity.ApiUrl;
				model.Token = entity.Token;
				model.AppId = entity.AppId;
				model.AppSecret = entity.AppSecret;
				model.AccessToken = entity.AccessToken;
				model.Remark = entity.Remark;
				model.Enable = entity.Enable;
				model.IsDefault = entity.IsDefault;
				model.Category = entity.Category;
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
        public virtual bool CheckImportData(string fileName, List<WC_OfficalAccountsModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.OfficalId, "OfficalId");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.OfficalName, "OfficalName");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.OfficalCode, "OfficalCode");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.OfficalPhoto, "OfficalPhoto");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.OfficalKey, "OfficalKey");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.ApiUrl, "ApiUrl");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.Token, "Token");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.AppId, "AppId");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.AppSecret, "AppSecret");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.AccessToken, "AccessToken");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.Remark, "Remark");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.Enable, "Enable");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.IsDefault, "IsDefault");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.Category, "Category");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.CreateTime, "CreateTime");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.CreateBy, "CreateBy");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.ModifyTime, "ModifyTime");
				 excelFile.AddMapping<WC_OfficalAccountsModel>(x => x.ModifyBy, "ModifyBy");
 
            //SheetName
            var excelContent = excelFile.Worksheet<WC_OfficalAccountsModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new WC_OfficalAccountsModel();
						 				  entity.Id = row.Id;
				  entity.OfficalId = row.OfficalId;
				  entity.OfficalName = row.OfficalName;
				  entity.OfficalCode = row.OfficalCode;
				  entity.OfficalPhoto = row.OfficalPhoto;
				  entity.OfficalKey = row.OfficalKey;
				  entity.ApiUrl = row.ApiUrl;
				  entity.Token = row.Token;
				  entity.AppId = row.AppId;
				  entity.AppSecret = row.AppSecret;
				  entity.AccessToken = row.AccessToken;
				  entity.Remark = row.Remark;
				  entity.Enable = row.Enable;
				  entity.IsDefault = row.IsDefault;
				  entity.Category = row.Category;
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
        public virtual void SaveImportData(IEnumerable<WC_OfficalAccountsModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        WC_OfficalAccounts entity = new WC_OfficalAccounts();
                       						entity.Id = ResultHelper.NewId;
						entity.OfficalId = model.OfficalId;
						entity.OfficalName = model.OfficalName;
						entity.OfficalCode = model.OfficalCode;
						entity.OfficalPhoto = model.OfficalPhoto;
						entity.OfficalKey = model.OfficalKey;
						entity.ApiUrl = model.ApiUrl;
						entity.Token = model.Token;
						entity.AppId = model.AppId;
						entity.AppSecret = model.AppSecret;
						entity.AccessToken = model.AccessToken;
						entity.Remark = model.Remark;
						entity.Enable = model.Enable;
						entity.IsDefault = model.IsDefault;
						entity.Category = model.Category;
						entity.CreateTime = ResultHelper.NowTime;
						entity.CreateBy = model.CreateBy;
						entity.ModifyTime = model.ModifyTime;
						entity.ModifyBy = model.ModifyBy;
 
                        db.WC_OfficalAccounts.Add(entity);
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
