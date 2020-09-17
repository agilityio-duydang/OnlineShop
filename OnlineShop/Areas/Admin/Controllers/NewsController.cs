using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        // GET: Admin/News
        public ActionResult Index(string SearchTittle = null, int page = 1, int pageSize = 10)
        {
            var news = new NewsDao().GetNews(SearchTittle, page, pageSize);
            ViewBag.SearchTittle = SearchTittle;
            return View(news);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                var newsDao = new NewsDao();
                var Id = newsDao.InsertNews(news);
                if (Id > 0)
                {
                    SetNotification("Thêm mới News thành công .", "success");
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới News không thành công .");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var news = new NewsDao().GetNewsById(Id);
            return View(news);
        }

        [HttpPost]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                var newsDao = new NewsDao();
                var result = newsDao.UpdateNews(news);
                if (result)
                {
                    SetNotification("Cập nhật News thành công .", "success");
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật News không thành công .");
                }

            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var result = new NewsDao().DeleteNews(Id);
            if (result)
            {
                SetNotification("Xoá News thành công .", "success");
                return RedirectToAction("Index", "News");
            }
            else
            {
                ModelState.AddModelError("", "Xoá News không thành công .");
            }
            return View();
        }
    }
}