using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class SaleOfTheDayOfferController : BaseController
    {
        // GET: Admin/SaleOfTheDayOffer
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var saleOfTheDayOffer = new SaleOfTheDayOfferDao().GetSaleOfTheDayOffers(page, pageSize);
            return View(saleOfTheDayOffer);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SaleOfTheDayOffer saleOfTheDayOffer)
        {
            if (ModelState.IsValid)
            {
                var saleOfTheDayOfferDao = new SaleOfTheDayOfferDao();
                int Id = saleOfTheDayOfferDao.InsertSaleOfTheDayOffer(saleOfTheDayOffer);
                if (Id > 0)
                {
                    SetNotification("Thêm mới SaleOfTheDayOffer thành công", "");
                    return RedirectToAction("Index", "SaleOfTheDayOffer");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới SaleOfTheDayOffer không thành công");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var saleOfTheDayOffer = new SaleOfTheDayOfferDao().SaleOfTheDayOfferById(Id);

            return View(saleOfTheDayOffer);
        }

        [HttpPost]
        public ActionResult Edit(SaleOfTheDayOffer saleOfTheDayOffer)
        {
            if (saleOfTheDayOffer == null)
                throw new ArgumentNullException(nameof(saleOfTheDayOffer));

            var result = new SaleOfTheDayOfferDao().UpdateSaleOfTheDayOffer(saleOfTheDayOffer);
            if (result)
            {
                SetNotification("Cập nhật SaleOfTheDayOffer thành công", "");
                return RedirectToAction("Index", "SaleOfTheDayOffer");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật SaleOfTheDayOffer không thành công");
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));
            var result = new SaleOfTheDayOfferDao().DeleteSaleOfTheDayOffer(Id);
            if (result)
            {
                SetNotification("Xoá SaleOfTheDayOffer thành công", "");
                return RedirectToAction("Index", "SaleOfTheDayOffer");
            }
            else
            {
                ModelState.AddModelError("", "Xoá SaleOfTheDayOffer không thành công");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ProductList(int saleOfTheDayOfferId)
        {
            if (saleOfTheDayOfferId == 0)
                throw new ArgumentNullException(nameof(saleOfTheDayOfferId));

            var saleOfTheDayOffer = new SaleOfTheDayOfferDao().SaleOfTheDayOfferById(saleOfTheDayOfferId);
            if (saleOfTheDayOffer == null)
                throw new ArgumentNullException(nameof(saleOfTheDayOffer));

            return PartialView("_ProductList", saleOfTheDayOffer);
        }
    }
}