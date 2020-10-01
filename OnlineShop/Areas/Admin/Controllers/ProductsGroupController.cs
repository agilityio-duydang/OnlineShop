using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductsGroupController : BaseController
    {
        // GET: Admin/ProductsGroup
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var productsGroup = new ProductsGroupDao().GetProductsGroups(page, pageSize);
            return View(productsGroup);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductsGroup productsGroup)
        {
            if (ModelState.IsValid)
            {
                var productsGroupDao = new ProductsGroupDao();
                int Id = productsGroupDao.InsertProductsGroup(productsGroup);
                if (Id > 0)
                {
                    SetNotification("Thêm mới ProductGroups thành công", "");
                    return RedirectToAction("Index", "ProductsGroup");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới ProductsGroup không thành công");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));

            var productsGroup = new ProductsGroupDao().GetProductsGroupById(Id);

            return View(productsGroup);
        }

        [HttpPost]
        public ActionResult Edit(ProductsGroup productsGroup)
        {
            if (productsGroup == null)
                throw new ArgumentNullException(nameof(productsGroup));

            var result = new ProductsGroupDao().UpdateProductsGroup(productsGroup);
            if (result)
            {
                SetNotification("Cập nhật ProductGroups thành công", "");
                return RedirectToAction("Index", "ProductsGroup");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật ProductsGroup không thành công");
            }
            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            if (Id == 0)
                throw new ArgumentNullException(nameof(Id));
            var result = new ProductsGroupDao().DeleteProductsGroup(Id);
            if (result)
            {
                SetNotification("Xoá ProductGroups thành công", "");
                return RedirectToAction("Index", "ProductsGroup");
            }
            else
            {
                ModelState.AddModelError("", "Xoá ProductsGroup không thành công");
            }
            return View();
        }

        public ActionResult CategoryList(int productsGroupId)
        {
            if (productsGroupId == 0)
                throw new ArgumentNullException(nameof(productsGroupId));

            var productsGroup = new ProductsGroupDao().GetProductsGroupById(productsGroupId);
            if (productsGroup == null)
                throw new ArgumentNullException(nameof(productsGroup));

            return PartialView("_CategoryList",productsGroup);
        }

        public ActionResult TabBuilder(int productsGroupItemId)
        {
            if (productsGroupItemId == 0)
                throw new ArgumentNullException(nameof(productsGroupItemId));

            var productsGroupItem = new ProductsGroupDao().GetProductsGroupById(productsGroupItemId);
            if (productsGroupItem == null)
                throw new ArgumentNullException(nameof(productsGroupItem));

            return PartialView("_TabBuilder", productsGroupItem);
        }
        public ActionResult TabList(int productsGroupItemId)
        {
            if (productsGroupItemId == 0)
                throw new ArgumentNullException(nameof(productsGroupItemId));

            var productsGroupItem = new ProductsGroupDao().GetProductsGroupById(productsGroupItemId);
            if (productsGroupItem == null)
                throw new ArgumentNullException(nameof(productsGroupItem));

            return PartialView("_TabList", productsGroupItem);
        }
    }
}