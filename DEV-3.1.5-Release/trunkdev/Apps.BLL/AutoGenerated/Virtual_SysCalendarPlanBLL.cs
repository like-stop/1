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
	public partial class SysCalendarPlanBLL: Virtual_SysCalendarPlanBLL,ISysCalendarPlanBLL
	{
        

	}
	public class Virtual_SysCalendarPlanBLL
	{
        [Dependency]
        public ISysCalendarPlanRepository m_Rep { get; set; }
		
		[Dependency]
        public ISysUserRepository m_userRep { get; set; }

		[Dependency]
        public ISysStructRepository m_structRep { get; set; }

		public virtual async Task<Tuple<GridPager,List<SysCalendarPlanModel>>> GetListAsync(GridPager pager, string queryStr)
        {

            IQueryable<SysCalendarPlan> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.Title.Contains(queryStr)
								|| a.PlanContent.Contains(queryStr)
								
								
								
								|| a.Url.Contains(queryStr)
								|| a.Color.Contains(queryStr)
								|| a.TextColor.Contains(queryStr)
								|| a.SysUserId.Contains(queryStr)
								|| a.Editable.Contains(queryStr)
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
			  
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<SysCalendarPlanModel> list = await CreateModelListAsync(queryData);
            return  new Tuple<GridPager,List<SysCalendarPlanModel>>(pager,list);
        }
		public virtual async Task<List<SysCalendarPlanModel>> CreateModelListAsync(IQueryable<SysCalendarPlan> queryData)
        {

            List<SysCalendarPlanModel> modelList = await (from r in queryData
                                              select new SysCalendarPlanModel
                                              {
                                                 													Id = r.Id,
													Title = r.Title,
													PlanContent = r.PlanContent,
													BeginDate = r.BeginDate,
													EndDate = r.EndDate,
													CreateTime = r.CreateTime,
													Url = r.Url,
													Color = r.Color,
													TextColor = r.TextColor,
													SysUserId = r.SysUserId,
													Editable = r.Editable,
          

                                              }).ToListAsync();

            return modelList;
        }

		public virtual List<SysCalendarPlanModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysCalendarPlan> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.Title.Contains(queryStr)
								|| a.PlanContent.Contains(queryStr)
								
								
								
								|| a.Url.Contains(queryStr)
								|| a.Color.Contains(queryStr)
								|| a.TextColor.Contains(queryStr)
								|| a.SysUserId.Contains(queryStr)
								|| a.Editable.Contains(queryStr)
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
			public virtual List<SysCalendarPlanModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<SysCalendarPlanModel>();
		}
		public virtual List<SysCalendarPlanModel> GetListByDepId(ref GridPager pager, string depId,string queryStr)
		{
			return new List<SysCalendarPlanModel>();
		}
		
		public virtual List<SysCalendarPlanModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<SysCalendarPlanModel>();
		}
		public virtual List<SysCalendarPlanModel> GetList(string id, string parentId)
        {
			return new List<SysCalendarPlanModel>();
		}
        public virtual List<SysCalendarPlanModel> CreateModelList(ref IQueryable<SysCalendarPlan> queryData)
        {

            List<SysCalendarPlanModel> modelList = (from r in queryData
                                              select new SysCalendarPlanModel
                                              {
													Id = r.Id,
													Title = r.Title,
													PlanContent = r.PlanContent,
													BeginDate = r.BeginDate,
													EndDate = r.EndDate,
													CreateTime = r.CreateTime,
													Url = r.Url,
													Color = r.Color,
													TextColor = r.TextColor,
													SysUserId = r.SysUserId,
													Editable = r.Editable,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, SysCalendarPlanModel model)
        {
            try
            {
                SysCalendarPlan entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new SysCalendarPlan();
               				entity.Id = model.Id;
				entity.Title = model.Title;
				entity.PlanContent = model.PlanContent;
				entity.BeginDate = model.BeginDate;
				entity.EndDate = model.EndDate;
				entity.CreateTime = model.CreateTime;
				entity.Url = model.Url;
				entity.Color = model.Color;
				entity.TextColor = model.TextColor;
				entity.SysUserId = model.SysUserId;
				entity.Editable = model.Editable;
  

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

		 public virtual async Task<Tuple<ValidationErrors, bool>> CreateAsync(SysCalendarPlanModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                SysCalendarPlan entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                entity = new SysCalendarPlan();
               				entity.Id = model.Id;
				entity.Title = model.Title;
				entity.PlanContent = model.PlanContent;
				entity.BeginDate = model.BeginDate;
				entity.EndDate = model.EndDate;
				entity.CreateTime = model.CreateTime;
				entity.Url = model.Url;
				entity.Color = model.Color;
				entity.TextColor = model.TextColor;
				entity.SysUserId = model.SysUserId;
				entity.Editable = model.Editable;
  

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
       

        public virtual bool Edit(ref ValidationErrors errors, SysCalendarPlanModel model)
        {
            try
            {
                SysCalendarPlan entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.Title = model.Title;
				entity.PlanContent = model.PlanContent;
				entity.BeginDate = model.BeginDate;
				entity.EndDate = model.EndDate;
				entity.CreateTime = model.CreateTime;
				entity.Url = model.Url;
				entity.Color = model.Color;
				entity.TextColor = model.TextColor;
				entity.SysUserId = model.SysUserId;
				entity.Editable = model.Editable;
 


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

      	 public virtual async Task<Tuple<ValidationErrors, bool>> EditAsync(SysCalendarPlanModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                SysCalendarPlan entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity == null)
                {
                     errors.Add(Resource.Disable);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                				entity.Id = model.Id;
				entity.Title = model.Title;
				entity.PlanContent = model.PlanContent;
				entity.BeginDate = model.BeginDate;
				entity.EndDate = model.EndDate;
				entity.CreateTime = model.CreateTime;
				entity.Url = model.Url;
				entity.Color = model.Color;
				entity.TextColor = model.TextColor;
				entity.SysUserId = model.SysUserId;
				entity.Editable = model.Editable;
 

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

        public virtual SysCalendarPlanModel GetById(object id)
        {
            if (IsExists(id))
            {
                SysCalendarPlan entity = m_Rep.GetById(id);
                SysCalendarPlanModel model = new SysCalendarPlanModel();
                              				model.Id = entity.Id;
				model.Title = entity.Title;
				model.PlanContent = entity.PlanContent;
				model.BeginDate = entity.BeginDate;
				model.EndDate = entity.EndDate;
				model.CreateTime = entity.CreateTime;
				model.Url = entity.Url;
				model.Color = entity.Color;
				model.TextColor = entity.TextColor;
				model.SysUserId = entity.SysUserId;
				model.Editable = entity.Editable;
 
                return model;
            }
            else
            {
                return null;
            }
        }

		 public virtual async Task<SysCalendarPlanModel> GetByIdAsync(object id)
        {
            if (IsExists(id))
            {
                SysCalendarPlan entity = await m_Rep.GetByIdAsync(id);
                SysCalendarPlanModel model = new SysCalendarPlanModel();
                              				model.Id = entity.Id;
				model.Title = entity.Title;
				model.PlanContent = entity.PlanContent;
				model.BeginDate = entity.BeginDate;
				model.EndDate = entity.EndDate;
				model.CreateTime = entity.CreateTime;
				model.Url = entity.Url;
				model.Color = entity.Color;
				model.TextColor = entity.TextColor;
				model.SysUserId = entity.SysUserId;
				model.Editable = entity.Editable;
 
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
        public virtual bool CheckImportData(string fileName, List<SysCalendarPlanModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<SysCalendarPlanModel>(x => x.Title, "Title");
				 excelFile.AddMapping<SysCalendarPlanModel>(x => x.PlanContent, "PlanContent");
				 excelFile.AddMapping<SysCalendarPlanModel>(x => x.BeginDate, "BeginDate");
				 excelFile.AddMapping<SysCalendarPlanModel>(x => x.EndDate, "EndDate");
				 excelFile.AddMapping<SysCalendarPlanModel>(x => x.CreateTime, "CreateTime");
				 excelFile.AddMapping<SysCalendarPlanModel>(x => x.Url, "Url");
				 excelFile.AddMapping<SysCalendarPlanModel>(x => x.Color, "Color");
				 excelFile.AddMapping<SysCalendarPlanModel>(x => x.TextColor, "TextColor");
				 excelFile.AddMapping<SysCalendarPlanModel>(x => x.SysUserId, "SysUserId");
				 excelFile.AddMapping<SysCalendarPlanModel>(x => x.Editable, "Editable");
 
            //SheetName
            var excelContent = excelFile.Worksheet<SysCalendarPlanModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new SysCalendarPlanModel();
						 				  entity.Id = row.Id;
				  entity.Title = row.Title;
				  entity.PlanContent = row.PlanContent;
				  entity.BeginDate = row.BeginDate;
				  entity.EndDate = row.EndDate;
				  entity.CreateTime = row.CreateTime;
				  entity.Url = row.Url;
				  entity.Color = row.Color;
				  entity.TextColor = row.TextColor;
				  entity.SysUserId = row.SysUserId;
				  entity.Editable = row.Editable;
 
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
        public virtual void SaveImportData(IEnumerable<SysCalendarPlanModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        SysCalendarPlan entity = new SysCalendarPlan();
                       						entity.Id = ResultHelper.NewId;
						entity.Title = model.Title;
						entity.PlanContent = model.PlanContent;
						entity.BeginDate = model.BeginDate;
						entity.EndDate = model.EndDate;
						entity.CreateTime = ResultHelper.NowTime;
						entity.Url = model.Url;
						entity.Color = model.Color;
						entity.TextColor = model.TextColor;
						entity.SysUserId = model.SysUserId;
						entity.Editable = model.Editable;
 
                        db.SysCalendarPlan.Add(entity);
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
