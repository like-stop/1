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
	public partial class Spl_ProductBLL: Virtual_Spl_ProductBLL,ISpl_ProductBLL
	{
        

	}
	public class Virtual_Spl_ProductBLL
	{
        [Dependency]
        public ISpl_ProductRepository m_Rep { get; set; }
		
		[Dependency]
        public ISysUserRepository m_userRep { get; set; }

		[Dependency]
        public ISysStructRepository m_structRep { get; set; }

		public virtual async Task<Tuple<GridPager,List<Spl_ProductModel>>> GetListAsync(GridPager pager, string queryStr)
        {

            IQueryable<Spl_Product> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.Name.Contains(queryStr)
								|| a.Code.Contains(queryStr)
								
								|| a.Color.Contains(queryStr)
								
								|| a.CategoryId.Contains(queryStr)
								
								|| a.CreateBy.Contains(queryStr)
								
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
			  
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            List<Spl_ProductModel> list = await CreateModelListAsync(queryData);
            return  new Tuple<GridPager,List<Spl_ProductModel>>(pager,list);
        }
		public virtual async Task<List<Spl_ProductModel>> CreateModelListAsync(IQueryable<Spl_Product> queryData)
        {

            List<Spl_ProductModel> modelList = await (from r in queryData
                                              select new Spl_ProductModel
                                              {
                                                 													Id = r.Id,
													Name = r.Name,
													Code = r.Code,
													Price = r.Price,
													Color = r.Color,
													Number = r.Number,
													CategoryId = r.CategoryId,
													CreateTime = r.CreateTime,
													CreateBy = r.CreateBy,
													CostPrice = r.CostPrice,
          

                                              }).ToListAsync();

            return modelList;
        }

		public virtual List<Spl_ProductModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Spl_Product> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								|| a.Name.Contains(queryStr)
								|| a.Code.Contains(queryStr)
								
								|| a.Color.Contains(queryStr)
								
								|| a.CategoryId.Contains(queryStr)
								
								|| a.CreateBy.Contains(queryStr)
								
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
			public virtual List<Spl_ProductModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<Spl_ProductModel>();
		}
		public virtual List<Spl_ProductModel> GetListByDepId(ref GridPager pager, string depId,string queryStr)
		{
			return new List<Spl_ProductModel>();
		}
		
		public virtual List<Spl_ProductModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<Spl_ProductModel>();
		}
		public virtual List<Spl_ProductModel> GetList(string id, string parentId)
        {
			return new List<Spl_ProductModel>();
		}
        public virtual List<Spl_ProductModel> CreateModelList(ref IQueryable<Spl_Product> queryData)
        {

            List<Spl_ProductModel> modelList = (from r in queryData
                                              select new Spl_ProductModel
                                              {
													Id = r.Id,
													Name = r.Name,
													Code = r.Code,
													Price = r.Price,
													Color = r.Color,
													Number = r.Number,
													CategoryId = r.CategoryId,
													CreateTime = r.CreateTime,
													CreateBy = r.CreateBy,
													CostPrice = r.CostPrice,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, Spl_ProductModel model)
        {
            try
            {
                Spl_Product entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new Spl_Product();
               				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.Code = model.Code;
				entity.Price = model.Price;
				entity.Color = model.Color;
				entity.Number = model.Number;
				entity.CategoryId = model.CategoryId;
				entity.CreateTime = model.CreateTime;
				entity.CreateBy = model.CreateBy;
				entity.CostPrice = model.CostPrice;
  

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

		 public virtual async Task<Tuple<ValidationErrors, bool>> CreateAsync(Spl_ProductModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                Spl_Product entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                entity = new Spl_Product();
               				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.Code = model.Code;
				entity.Price = model.Price;
				entity.Color = model.Color;
				entity.Number = model.Number;
				entity.CategoryId = model.CategoryId;
				entity.CreateTime = model.CreateTime;
				entity.CreateBy = model.CreateBy;
				entity.CostPrice = model.CostPrice;
  

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
       

        public virtual bool Edit(ref ValidationErrors errors, Spl_ProductModel model)
        {
            try
            {
                Spl_Product entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.Code = model.Code;
				entity.Price = model.Price;
				entity.Color = model.Color;
				entity.Number = model.Number;
				entity.CategoryId = model.CategoryId;
				entity.CreateTime = model.CreateTime;
				entity.CreateBy = model.CreateBy;
				entity.CostPrice = model.CostPrice;
 


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

      	 public virtual async Task<Tuple<ValidationErrors, bool>> EditAsync(Spl_ProductModel model)
        {
			ValidationErrors errors = new ValidationErrors();
            try
            {
                Spl_Product entity = await m_Rep.GetByIdAsync(model.Id);
                if (entity == null)
                {
                     errors.Add(Resource.Disable);
                    return new Tuple<ValidationErrors, bool>(errors,false);
                }
                				entity.Id = model.Id;
				entity.Name = model.Name;
				entity.Code = model.Code;
				entity.Price = model.Price;
				entity.Color = model.Color;
				entity.Number = model.Number;
				entity.CategoryId = model.CategoryId;
				entity.CreateTime = model.CreateTime;
				entity.CreateBy = model.CreateBy;
				entity.CostPrice = model.CostPrice;
 

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

        public virtual Spl_ProductModel GetById(object id)
        {
            if (IsExists(id))
            {
                Spl_Product entity = m_Rep.GetById(id);
                Spl_ProductModel model = new Spl_ProductModel();
                              				model.Id = entity.Id;
				model.Name = entity.Name;
				model.Code = entity.Code;
				model.Price = entity.Price;
				model.Color = entity.Color;
				model.Number = entity.Number;
				model.CategoryId = entity.CategoryId;
				model.CreateTime = entity.CreateTime;
				model.CreateBy = entity.CreateBy;
				model.CostPrice = entity.CostPrice;
 
                return model;
            }
            else
            {
                return null;
            }
        }

		 public virtual async Task<Spl_ProductModel> GetByIdAsync(object id)
        {
            if (IsExists(id))
            {
                Spl_Product entity = await m_Rep.GetByIdAsync(id);
                Spl_ProductModel model = new Spl_ProductModel();
                              				model.Id = entity.Id;
				model.Name = entity.Name;
				model.Code = entity.Code;
				model.Price = entity.Price;
				model.Color = entity.Color;
				model.Number = entity.Number;
				model.CategoryId = entity.CategoryId;
				model.CreateTime = entity.CreateTime;
				model.CreateBy = entity.CreateBy;
				model.CostPrice = entity.CostPrice;
 
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
        public virtual bool CheckImportData(string fileName, List<Spl_ProductModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<Spl_ProductModel>(x => x.Name, "Name");
				 excelFile.AddMapping<Spl_ProductModel>(x => x.Code, "Code");
				 excelFile.AddMapping<Spl_ProductModel>(x => x.Price, "Price");
				 excelFile.AddMapping<Spl_ProductModel>(x => x.Color, "Color");
				 excelFile.AddMapping<Spl_ProductModel>(x => x.Number, "Number");
				 excelFile.AddMapping<Spl_ProductModel>(x => x.CategoryId, "CategoryId");
				 excelFile.AddMapping<Spl_ProductModel>(x => x.CreateTime, "CreateTime");
				 excelFile.AddMapping<Spl_ProductModel>(x => x.CreateBy, "CreateBy");
				 excelFile.AddMapping<Spl_ProductModel>(x => x.CostPrice, "CostPrice");
 
            //SheetName
            var excelContent = excelFile.Worksheet<Spl_ProductModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new Spl_ProductModel();
						 				  entity.Id = row.Id;
				  entity.Name = row.Name;
				  entity.Code = row.Code;
				  entity.Price = row.Price;
				  entity.Color = row.Color;
				  entity.Number = row.Number;
				  entity.CategoryId = row.CategoryId;
				  entity.CreateTime = row.CreateTime;
				  entity.CreateBy = row.CreateBy;
				  entity.CostPrice = row.CostPrice;
 
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
        public virtual void SaveImportData(IEnumerable<Spl_ProductModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        Spl_Product entity = new Spl_Product();
                       						entity.Id = ResultHelper.NewId;
						entity.Name = model.Name;
						entity.Code = model.Code;
						entity.Price = model.Price;
						entity.Color = model.Color;
						entity.Number = model.Number;
						entity.CategoryId = model.CategoryId;
						entity.CreateTime = ResultHelper.NowTime;
						entity.CreateBy = model.CreateBy;
						entity.CostPrice = model.CostPrice;
 
                        db.Spl_Product.Add(entity);
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
