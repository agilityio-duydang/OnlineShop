using Models.Dao;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ShippingReturns()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PrivacyNotice()
        {
            ViewBag.Message = "Privacy notice";

            return View();
        }

        public ActionResult ConditionsOfUse()
        {
            ViewBag.Message = "ConditionsOfUse";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var model = new CategoryDao().GetCategories();
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult HeaderFormHolder()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult HeaderFormHolder(LoginModel loginModel)
        {
            return PartialView(loginModel);
        }
        public ActionResult MobileFlyoutWrapper()
        {
            if (Session[OnlineShop.Common.CommonConstants.USER_SESSION] != null)
            {
                var customerSession = (OnlineShop.Common.UserLogin)Session[OnlineShop.Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(customerSession.UserId));
                var listShoppingCart = new List<ShoppingCartItemModel>();

                foreach (var item in customer.ShoppingCartItems)
                {
                    if (item.ShoppingCartTypeId == 1)
                    {
                        var shoppingCartItem = new ShoppingCartItemModel();
                        shoppingCartItem.Product = item.Product;
                        shoppingCartItem.Quantity = item.Quantity;
                        shoppingCartItem.ShoppingCartTypeId = item.ShoppingCartTypeId;
                        listShoppingCart.Add(shoppingCartItem);
                    }
                }
                return PartialView(listShoppingCart);
            }
            else
            {
                var shoppingCart = Session[OnlineShop.Common.CommonConstants.ShoppingCartSession];
                var listShoppingCart = new List<ShoppingCartItemModel>();
                if (shoppingCart != null)
                {
                    listShoppingCart = (List<ShoppingCartItemModel>)shoppingCart;
                }
                return PartialView(listShoppingCart);
            }
        }

        public ActionResult HeaderMenuParent()
        {
            var model = new CategoryDao().GetCategories();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult NivoSlider()
        {
            var model = new AnywhereSliderDao().GetAnywhereSliders();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult HomePageAdvantages()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult JCarouselMain()
        {
            var model = new JCarouselDao().GetJCarousels();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult HomePageCategory()
        {
            var model = new CategoryDao().GetCategories();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult SPCCategories()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult SaleOfTheDay()
        {
            var model = new SaleOfTheDayOfferDao().GetSaleOfTheDayOffers();
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult TwoRowCarousels()
        {
            var model = new JCarouselDao().GetJCarousels();
            return PartialView(model);
        }
    }
}