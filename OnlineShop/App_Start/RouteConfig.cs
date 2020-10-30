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
                name: "Contact us",
                url: "contactus",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "Register",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Edit Customer",
                url: "Customer/AddressEdit",
                defaults: new { controller = "Customer", action = "AddressEdit", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Password recovery",
                url: "PasswordRecovery",
                defaults: new { controller = "User", action = "PasswordRecovery", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Order Detail",
                url: "OrderDetails/{id}",
                defaults: new { controller = "Order", action = "Details", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Re Order ",
                url: "ReOrder/{id}",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "About us",
                url: "about-us",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Shipping & returns",
                url: "shipping-returns",
                defaults: new { controller = "Home", action = "ShippingReturns", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Privacy notice",
                url: "privacy-notice",
                defaults: new { controller = "Home", action = "PrivacyNotice", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Recently viewed",
                url: "recentlyviewedproducts",
                defaults: new { controller = "Product", action = "RecentlyViewedProducts", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Conditions of Use",
                url: "conditions-of-use",
                defaults: new { controller = "Home", action = "ConditionsOfUse", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Search",
                url: "filterSearch",
                defaults: new { controller = "FilterSearch", action = "Index", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );
            routes.MapRoute(
                name: "Category",
                url: "category/{categoryName}",
                defaults: new { controller = "Category", action = "CategoryDetails", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );

            routes.MapRoute(
                name: "Add Product To Cart Ajax",
                url: "AddProductToCartAjax",
                defaults: new { controller = "Cart", action = "AddProductToCartAjax", id = UrlParameter.Optional },
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
