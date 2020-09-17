using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BlogPostController : BaseController
    {
        // GET: Admin/BlogPost
        public ActionResult Index(string SearchTittle = null, int page = 1, int pageSize = 10)
        {
            var blogPosts = new BlogPostDao().GetBlogPosts(SearchTittle, page, pageSize);
            ViewBag.SearchTittle = SearchTittle;
            return View(blogPosts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var blogPostDao = new BlogPostDao();
                var Id = blogPostDao.InsertBlogPost(blogPost);
                if (Id > 0)
                {
                    SetNotification("Thêm mới Blog thành công .", "success");
                    return RedirectToAction("Index", "BlogPost");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới Blog không thành công .");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var blogPost = new BlogPostDao().GetBlogPostById(Id);
            return View(blogPost);
        }

        [HttpPost]
        public ActionResult Edit(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var blogPostDao = new BlogPostDao();
                var result = blogPostDao.UpdateBlogPost(blogPost);
                if (result)
                {
                    SetNotification("Cập nhật Blog thành công .", "success");
                    return RedirectToAction("Index", "BlogPost");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Blog không thành công .");
                }

            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var result = new BlogPostDao().DeleteBlogPost(Id);
            if (result)
            {
                SetNotification("Xoá Blog thành công .", "success");
                return RedirectToAction("Index", "ProductReview");
            }
            else
            {
                ModelState.AddModelError("", "Xoá Blog không thành công .");
            }
            return View();
        }
    }
}