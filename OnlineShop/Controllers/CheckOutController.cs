using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BillingAddress()
        {
            return View();
        }

        public ActionResult ShippingMethod()
        {
            return View();
        }
        public ActionResult PaymentMethod()
        {
            return View();
        }

        public ActionResult PaymentInfo()
        {
            return View();
        }

        public ActionResult Confirm()
        {
            return View();
        }
        public ActionResult Completed()
        {
            return View();
        }
    }
}