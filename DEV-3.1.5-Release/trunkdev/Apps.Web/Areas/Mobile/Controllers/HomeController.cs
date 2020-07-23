using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps.Web.Areas.Mobile.Controllers
{
    public class HomeController : Controller
    {
        // 首页
        public ActionResult Index()
        {
            return View();
        }
        //帮助文档
        public ActionResult ShortCodes()
        {
            return View();
        }

        //待办
        public ActionResult Todo()
        {
            return View();
        }

        //详情
        public ActionResult Details()
        {
            return View();
        }
    }
}