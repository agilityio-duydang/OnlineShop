using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [ChildActionOnly]
        public ActionResult Header()
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