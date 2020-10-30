using iTextSharp.text;
using iTextSharp.text.pdf;
using Models.Dao;
using Models.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }


        //My account / Order details page
        public ActionResult Details(int Id)
        {
            var order = new OrderDao().GetOrderByOrderNumber(Id);
            if (order == null || order.Deleted)
                return null;

            return View(order);
        }

        //My account / Order details page / Print
        public ActionResult PrintOrderDetails(int orderId)
        {
            var order = new OrderDao().GetOrderById(orderId);
            if (order == null || order.Deleted)
                return null;

            return View("Details", order);
        }

        //My account / Order details page / PDF invoice
        public ActionResult GetPdfInvoice(int orderId)
        {
            //var order = new OrderDao().GetOrderById(orderId);
            //if (order == null || order.Deleted )
            //    return null;

            //var orders = new List<Order>();
            //orders.Add(order);
            //byte[] bytes;
            //using (var stream = new MemoryStream())
            //{
            //    //_pdfService.PrintOrdersToPdf(stream, orders, _workContext.WorkingLanguage.Id);
            //    bytes = stream.ToArray();
            //}
            //return File(bytes, MimeTypes.ApplicationPdf, $"order_{order.Id}.pdf");
            return View();
        }

        //My account / Order details page / re-order
        public ActionResult ReOrder(int orderId)
        {
            var order = new OrderDao().GetOrderById(orderId);
            if (order == null || order.Deleted)
                return null;

            return RedirectToRoute("ShoppingCart");
        }

        [HttpGet]
        public ActionResult History()
        {
            if (Session[Common.CommonConstants.USER_SESSION] != null)
            {
                var session = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
                var customer = new CustomerDao().GetCustomerById(Convert.ToInt32(session.UserId));
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}