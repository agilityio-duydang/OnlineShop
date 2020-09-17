using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(DateTime? SearchStartDate, DateTime? SearchEndDate, string SearchProductName = null, int SearchOrderStatus = 0, int SearchPaymentStatus = 0, int SearchShippingStatus = 0, string SearchPaymentMeThod = null, int page = 1, int pageSize = 10)
        {
            var StartDate = SearchStartDate.HasValue ? Convert.ToDateTime(SearchStartDate) : DateTime.MinValue;
            var EndDate = SearchEndDate.HasValue ? Convert.ToDateTime(SearchEndDate) : DateTime.MinValue;
            var orders = new OrderDao().GetOrders(StartDate, EndDate, SearchProductName, SearchOrderStatus, SearchPaymentStatus, SearchShippingStatus, SearchPaymentMeThod, page, pageSize);

            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var orderDao = new OrderDao();
                int Id = orderDao.InserOrder(order);
                if (Id > 0)
                {
                    SetNotification("Thêm mới Order thành công", "success");
                    RedirectToAction("Index", "Order");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới Order không thành công ");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var order = new OrderDao().GetOrderById(Id);
            if (order == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                var orderDao = new OrderDao();
                var result = orderDao.UpdateOrder(order);
                if (result)
                {
                    SetNotification("Cập nhật Order thành công", "success");
                    RedirectToAction("Index", "Order");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Order không thành công ");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var orderDao = new OrderDao();
            var result = orderDao.DeleteOrder(Id);
            if (result)
            {
                SetNotification("Xoá Order thành công", "success");
                RedirectToAction("Index", "Order");
            }
            else
            {
                ModelState.AddModelError("", "Xoá Order không thành công ");
            }
            return View();
        }
    }
}