using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class AnywhereSlidersController : BaseController
    {
        // GET: Admin/AnywhereSliders
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var anywhereSliders = new AnywhereSliderDao().GetAnywhereSliders(page, pageSize);
            return View(anywhereSliders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AnywhereSlider anywhereSlider)
        {
            if (ModelState.IsValid)
            {
                var anywhereSliderDao = new AnywhereSliderDao();
                int Id = anywhereSliderDao.InsertAnywhereSlider(anywhereSlider);
                if (Id > 0)
                {
                    SetNotification("Thêm mới AnywhereSlider thành công ", "success");
                    return RedirectToAction("Index", "AnywhereSliders");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới AnywhereSlider không thành công ");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var anywhereSlider = new AnywhereSliderDao().GetAnywhereSliderById(Id);
            return View(anywhereSlider);
        }

        [HttpPost]
        public ActionResult Edit(AnywhereSlider anywhereSlider)
        {
            if (ModelState.IsValid)
            {
                var anywhereSliderDao = new AnywhereSliderDao();
                var result = anywhereSliderDao.UpdateAnywhereSlider(anywhereSlider);
                if (result)
                {
                    SetNotification("Thêm mới AnywhereSlider thành công ", "success");
                    return RedirectToAction("Index", "AnywhereSliders");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới AnywhereSlider không thành công ");
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var result = new AnywhereSliderDao().DeleteAnywhereSlider(Id);
            if (result)
            {
                SetNotification("Xoá AnywhereSlider thành công ", "success");
                return RedirectToAction("Index", "AnywhereSliders");
            }
            else
            {
                ModelState.AddModelError("", "Xoá AnywhereSlider không thành công ");
            }

            return View();
        }

        public ActionResult PictureAdd(int PictureId,bool Visible, int DisplayOrder,string Url, string AltAttribute, string TitleAttribute, int SliderId)
        {
            if (PictureId == 0)
                throw new ArgumentException(nameof(PictureId));

            var anywhereSlider = new AnywhereSliderDao().GetAnywhereSliderById(SliderId);
            if (anywhereSlider == null)
                throw new ArgumentNullException();

            var picture = new PictureDao().GetPictureById(PictureId);
            if (picture == null)
                throw new ArgumentNullException(nameof(PictureId));

            picture.AltAttribute = AltAttribute;
            picture.TitleAttribute = TitleAttribute;

            var result = new PictureDao().UpdatePicture(picture);

            var sliderImage = new SliderImage
            {
                DisplayText = TitleAttribute,
                Alt = AltAttribute,
                Url = Url,
                Visible = Visible,
                DisplayOrder = DisplayOrder,
                PictureId = picture.Id,
                SliderId = SliderId
            };
            int Id = new SliderImageDao().InsertSliderImage(sliderImage);

            return Json(new { Result = true });
        }

        [HttpPost]
        public ActionResult PictureList(int SliderId)
        {
            if (SliderId == 0)
                throw new ArgumentNullException(nameof(SliderId));

            var anywhereSlider = new AnywhereSliderDao().GetAnywhereSliderById(SliderId);
            if (anywhereSlider == null)
                throw new ArgumentNullException(nameof(anywhereSlider));

            return PartialView("_PictureList", anywhereSlider);
        }
    }
}