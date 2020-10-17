using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Category",
                url: "category/{categoryName}",
                defaults: new { controller = "Category", action = "CategoryDetails", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "QuickViewData",
                url: "product/quickviewdata/{id}",
                defaults: new { controller = "Product", action = "QuickViewData", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Product",
                url: "product/{productName}",
                defaults: new { controller = "Product", action = "ProductDetails", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );
        }
    }
}
