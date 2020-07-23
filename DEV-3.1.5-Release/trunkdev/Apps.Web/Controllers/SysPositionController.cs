


using System.Collections.Generic;
using System.Linq;
using Apps.Web.Core;
using Apps.IBLL;
using Apps.Locale;
using System.Web.Mvc;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Threading.Tasks;
using Unity.Attributes;
using Apps.IBLL.Sys;

namespace Apps.Web.Controllers
{
    public class SysPositionController : BaseController
    {
        [Dependency]
        public ISysPositionBLL m_BLL { get; set; }
        [Dependency]
        public ISysStructBLL m_SysStructBLL { get; set; }
        ValidationErrors errors = new ValidationErrors();

        [SupportFilter]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(GridPager pager, string queryStr, string parentId)
        {
            List<SysPositionModel> list = m_BLL.GetListByParentId(ref pager, queryStr, parentId);
            GridRows<SysPositionModel> grs = new GridRows<SysPositionModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }
        #region 创建
        [SupportFilter]
        public ActionResult Create(string parentId)
        {
            SysStructModel smodel = m_SysStructBLL.GetById(parentId);
            SysPositionModel model = new SysPositionModel()
            {
                DepId = parentId,
                DepName = smodel.Name

            };
            return View(model);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(SysPositionModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "SysPosition");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "SysPosition");
                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail));
            }
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            SysPositionModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(SysPositionModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "SysPosition");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "SysPosition");
                    return Json(JsonHandler.CreateMessage(0, Resource.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.EditFail));
            }
        }
        #endregion

        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            SysPositionModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        #endregion

        #region 删除
        [HttpPost]
        [SupportFilter]
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (m_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "SysPosition");
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "SysPosition");
                    return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail));
            }
        }
        #endregion

        #region 导出导入
        [HttpPost]
        [SupportFilter]
        public ActionResult Import(string filePath)
        {
            var list = new List<SysPositionModel>();
            bool checkResult = m_BLL.CheckImportData(Utils.GetMapPath(filePath), list, ref errors);
            //校验通过直接保存
            if (checkResult)
            {
                m_BLL.SaveImportData(list);
                LogHandler.WriteServiceLog(GetUserId(), "导入成功", "成功", "导入", "SysPosition");
                return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
            }
            else
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(GetUserId(), ErrorCol, "失败", "导入", "SysPosition");
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));
            }
        }
        [HttpPost]
        [SupportFilter(ActionName = "Export")]
        public JsonResult CheckExportData()
        {
            List<SysPositionModel> list = m_BLL.GetList(ref setNoPagerAscById, "");
            if (list.Count().Equals(0))
            {
                return Json(JsonHandler.CreateMessage(0, "没有可以导出的数据"));
            }
            else
            {
                return Json(JsonHandler.CreateMessage(1, "可以导出"));
            }
        }
        [SupportFilter]
        public ActionResult Export()
        {
            List<SysPositionModel> list = m_BLL.GetList(ref setNoPagerAscById, "");
            JArray jObjects = new JArray();
            foreach (var item in list)
            {
                var jo = new JObject();
                jo.Add("Id", item.Id);
                jo.Add("Name", item.Name);
                jo.Add("Remark", item.Remark);
                jo.Add("Sort", item.Sort);
                jo.Add("CreateTime", item.CreateTime);
                jo.Add("Enable", item.Enable);
                jo.Add("MemberCount", item.MemberCount);
                jo.Add("DepId", item.DepId);
                jObjects.Add(jo);
            }
            var dt = JsonConvert.DeserializeObject<DataTable>(jObjects.ToString());
            var exportFileName = string.Concat(
                "File",
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                ".xlsx");
            return new ExportExcelResult
            {
                SheetName = "Sheet1",
                FileName = exportFileName,
                ExportData = dt
            };
        }
        #endregion
        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetListParent(string id)
        {
            if (id == null)
                id = "0";
            List<SysStructModel> list = m_SysStructBLL.GetList(id);
            var json = from r in list
                       select new SysStructModel()
                       {
                           Id = r.Id,
                           Name = r.Name,
                           ParentId = r.ParentId,
                           Sort = r.Sort,
                           Enable = r.Enable,
                           Higher = r.Higher,
                           Remark = r.Remark,
                           CreateTime = r.CreateTime,
                           state = (m_SysStructBLL.GetList(r.Id).Count > 0) ? "closed" : "open"
                       };


            return Json(json);
        }
        #region 创建
        [SupportFilter(ActionName = "Create")]
        public ActionResult CreateParent(string id)
        {

            SysStructModel entity = new SysStructModel()
            {
                ParentId = id,
                Enable = true
            };
            return View(entity);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Create")]
        public JsonResult CreateParent(SysStructModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (m_SysStructBLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "SysStruct");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "SysStruct");
                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail));
            }
        }
        #endregion

        #region 修改
        [SupportFilter(ActionName = "Edit")]
        public ActionResult EditParent(string id)
        {
            SysStructModel entity = m_SysStructBLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        public JsonResult EditParent(SysStructModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_SysStructBLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "SysPosition");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "SysPosition");
                    return Json(JsonHandler.CreateMessage(0, Resource.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.EditFail));
            }
        }
        #endregion

        #region 详细
        [SupportFilter(ActionName = "Details")]
        public ActionResult DetailsParent(string id)
        {
            SysStructModel entity = m_SysStructBLL.GetById(id);
            return View(entity);
        }

        #endregion

        #region 删除
        [HttpPost]
        [SupportFilter(ActionName = "Delete")]
        public ActionResult DeleteParent(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (m_SysStructBLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "SysPosition");
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "SysPosition");
                    return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail));
            }
        }
        #endregion
        [HttpPost]
        public JsonResult GetPosListByComTree(string depId)
        {
            List<SysPositionModel> list = m_BLL.GetPosListByDepId(ref setNoPagerAscBySort, depId);
            var json = from r in list
                       select new SysPositionEditModel()
                       {
                           id = r.Id,
                           text = r.Name,
                           state = "open"
                       };


            return Json(json);
        }
    }
}

