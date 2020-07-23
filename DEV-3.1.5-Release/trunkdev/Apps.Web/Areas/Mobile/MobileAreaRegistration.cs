using System.Web.Mvc;

namespace Apps.Web.Areas.Mobile
{
    public class MobileAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Mobile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
               "MobileGlobalization", // 路由名称
               "{lang}/Mobile/{controller}/{action}/{id}", // 带有参数的 URL
               new { lang = "zh", controller = "Home", action = "Index", id = UrlParameter.Optional }, // 参数默认值
               new { lang = "^[a-zA-Z]{2}(-[a-zA-Z]{2})?$" },    //参数约束
               new string[] { "Apps.Web.Areas.Mobile.Controllers" }
           );
            context.MapRoute(
                "Mobile_default",
                "Mobile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Apps.Web.Areas.Mobile.Controllers" }
            );
        }
    }
}