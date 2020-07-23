using Apps.Common;
using Apps.Models;
using System.Linq;
using System.Collections.Generic;
using Apps.Models.Spl;
using Unity.Attributes;
using Apps.IDAL.Spl;
using Apps.BLL.Core;

namespace Apps.BLL.Spl
{
    public  partial class Spl_WareDetailsBLL
    {

        [Dependency]
        public ISpl_WareCategoryRepository categoryBLL { get; set; }


        public List<Spl_WareDetailsModel> GetList(ref GridPager pager, string queryStr,string category)
        {

            IQueryable<Spl_WareDetails> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
                                a => (a.Name.Contains(queryStr)
                                || a.Code.Contains(queryStr)
                                || a.BarCode.Contains(queryStr)
                                || a.WareCategoryId.Contains(queryStr)
                                || a.Unit.Contains(queryStr)
                                || a.Lable.Contains(queryStr))
                                );
            }
            else
            {
                queryData = m_Rep.GetList();
            }

            if (!string.IsNullOrEmpty(category) && category != "root" )
            {
                //查询获取所有级联的级别
                List<string> categoryIdList = GetChildRows(category).Select(a=>a.Id).ToList();
                //同时也包含了自己
                categoryIdList.Add(category);
                //获得匹配
                queryData = queryData.Where(a=> categoryIdList.Contains(a.WareCategoryId));
            }

            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }

        public IEnumerable<Spl_WareCategory> GetChildRows(string categoryId)
        {
            DBContainer db = new DBContainer();

            var query = from c in db.Spl_WareCategory
                        where c.ParentId == categoryId
                        select c;

            return query.ToList().Concat(query.ToList().SelectMany(t => GetChildRows(t.Id)));
        }


        public override List<Spl_WareDetailsModel> CreateModelList(ref IQueryable<Spl_WareDetails> queryData)
        {

            List<Spl_WareDetailsModel> modelList = (from r in queryData
                                              select new Spl_WareDetailsModel
                                              {
                                                  Id = r.Id,
                                                  Name = r.Name,
                                                  Code = r.Code,
                                                  BarCode = r.BarCode,
                                                  WareCategoryId = r.WareCategoryId,
                                                  Unit = r.Unit,
                                                  Lable = r.Lable,
                                                  BuyPrice = r.BuyPrice,
                                                  SalePrice = r.SalePrice,
                                                  RetailPrice = r.RetailPrice,
                                                  Remark = r.Remark,
                                                  Vender = r.Vender,
                                                  Brand = r.Brand,
                                                  Color = r.Color,
                                                  Material = r.Material,
                                                  Size = r.Size,
                                                  Weight = r.Weight,
                                                  ComeFrom = r.ComeFrom,
                                                  UpperLimit = r.UpperLimit,
                                                  LowerLimit = r.LowerLimit,
                                                  PrimeCost = r.PrimeCost,
                                                  Price1 = r.Price1,
                                                  Price2 = r.Price2,
                                                  Price3 = r.Price3,
                                                  Price4 = r.Price4,
                                                  Price5 = r.Price5,
                                                  Photo1 = r.Photo1,
                                                  Photo2 = r.Photo2,
                                                  Photo3 = r.Photo3,
                                                  Photo4 = r.Photo4,
                                                  Photo5 = r.Photo5,
                                                  Enable = r.Enable,
                                                  CreateTime = r.CreateTime,
                                                  WareCategoryName = r.Spl_WareCategory.Name,
                                              }).ToList();
            return modelList;
        }
    }
 }

