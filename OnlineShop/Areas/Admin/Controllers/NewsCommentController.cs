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
    public class NewsCommentController : BaseController
    {
        // GET: Admin/NewsComment
        public ActionResult Index(DateTime? SearchCreatedFrom, DateTime? SearchCreatedTo, int SearchIsApproved = 0,string SearchMessage =null, int page = 1, int pageSize = 10)
        {
            var CreatedFrom = SearchCreatedFrom.HasValue ? Convert.ToDateTime(SearchCreatedFrom) : DateTime.Now;
            var CreatedTo = SearchCreatedTo.HasValue ? Convert.ToDateTime(SearchCreatedTo) : DateTime.Now;

            var newsComment = new NewsCommentDao().GetNewsComments(CreatedFrom, CreatedTo, SearchIsApproved,SearchMessage, page, pageSize);
            ViewBag.SearchCreatedFrom = CreatedFrom;
            ViewBag.SearchCreatedTo = CreatedTo;
            ViewBag.SearchIsApproved = SearchIsApproved;
            return View(newsComment);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewsComment newsComment)
        {
            if (ModelState.IsValid)
            {
                var newsCommentDao = new NewsCommentDao();
                var Id = newsCommentDao.InsertNewsComment(newsComment);
                if (Id > 0)
                {
                    SetNotification("Thêm mới News Comment thành công .", "success");
                    return RedirectToAction("Index", "NewsComment");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới News Comment không thành công .");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var newsComment = new NewsCommentDao().GetNewsCommentById(Id);
            if (newsComment == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View();
            }
            return View(newsComment);
        }

        [HttpPost]
        public ActionResult Edit(NewsComment newsComment)
        {
            if (ModelState.IsValid)
            {
                var newsCommentDao = new NewsCommentDao();
                var result = newsCommentDao.UpdateNewsComment(newsComment);
                if (result)
                {
                    SetNotification("Cập nhật News Comment thành công .", "success");
                    return RedirectToAction("Index", "NewsComment");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật News Comment không thành công .");
                }

            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var result = new NewsCommentDao().DeleteNewsComment(Id);
            if (result)
            {
                SetNotification("Xoá News Comment thành công .", "success");
                return RedirectToAction("Index", "NewsComment");
            }
            else
            {
                ModelState.AddModelError("", "Xoá News Comment không thành công .");
            }
            return View();
        }
    }
}