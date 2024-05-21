using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                name: "Products",
                url: "{controller}",
                defaults: new {controller = "Products", action = "Index"}
            );

            routes.MapRoute(
                name: "CreateProduct",
                url: "{controller}/{action}",
                defaults: new { controller = "Products", action = "Create" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "{controller}/{action}",
                defaults: new { controller = "Products", action = "Cart" }
            );

            routes.MapRoute(
                name: "SignIn",
                url: "{controller}/{action}",
                defaults: new { controller = "Users", action = "SignIn" }
            );

            routes.MapRoute(
                name: "SignUp",
                url: "{controller}/{action}",
                defaults: new { controller = "Users", action = "SignUp" }
            );
        }
    }
}
