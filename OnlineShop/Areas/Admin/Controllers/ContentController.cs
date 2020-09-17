using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString , int pageNumber=1 , int pageSize=10)
        {
            var dao = new ContentDao();
            var model = dao.GetAllContents(searchString, pageNumber, pageSize);
            ViewBag.SearchString = searchString;
            SetViewBag();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Content content)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                long id = dao.Insert(content);
                if (id > 0)
                {
                    SetAlert("Thêm mới nội dung thành công .", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới nội dung không thành công .");
                }
            }
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            SetViewBag();
            var dao = new ContentDao();
            var content = dao.GetContentById(id);
            return View(content);
        }

        [HttpPost]
        public ActionResult Edit(Content content)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                var result = dao.Update(content);
                if (result)
                {
                    SetAlert("Cập nhật thông tin thành công .", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin không thành công .");
                }
            }
            SetViewBag(content.CategoryID);
            return View();
        }
        public void SetViewBag(long? selectedId=null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryId = new SelectList(dao.ListAllCategories(), "ID", "Name",selectedId);
        }
    }
}