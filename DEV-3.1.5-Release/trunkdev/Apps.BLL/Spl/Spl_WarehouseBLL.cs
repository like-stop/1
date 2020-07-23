using Apps.Common;
using Apps.Models;
using System.Linq;
using System.Collections.Generic;
using Apps.Models.Spl;

namespace Apps.BLL.Spl
{
    public  partial class Spl_WarehouseBLL
    {

        public override List<Spl_WarehouseModel> CreateModelList(ref IQueryable<Spl_Warehouse> queryData)
        {

            List<Spl_WarehouseModel> modelList = (from r in queryData
                                              select new Spl_WarehouseModel
                                              {
                                                  Id = r.Id,
                                                  Name = r.Name,
                                                  Code = r.Code,
                                                  IsDefault = r.IsDefault,
                                                  ContactPerson = r.ContactPerson,
                                                  ContactPhone = r.ContactPhone,
                                                  Address = r.Address,
                                                  Remark = r.Remark,
                                                  Enable = r.Enable,
                                                  CreateTime = r.CreateTime,
                                                  WarehouseCategoryName = r.Spl_WarehouseCategory.Name,
                                              }).ToList();
            return modelList;
        }
    }
 }

