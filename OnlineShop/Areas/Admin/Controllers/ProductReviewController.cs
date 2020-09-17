using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductReviewController : BaseController
    {
        // GET: Admin/ProductReview
        public ActionResult Index(DateTime? CreateFrom, DateTime? CreateTo, string Message = null, bool Approved = false, int ProductId = 0, int page = 1, int pageSize = 10)
        {
            var createFrom = CreateFrom ?? DateTime.Now;
            var createTo = CreateTo ?? DateTime.Now;
            var productReviewDao = new ProductReviewDao();
            var model = productReviewDao.GetProductReviews(createFrom, createTo, Message, Approved, ProductId, page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductReview productReview)
        {
            if (ModelState.IsValid)
            {
                var productReviewDao = new ProductReviewDao();
                var Id = productReviewDao.InsertProductReview(productReview);
                if (Id > 0)
                {
                    SetNotification("Thêm mới Review thành công .", "success");
                    return RedirectToAction("Index", "ProductReview");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới Review không thành công .");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var producrReview = new ProductReviewDao().GetProductReviewById(id);
            return View(producrReview);
        }

        [HttpPost]
        public ActionResult Edit(ProductReview productReview)
        {
            if (ModelState.IsValid)
            {
                var productReviewDao = new ProductReviewDao();
                var result = productReviewDao.UpdateProductReview(productReview);
                if (result)
                {
                    SetNotification("Cập nhật Review thành công .", "success");
                    return RedirectToAction("Index", "ProductReview");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Review không thành công .");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var result = new ProductReviewDao().DeleteProductReview(Id);
            if (result)
            {
                SetNotification("Xoá Review thành công .", "success");
                return RedirectToAction("Index", "ProductReview");
            }
            else
            {
                ModelState.AddModelError("", "Xoá Review không thành công .");
            }
            return View();
        }
    }
}