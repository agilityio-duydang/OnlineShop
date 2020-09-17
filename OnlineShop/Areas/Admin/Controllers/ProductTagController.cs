using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductTagController : BaseController
    {
        // GET: Admin/ProductTag
        public ActionResult Index(string SearchName=null , int page=1 , int pageSize=10)
        {
            var productTagDao = new ProductTagDao();
            var productTags = productTagDao.GetProductTags(SearchName, page, pageSize);
            ViewBag.SearchName = SearchName;
            return View(productTags);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductTag productTag)
        {
            if (!String.IsNullOrWhiteSpace(productTag.Name))
            {
                var productTags = new ProductTagDao().GetProductTagByName(productTag.Name);
                if (productTags != null)
                {
                    ModelState.AddModelError("", "Name Product Tags is already registered");
                }
            }
            if (ModelState.IsValid)
            {
                var productTagDao = new ProductTagDao();
                int  Id = productTagDao.InsertProductTag(productTag);
                if (Id > 0)
                {
                    SetNotification("Thêm mới Product Tags thành công .", "success");
                    return RedirectToAction("Index", "ProductTag");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới Product Tags không thành công .");
                }
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productTag = new ProductTagDao().GetProductTagById(id);
            return View(productTag);
        }

        [HttpPost]
        public ActionResult Edit(ProductTag productTag)
        {
            if (ModelState.IsValid)
            {
                var productTagDao = new ProductTagDao();
                var result = productTagDao.UpdateProductTag(productTag);
                if (result)
                {
                    SetNotification("Cập nhật Product Tags thành công .", "success");
                    return RedirectToAction("Index", "ProductTag");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Product Tags không thành công .");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int productTagId)
        {
            var result = new ProductTagDao().DeleteProductTag(productTagId);
            if (result)
            {
                SetNotification("Xoá Product Tag thành công .", "success");
                return RedirectToAction("Index", "Vendor");
            }
            else
            {
                ModelState.AddModelError("", "Xoá Product Tags không thành công .");
            }
            return RedirectToAction("Index");
        }
    }
}