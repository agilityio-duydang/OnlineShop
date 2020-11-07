using Models.Dao;
using Models.EF;
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
    public class WishListController : Controller
    {
        private const string WishListSession = "WishListSession";
        private const string ShoppingCartSession = "ShoppingCartSession";

        // GET: WishList
        public ActionResult Index()
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var listShoppingCart = new List<ShoppingCartItemModel>();

                foreach (var item in customer.ShoppingCartItems)
                {
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.Wishlist)
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
                var shoppingCart = Session[WishListSession];
                var listShoppingCart = new List<ShoppingCartItemModel>();
                if (shoppingCart != null)
                {
                    listShoppingCart = (List<ShoppingCartItemModel>)shoppingCart;
                }
                return View(listShoppingCart);
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

        public ActionResult Add(int productId, int shoppingCartTypeId, int quantity)
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customerDao = new CustomerDao();
                var productDao = new ProductDao();
                var customer = customerDao.GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var model =  productDao.GetProductById(productId);
                var shoppingCartItemDao = new ShoppingCartItemDao();

                if (customer.ShoppingCartItems.ToList().Exists(x => x.Product.Id == productId))
                {
                    foreach (var item in customer.ShoppingCartItems)
                    {
                        if (item.Product.Id == productId && item.ShoppingCartTypeId == (int)ShoppingCartType.Wishlist)
                        {
                            item.Quantity += quantity;
                            shoppingCartItemDao.UpdateShoppingCartItem(item);
                        }
                    }
                }
                else
                {

                    var shoppingCartItem = new ShoppingCartItem();
                    //shoppingCartItem.Product = model;
                    //shoppingCartItem.Customer = customer;
                    shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.Wishlist;
                    shoppingCartItem.CustomerId = customer.Id;
                    shoppingCartItem.ProductId = model.Id;
                    shoppingCartItem.CustomerEnteredPrice = model.Price;
                    shoppingCartItem.Quantity = quantity;
                    shoppingCartItem.CreatedOnUtc = DateTime.UtcNow;
                    shoppingCartItem.UpdatedOnUtc = DateTime.UtcNow;
                    customer.ShoppingCartItems.Add(shoppingCartItem);
                    customerDao.UpdateCustomer(customer);
                }
                var html = RenderViewToString(this.ControllerContext, "MiniViewAddWishList", model);
                var headerWishList = "(" + customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 2).Count().ToString() + ")";
                return Json(new
                {
                    success = true,
                    html,
                    headerWishList
                });
            }
            else
            {
                var cartType = (ShoppingCartType)shoppingCartTypeId;
                var listwishListItem = new List<ShoppingCartItemModel>();

                var model = new ProductDao().GetProductById(productId);
                var wishListItem = Session[WishListSession];
                if (wishListItem != null)
                {
                    listwishListItem = (List<ShoppingCartItemModel>)wishListItem;
                    if (listwishListItem.Exists(x => x.Product.Id == productId))
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
                        var wishList = new ShoppingCartItemModel();
                        wishList.Product = model;
                        wishList.Quantity = quantity;
                        wishList.ShoppingCartTypeId = (int)ShoppingCartType.Wishlist;

                        listwishListItem.Add(wishList);
                    }
                    Session[WishListSession] = listwishListItem;
                }
                else
                {
                    var wishList = new ShoppingCartItemModel();
                    wishList.Product = model;
                    wishList.Quantity = quantity;
                    wishList.ShoppingCartTypeId = (int)ShoppingCartType.Wishlist;

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

        public JsonResult Delete(long Id)
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var shoppingCartItemDao = new ShoppingCartItemDao();
                foreach (var item in customer.ShoppingCartItems)
                {
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.Wishlist && item.ProductId == Id)
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
                var shoppingCart = (List<ShoppingCartItemModel>)Session[WishListSession];
                shoppingCart.RemoveAll(x => x.Product.Id == Id && x.ShoppingCartTypeId == 2);
                Session[WishListSession] = shoppingCart;
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
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.Wishlist)
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
                Session[WishListSession] = null;
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
                    if (item.ShoppingCartTypeId == (int)ShoppingCartType.Wishlist)
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
                var shoppingCart = (List<ShoppingCartItemModel>)Session[WishListSession];
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
                Session[WishListSession] = shoppingCart;
                return Json(new
                {
                    Status = true
                });
            }
        }

        public JsonResult AddToCart(string cartModel)
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerDao = new CustomerDao();
                var productDao = new ProductDao();
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = customerDao.GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var jSonShoppingCart = new JavaScriptSerializer().Deserialize<List<ShoppingCartItemModel>>(cartModel);
                var shoppingCartItemDao = new ShoppingCartItemDao();
                if (customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == 1).Count() > 0)
                {
                    foreach (var ite in jSonShoppingCart)
                    {
                        if (customer.ShoppingCartItems.ToList().Exists(x => x.Product.Id == ite.Product.Id && x.ShoppingCartTypeId == 1))
                        {
                            foreach (var item in customer.ShoppingCartItems)
                            {
                                if (item.Product.Id == ite.Product.Id && item.ShoppingCartTypeId == 1)
                                {
                                    item.Quantity += ite.Quantity;
                                    shoppingCartItemDao.UpdateShoppingCartItem(item);
                                }
                            }
                        }
                        else
                        {
                            var shoppingCartItem = new ShoppingCartItem();
                            var model = productDao.GetProductById(ite.Product.Id);
                            shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;
                            shoppingCartItem.CustomerId = customer.Id;
                            shoppingCartItem.ProductId = model.Id;
                            shoppingCartItem.CustomerEnteredPrice = model.Price;
                            shoppingCartItem.Quantity = ite.Quantity;
                            shoppingCartItem.CreatedOnUtc = DateTime.UtcNow;
                            shoppingCartItem.UpdatedOnUtc = DateTime.UtcNow;
                            shoppingCartItemDao.InsertShoppingCartItem(shoppingCartItem);
                            customer.ShoppingCartItems.Add(shoppingCartItem);
                            customerDao.UpdateCustomer(customer);
                        }
                    }
                    foreach (var item in customer.ShoppingCartItems)
                    {
                        if (item.ShoppingCartTypeId == (int)ShoppingCartType.Wishlist)
                        {
                            shoppingCartItemDao.DeleteShoppingCartItem(item.Id);
                        }
                    }
                    Session[WishListSession] = null;
                    return Json(new
                    {
                        Status = true
                    });
                }
                else
                {
                    foreach (var item in jSonShoppingCart)
                    {
                        var shoppingCartItem = new ShoppingCartItem();
                        var model = productDao.GetProductById(item.Product.Id);
                        shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;
                        shoppingCartItem.CustomerId = customer.Id;
                        shoppingCartItem.ProductId = model.Id;
                        shoppingCartItem.CustomerEnteredPrice = model.Price;
                        shoppingCartItem.Quantity = item.Quantity;
                        shoppingCartItem.CreatedOnUtc = DateTime.UtcNow;
                        shoppingCartItem.UpdatedOnUtc = DateTime.UtcNow;
                        customer.ShoppingCartItems.Add(shoppingCartItem);
                        customerDao.UpdateCustomer(customer);
                    }
                    foreach (var item in customer.ShoppingCartItems)
                    {
                        if (item.ShoppingCartTypeId == (int)ShoppingCartType.Wishlist)
                        {
                            shoppingCartItemDao.DeleteShoppingCartItem(item.Id);
                        }
                    }
                    Session[WishListSession] = null;
                    return Json(new
                    {
                        Status = true
                    });
                }
            }
            else
            {
                var jSonShoppingCart = new JavaScriptSerializer().Deserialize<List<ShoppingCartItemModel>>(cartModel);
                var shoppingCart = (List<ShoppingCartItemModel>)Session[ShoppingCartSession];
                if (shoppingCart != null)
                {
                    foreach (var ite in jSonShoppingCart)
                    {
                        if (shoppingCart.Exists(x => x.Product.Id == ite.Product.Id && x.ShoppingCartTypeId == 1))
                        {
                            foreach (var item in shoppingCart)
                            {
                                if (item.Product.Id == ite.Product.Id && item.ShoppingCartTypeId == 1)
                                {
                                    item.Quantity += ite.Quantity;
                                }
                            }
                        }
                        else
                        {
                            var model = new ProductDao().GetProductById(ite.Product.Id);
                            var shoppingCartItem = new ShoppingCartItemModel();
                            shoppingCartItem.Product = model;
                            shoppingCartItem.Quantity = ite.Quantity;
                            shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;
                            shoppingCart.Add(shoppingCartItem);
                        }
                    }
                    Session[ShoppingCartSession] = shoppingCart;
                }
                else
                {
                    var listShoppingCart = new List<ShoppingCartItemModel>();
                    foreach (var item in jSonShoppingCart)
                    {
                        var model = new ProductDao().GetProductById(item.Product.Id);
                        var shoppingCartItem = new ShoppingCartItemModel();
                        shoppingCartItem.Product = model;
                        shoppingCartItem.Quantity = item.Quantity;
                        shoppingCartItem.ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart;
                        listShoppingCart.Add(shoppingCartItem);
                    }
                    Session[ShoppingCartSession] = listShoppingCart;
                }
                Session[WishListSession] = null;
                return Json(new
                {
                    Status = true
                });
            }
        }
    }
}