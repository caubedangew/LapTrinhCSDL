using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var cloudinaryConfig = new CloudinaryConfig()
            {
                CloudName = "dn84ltxow",
                ApiKey = "251333144845721",
                ApiSecret = "YFNzWK9pM2ktEb30zBbmIfMpczs"
            };

            Account account = new Account(cloudinaryConfig.CloudName, cloudinaryConfig.ApiKey, cloudinaryConfig.ApiSecret);

            Cloudinary cloudinary = new Cloudinary(account);

            HttpContext.Current.Application["cloudinary"] = cloudinary;
        }
    }
}
