using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication5
{
    public class RouteConfig
    {
        private string alias { get; set; }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{alias}/{controller}/{action}/{id}",
                defaults: new { alias = "pdvplus",  controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
