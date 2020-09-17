using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BlogCommentController : BaseController
    {
        // GET: Admin/BlogComment
        public ActionResult Index(DateTime? SearchCreatedFrom, DateTime? SearchCreatedTo, int SearchIsApproved = 0, string SearchMessage = null, int page = 1, int pageSize = 10)
        {
            var CreatedFrom = SearchCreatedFrom.HasValue ? Convert.ToDateTime(SearchCreatedFrom) : DateTime.Now;
            var CreatedTo = SearchCreatedTo.HasValue ? Convert.ToDateTime(SearchCreatedTo) : DateTime.Now;

            var blogComment = new BlogCommentDao().GetBlogComments(CreatedFrom, CreatedTo, SearchIsApproved, SearchMessage, page, pageSize);
            ViewBag.SearchCreatedFrom = CreatedFrom;
            ViewBag.SearchCreatedTo = CreatedTo;
            ViewBag.SearchIsApproved = SearchIsApproved;
            ViewBag.SearchMessage = SearchMessage;
            return View(blogComment);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
                var blogCommentDao = new BlogCommentDao();
                var Id = blogCommentDao.InsertBlogComment(blogComment);
                if (Id > 0)
                {
                    SetNotification("Thêm mới Blog Comment thành công .", "success");
                    return RedirectToAction("Index", "BlogComment");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới Blog Comment không thành công .");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var blogComment = new BlogCommentDao().GetBlogCommentById(Id);
            if (blogComment == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View();
            }
            return View(blogComment);
        }

        [HttpPost]
        public ActionResult Edit(BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
                var blogCommentDao = new BlogCommentDao();
                var result = blogCommentDao.UpdateBlogComment(blogComment);
                if (result)
                {
                    SetNotification("Cập nhật Blog Comment thành công .", "success");
                    return RedirectToAction("Index", "BlogComment");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Blog Comment không thành công .");
                }

            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var result = new BlogCommentDao().DeleteBlogComment(Id);
            if (result)
            {
                SetNotification("Xoá Blog Comment thành công .", "success");
                return RedirectToAction("Index", "BlogComment");
            }
            else
            {
                ModelState.AddModelError("", "Xoá Blog Comment không thành công .");
            }
            return View();
        }
    }
}