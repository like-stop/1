using Apps.Common;
using Apps.Models;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System;
using Apps.Models.Sys;
using Apps.Locale;
using Apps.BLL.Core;

namespace Apps.BLL.Sys
{
    public  partial class SysValidTokenBLL
    {
        public override bool Create(ref ValidationErrors errors, SysValidTokenModel model)
        {
            try
            {
                SysValidToken entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    //已经存在token，更新token
                    entity.Token = model.Token;
                    entity.ModifyTime = ResultHelper.NowTime;
                    if (m_Rep.Edit(entity))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                entity = new SysValidToken();
                entity.Id = model.Id;
                entity.Token = model.Token;
                entity.OverTime = model.OverTime;
                entity.ModifyTime = model.ModifyTime;
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
        public override List<SysValidTokenModel> CreateModelList(ref IQueryable<SysValidToken> queryData)
        {

            List<SysValidTokenModel> modelList = (from r in queryData
                                              select new SysValidTokenModel
                                              {
                                                  CreateTime = r.CreateTime,
                                                  Id = r.Id,
                                                  ModifyTime = r.ModifyTime,
                                                  OverTime = r.OverTime,
                                                  Token = r.Token,
                                              }).ToList();
            return modelList;
        }
    }
 }

