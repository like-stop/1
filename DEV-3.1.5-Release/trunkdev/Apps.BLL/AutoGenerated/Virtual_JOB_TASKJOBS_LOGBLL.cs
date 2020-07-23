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
using Apps.IDAL.JOB;
using Apps.Models.JOB;
using Apps.IBLL.JOB;
using Apps.IDAL.Sys;
namespace Apps.BLL.JOB
{
	public partial class JOB_TASKJOBS_LOGBLL: Virtual_JOB_TASKJOBS_LOGBLL,IJOB_TASKJOBS_LOGBLL
	{
        

	}
	public class Virtual_JOB_TASKJOBS_LOGBLL
	{
        [Dependency]
        public IJOB_TASKJOBS_LOGRepository m_Rep { get; set; }
		
		[Dependency]
        public ISysUserRepository m_userRep { get; set; }

		[Dependency]
        public ISysStructRepository m_structRep { get; set; }

		public virtual async Task<Tuple<GridPager,List<JOB_TASKJOBS_LOGModel>>> GetListAsync(GridPager pager, string queryStr)
        {

            IQueryable<JOB_TASKJOBS_LOG> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								
								a=>a.sno.Contains(queryStr)
								|| a.taskName.Contains(queryStr)
								|| a.Id.Contains(queryStr)
								
								|| a.executeStep.Contains(queryStr)
								|| a.result.Contains(queryStr)
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
			  
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<JOB_TASKJOBS_LOGModel> list = await CreateModelListAsync(queryData);
            return  new Tuple<GridPager,List<JOB_TASKJOBS_LOGModel>>(pager,list);
        }
		public virtual async Task<List<JOB_TASKJOBS_LOGModel>> CreateModelListAsync(IQueryable<JOB_TASKJOBS_LOG> queryData)
        {

            List<JOB_TASKJOBS_LOGModel> modelList = await (from r in queryData
                                              select new JOB_TASKJOBS_LOGModel
                                              {
                                                 													itemID = r.itemID,
													sno = r.sno,
													taskName = r.taskName,
													Id = r.Id,
													executeDt = r.executeDt,
													executeStep = r.executeStep,
													result = r.result,
          

                                              }).ToListAsync();

            return modelList;
        }

		public virtual List<JOB_TASKJOBS_LOGModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<JOB_TASKJOBS_LOG> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								
								a=>a.sno.Contains(queryStr)
								|| a.taskName.Contains(queryStr)
								|| a.Id.Contains(queryStr)
								
								|| a.executeStep.Contains(queryStr)
								|| a.result.Contains(queryStr)
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
			public virtual List<JOB_TASKJOBS_LOGModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<JOB_TASKJOBS_LOGModel>();
		}
		public virtual List<JOB_TASKJOBS_LOGModel> GetListByDepId(ref GridPager pager, string depId,string queryStr)
		{
			return new List<JOB_TASKJOBS_LOGModel>();
		}
		
		public virtual List<JOB_TASKJOBS_LOGModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<JOB_TASKJOBS_LOGModel>();
		}
		public virtual List<JOB_TASKJOBS_LOGModel> GetList(string id, string parentId)
        {
			return new List<JOB_TASKJOBS_LOGModel>();
		}
        public virtual List<JOB_TASKJOBS_LOGModel> CreateModelList(ref IQueryable<JOB_TASKJOBS_LOG> queryData)
        {

            List<JOB_TASKJOBS_LOGModel> modelList = (from r in queryData
                                              select new JOB_TASKJOBS_LOGModel
                                              {
													itemID = r.itemID,
													sno = r.sno,
													taskName = r.taskName,
													Id = r.Id,
													executeDt = r.executeDt,
													executeStep = r.executeStep,
													result = r.result,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, JOB_TASKJOBS_LOGModel model)
        {
            try
            {
                JOB_TASKJOBS_LOG entity = m_Rep.GetById(model.itemID);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new JOB_TASKJOBS_LOG();
               				entity.itemID = model.itemID;
				entity.sno = model.sno;
				entity.taskName = model.taskName;
				entity.Id = model.Id;
				entity.executeDt = model.executeDt;
				entity.executeStep = model.executeStep;
				entity.result = model.result;
  

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

		 public virtual async Task<Tuple<ValidationErrors, bool>> CreateAsync(JOB_TASKJOBS_LOGModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                JOB_TASKJOBS_LOG entity = await m_Rep.GetByIdAsync(model.itemID);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                entity = new JOB_TASKJOBS_LOG();
               				entity.itemID = model.itemID;
				entity.sno = model.sno;
				entity.taskName = model.taskName;
				entity.Id = model.Id;
				entity.executeDt = model.executeDt;
				entity.executeStep = model.executeStep;
				entity.result = model.result;
  

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
       

        public virtual bool Edit(ref ValidationErrors errors, JOB_TASKJOBS_LOGModel model)
        {
            try
            {
                JOB_TASKJOBS_LOG entity = m_Rep.GetById(model.itemID);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.itemID = model.itemID;
				entity.sno = model.sno;
				entity.taskName = model.taskName;
				entity.Id = model.Id;
				entity.executeDt = model.executeDt;
				entity.executeStep = model.executeStep;
				entity.result = model.result;
 


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

      	 public virtual async Task<Tuple<ValidationErrors, bool>> EditAsync(JOB_TASKJOBS_LOGModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                JOB_TASKJOBS_LOG entity = await m_Rep.GetByIdAsync(model.itemID);
                if (entity == null)
                {
                     errors.Add(Resource.Disable);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                				entity.itemID = model.itemID;
				entity.sno = model.sno;
				entity.taskName = model.taskName;
				entity.Id = model.Id;
				entity.executeDt = model.executeDt;
				entity.executeStep = model.executeStep;
				entity.result = model.result;
 

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

        public virtual JOB_TASKJOBS_LOGModel GetById(object id)
        {
            if (IsExists(id))
            {
                JOB_TASKJOBS_LOG entity = m_Rep.GetById(id);
                JOB_TASKJOBS_LOGModel model = new JOB_TASKJOBS_LOGModel();
                              				model.itemID = entity.itemID;
				model.sno = entity.sno;
				model.taskName = entity.taskName;
				model.Id = entity.Id;
				model.executeDt = entity.executeDt;
				model.executeStep = entity.executeStep;
				model.result = entity.result;
 
                return model;
            }
            else
            {
                return null;
            }
        }

		 public virtual async Task<JOB_TASKJOBS_LOGModel> GetByIdAsync(object id)
        {
            if (IsExists(id))
            {
                JOB_TASKJOBS_LOG entity = await m_Rep.GetByIdAsync(id);
                JOB_TASKJOBS_LOGModel model = new JOB_TASKJOBS_LOGModel();
                              				model.itemID = entity.itemID;
				model.sno = entity.sno;
				model.taskName = entity.taskName;
				model.Id = entity.Id;
				model.executeDt = entity.executeDt;
				model.executeStep = entity.executeStep;
				model.result = entity.result;
 
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
        public virtual bool CheckImportData(string fileName, List<JOB_TASKJOBS_LOGModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<JOB_TASKJOBS_LOGModel>(x => x.itemID, "itemID");
				 excelFile.AddMapping<JOB_TASKJOBS_LOGModel>(x => x.sno, "sno");
				 excelFile.AddMapping<JOB_TASKJOBS_LOGModel>(x => x.taskName, "taskName");
				 excelFile.AddMapping<JOB_TASKJOBS_LOGModel>(x => x.executeDt, "executeDt");
				 excelFile.AddMapping<JOB_TASKJOBS_LOGModel>(x => x.executeStep, "executeStep");
				 excelFile.AddMapping<JOB_TASKJOBS_LOGModel>(x => x.result, "result");
 
            //SheetName
            var excelContent = excelFile.Worksheet<JOB_TASKJOBS_LOGModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new JOB_TASKJOBS_LOGModel();
						 				  entity.itemID = row.itemID;
				  entity.sno = row.sno;
				  entity.taskName = row.taskName;
				  entity.Id = row.Id;
				  entity.executeDt = row.executeDt;
				  entity.executeStep = row.executeStep;
				  entity.result = row.result;
 
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
        public virtual void SaveImportData(IEnumerable<JOB_TASKJOBS_LOGModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        JOB_TASKJOBS_LOG entity = new JOB_TASKJOBS_LOG();
                       						entity.itemID = model.itemID;
						entity.sno = model.sno;
						entity.taskName = model.taskName;
						entity.Id = ResultHelper.NewId;
						entity.executeDt = model.executeDt;
						entity.executeStep = model.executeStep;
						entity.result = model.result;
 
                        db.JOB_TASKJOBS_LOG.Add(entity);
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
