using Apps.Common;
using Apps.IBLL;
using Apps.WebApi.Core;
using Unity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Apps.IBLL.Sys;
using Apps.Models.Sys;
using Apps.Locale;
using System.Threading.Tasks;

namespace Apps.WebApi.Controllers
{
    /// <summary>
    /// 样例测试
    /// </summary>
    public class SysSampleController : BaseApiController
    {
        /// <summary>
        /// 业务层注入
        /// </summary>
        [Dependency]
        public ISysSampleBLL m_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();

        /// <summary>
        /// 查询样例列表
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetList(BasePager model)
        {
            //string userName = GetUserName();
            //转化出GridPager
            GridPager pa = new GridPager()
            {
                page = model.page,
                rows = model.rows,
                order = model.order,
                sort = model.sort
            };

            List<SysSampleModel> list = m_BLL.GetList(ref pa, model.queryStr);

            GridRows<SysSampleModel> grs = new GridRows<SysSampleModel>();
            grs.rows = list;
            grs.total = pa.totalRows;
            return Json(JsonHandler.CreateMessage(1, "获取数据成功", grs));
        }


        /// <summary>
        /// 创建样例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(SysSampleModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail));
            }
        }




        /// <summary>
        /// 修改样例
        /// </summary>
        /// <param name="modelEdit"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Edit(SysSampleModel modelEdit)
        {
            if (modelEdit != null && ModelState.IsValid)
            {
                SysSampleModel model = m_BLL.GetById(modelEdit.Id);
                if (m_BLL.Edit(ref errors, model))
                {
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    return Json(JsonHandler.CreateMessage(0, Resource.EditFail + ":" + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.EditFail));
            }
        }

        /// <summary>
        /// 获取样例明细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Details(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                SysSampleModel entity = m_BLL.GetById(Id);
                return Json(JsonHandler.CreateMessage(1,"获取成功", entity));
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.EditFail));
            }
        }

   
        /// <summary>
        /// 删除样例
        /// </summary>
        /// <param name="Ids">Id逗号隔开</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Deletes(string Ids)
        {
            if (!string.IsNullOrEmpty(Ids))
            {
                string[] idarr = Ids.Split(',');
                if (m_BLL.Delete(ref errors, idarr))
                {
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail));
            }


        }



    }
}
