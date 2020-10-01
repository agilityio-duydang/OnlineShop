using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class JCarouselsController : BaseController
    {
        // GET: Admin/JCarousels
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var jCarousels = new JCarouselDao().GetJCarousels(page, pageSize);
            return View(jCarousels);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(JCarousel jCarousel)
        {
            if (ModelState.IsValid)
            {
                var jCarouselDao = new JCarouselDao();
                int Id = jCarouselDao.InsertJCarousels(jCarousel);
                if (Id > 0)
                {
                    SetNotification("Thêm mới JCarousels thành công ", "success");
                    return RedirectToAction("Index", "JCrousels");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật JCrousels không thành công");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var jCarousel = new JCarouselDao().GetJCarouselById(Id);
            return View(jCarousel);
        }

        [HttpPost]
        public ActionResult Edit(JCarousel jCarousel)
        {
            if (ModelState.IsValid)
            {
                var jCarouselDao = new JCarouselDao();
                var result = jCarouselDao.UpdateJCarousels(jCarousel);
                if (result)
                {
                    SetNotification("Cập nhật JCarousels thành công ", "success");
                    return RedirectToAction("Index", "JCarousels");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật JCarousels không thành công");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var jCarouselDao = new JCarouselDao();
            var result = jCarouselDao.DeleteJCarousels(Id);
            if (result)
            {
                SetNotification("Xoá JCarousels thành công ", "success");
                return RedirectToAction("Index", "JCarousels");
            }
            else
            {
                ModelState.AddModelError("", "Xoá JCarousels không thành công");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ProductList(int jCarouselId)
        {
            if (jCarouselId == 0)
                throw new ArgumentNullException(nameof(jCarouselId));

            var jCarousel = new JCarouselDao().GetJCarouselById(jCarouselId);
            if (jCarousel == null)
                throw new ArgumentNullException(nameof(jCarousel));

            return PartialView("_ProductList", jCarousel);
        }
    }
}