﻿using Apps.Common;
using Apps.Web.Areas.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Apps.Web.Areas.Demo.Controllers
{
    public class PageDataGridToDataGridController : Controller
    {
        // GET: Demo/SinglePage
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<TableModel> list = SqlHelper.GetAllTableName();
            GridRows<TableModel> grs = new GridRows<TableModel>();
            grs.rows = list;
            grs.total = list.Count;
            return Json(grs);
        }

        public JsonResult GetHtml(string tableName, string parentTableName, string parentName,string parentNameS)
        {
            TypeModel model = new TypeModel();
            model.controller = GetController(tableName, parentTableName, parentName);
            model.create = GetCreate(tableName, parentTableName, parentName);
            model.edit = GetEdit(tableName, parentTableName, parentName);
            model.index = GetIndex(tableName, parentTableName, parentName, parentNameS);
            model.bll = GetBLL(tableName, parentTableName, parentName);
            model.model = GetModel(tableName, parentTableName, parentName, parentNameS);
            return Json(JsonHandler.CreateMessage(1, "", model), JsonRequestBehavior.AllowGet);
        }

        public string GetController(string tableName, string parentTableName, string parentName)
        {
            string leftStr = CodeHelper.GetLeftStr(tableName);
            List<CompleteField> fields = SqlHelper.GetColumnCompleteField(tableName);
            string table1BLL = "m_" + (parentTableName.Trim().IndexOf("_") > 0 ? parentTableName.Trim().Split('_')[1] : parentTableName.Trim());
            string parentTable1 = parentTableName.Trim();

            StringBuilder sb = new StringBuilder();
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System.Linq;\r\n");
            sb.Append("using Apps.Web.Core;\r\n");
            sb.Append("using Apps.IBLL" + (leftStr == "Sys" ? "" : ("." + leftStr)) + ";\r\n");
            sb.Append("using Apps.Locale;\r\n");
            sb.Append("using System.Web.Mvc;\r\n");
            sb.Append("using Apps.Common;\r\n");
            sb.Append("using Apps.IBLL;\r\n");
            sb.Append("using Apps.Models." + leftStr + ";\r\n");
            sb.Append("using Microsoft.Practices.Unity;\r\n");
            sb.Append("using Newtonsoft.Json.Linq;\r\n");
            sb.Append("using Newtonsoft.Json;\r\n");
            sb.Append("using System;\r\n");
            sb.Append("using System.Data;\r\n");
            sb.Append("using System.Threading.Tasks;\r\n");
            sb.Append("\r\n");
            sb.Append("namespace Apps.Web" + (leftStr == "Sys" ? "" : (".Areas." + leftStr)) + ".Controllers\r\n");
            sb.Append("{\r\n");
            sb.Append("    public class " + tableName.Replace(leftStr + "_", "") + "Controller : BaseController\r\n");
            sb.Append("    {\r\n");
            sb.Append("        [Dependency]\r\n");
            sb.Append("        public I" + tableName + "BLL m_BLL { get; set; }\r\n");

            //启用表关联
            //表1
            if (!string.IsNullOrWhiteSpace(parentTableName))
            {
                sb.Append("        [Dependency]\r\n");
                sb.Append("        public I" + parentTableName.Trim() + "BLL " + table1BLL + "BLL { get; set; }\r\n");
            }

            sb.Append("        ValidationErrors errors = new ValidationErrors();\r\n");
            sb.Append("        \r\n");
            sb.Append("        [SupportFilter]\r\n");
            sb.Append("        public ActionResult Index()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return View();\r\n");
            sb.Append("        }\r\n");

            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [SupportFilter(ActionName=\"Index\")]\r\n");
            sb.Append("        public JsonResult GetList(GridPager pager, string queryStr,string parentId)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            List<" + tableName + "Model> list = m_BLL.GetListByParentId(ref pager, queryStr,parentId);\r\n");
            sb.Append("            GridRows<" + tableName + "Model> grs = new GridRows<" + tableName + "Model>();\r\n");
            sb.Append("            grs.rows = list;\r\n");
            sb.Append("            grs.total = pager.totalRows;\r\n");
            sb.Append("            return Json(grs);\r\n");
            sb.Append("        }\r\n");


            #region 子表

            sb.Append("        #region 创建\r\n");
            sb.Append("        [SupportFilter]\r\n");
            sb.Append("        public ActionResult Create()\r\n");
            sb.Append("        {\r\n");
            //表1
            if (!string.IsNullOrWhiteSpace(parentTableName))
            {
                sb.Append("         ViewBag." + table1BLL.Replace("m_", "") + " = new SelectList(" + table1BLL + "BLL.GetList(ref setNoPagerAscById, \"\"), \"Id\", \"Name\");\r\n");
            }
            sb.Append("            return View();\r\n");
            sb.Append("        }\r\n");
            sb.Append("\r\n");
            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [SupportFilter]\r\n");
            sb.Append("        public JsonResult Create(" + tableName + "Model model)\r\n");
            sb.Append("        {\r\n");
            if (fields[0].xType != "56" && fields[0].xType != "127")//非int型主键
            {
                sb.Append("            model.Id = ResultHelper.NewId;\r\n");
            }
            else
            {
                sb.Append("            model.Id = 0;\r\n");
            }
            sb.Append("            model.CreateTime = ResultHelper.NowTime;\r\n");
            sb.Append("            if (model != null && ModelState.IsValid)\r\n");
            sb.Append("            {\r\n");
            sb.Append("\r\n");
            sb.Append("                if (m_BLL.Create(ref errors, model))\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    LogHandler.WriteServiceLog(GetUserId(), \"" + fields[0].name + "\" + model." + fields[0].name + " + \"," + fields[1].name + "\" + model." + fields[1].name + ", \"成功\", \"创建\", \"" + tableName + "\");\r\n");
            sb.Append("                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));\r\n");
            sb.Append("                }\r\n");
            sb.Append("                else\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    string ErrorCol = errors.Error;\r\n");
            sb.Append("                    LogHandler.WriteServiceLog(GetUserId(), \"" + fields[0].name + "\" + model." + fields[0].name + " + \"," + fields[1].name + "\" + model." + fields[1].name + " + \",\" + ErrorCol, \"失败\", \"创建\", \"" + tableName + "\");\r\n");
            sb.Append("                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }\r\n");
            sb.Append("            else\r\n");
            sb.Append("            {\r\n");
            sb.Append("                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail));\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("\r\n");
            sb.Append("        #region 修改\r\n");
            sb.Append("        [SupportFilter]\r\n");
            if (fields[0].xType != "56" && fields[0].xType != "127")//非int型主键
            {
                sb.Append("        public ActionResult Edit(string id)\r\n");
            }
            else
            {
                sb.Append("        public ActionResult Edit(long id)\r\n");
            }
            sb.Append("        {\r\n");

            sb.Append("            " + tableName + "Model entity = m_BLL.GetById(id);\r\n");
            //启用表关联
            sb.Append("            ViewBag." + table1BLL.Replace("m_", "") + " = new SelectList(" + table1BLL + "BLL.GetList(ref setNoPagerAscById, \"\"), \"Id\", \"Name\",entity." + parentName + ");\r\n");
            sb.Append("            return View(entity);\r\n");
            sb.Append("        }\r\n");
            sb.Append("\r\n");
            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [SupportFilter]\r\n");
            sb.Append("        public JsonResult Edit(" + tableName + "Model model)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            if (model != null && ModelState.IsValid)\r\n");
            sb.Append("            {\r\n");
            sb.Append("\r\n");
            sb.Append("                if (m_BLL.Edit(ref errors, model))\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    LogHandler.WriteServiceLog(GetUserId(), \"" + fields[0].name + "\" + model." + fields[0].name + " + \"," + fields[1].name + "\" + model." + fields[1].name + ", \"成功\", \"修改\", \"" + tableName + "\");\r\n");
            sb.Append("                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));\r\n");
            sb.Append("                }\r\n");
            sb.Append("                else\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    string ErrorCol = errors.Error;\r\n");
            sb.Append("                    LogHandler.WriteServiceLog(GetUserId(), \"" + fields[0].name + "\" + model." + fields[0].name + " + \"," + fields[1].name + "\" + model." + fields[1].name + " + \",\" + ErrorCol, \"失败\", \"修改\", \"" + tableName + "\");\r\n");
            sb.Append("                    return Json(JsonHandler.CreateMessage(0, Resource.EditFail + ErrorCol));\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }\r\n");
            sb.Append("            else\r\n");
            sb.Append("            {\r\n");
            sb.Append("                return Json(JsonHandler.CreateMessage(0, Resource.EditFail));\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("\r\n");
            sb.Append("        #region 详细\r\n");
            sb.Append("        [SupportFilter]\r\n");
            if (fields[0].xType != "56" && fields[0].xType != "127")//非int型主键
            {
                sb.Append("        public ActionResult Details(string id)\r\n");
            }
            else
            {
                sb.Append("        public ActionResult Details(long id)\r\n");
            }
            sb.Append("        {\r\n");
            sb.Append("            " + tableName + "Model entity = m_BLL.GetById(id);\r\n");
            sb.Append("            return View(entity);\r\n");
            sb.Append("        }\r\n");
            sb.Append("\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("\r\n");
            sb.Append("        #region 删除\r\n");
            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [SupportFilter]\r\n");

            if (fields[0].xType != "56" && fields[0].xType != "127")//非int型主键
            {
                sb.Append("        public ActionResult Delete(string id)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            if(!string.IsNullOrWhiteSpace(id))\r\n");
                sb.Append("            {\r\n");
                sb.Append("                if (m_BLL.Delete(ref errors, id))\r\n");
                sb.Append("                {\r\n");
                sb.Append("                    LogHandler.WriteServiceLog(GetUserId(), \"" + fields[0].name + ":\" + id, \"成功\", \"删除\", \"" + tableName + "\");\r\n");
                sb.Append("                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));\r\n");
                sb.Append("                }\r\n");
                sb.Append("                else\r\n");
                sb.Append("                {\r\n");
                sb.Append("                    string ErrorCol = errors.Error;\r\n");
                sb.Append("                    LogHandler.WriteServiceLog(GetUserId(), \"" + fields[0].name + "\" + id + \",\" + ErrorCol, \"失败\", \"删除\", \"" + tableName + "\");\r\n");
                sb.Append("                    return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail + ErrorCol));\r\n");
                sb.Append("                }\r\n");
                sb.Append("            }\r\n");
                sb.Append("            else\r\n");
                sb.Append("            {\r\n");
                sb.Append("                return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail));\r\n");
                sb.Append("            }\r\n");
                sb.Append("        }\r\n");
            }
            else
            {
                sb.Append("        public ActionResult Delete(long id)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            if(id!=0)\r\n");
                sb.Append("            {\r\n");
                sb.Append("                if (m_BLL.Delete(ref errors, id))\r\n");
                sb.Append("                {\r\n");
                sb.Append("                    LogHandler.WriteServiceLog(GetUserId(), \"" + fields[0].name + ":\" + id, \"成功\", \"删除\", \"" + tableName + "\");\r\n");
                sb.Append("                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));\r\n");
                sb.Append("                }\r\n");
                sb.Append("                else\r\n");
                sb.Append("                {\r\n");
                sb.Append("                    string ErrorCol = errors.Error;\r\n");
                sb.Append("                    LogHandler.WriteServiceLog(GetUserId(), \"" + fields[0].name + "\" + id + \",\" + ErrorCol, \"失败\", \"删除\", \"" + tableName + "\");\r\n");
                sb.Append("                    return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail + ErrorCol));\r\n");
                sb.Append("                }\r\n");
                sb.Append("            }\r\n");
                sb.Append("            else\r\n");
                sb.Append("            {\r\n");
                sb.Append("                return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail));\r\n");
                sb.Append("            }\r\n");
                sb.Append("        }\r\n");
            }
            sb.Append("        #endregion\r\n");
            //导入导出

            sb.Append("        #region 导出导入\r\n");

            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [SupportFilter]\r\n");
            sb.Append("        public ActionResult Import(string filePath)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            var list = new List<" + tableName + "Model>();\r\n");
            sb.Append("            bool checkResult = m_BLL.CheckImportData(Utils.GetMapPath(filePath), list, ref errors);\r\n");
            sb.Append("            //校验通过直接保存\r\n");
            sb.Append("            if (checkResult)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                m_BLL.SaveImportData(list);\r\n");
            sb.Append("                LogHandler.WriteServiceLog(GetUserId(), \"导入成功\", \"成功\", \"导入\", \"" + tableName + "\");\r\n");
            sb.Append("                return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));\r\n");
            sb.Append("            }\r\n");
            sb.Append("            else\r\n");
            sb.Append("            {\r\n");
            sb.Append("                string ErrorCol = errors.Error;\r\n");
            sb.Append("                LogHandler.WriteServiceLog(GetUserId(), ErrorCol, \"失败\", \"导入\", \"" + tableName + "\");\r\n");
            sb.Append("                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n");


            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [SupportFilter(ActionName = \"Export\")]\r\n");
            sb.Append("        public JsonResult CheckExportData()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            List<" + tableName + "Model> list = m_BLL.GetList(ref setNoPagerAscById, \"\");\r\n");
            sb.Append("            if (list.Count().Equals(0))\r\n");
            sb.Append("            {\r\n");
            sb.Append("                return Json(JsonHandler.CreateMessage(0, \"没有可以导出的数据\"));\r\n");
            sb.Append("            }\r\n");
            sb.Append("            else\r\n");
            sb.Append("            {\r\n");
            sb.Append("                return Json(JsonHandler.CreateMessage(1, \"可以导出\"));\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n");

            sb.Append("        [SupportFilter]\r\n");
            sb.Append("        public ActionResult Export()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            List<" + tableName + "Model> list = m_BLL.GetList(ref setNoPagerAscById, \"\");\r\n");
            sb.Append("            JArray jObjects = new JArray();\r\n");
            sb.Append("                foreach (var item in list)\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    var jo = new JObject();\r\n");
            foreach (CompleteField field in fields)
            {
                sb.AppendFormat("                    jo.Add(\"{0}\", item.{0});\r\n", field.name);
            }
            sb.Append("                    jObjects.Add(jo);\r\n");
            sb.Append("                }\r\n");
            sb.Append("                var dt = JsonConvert.DeserializeObject<DataTable>(jObjects.ToString());\r\n");

            sb.Append("                var exportFileName = string.Concat(\r\n");
            sb.Append("                    \"File\",\r\n");
            sb.Append("                    DateTime.Now.ToString(\"yyyyMMddHHmmss\"),\r\n");
            sb.Append("                    \".xlsx\");\r\n");

            sb.Append("                return new ExportExcelResult\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    SheetName = \"Sheet1\",\r\n");
            sb.Append("                    FileName = exportFileName,\r\n");
            sb.Append("                    ExportData = dt\r\n");
            sb.Append("                };\r\n");
            sb.Append("            }\r\n");
            sb.Append("        #endregion\r\n");
            #endregion

            #region 父表

            List<CompleteField> parentFields = SqlHelper.GetColumnCompleteField(parentTableName.Trim());

            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [SupportFilter(ActionName=\"Index\")]\r\n");
            sb.Append("        public JsonResult GetListParent(GridPager pager, string queryStr)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            List<" + parentTable1 + "Model> list = " + table1BLL + "BLL.GetList(ref pager, queryStr);\r\n");
            sb.Append("            GridRows<" + parentTable1 + "Model> grs = new GridRows<" + parentTable1 + "Model>();\r\n");
            sb.Append("            grs.rows = list;\r\n");
            sb.Append("            grs.total = pager.totalRows;\r\n");
            sb.Append("            return Json(grs);\r\n");
            sb.Append("        }\r\n");

            #endregion



            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }

        public string GetIndex(string tableName, string parentTableName, string parentName,string parentNameS)
        {
            string leftStr = CodeHelper.GetLeftStr(tableName);
            List<CompleteField> fields = SqlHelper.GetColumnCompleteField(tableName);

            List<CompleteField> parentFields = new List<CompleteField>();
            bool isSort = false;
            bool isCreateTime = false;

            parentFields = SqlHelper.GetColumnCompleteField(parentTableName);

            StringBuilder sb = new StringBuilder();
            sb.Append("@using Apps.Web.Core;\r\n");
            sb.Append("@using Apps.Web;\r\n");
            sb.Append("@using Apps.Common;\r\n");
            sb.Append("@using Apps.Models.Sys;\r\n");
            sb.Append("@using Apps.Locale;\r\n");
            sb.Append("@{\r\n");
            sb.Append("    ViewBag.Title = \"" + tableName + "\";\r\n");
            sb.Append("    Layout = \"~/Views/Shared/_Index_Layout.cshtml\";\r\n");
            sb.Append("    List<permModel> perm = null;\r\n");
            sb.Append("}\r\n");
            //导出
            sb.Append("<div id = \"uploadExcel\" class=\"easyui-window\" data-options=\"modal:true,closed:true,minimizable:false,shadow:false\">\r\n");
            sb.Append("<form name = \"form1\" method=\"post\" id=\"form1\">\r\n");
            sb.Append("    <table>\r\n");
            sb.Append("        <tr>\r\n");
            sb.Append("            <th style=\"padding:20px;\"> Excel：</th>\r\n");
            sb.Append("            <td style=\"padding:20px;\">\r\n");
            sb.Append("                <input name=\"ExcelPath\" type=\"text\" maxlength=\"255\" id=\"txtExcelPath\" readonly=\"readonly\" style=\"width:200px\" class=\"txtInput normal left\">\r\n");
            sb.Append("                <a class=\"files\">@Resource.Browse<input  type=\"file\" id=\"FileUpload\" name=\"FileUpload\" onchange=\"Upload('ExcelFile', 'txtExcelPath', 'FileUpload'); \">\r\n</a>\r\n");
            sb.Append("                <span class=\"uploading\">@Resource.Uploading</span>\r\n");
            sb.Append("            </td>\r\n");
            sb.Append("        </tr>\r\n");
            sb.Append("    </table>\r\n");
            sb.Append("    <div class=\"endbtndiv\">\r\n");
            sb.Append("        <a id = \"btnSave\" href=\"javascript:ImportData()\" class=\"easyui-linkbutton btns\">直接保存</a>\r\n");
            //sb.Append("        <a id = \"btnSaveBefor\" href=\"javascript:ImportDataBefor()\" class=\"easyui-linkbutton btnsb\" style=\"width:80px; \">保存前编辑</a>\r\n");
            sb.Append("        <a id = \"btnReturn\" href=\"javascript:$('#uploadExcel').window('close')\" class=\"easyui-linkbutton btnc\">@Resource.Cancel</a>\r\n");
            sb.Append("    </div>\r\n");
            sb.Append("</form>\r\n");
            sb.Append("</div>\r\n");
            //开启父表联合生成

            sb.Append("<table style=\"width:100%\">\r\n");
            sb.Append("<tbody>\r\n");
            sb.Append("    <tr>\r\n");
            sb.Append("        <td style=\"width:330px;vertical-align: top\">\r\n");
            sb.Append("             <div class=\"mvctool\">\r\n");
            sb.Append("             </div>\r\n");
            sb.Append("            <table id=\"ListParent\"></table>\r\n");
            sb.Append("        </td>");
            sb.Append("        <td style=\"width:3px;\"></td>\r\n");
            sb.Append("        <td style=\"vertical-align:top\">\r\n");
            sb.Append("             <div class=\"mvctool\">\r\n");
            sb.Append("                 <input id=\"txtQuery\" type=\"text\" class=\"searchText\" />\r\n");
            sb.Append("                 @Html.ToolButton(\"btnQuery\", \"fa fa-search\", Resource.Query,ref perm, \"Query\", true)\r\n");
            sb.Append("                 @Html.ToolButton(\"btnCreate\", \"fa fa-plus\", Resource.Create,ref perm, \"Create\", true)\r\n");
            sb.Append("                 @Html.ToolButton(\"btnEdit\", \"fa fa-pencil\", Resource.Edit,ref perm, \"Edit\", true)\r\n");
            sb.Append("                 @Html.ToolButton(\"btnDelete\", \"fa fa-trash\", Resource.Delete,ref perm, \"Delete\", true)\r\n");
            sb.Append("                 @Html.ToolButton(\"btnImport\", \"fa fa-level-down\", Resource.Import, ref perm, \"Import\", true)\r\n");
            sb.Append("                 @Html.ToolButton(\"btnExport\", \"fa fa-level-up\", Resource.Export, ref perm, \"Export\", true)\r\n");
            sb.Append("             </div>\r\n");
            sb.Append("            <table id=\"List\"></table>\r\n");
            sb.Append("        </td>\r\n");
            sb.Append("    </tr>\r\n");
            sb.Append("</tbody>\r\n");
            sb.Append("</table>\r\n");

            sb.Append("\r\n");
            sb.Append("<div id=\"modalwindow\" class=\"easyui-window\" style=\"width:800px; height:400px;\" data-options=\"modal:true,closed:true,minimizable:false,shadow:false\"></div>\r\n");
            sb.Append("<script type=\"text/javascript\">\r\n");
            sb.Append("    $(function () {\r\n");

            sb.Append("        $('#ListParent').datagrid({\r\n");
            sb.Append("            url: '@Url.Action(\"GetListParent\")',\r\n");
            sb.Append("            width:430,\r\n");
            sb.Append("            methord: 'post',\r\n");
            sb.Append("            height: SetGridHeightSub(45),\r\n");
            sb.Append("            fitColumns: true,\r\n");
            isSort = false;
            isCreateTime = false;
            foreach (CompleteField field in parentFields)
            {
                if (field.name == "Sort")
                {
                    isSort = true;
                }
                if (field.name == "CreateTime")
                {
                    isCreateTime = true;
                }
            }
            if (isSort)
            {
                sb.Append("            sortName: 'Sort',\r\n");
                sb.Append("            sortOrder: 'asc',\r\n");
            }
            else if (isCreateTime)
            {
                sb.Append("            sortName: 'CreateTime',\r\n");
                sb.Append("            sortOrder: 'desc',\r\n");
            }
            else
            {
                sb.Append("            sortName: 'Id',\r\n");
                sb.Append("            sortOrder: 'desc',\r\n");
            }

            sb.Append("            idField: 'Id',\r\n");
            sb.Append("            pageSize: 15,\r\n");
            sb.Append("            pageList: [15, 20, 30, 40, 50],\r\n");
            sb.Append("            pagination: true,\r\n");
            sb.Append("            striped: true, //奇偶行是否区分\r\n");
            sb.Append("            singleSelect: true,//单选模式\r\n");
            sb.Append("            //rownumbers: true,//行号\r\n");
            sb.Append("            onLoadSuccess: function(data) {");
            sb.Append("            },\r\n");
            sb.Append("            columns: [[\r\n");
            foreach (CompleteField field in parentFields)
            {
                //主键，隐藏
                if (field.name == "Id")
                {
                    sb.Append("                { field: '" + field.name + "', title: '" + (field.remark == "" ? field.name : field.remark) + "', width: 80,hidden:true },\r\n");
                }//布尔类型，加formatter
                else if (field.xType == "104" || field.xType == "bool")
                {
                    sb.Append("                { field: '" + field.name + "', title: '" + (field.remark == "" ? field.name : field.remark) + "', width: 40,sortable:true,align:'center', formatter: function (value) {return EnableFormatter(value)}},\r\n");
                }
                else if (field.name.ToLower().Contains("img") || field.name.ToLower().Contains("photo"))
                {
                    sb.Append("                { field: '" + field.name + "', title: '" + (field.remark == "" ? field.name : field.remark) + "', width: 40,sortable:true, align: 'center', formatter: function (value, row, index) {return '<img width=\"80px\" alt=\"example\" src=\"' + value + '\" />';}},\r\n");
                }
                else
                {
                    sb.Append("                { field: '" + field.name + "', title: '" + (field.remark == "" ? field.name : field.remark) + "', width: 80,sortable:true },\r\n");
                }

            }

            sb.Remove(sb.ToString().LastIndexOf(','), 1);
            sb.Append("            ]]\r\n");

            sb.Append("         ,onClickRow: function(index, row) {\r\n");
            sb.Append("             $('#List').datagrid(\"load\", { ParentId: row.Id });\r\n");
            sb.Append("}\r\n");

            sb.Append("        }).datagrid('getPager').pagination({ showPageList: false, showRefresh: false });\r\n");
            sb.Append("         $(window).resize(function() {\r\n");
            sb.Append("             resizeLayout();\r\n");
            sb.Append("         });\r\n");
            sb.Append("        $('#List').datagrid({\r\n");
            sb.Append("            url: '@Url.Action(\"GetList\")?parentId=0',\r\n");
            sb.Append("            width:SetGridWidthSub(450),\r\n");
            sb.Append("            methord: 'post',\r\n");
            sb.Append("            height: SetGridHeightSub(45),\r\n");
            sb.Append("            fitColumns: true,\r\n");
            isSort = false;
            isCreateTime = false;
            foreach (CompleteField field in fields)
            {
                if (field.name == "Sort")
                {
                    isSort = true;
                }
                else if (field.name == "CreateTime")
                {
                    isCreateTime = true;
                }
            }
            if (isSort)
            {
                sb.Append("            sortName: 'Sort',\r\n");
                sb.Append("            sortOrder: 'asc',\r\n");
            }
            else if (isCreateTime)
            {
                sb.Append("            sortName: 'CreateTime',\r\n");
                sb.Append("            sortOrder: 'desc',\r\n");
            }
            else
            {
                sb.Append("            sortName: 'Id',\r\n");
                sb.Append("            sortOrder: 'desc',\r\n");
            }

            sb.Append("            idField: 'Id',\r\n");
            sb.Append("            pageSize: 15,\r\n");
            sb.Append("            pageList: [15, 20, 30, 40, 50],\r\n");
            sb.Append("            pagination: true,\r\n");
            sb.Append("            striped: true, //奇偶行是否区分\r\n");
            sb.Append("            singleSelect: true,//单选模式\r\n");
            sb.Append("            //rownumbers: true,//行号\r\n");
            sb.Append("            onLoadSuccess: function(data) {");
            sb.Append("                @foreach(var r in perm){ if (r.Category == 2) { @Html.Raw(\"$(this).datagrid('hideColumn','\" + r.KeyCode + \"'); \"); } }\r\n");
            sb.Append("            },\r\n");
            sb.Append("            columns: [[\r\n");
            foreach (CompleteField field in fields)
            {
                //主键，隐藏
                if (field.name == "Id")
                {
                    sb.Append("                { field: '" + field.name + "', title: '" + (field.remark == "" ? field.name : field.remark) + "', width: 80,hidden:true },\r\n");
                }//布尔类型，加formatter
                else if (field.xType == "104" || field.xType == "bool")
                {
                    sb.Append("                { field: '" + field.name + "', title: '" + (field.remark == "" ? field.name : field.remark) + "', width: 40,sortable:true,align:'center', formatter: function (value) {return EnableFormatter(value)}},\r\n");
                }
                else if (field.name.ToLower().Contains("img") || field.name.ToLower().Contains("photo"))
                {
                    sb.Append("                { field: '" + field.name + "', title: '" + (field.remark == "" ? field.name : field.remark) + "', width: 40,sortable:true, align: 'center', formatter: function (value, row, index) {return '<img width=\"80px\" alt=\"example\" src=\"' + value + '\" />';}},\r\n");
                }
                else
                {
                    sb.Append("                { field: '" + field.name + "', title: '" + (field.remark == "" ? field.name : field.remark) + "', width: 80,sortable:true },\r\n");
                }

            }
            //启用表关联
            //表1
            if (!string.IsNullOrWhiteSpace(parentTableName))
            {
                sb.Append("                { field: '" + parentTableName.Replace(leftStr + "_", "") + parentNameS + "', title: '类别名称" + "', width: 80 },\r\n");
            }
            sb.Remove(sb.ToString().LastIndexOf(','), 1);
            sb.Append("            ]]\r\n");
            sb.Append("        });\r\n");
            sb.Append("    });\r\n");
            sb.Append("    //ifram 返回\r\n");
            sb.Append("    function frameReturnByClose() {\r\n");
            sb.Append("        $(\"#modalwindow\").window('close');\r\n");
            sb.Append("    }\r\n");
            sb.Append("    function frameReturnByReload(flag) {\r\n");
            sb.Append("        if (flag)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            $(\"#List\").datagrid('load');\r\n");
            sb.Append("        }\r\n");
            sb.Append("        else\r\n");
            sb.Append("        {\r\n");
            sb.Append("            $(\"#List,#ListParent\").datagrid('reload');\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append("    function frameReturnByMes(mes) {\r\n");
            sb.Append("        $.messageBox5s(Lang.Tip, mes);\r\n");
            sb.Append("    }\r\n");
            sb.Append("    $(function () {\r\n");
            sb.Append("        $(\"#btnCreate\").click(function () {\r\n");
            sb.Append("            $.modalWindow(Lang.Create, '@Url.Action(\"Create\")', 700, 400, 'fa fa-plus');\r\n");
            sb.Append("        });\r\n");
            sb.Append("        $(\"#btnEdit\").click(function () {\r\n");
            sb.Append("            var row = $('#List').datagrid('getSelected');\r\n");
            sb.Append("            if (row != null) {\r\n");
            sb.Append("                $.modalWindow(Lang.Edit, '@Url.Action(\"Edit\")?id=' + row.Id + '&Ieguid=' + GetGuid(), 700, 400, 'fa fa-pencil');\r\n");
            sb.Append("            } else { $.messageBox5s(Lang.Tip, Lang.PleaseSelectTheOperatingRecord); }\r\n");
            sb.Append("        });\r\n");
            sb.Append("        $(\"#btnDetails\").click(function () {\r\n");
            sb.Append("            var row = $('#List').datagrid('getSelected');\r\n");
            sb.Append("            if (row != null) {\r\n");
            sb.Append("                $.modalWindow(Lang.Details, '@Url.Action(\"Details\")?id=' + row.Id + '&Ieguid=' + GetGuid(), 700, 400, 'fa fa-list');\r\n");
            sb.Append("            } else { $.messageBox5s(Lang.Tip, Lang.PleaseSelectTheOperatingRecord); }\r\n");
            sb.Append("	        });\r\n");
            sb.Append("        $(\"#btnQuery\").click(function () {\r\n");
            sb.Append("            var queryStr = $(\"#txtQuery\").val();\r\n");
            sb.Append("            if (queryStr == null) {\r\n");
            sb.Append("                queryStr = \"%\";\r\n");
            sb.Append("            }\r\n");
            sb.Append("            $(\"#List\").datagrid(\"load\", { queryStr: queryStr });\r\n");
            sb.Append("\r\n");
            sb.Append("        });\r\n");
            sb.Append("        $(\"#btnDelete\").click(function () {\r\n");
            sb.Append("	            dataDelete(\"@Url.Action(\"Delete\")\", \"List\");\r\n");
            sb.Append("	        });\r\n");
            //导入导出
            sb.Append("        $(\"#btnImport\").click(function() {\r\n");
            sb.Append("             $(\"#txtExcelPath\").val(\"\");\r\n");
            sb.Append("             $(\"#uploadExcel\").window({ title: '@Resource.Import', width: 450, height: 155, iconCls: 'fa fa-level-down' }).window('open');\r\n");
            sb.Append("        });\r\n");
            sb.Append("        $(\"#btnExport\").click(function() {\r\n");
            sb.Append("          $.post(\"@Url.Action(\"CheckExportData\")\", function(data) {\r\n");
            sb.Append("                if (data.type == 1)\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    window.location = \"@Url.Action(\"Export\")\";\r\n");
            sb.Append("                }\r\n");
            sb.Append("                else\r\n");
            sb.Append("                {\r\n");
            sb.Append("                $.messageBox5s(Lang.Tip, data.message);\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }, \"json\");\r\n");
            sb.Append("        });\r\n");
            sb.Append("    });\r\n");

            //导入导出
            sb.Append("    function ImportData()\r\n");
            sb.Append("    {\r\n");
            sb.Append("        showLoading();\r\n");
            sb.Append("        var url = \"@Url.Action(\"Import\")?filePath=\"+$(\"#txtExcelPath\").val();\r\n");
            sb.Append("        $.post(url, function(data) {\r\n");
            sb.Append("            if (data.type == 1)\r\n");
            sb.Append("            {\r\n");
            sb.Append("             $(\"#List\").datagrid('load');\r\n");
            sb.Append("             $('#uploadExcel').window('close');\r\n");
            sb.Append("             $('#FileUpload').val('');\r\n");
            sb.Append("            }\r\n");
            sb.Append("            hideLoading();\r\n");
            sb.Append("            $.messageBox5s('提示', data.message);\r\n");
            sb.Append("        }, \"json\");\r\n");
            sb.Append("    }\r\n");

            sb.Append("    function resizeLayout()\r\n");
            sb.Append("     {\r\n");
            sb.Append("         setTimeout(function () {\r\n");
            sb.Append("             $('#ListParent').datagrid('resize', {\r\n");
            sb.Append("             }).datagrid('resize', {\r\n");
            sb.Append("                 height: SetGridHeightSub(81)\r\n");
            sb.Append("             });\r\n");
            sb.Append("             $('#List').datagrid('resize', {\r\n");
            sb.Append("             }).datagrid('resize', {\r\n");
            sb.Append("                 width: $(window).width() - 450,\r\n");
            sb.Append("                 height: SetGridHeightSub(45)\r\n");
            sb.Append("             });\r\n");
            sb.Append("         },100);\r\n");
            sb.Append("	    }\r\n");
            sb.Append("</script>\r\n");

            return sb.ToString();
        }

        public string GetCreate(string tableName, string parentTableName, string parentName)
        {
            string leftStr = CodeHelper.GetLeftStr(tableName);
            List<CompleteField> fields = SqlHelper.GetColumnCompleteField(tableName);

            StringBuilder sb = new StringBuilder();
            sb.Append("@model Apps.Models." + (leftStr == "Sys" ? "" : (leftStr + ".")) + "" + tableName + "Model\r\n");
            sb.Append("@using Apps.Web.Core;\r\n");
            sb.Append("@using Apps.Common;\r\n");
            sb.Append("@using Apps.Models." + leftStr + ";\r\n");
            sb.Append("@using Apps.Web;\r\n");
            sb.Append("@using Apps.Locale;\r\n");
            sb.Append("@using Apps.Models.Sys;\r\n");
            sb.Append("@{\r\n");
            sb.Append("    ViewBag.Title = \"创建\";\r\n");
            sb.Append("    Layout = \"~/Views/Shared/_Index_LayoutEdit.cshtml\";\r\n");
            sb.Append("    List<permModel> perm = null;\r\n");
            sb.Append("}\r\n");
            sb.Append("\r\n");
            sb.Append("<script type=\"text/javascript\">\r\n");
            sb.Append("$(function () {\r\n");
            sb.Append("    $(\"#btnSave\").click(function () {\r\n");
            sb.Append("        if ($(\"form\").valid()) {\r\n");
            sb.Append("            $.ajax({\r\n");
            sb.Append("                url: \"@Url.Action(\"Create\")\",\r\n");
            sb.Append("                type: \"Post\",\r\n");
            sb.Append("                data: $(\"form\").serialize(),\r\n");
            sb.Append("                dataType: \"json\",\r\n");
            sb.Append("                success: function (data) {\r\n");
            sb.Append("                    if (data.type == 1) {\r\n");
            sb.Append("                        window.parent.frameReturnByMes(data.message);\r\n");
            sb.Append("                        window.parent.frameReturnByReload(true);\r\n");
            sb.Append("                        window.parent.frameReturnByClose()\r\n");
            sb.Append("                    }\r\n");
            sb.Append("                    else {\r\n");
            sb.Append("                        window.parent.frameReturnByMes(data.message);\r\n");
            sb.Append("                    }\r\n");
            sb.Append("                }\r\n");
            sb.Append("            });\r\n");
            sb.Append("        }\r\n");
            sb.Append("        return false;\r\n");
            sb.Append("    });\r\n");
            sb.Append("    $(\"#btnReturn\").click(function () {\r\n");
            sb.Append("         window.parent.frameReturnByClose();\r\n");
            sb.Append("    });\r\n");
            sb.Append("});\r\n");
            sb.Append("function frameReturnByClose() {\r\n");
            sb.Append("$(\"#modalwindow\").window('close');\r\n");
            sb.Append("}\r\n");
            sb.Append("function frameReturnByMes(mes) {\r\n");
            sb.Append("$.messageBox5s(Lang.Tip, mes);\r\n");
            sb.Append("}\r\n");
            sb.Append("</script>\r\n");
            sb.Append("<div id=\"modalwindow\" class=\"easyui-window\" style=\"width:800px; height:400px;\" data-options=\"modal:true,closed:true,minimizable:false,shadow:false\"></div>\r\n");
            sb.Append("<div class=\"mvctool bgb\">\r\n");
            sb.Append("@Html.ToolButton(\"btnSave\", \"fa fa-save\", Resource.Save,ref perm, \"Save\", true)\r\n");
            sb.Append("@Html.ToolButton(\"btnReturn\", \"fa fa-reply\", Resource.Reply,false)\r\n");
            sb.Append("</div>\r\n");
            sb.Append("@using (Html.BeginForm())\r\n");
            sb.Append("{\r\n");
            foreach (CompleteField field in fields)
            {

                if (field.name == "Id")
                {
                    if (field.xType != "56" && field.xType != "127")//非int型主键
                    {
                        sb.Append("             @Html.HiddenFor(model => model." + field.name + ")\r\n");
                    }
                    else
                    {
                        sb.Append("             @Html.HiddenFor(model => model." + field.name + ", new { @Value = 0})\r\n");
                    }
                }
                if (field.name == "CreateTime")
                {
                    sb.Append("             <input id=\"CreateTime\" type=\"hidden\" name=\"CreateTime\" value=\"2000-1-1\" />\r\n");
                }
            }
            sb.Append(" <table class=\"formtable\">\r\n");
            sb.Append("    <tbody>\r\n");

            //启用表关联

            //表1
            if (!string.IsNullOrWhiteSpace(parentTableName))
            {
                sb.Append("        <tr>\r\n");
                sb.Append("            <th>\r\n");
                sb.Append("                @Html.LabelFor(model => model." + parentName.Trim() + ")：\r\n");
                sb.Append("            </th>\r\n");
                sb.Append("            <td>\r\n");
                sb.Append("                 @Html.DropDownListFor(model => model." + parentName.Trim() + ", ViewBag." + (parentTableName.Trim().IndexOf("_") > 0 ? parentTableName.Trim().Split('_')[1] : parentTableName.Trim()) + " as SelectList)\r\n");
                sb.Append("            </td>\r\n");
                sb.Append("            <td>@Html.ValidationMessageFor(model => model." + parentName.Trim() + ")</td>\r\n");
                sb.Append("        </tr>\r\n");
            }


            foreach (CompleteField field in fields)
            {
                //启用表关联

                //表1
                if (!string.IsNullOrWhiteSpace(parentTableName))
                {
                    if (field.name.ToLower() == parentName.ToLower().Trim())
                        continue;
                }

                if (field.name != "Id" && field.name != "CreateTime")
                {
                    if (field.xType == "104" || field.xType == "bool")
                    {
                        sb.Append("        <tr>\r\n");
                        sb.Append("            <th>\r\n");
                        sb.Append("                @Html.LabelFor(model => model." + field.name + ")：\r\n");
                        sb.Append("            </th>\r\n");
                        sb.Append("            <td>\r\n");
                        sb.Append("                  @Html.RadioFor(\"" + field.name + "\", true,\"\",\"\")\r\n");
                        sb.Append("            </td>\r\n");
                        sb.Append("            <td>@Html.ValidationMessageFor(model => model." + field.name + ")</td>\r\n");
                        sb.Append("        </tr>\r\n");

                    }
                    else if (field.name.ToLower().Contains("img") || field.name.ToLower().Contains("photo"))
                    {
                        sb.Append("        <tr>\r\n");
                        sb.Append("            <th>\r\n");
                        sb.Append("                @Html.LabelFor(model => model." + field.name + ")：\r\n");
                        sb.Append("            </th>\r\n");
                        sb.Append("            <td>\r\n");
                        sb.Append("             @Html.HiddenFor(model => model." + field.name + ")\r\n");
                        sb.Append("             <img class=\"expic\" src=\"/Content/Images/NotPic.jpg\" /><br />\r\n");
                        sb.Append("             <a class=\"files\">@Resource.Browse<input type=\"file\" id=\"FileUpload_" + field.name + "\" name=\"FileUpload_" + field.name + "\" onchange=\"Upload('SingleFile', '" + field.name + "', 'FileUpload_" + field.name + "','1','1');\" />\r\n</a><a onclick=\"resetImg(this)\" class=\"files\">清除</a>\r\n");
                        sb.Append("             <span class=\"uploading\">@Resource.Uploading</span>\r\n");
                        sb.Append("            </td>\r\n");
                        sb.Append("            <td>@Html.ValidationMessageFor(model => model." + field.name + ")</td>\r\n");
                        sb.Append("        </tr>\r\n");
                    }
                    else if (field.xType == "61" || field.xType == "datetime")
                    {
                        sb.Append("        <tr>\r\n");
                        sb.Append("            <th>\r\n");
                        sb.Append("                @Html.LabelFor(model => model." + field.name + ")：\r\n");
                        sb.Append("            </th>\r\n");
                        sb.Append("            <td >\r\n");
                        sb.Append("               @Html.TextBoxFor(model => model." + field.name + ", new { @onClick = \"WdatePicker()\",@style = \"width: 105px\" })\r\n");
                        sb.Append("            </td>\r\n");
                        sb.Append("            <td>@Html.ValidationMessageFor(model => model." + field.name + ")</td>\r\n");
                        sb.Append("        </tr>\r\n");
                    }
                    else
                    {
                        sb.Append("        <tr>\r\n");
                        sb.Append("            <th>\r\n");
                        sb.Append("                @Html.LabelFor(model => model." + field.name + ")：\r\n");
                        sb.Append("            </th>\r\n");
                        sb.Append("            <td>\r\n");
                        sb.Append("                @Html.EditorFor(model => model." + field.name + ")\r\n");
                        sb.Append("            </td>\r\n");
                        sb.Append("            <td>@Html.ValidationMessageFor(model => model." + field.name + ")</td>\r\n");
                        sb.Append("        </tr>\r\n");
                    }
                }
            }
            sb.Append("    </tbody>\r\n");
            sb.Append("</table>\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }

        public string GetEdit(string tableName, string parentTableName, string parentName)
        {
            string leftStr = CodeHelper.GetLeftStr(tableName);
            List<CompleteField> fields = SqlHelper.GetColumnCompleteField(tableName);

            StringBuilder sb = new StringBuilder();
            sb.Append("@model Apps.Models." + (leftStr == "Sys" ? "" : (leftStr + ".")) + "" + tableName + "Model\r\n");
            sb.Append("@using Apps.Web.Core;\r\n");
            sb.Append("@using Apps.Common;\r\n");
            sb.Append("@using Apps.Models." + leftStr.Replace(".", "") + ";\r\n");
            sb.Append("@using Apps.Web;\r\n");
            sb.Append("@using Apps.Locale;\r\n");
            sb.Append("@using Apps.Models.Sys;\r\n");
            sb.Append("@{\r\n");
            sb.Append("    ViewBag.Title = \"修改\";\r\n");
            sb.Append("    Layout = \"~/Views/Shared/_Index_LayoutEdit.cshtml\";\r\n");
            sb.Append("    List<permModel> perm = null;\r\n");
            sb.Append("}\r\n");
            sb.Append("\r\n");
            sb.Append("<script type=\"text/javascript\">\r\n");
            sb.Append("$(function () {\r\n");
            sb.Append("    $(\"#btnSave\").click(function () {\r\n");
            sb.Append("        if ($(\"form\").valid()) {\r\n");
            sb.Append("            $.ajax({\r\n");
            sb.Append("                url: \"@Url.Action(\"Edit\")\",\r\n");
            sb.Append("                type: \"Post\",\r\n");
            sb.Append("                data: $(\"form\").serialize(),\r\n");
            sb.Append("                dataType: \"json\",\r\n");
            sb.Append("                success: function (data) {\r\n");
            sb.Append("                    if (data.type == 1) {\r\n");
            sb.Append("                        window.parent.frameReturnByMes(data.message);\r\n");
            sb.Append("                        window.parent.frameReturnByReload(false);\r\n");
            sb.Append("                        window.parent.frameReturnByClose()\r\n");
            sb.Append("                    }\r\n");
            sb.Append("                    else {\r\n");
            sb.Append("                        window.parent.frameReturnByMes(data.message);\r\n");
            sb.Append("                    }\r\n");
            sb.Append("                }\r\n");
            sb.Append("            });\r\n");
            sb.Append("        }\r\n");
            sb.Append("        return false;\r\n");
            sb.Append("    });\r\n");
            sb.Append("    $(\"#btnReturn\").click(function () {\r\n");
            sb.Append("         window.parent.frameReturnByClose();\r\n");
            sb.Append("    });\r\n");
            sb.Append("});\r\n");
            sb.Append("function frameReturnByClose() {\r\n");
            sb.Append("$(\"#modalwindow\").window('close');\r\n");
            sb.Append("}\r\n");
            sb.Append("function frameReturnByMes(mes) {\r\n");
            sb.Append("$.messageBox5s(Lang.Tip, mes);\r\n");
            sb.Append("}\r\n");
            sb.Append("</script>\r\n");
            sb.Append("<div id=\"modalwindow\" class=\"easyui-window\" style=\"width:800px; height:400px;\" data-options=\"modal:true,closed:true,minimizable:false,shadow:false\"></div>\r\n");
            sb.Append("<div class=\"mvctool bgb\">\r\n");
            sb.Append("@Html.ToolButton(\"btnSave\", \"fa fa-save\", Resource.Save,ref perm, \"Save\", true)\r\n");
            sb.Append("@Html.ToolButton(\"btnReturn\", \"fa fa-reply\", Resource.Reply,false)\r\n");
            sb.Append("</div>\r\n");
            sb.Append("@using (Html.BeginForm())\r\n");
            sb.Append("{\r\n");
            foreach (CompleteField field in fields)
            {
                if (field.name == "Id" || field.name == "CreateTime")
                {
                    sb.Append("             @Html.HiddenFor(model => model." + field.name + ")\r\n");
                }
            }
            sb.Append(" <table class=\"formtable\">\r\n");
            sb.Append("    <tbody>\r\n");

            //启用表关联
            //表1
            if (!string.IsNullOrWhiteSpace(parentTableName))
            {
                sb.Append("        <tr>\r\n");
                sb.Append("            <th>\r\n");
                sb.Append("                @Html.LabelFor(model => model." + parentName.Trim() + ")：\r\n");
                sb.Append("            </th>\r\n");
                sb.Append("            <td>\r\n");
                sb.Append("                 @Html.DropDownListFor(model => model." + parentName.Trim() + ", ViewBag." + (parentTableName.Trim().IndexOf("_") > 0 ? parentTableName.Trim().Split('_')[1] : parentTableName.Trim()) + " as SelectList)\r\n");
                sb.Append("            </td>\r\n");
                sb.Append("            <td>@Html.ValidationMessageFor(model => model." + parentName.Trim() + ")</td>\r\n");
                sb.Append("        </tr>\r\n");
            }


            foreach (CompleteField field in fields)
            {
                //启用表关联

                //表1
                if (!string.IsNullOrWhiteSpace(parentTableName))
                {
                    if (field.name.ToLower() == parentName.ToLower().Trim())
                        continue;
                }

                if (field.name != "Id" && field.name != "CreateTime")
                {
                    if (field.xType == "104" || field.xType == "bool")
                    {
                        sb.Append("        <tr>\r\n");
                        sb.Append("            <th>\r\n");
                        sb.Append("                @Html.LabelFor(model => model." + field.name + ")：\r\n");
                        sb.Append("            </th>\r\n");
                        sb.Append("            <td >\r\n");
                        sb.Append("               @Html.RadioFor(\"" + field.name + "\", Model." + field.name + ",\"\",\"\")\r\n");
                        sb.Append("            </td>\r\n");
                        sb.Append("            <td>@Html.ValidationMessageFor(model => model." + field.name + ")</td>\r\n");
                        sb.Append("        </tr>\r\n");

                    }
                    else if (field.name.ToLower().Contains("img") || field.name.ToLower().Contains("photo"))
                    {
                        sb.Append("        <tr>\r\n");
                        sb.Append("            <th>\r\n");
                        sb.Append("                @Html.LabelFor(model => model." + field.name + ")：\r\n");
                        sb.Append("            </th>\r\n");
                        sb.Append("            <td>\r\n");
                        sb.Append("             @Html.HiddenFor(model => model." + field.name + ")\r\n");
                        sb.Append("             <img class=\"expic\" src=\"/Content/Images/NotPic.jpg\" /><br />\r\n");
                        sb.Append("             <a class=\"files\">@Resource.Browse<input type=\"file\" id=\"FileUpload_" + field.name + "\" name=\"FileUpload_" + field.name + "\" onchange=\"Upload('SingleFile', '" + field.name + "', 'FileUpload_" + field.name + "','1','1');\" />\r\n</a><a onclick=\"resetImg(this)\" class=\"files\">清除</a>\r\n");
                        sb.Append("             <span class=\"uploading\">@Resource.Uploading</span>\r\n");
                        sb.Append("            </td>\r\n");
                        sb.Append("            <td>@Html.ValidationMessageFor(model => model." + field.name + ")</td>\r\n");
                        sb.Append("        </tr>\r\n");
                    }
                    else if (field.xType == "61" || field.xType == "datetime")
                    {
                        sb.Append("        <tr>\r\n");
                        sb.Append("            <th>\r\n");
                        sb.Append("                @Html.LabelFor(model => model." + field.name + ")：\r\n");
                        sb.Append("            </th>\r\n");
                        sb.Append("            <td >\r\n");
                        sb.Append("               @Html.TextBoxFor(model => model." + field.name + ", new { @onClick = \"WdatePicker()\",@style = \"width: 105px\" })\r\n");
                        sb.Append("            </td>\r\n");
                        sb.Append("            <td>@Html.ValidationMessageFor(model => model." + field.name + ")</td>\r\n");
                        sb.Append("        </tr>\r\n");
                    }
                    else
                    {
                        sb.Append("        <tr>\r\n");
                        sb.Append("            <th>\r\n");
                        sb.Append("                @Html.LabelFor(model => model." + field.name + ")：\r\n");
                        sb.Append("            </th>\r\n");
                        sb.Append("            <td >\r\n");
                        sb.Append("                @Html.EditorFor(model => model." + field.name + ")\r\n");
                        sb.Append("            </td>\r\n");
                        sb.Append("            <td>@Html.ValidationMessageFor(model => model." + field.name + ")</td>\r\n");
                        sb.Append("        </tr>\r\n");
                    }
                }
            }
            sb.Append("    </tbody>\r\n");
            sb.Append("</table>\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }

        public string GetBLL(string tableName, string parentTableName, string parentName)
        {
            string leftStr = CodeHelper.GetLeftStr(tableName);
            List<string> fields = SqlHelper.GetColumnField(tableName);

            StringBuilder sb = new StringBuilder();
            sb.Append("using Apps.Common;\r\n");
            sb.Append("using Apps.Models;\r\n");
            sb.Append("using System.Linq;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System.Linq;\r\n");
            sb.Append("using System;\r\n");
            sb.Append("using Apps.Models." + leftStr.Replace(".", "") + ";\r\n");
            sb.Append("\r\n");
            sb.Append("namespace Apps.BLL" + (leftStr == ".Sys" ? "" : "." + leftStr) + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    public  partial class " + tableName + "BLL\r\n");
            sb.Append("    {\r\n");
            sb.Append("\r\n");

            List<CompleteField> comFields = SqlHelper.GetColumnCompleteField(tableName);
            CompleteField parentField = null;
            foreach (CompleteField field in comFields)
            {
                if (field.name.ToLower() == parentName.ToLower().Trim())
                {
                    parentField = field;
                }
            }

            if (parentField == null)
            {
                return "关外键" + parentName + "不正确，请重新确认！不然生成数据需要手动更改";
            }

            sb.Append("    public override List<" + tableName + "Model> GetListByParentId(ref GridPager pager, string queryStr, object parentId)\r\n");
            sb.Append("    {\r\n");
            sb.Append("        IQueryable<" + tableName + "> queryData = null;\r\n");
            if (parentField.xType != "56" && parentField.xType != "127")//非int型主键
            {
                sb.Append("        string pid = parentId.ToString();\r\n");
            }
            else
            {
                sb.Append("        int pid = Convert.ToInt32(parentId);\r\n");
            }

            sb.Append("        if (pid != " + (parentField.xType != "56" && parentField.xType != "127" ? "\"0\"" : "0") + ")\r\n");
            sb.Append("        {\r\n");
            sb.Append("             queryData = m_Rep.GetList(a => a." + parentName + " == pid);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        else\r\n");
            sb.Append("        {\r\n");
            sb.Append("             queryData = m_Rep.GetList();\r\n");
            sb.Append("        }\r\n");

            sb.Append("        if (!string.IsNullOrWhiteSpace(queryStr))\r\n");
            sb.Append("        {\r\n");
            sb.Append("            queryData = m_Rep.GetList(\r\n");
            sb.Append("                        a => (\r\n");
            bool orEnable = false;
            foreach (CompleteField field in comFields)
            {
                if (field.xType == "167" || field.xType == "35" || field.xType == "231" || field.xType == "239")
                {
                    sb.Append("                               " + (orEnable ? "||" : "") + " a." + field.name + ".Contains(queryStr)\r\n");
                    orEnable = true;
                }
            }
            sb.Append("                             )\r\n");
            sb.Append("                        );\r\n");
            sb.Append("        }\r\n");
            sb.Append("        pager.totalRows = queryData.Count();\r\n");
            sb.Append("        queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);\r\n");
            sb.Append("        return CreateModelList(ref queryData);\r\n");
            sb.Append("    }\r\n");
            sb.Append("        public List<" + tableName + "Model> GetList(string parentId)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            IQueryable <" + tableName + "> queryData = null;\r\n");
            sb.Append("            queryData = m_Rep.GetList(a => a.ParentId == parentId).OrderBy(a => a.CreateTime);\r\n");
            sb.Append("            return CreateModelList(ref queryData);\r\n");
            sb.Append("        }\r\n");


            sb.Append("        public override List<" + tableName + "Model> CreateModelList(ref IQueryable<" + tableName + "> queryData)\r\n");
            sb.Append("        {\r\n");
            sb.Append("\r\n");
            sb.Append("            List<" + tableName + "Model> modelList = (from r in queryData\r\n");
            sb.Append("                                              select new " + tableName + "Model\r\n");
            sb.Append("                                              {\r\n");
            foreach (string field in fields)
            {
                sb.Append("                                                  " + field + " = r." + field + ",\r\n");
            }
            //启用表关联

            //表1
            if (!string.IsNullOrWhiteSpace(parentTableName))
            {
                sb.Append("                                                  " + parentTableName.Replace(leftStr + "_", "") + "Name".Trim() + " = r." + parentTableName.Trim() + ".Name,\r\n");
            }

            sb.Append("                                              }).ToList();\r\n");
            sb.Append("            return modelList;\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append(" }\r\n");
            return sb.ToString();
        }

        public string GetModel(string tableName, string parentTableName, string parentName,string parentNameS)
        {
            string leftStr = CodeHelper.GetLeftStr(tableName);
            List<CompleteField> fields = SqlHelper.GetColumnCompleteField(tableName);

            StringBuilder sb = new StringBuilder();
            sb.Append("using System;\r\n");
            sb.Append("using System.ComponentModel.DataAnnotations;\r\n");
            sb.Append("using Apps.Models;\r\n");
            sb.Append("namespace Apps.Models." + leftStr.Replace(".", "") + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    public partial class " + tableName + "Model\r\n");
            sb.Append("    {\r\n");
            //表1
            if (!string.IsNullOrWhiteSpace(parentTableName))
            {
                sb.Append("        [Display(Name = \"类别名称\")]\r\n");
                sb.Append("        public string " + parentTableName.Replace(leftStr + "_", "") + parentNameS.Trim() + " { get; set; }\r\n");
            }
            sb.Append("     }\r\n");
            sb.Append("}\r\n");


            return sb.ToString();
        }
    }
}