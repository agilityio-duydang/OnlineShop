using CKSource.CKFinder.Connector.Core.Json;
using Models.Dao;
using Models.EF;
using Newtonsoft.Json;
using OnlineShop.Common;
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
        private const string WishListSession = "WishListSession";
        // GET: Cart
        public ActionResult Index()
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var listShoppingCart = new List<ShoppingCartItemModel>();

                foreach (var item in customer.ShoppingCartItems)
                {
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart)
                    {
                        var shoppingCartItem = new ShoppingCartItemModel();
                        shoppingCartItem.Product = item.Product;
                        shoppingCartItem.Quantity = item.Quantity;
                        shoppingCartItem.ShoppingCartTypeId = item.ShoppingCartTypeId;
                        listShoppingCart.Add(shoppingCartItem);
                    }
                }
                return View(listShoppingCart);
            }
            else
            {
                var shoppingCart = Session[ShoppingCartSession];
                var listShoppingCart = new List<ShoppingCartItemModel>();
                if (shoppingCart != null)
                {
                    listShoppingCart = (List<ShoppingCartItemModel>)shoppingCart;
                }
                return View(listShoppingCart);
            }
        }

        public JsonResult Delete(long Id)
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var shoppingCartItemDao = new ShoppingCartItemDao();
                foreach (var item in customer.ShoppingCartItems)
                {
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart && item.ProductId == Id)
                    {
                        shoppingCartItemDao.DeleteShoppingCartItem(item.Id);
                    }
                }
                return Json(new
                {
                    Status = true
                });
            }
            else
            {
                var shoppingCart = (List<ShoppingCartItemModel>)Session[ShoppingCartSession];
                shoppingCart.RemoveAll(x => x.Product.Id == Id && x.ShoppingCartTypeId == 1);
                Session[ShoppingCartSession] = shoppingCart;
                return Json(new
                {
                    Status = true
                });
            }
        }
        public JsonResult DeleteAll()
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var shoppingCartItemDao = new ShoppingCartItemDao();
                foreach (var item in customer.ShoppingCartItems)
                {
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart)
                    {
                        shoppingCartItemDao.DeleteShoppingCartItem(item.Id);
                    }
                }
                return Json(new
                {
                    Status = true
                });
            }
            else
            {
                Session[ShoppingCartSession] = null;
                return Json(new
                {
                    Status = true
                });
            }
        }
        public JsonResult Update(string cartModel)
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var jSonShoppingCart = new JavaScriptSerializer().Deserialize<List<ShoppingCartItemModel>>(cartModel);
                var shoppingCartItemDao = new ShoppingCartItemDao();
                foreach (var item in customer.ShoppingCartItems)
                {
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart)
                    {
                        var jsonItem = jSonShoppingCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                        if (jsonItem != null)
                        {
                            item.Quantity = jsonItem.Quantity;
                            shoppingCartItemDao.UpdateShoppingCartItem(item);
                        }
                    }
                }
                return Json(new
                {
                    Status = true
                });
            }
            else
            {
                var jSonShoppingCart = new JavaScriptSerializer().Deserialize<List<ShoppingCartItemModel>>(cartModel);
                var shoppingCart = (List<ShoppingCartItemModel>)Session[ShoppingCartSession];
                if (shoppingCart != null)
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
        }
        public ActionResult AddProductToCartAjax(int productId, int quantity, bool isAddToCartButton)
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerDao = new CustomerDao();
                var productDao = new ProductDao();

                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = customerDao.GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var model = productDao.GetProductById(productId);
                var shoppingCartItemDao = new ShoppingCartItemDao();

                if (customer.ShoppingCartItems.ToList().Exists(x => x.Product.Id == productId && x.ShoppingCartTypeId == 1))
                {
                    foreach (var item in customer.ShoppingCartItems)
                    {
                        if (item.Product.Id == productId && item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart)
                        {
                            item.Quantity += quantity;
                            shoppingCartItemDao.UpdateShoppingCartItem(item);
                        }
                    }
                }
                else
                {

                    var shoppingCartItem = new ShoppingCartItem();
                    shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;
                    shoppingCartItem.CustomerId = customer.Id;
                    shoppingCartItem.ProductId = model.Id;
                    shoppingCartItem.CustomerEnteredPrice = model.Price;
                    shoppingCartItem.Quantity = quantity;
                    shoppingCartItem.CreatedOnUtc = DateTime.UtcNow;
                    shoppingCartItem.UpdatedOnUtc = DateTime.UtcNow;
                    customer.ShoppingCartItems.Add(shoppingCartItem);
                    customerDao.UpdateCustomer(customer);
                }
                if (!isAddToCartButton)
                {
                    foreach (var item in customer.ShoppingCartItems)
                    {
                        if (item.ShoppingCartTypeId == (int)ShoppingCartType.Wishlist && item.ProductId == productId)
                        {
                            shoppingCartItemDao.DeleteShoppingCartItem(item.Id);
                        }
                    }
                }

                var listShoppingCart = new List<ShoppingCartItemModel>();

                foreach (var item in customer.ShoppingCartItems)
                {
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart)
                    {
                        var shoppingCartItem = new ShoppingCartItemModel();
                        shoppingCartItem.Product = item.Product;
                        shoppingCartItem.Quantity = item.Quantity;
                        shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;
                        listShoppingCart.Add(shoppingCartItem);
                    }
                }
                var html = RenderViewToString(this.ControllerContext, "AddProductToCartAjax", model);
                var flyoutShoppingCart = RenderViewToString(this.ControllerContext, "FlyoutShoppingCart", listShoppingCart);
                var headerCart = "" + customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count().ToString() + "";
                return Json(new
                {
                    success = true,
                    html,
                    flyoutShoppingCart,
                    headerCart
                });
            }
            else
            {
                var listShoppingCart = new List<ShoppingCartItemModel>();
                var model = new ProductDao().GetProductById(productId);

                var shoppingCart = Session[ShoppingCartSession];
                if (shoppingCart != null)
                {
                    listShoppingCart = (List<ShoppingCartItemModel>)shoppingCart;
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
                        var shoppingCartItem = new ShoppingCartItemModel();
                        shoppingCartItem.Product = model;
                        shoppingCartItem.Quantity = quantity;
                        shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;

                        listShoppingCart.Add(shoppingCartItem);
                    }

                    Session[ShoppingCartSession] = listShoppingCart;
                }
                else
                {
                    var shoppingCartItem = new ShoppingCartItemModel();
                    shoppingCartItem.Product = model;
                    shoppingCartItem.Quantity = quantity;
                    shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;
                    listShoppingCart.Add(shoppingCartItem);

                    Session[ShoppingCartSession] = listShoppingCart;
                }
                if (!isAddToCartButton)
                {
                    var wishlistCart = (List<ShoppingCartItemModel>)Session[WishListSession];
                    wishlistCart.RemoveAll(x => x.Product.Id == productId && x.ShoppingCartTypeId == 2);
                    Session[WishListSession] = wishlistCart;
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
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));

                var listShoppingCart = new List<ShoppingCartItemModel>();

                foreach (var item in customer.ShoppingCartItems)
                {
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart)
                    {
                        var shoppingCartItem = new ShoppingCartItemModel();
                        shoppingCartItem.Product = item.Product;
                        shoppingCartItem.Quantity = item.Quantity;
                        shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;
                        listShoppingCart.Add(shoppingCartItem);
                    }
                }
                return PartialView(listShoppingCart);
            }
            else
            {
                var shoppingCart = Session[ShoppingCartSession];
                var listShoppingCart = new List<ShoppingCartItemModel>();
                if (shoppingCart != null)
                {
                    listShoppingCart = (List<ShoppingCartItemModel>)shoppingCart;
                }
                return PartialView(listShoppingCart);
            }
        }
        public ActionResult MiniShoppingCart()
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));

                var listShoppingCart = new List<ShoppingCartItemModel>();

                foreach (var item in customer.ShoppingCartItems)
                {
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.ShoppingCart)
                    {
                        var shoppingCartItem = new ShoppingCartItemModel();
                        shoppingCartItem.Product = item.Product;
                        shoppingCartItem.Quantity = item.Quantity;
                        shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;
                        listShoppingCart.Add(shoppingCartItem);
                    }
                }
                return PartialView(listShoppingCart);
            }
            else
            {
                var shoppingCart = Session[ShoppingCartSession];
                var listShoppingCart = new List<ShoppingCartItemModel>();
                if (shoppingCart != null)
                {
                    listShoppingCart = (List<ShoppingCartItemModel>)shoppingCart;
                }
                return PartialView(listShoppingCart);
            }
        }

        [HttpPost]
        public ActionResult CheckOut()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));
                if (customer.ShoppingCartItems.Count() == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
              return  RedirectToAction("BillingAddress", "CheckOut");
            }
            else
            {
                return RedirectToAction("CheckOutAsGuest","User");
            }
        }
    }
}