using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProductManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "ListProduct",
               url: "ListProduct/{action}/{id}",
               defaults: new { controller = "ListProduct", action = "Index", id = UrlParameter.Optional }
            );     

            routes.MapRoute(
                name: "AddProduct",
                url: "AddProduct/{action}/{id}",
                defaults: new { controller = "AddProduct", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Login",
            url: "Login/{action}/{id}",
            defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
