using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Apps.Models.Sys;
using Apps.Models;
using Unity.Attributes;
using Apps.IBLL;
using Apps.Common;
using System.Globalization;
using System.Threading;
using System.Text;
using System;
using Apps.Web.Core;
using Apps.Locale;
using Apps.Core.OnlineStat;
using Apps.Models.MIS;
using Apps.IBLL.MIS;
using Apps.Models.Flow;
using Apps.IBLL.Flow;
using Apps.IBLL.Sys;
using Apps.BLL.Sys;

namespace Apps.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region UI框架
        [Dependency]
        public IHomeBLL homeBLL { get; set; }
        [Dependency]
        public ISysModuleBLL m_BLL { get; set; }


        [Dependency]
        public ISysValidTokenBLL tokenBLL { get; set; }

        private SysConfigModel siteConfig = new SysConfigBLL().loadConfig(Utils.GetXmlMapPath("Configpath"));
        ValidationErrors errors = new ValidationErrors();
        [Dependency]
        public ISysUserConfigBLL userConfigBLL { get; set; }

        public ActionResult Index()
        {
            if (CookieHelper.GetCookie("Account", true) != "")
            {

                //系统名称
                ViewBag.WebName = siteConfig.webname;
                //公司名称
                ViewBag.ComName = siteConfig.webcompany;
                //版权
                ViewBag.CopyRight = siteConfig.webcopyright;
                //在线人数
                //OnlineUserRecorder recorder = HttpContext.Cache[OnlineHttpModule.g_onlineUserRecorderCacheKey] as OnlineUserRecorder;
                ViewBag.OnlineCount = 100;// recorder.GetUserList().Count;
                AccountModel account = new AccountModel();
                account = JsonHandler.Deserialize<AccountModel>(CookieHelper.GetCookie("Account", true));
                GetThemes(GetUserId());
                return View(account);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }


        }
        public void GetThemes(string userid)
        {
            SysUserConfigModel entity = userConfigBLL.GetById("themes", userid);
            SysUserConfigModel menuEntity = userConfigBLL.GetById("menu", userid);
            if (entity != null)
            {
                Session["themes"] = entity.Value;
            }
            else
            {
                Session["themes"] = "bluelight";
            }
            if (menuEntity != null)
            {
                Session["menu"] = menuEntity.Value;
            }
            else
            {
                Session["menu"] = "accordion";
            }
        }
        //父ID=0的数据为顶级菜单
        public JsonResult GetTopMenu()
        {
            //加入本地化
            CultureInfo info = Thread.CurrentThread.CurrentCulture;
            string infoName = info.Name;
            var cookie = CookieHelper.GetCookie("Account", true);
            if (cookie != "")
            {
                //加入本地化
                AccountModel account = JsonHandler.Deserialize<AccountModel>(cookie);
                List<SysModuleModel> list = homeBLL.GetMenuByPersonId(account.Id, "0");
                var json = from r in list
                           select new SysModuleNavModel()
                           {
                               id = r.Id,
                               text = infoName.IndexOf("zh") > -1 || infoName == "" ? r.Name : r.EnglishName,     //text
                               attributes = (infoName.IndexOf("zh") > -1 || infoName == "" ? "zh-CN" : "en-US") + "/" + r.Url,
                               iconCls = r.Iconic
                           };


                return Json(json);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }



        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>树</returns>
        public JsonResult GetTreeByEasyui(string id)
        {
            //加入本地化
            CultureInfo info = Thread.CurrentThread.CurrentCulture;
            string infoName = info.Name;
            bool flag = true;
            //加入本地化
            List<SysModuleModel> list = homeBLL.GetMenuByPersonId(GetUserId(), id);
            List<SysModuleNavModel> navList = new List<SysModuleNavModel>();
            SysModuleNavModel model = new SysModuleNavModel();
            foreach (var r in list)
            {
                model = new SysModuleNavModel();
                model.id = r.Id;
                model.text = infoName.IndexOf("zh-CN") > -1 || infoName == "" ? r.Name : r.EnglishName;    //text
                model.attributes = !string.IsNullOrEmpty(r.Url) && r.Url.StartsWith("http") ? r.Url : (infoName.IndexOf("zh-CN") > -1 || infoName == "" ? "zh-CN" : "en-US") + "/" + r.Url;
                model.iconCls = r.Iconic;
                model.state = flag ? "open" : (!r.IsLast) ? "closed" : "open";
                model.children = (!r.IsLast) ? GetChildren(r.Id) : null;
                navList.Add(model);
                flag = false;
            }



            return Json(navList);

        }

        public List<SysModuleNavModel> GetChildren(string id)
        {
            CultureInfo info = Thread.CurrentThread.CurrentCulture;
            string infoName = info.Name;
            List<SysModuleNavModel> childList = new List<SysModuleNavModel>();
            List<SysModuleModel> list = homeBLL.GetMenuByPersonId(GetUserId(), id);

            if (list.Count() < 1)
            {
                return null;
            }

            childList = (from r in list
                         select new SysModuleNavModel()
                         {
                             id = r.Id,
                             text = infoName.IndexOf("zh-CN") > -1 || infoName == "" ? r.Name : r.EnglishName,     //text
                             attributes = !string.IsNullOrEmpty(r.Url) && r.Url.StartsWith("http") ? r.Url : (infoName.IndexOf("zh-CN") > -1 || infoName == "" ? "zh-CN" : infoName.IndexOf("zh-TW") > -1 ? "zh-TW" : "en-US") + "/" + r.Url,

                             iconCls = r.Iconic,
                             state = (!r.IsLast) ? "closed" : "open",
                             children = (!r.IsLast) ? GetChildren(r.Id) : null
                         }).ToList();

            return childList;
        }

        [HttpPost]
        public JsonResult SetThemes(string theme, string menu, bool topmenu)
        {
            SysUserConfigModel entity = userConfigBLL.GetById("themes", GetUserId());
            if (entity != null)
            {
                entity.Value = theme;
                userConfigBLL.Edit(ref errors, entity);
            }
            else
            {
                entity = new SysUserConfigModel()
                {
                    Id = "themes",
                    Name = "用户自定义主题",
                    Value = theme,
                    Type = "themes",
                    State = true,
                    UserId = GetUserId()
                };
                userConfigBLL.Create(ref errors, entity);

            }
            Session["themes"] = theme;

            //开启顶部菜单，顶部菜单必须配置多一层
            if (topmenu)
            {
                menu = menu + ",topmenu";
            }
            SysUserConfigModel entityMenu = userConfigBLL.GetById("menu", GetUserId());
            if (entityMenu != null)
            {
                entityMenu.Value = menu;
                userConfigBLL.Edit(ref errors, entityMenu);
            }
            else
            {
                entityMenu = new SysUserConfigModel()
                {
                    Id = "menu",
                    Name = "用户自定义菜单",
                    Value = menu,
                    Type = "menu",
                    State = true,
                    UserId = GetUserId()
                };
                userConfigBLL.Create(ref errors, entityMenu);

            }

            Session["menu"] = menu;
            return Json("1", JsonRequestBehavior.AllowGet);
        }


        public ActionResult TopInfo()
        {
            var cookie = CookieHelper.GetCookie("Account", true);
            if (cookie != "")
            {
                //加入本地化
                AccountModel account = JsonHandler.Deserialize<AccountModel>(cookie);
                return View(account);
            }
            return View();
        }

        #endregion

        #region js配置

        public JavaScriptResult ConfigJs()
        {
            CultureInfo info = Thread.CurrentThread.CurrentCulture;
            StringBuilder sb = new StringBuilder();
            string userId = GetUserId();
            SysValidTokenModel token = tokenBLL.GetById(userId);
            var Token = "";
            if (token == null)
            {
                Token = DESEncrypt.Encrypt(string.Format("{0}&{1}", GetUserId(), DateTime.Now.ToString("yyyyMMddHHmmssfffffff")));
                SysValidTokenModel tokenModel = new SysValidTokenModel();
                tokenModel.Id = GetUserId();
                tokenModel.Token = Token;
                tokenModel.OverTime = ResultHelper.NowTime.AddDays(3);
                tokenModel.CreateTime = ResultHelper.NowTime;
                //不存在就创建一个新的token
                tokenBLL.Create(ref errors, tokenModel);
            }
            else
            {
                //如果过期，那么刷新时间
                if (token.OverTime <= ResultHelper.NowTime)
                {
                    token.OverTime = ResultHelper.NowTime.AddDays(3);
                    tokenBLL.Edit(ref errors, token);
                }

            }

            sb.Append("var _YMGlobal;");
            sb.Append("(function(_YMGlobal) {");
            sb.Append("    var Config = (function() {");
            sb.Append("        function Config() {}");
            sb.AppendFormat("  Config.currentCulture = '{0}';", info.Name);
            sb.AppendFormat("  Config.apiUrl = '{0}';", "");
            sb.AppendFormat("  Config.token = '{0}';", Token);
            sb.Append("       return Config;");
            sb.Append("   })();");
            sb.Append("   _YMGlobal.Config = Config;");
            sb.Append(" })(_YMGlobal || (_YMGlobal = { }));");

            return JavaScript(sb.ToString());
        }

        #endregion


        [Dependency]
        public ISysUserBLL userBLL { get; set; }
        [Dependency]
        public ISysStructBLL structBLL { get; set; }
        [Dependency]
        public ISysAreasBLL areasBLL { get; set; }
        [Dependency]
        public ISysUserBLL sysUserBLL { get; set; }
        [Dependency]
        public IAccountBLL accountBLL { get; set; }
        [Dependency]
        public IMIS_ArticleBLL atr_BLL { get; set; }
        [Dependency]
        public IFlow_FormContentBLL formContentBLL { get; set; }

        #region 我的资料
        public ActionResult Info()
        {
            SysUserModel model = sysUserBLL.GetById(GetUserId());
            //防止读取错误

            return View(model);
        }


        [HttpPost]
        public JsonResult EditPwd(string oldPwd, string newPwd)
        {
            SysUserModel user = accountBLL.Login(GetAccount().UserName, ValueConvert.MD5(oldPwd));
            if (user == null)
            {
                return Json(JsonHandler.CreateMessage(0, "旧密码不匹配！"), JsonRequestBehavior.AllowGet);
            }
            SysUserModel editModel = new SysUserModel();
            editModel.Id = GetUserId();
            editModel.Password = ValueConvert.MD5(newPwd);

            if (userBLL.EditPwd(ref errors, editModel))
            {
                LogHandler.WriteServiceLog(GetUserId(), "Id:" + GetUserId() + ",密码:********", "成功", "初始化密码", "用户设置");
                return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed), JsonRequestBehavior.AllowGet);
            }
            else
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(GetUserId(), "Id:" + GetUserId() + ",,密码:********" + ErrorCol, "失败", "初始化密码", "用户设置");
                return Json(JsonHandler.CreateMessage(0, Resource.EditFail + ":" + ErrorCol), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region webpart
        [Dependency]
        public IWebpartBLL webPartBLL { get; set; }


        public ActionResult Desktop()
        {
            SysUserConfig ss = webPartBLL.GetByIdAndUserId("webpart", GetUserId());
            if (ss != null)
            {
                ViewBag.Value = ss.Value;
            }
            else
            {
                ViewBag.Value = "";
            }
            return View();
        }
        [HttpPost]
        public JsonResult GetPartDataByShortcut()
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPartDataByMyJob()
        {
            GridPager pager = new GridPager()
            {
                page = 1,
                rows = 6,
                sort = "CreateTime",
                order = "desc"
            };
            List<Flow_FormContentModel> list = formContentBLL.GeExaminetListByUserId(ref pager, "", GetUserId());

            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new Flow_FormContentModel()
                        {
                            Id = r.Id,
                            Title = r.Title,
                            UserId = r.UserId,
                            UserName = r.UserName,
                            FormId = r.FormId,
                            FormLevel = r.FormLevel,
                            CreateTime = r.CreateTime,
                            TimeOut = r.TimeOut,
                            CurrentStep = formContentBLL.GetCurrentFormStep(r),
                            CurrentState = formContentBLL.GetCurrentFormState(r)

                        }).OrderByDescending(a => a.CurrentState).ToArray()

            };
            return Json(json);
        }
        [HttpPost]
        public JsonResult GetPartDataByNotice()
        {

            List<MIS_ArticleModel> list = new List<MIS_ArticleModel>();
            GridPager pager = new GridPager()
            {
                page = 1,
                rows = 6,
                sort = "Id",
                order = "desc"
            };
            list = atr_BLL.GetList(ref pager, "", "", true, "", 2);

            GridRows<MIS_ArticleModel> grs = new GridRows<MIS_ArticleModel>();
            grs.rows = list;//启用数据过滤
            grs.total = pager.totalRows;

            return Json(grs);
        }
        [HttpPost]
        public JsonResult GetPartDataByData()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPartDataByNote()
        {
            return Json("<span style='color:#b200ff'>语言版本进行大部分翻译，其他未翻译部分需要自行翻译<span>", JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public JsonResult GetPartData6()
        //{

        //    // 获取在线用户记录器
        //    OnlineUserRecorder recorder = HttpContext.Cache[OnlineHttpModule.g_onlineUserRecorderCacheKey] as OnlineUserRecorder;
        //    StringBuilder sb = new StringBuilder("");
        //    if (recorder == null)
        //        return Json("在线用户", JsonRequestBehavior.AllowGet);

        //    //// 绑定在线用户列表
        //    IList<OnlineUser> userList = recorder.GetUserList();
        //    sb.AppendFormat("在线用户：<br>");
        //    foreach (var OnlineUser in userList)
        //    {
        //        sb.AppendFormat(OnlineUser.UserName + "<br>");
        //    }
        //    return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        //}

        ValidationErrors validationErrors = new ValidationErrors();
        [ValidateInput(false)]
        public JsonResult SaveHtml(string html)
        {
            webPartBLL.SaveHtml(ref validationErrors, GetUserId(), html);
            return Json("保存成功!", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 我的桌面
        public ActionResult Main()
        {
            return View();
        }
        #endregion
    }
}