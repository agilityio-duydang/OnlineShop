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
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult NivoSlider()
        {
            return PartialView();
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
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult HomePageCategory()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult SPCCategories()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult SaleOfTheDay()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult TwoRowCarousels()
        {
            return PartialView();
        }
    }
}