using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class GiftCardController : BaseController
    {
        // GET: Admin/GiftCard
        public ActionResult Index(string SearchRecipientName = null, bool Active = false, string GiftCardCouponCode = null, int page = 1, int pageSize = 10)
        {
            var giftCards = new GiftCardDao().GetGiftCards(SearchRecipientName, GiftCardCouponCode, Active, page, pageSize);
            ViewBag.SearchRecipientName = SearchRecipientName;
            ViewBag.Active = Active;
            ViewBag.GiftCardCouponCode = GiftCardCouponCode;
            return View(giftCards);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GiftCard giftCard)
        {
            if (ModelState.IsValid)
            {
                var giftCardDao = new GiftCardDao();
                int Id = giftCardDao.InsertGiftCard(giftCard);
                if (Id > 0)
                {
                    SetNotification("Thêm mới GiftCard thành công", "success");
                    return RedirectToAction("Index", "GiftCard");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới GiftCard không thành công ");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                throw new ArgumentException(nameof(Id));

            var giftCard = new GiftCardDao().GetGiftCardById(Id);
            if (giftCard == null)
                throw new ArgumentNullException(nameof(Id));

            return View(giftCard);
        }

        [HttpPost]
        public ActionResult Edit(GiftCard giftCard)
        {
            if (ModelState.IsValid)
            {
                var giftCardDao = new GiftCardDao();
                var result = giftCardDao.UpdateGiftCard(giftCard);
                if (result)
                {
                    SetNotification("Cập nhật GiftCard thành công", "success");
                    return RedirectToAction("Index", "GiftCard");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật GiftCard không thành công");
                }
            }
            return View();
        }
        public ActionResult Delete(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var result = new GiftCardDao().DeleteGiftCard(Id);
            if (result)
            {
                SetNotification("Xoá GiftCard thành công", "sucess");
                return RedirectToAction("Index", "GiftCard");
            }
            else
            {
                ModelState.AddModelError("", "Xoá GiftCard không thành công");
            }
            return View();
        }
    }
}