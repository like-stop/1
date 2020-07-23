using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps.Web.Areas.Mobile.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 系统登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public ActionResult Person()
        {
            return View();
        }

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        public ActionResult Aboutme()
        {
            return View();
        }
    }
}