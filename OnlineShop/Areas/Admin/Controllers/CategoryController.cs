using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string SearchName = null, int SearchPublished = 0, int pageNumber = 1, int pageSize = 10)
        {
            var dao = new CategoryDao();
            var categories = dao.ListAllCategories(SearchName, SearchPublished, pageNumber, pageSize);
            ViewBag.SearchName = SearchName;
            ViewBag.SearchPublished = SearchPublished;
            SetViewBag();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SetViewBag();
            var customer = new CategoryDao().GetCategoryById(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (!String.IsNullOrWhiteSpace(category.Name))
            {
                var categories = new CategoryDao().GetCategoryByName(category.Name);
                if (categories != null)
                {
                    ModelState.AddModelError("", "Name Category is already registered");
                }
            }
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                long id = dao.InsertCategory(category);
                if (id > 0)
                {
                    SetNotification("Thêm mới danh mục thành công .", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới danh mục không thành công .");
                }
            }
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                var result = dao.UpdateCategory(category);
                if (result)
                {
                    SetNotification("Cập nhật danh mục thành công .", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật danh mục không thành công .");
                }
                SetViewBag();
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var user = new CategoryDao().DeleteCategory(id);
            return RedirectToAction("Index");
        }
        public void SetViewBag(int? selectedId = null)
        {
            var category = new CategoryDao();

            ViewBag.ParentCategoryId = new SelectList(category.ListAllCategories(), "Id", "Name", selectedId);
        }
    }
}