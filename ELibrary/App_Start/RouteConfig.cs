using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ELibrary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            // "MyRoute", // Route name
            // "BasicAdmin/", // URL 
            // new { controller = "BasicAdmin", action = "UpdateBA" } // Parameter defaults
            // );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "SmartLogin", id = UrlParameter.Optional }
            );
        }
    }
}
