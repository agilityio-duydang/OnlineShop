using CKSource.CKFinder.Connector.Core.Json;
using Models.Dao;
using Newtonsoft.Json;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private const string ShoppingCartSession = "ShoppingCartSession";

        // GET: Cart
        public ActionResult Index()
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var listShoppingCart = new List<ShoppingCartItem>();

                foreach (var item in customer.ShoppingCartItems)
                {
                    var shoppingCartItem = new ShoppingCartItem();
                    shoppingCartItem.Product = item.Product;
                    shoppingCartItem.Quantity = item.Quantity;
                    listShoppingCart.Add(shoppingCartItem);
                }
                return View(listShoppingCart);
            }
            else
            {
                var shoppingCart = Session[ShoppingCartSession];
                var listShoppingCart = new List<ShoppingCartItem>();
                if (shoppingCart != null)
                {
                    listShoppingCart = (List<ShoppingCartItem>)shoppingCart;
                }
                return View(listShoppingCart);
            }
        }

        public JsonResult Delete(long Id)
        {
            var shoppingCart = (List<ShoppingCartItem>)Session[ShoppingCartSession];
            shoppingCart.RemoveAll(x => x.Product.Id == Id);
            Session[ShoppingCartSession] = shoppingCart;
            return Json(new
            {
                Status = true
            });
        }
        public JsonResult DeleteAll()
        {
            Session[ShoppingCartSession] = null;
            return Json(new
            {
                Status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jSonShoppingCart = new JavaScriptSerializer().Deserialize<List<ShoppingCartItem>>(cartModel);
            var shoppingCart = (List<ShoppingCartItem>)Session[ShoppingCartSession];
            if (shoppingCart !=null)
            {
                foreach (var item in shoppingCart)
                {
                    var jsonItem = jSonShoppingCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                    if (jsonItem != null)
                    {
                        item.Quantity = jsonItem.Quantity;
                    }
                }
            }
            Session[ShoppingCartSession] = shoppingCart;
            return Json(new
            {
                Status = true
            });
        }
        public ActionResult AddProductToCartAjax(int productId, int quantity ,bool isAddToCartButton)
        {
            var listShoppingCart = new List<ShoppingCartItem>();
            var model = new ProductDao().GetProductById(productId);
            if (isAddToCartButton)
            {
                var shoppingCart = Session[ShoppingCartSession];
                if (shoppingCart != null)
                {
                    listShoppingCart = (List<ShoppingCartItem>)shoppingCart;
                    if (listShoppingCart.Exists(x => x.Product.Id == productId))
                    {
                        foreach (var item in listShoppingCart)
                        {
                            if (item.Product.Id == productId)
                            {
                                item.Quantity += quantity;
                            }
                        }
                    }
                    else
                    {
                        var shoppingCartItem = new ShoppingCartItem();
                        shoppingCartItem.Product = model;
                        shoppingCartItem.Quantity = quantity;

                        listShoppingCart.Add(shoppingCartItem);
                    }

                    Session[ShoppingCartSession] = listShoppingCart;
                }
                else
                {
                    var shoppingCartItem = new ShoppingCartItem();
                    shoppingCartItem.Product = model;
                    shoppingCartItem.Quantity = quantity;

                    listShoppingCart.Add(shoppingCartItem);

                    Session[ShoppingCartSession] = listShoppingCart;
                }
            }
            var html = RenderViewToString(this.ControllerContext, "AddProductToCartAjax", model);
            var flyoutShoppingCart = RenderViewToString(this.ControllerContext, "FlyoutShoppingCart", listShoppingCart);
            var headerCart = "" + listShoppingCart.Count().ToString() + "";
            return Json(new
            {
                success = true,
                html,
                flyoutShoppingCart,
                headerCart
            });
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
        public class WishlistAndCart
        {
            public object PopupTitle { get; set; }
            public string Status { get; set; }
            public string AddToCartWarnings { get; set; }
            public string ErrorMessage { get; set; }
            public string RedirectUrl { get; set; }
            public string ProductAddedToCartWindow { get; set; }
        }

        public ActionResult FlyoutShoppingCart()
        {
            var shoppingCart = Session[ShoppingCartSession];
            var listShoppingCart = new List<ShoppingCartItem>();
            if (shoppingCart != null)
            {
                listShoppingCart = (List<ShoppingCartItem>)shoppingCart;
            }
            return PartialView(listShoppingCart);
        }
        public ActionResult MiniShoppingCart()
        {
            var shoppingCart = Session[ShoppingCartSession];
            var listShoppingCart = new List<ShoppingCartItem>();
            if (shoppingCart != null)
            {
                listShoppingCart = (List<ShoppingCartItem>)shoppingCart;
            }
            return PartialView(listShoppingCart);
        }
    }
}