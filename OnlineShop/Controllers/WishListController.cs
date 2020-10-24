using Models.Dao;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class WishListController : Controller
    {
        private const string WishListSession = "WishListSession";

        // GET: WishList
        public ActionResult Index()
        {
            var wishListItem = Session[WishListSession];
            var listwishListItem = new List<WishListItem>();
            if (wishListItem != null)
            {
                listwishListItem = (List<WishListItem>)wishListItem;
            }
            return View(listwishListItem);
        }
        public static string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult Add(int productId, int shoppingCartTypeId,int quantity)
        {
            var cartType = (ShoppingCartType)shoppingCartTypeId;
            var listwishListItem = new List<WishListItem>();

            var model = new ProductDao().GetProductById(productId);
            var wishListItem = Session[WishListSession];
            if (wishListItem != null)
            {
                listwishListItem = (List<WishListItem>)wishListItem;
                if (!listwishListItem.Exists(x => x.Product.Id == productId))
                {
                    foreach (var item in listwishListItem)
                    {
                        if (item.Product.Id == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var wishList = new WishListItem();
                    wishList.Product = model;
                    wishList.Quantity = quantity;

                    listwishListItem.Add(wishList);
                }
                Session[WishListSession] = listwishListItem;
            }
            else
            {
                var wishList = new WishListItem();
                wishList.Product = model;
                wishList.Quantity = quantity;

                listwishListItem.Add(wishList);

                Session[WishListSession] = listwishListItem;
            }
            var html = RenderViewToString(this.ControllerContext, "MiniViewAddWishList", model);
            var headerWishList = "(" + listwishListItem.Count().ToString() + ")";
            return Json(new
            {
                success = true,
                html,
                headerWishList
            });
        }
    }
}