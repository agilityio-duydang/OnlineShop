using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class PollController : BaseController
    {
        // GET: Admin/Poll
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var polls = new PollDao().GetPolls(page, pageSize);
            return View(polls);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Poll poll)
        {
            if (ModelState.IsValid)
            {
                var pollDao = new PollDao();
                int Id = pollDao.InsertPoll(poll);
                if (Id > 0)
                {
                    SetNotification("Thêm mới Poll thành công", "success");
                    RedirectToAction("Index", "Poll");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới Poll không thành công");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var poll = new PollDao().GetPollById(Id);
            if (poll == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View();
            }
            return View(poll);
        }

        [HttpPost]
        public ActionResult Edit(Poll poll)
        {
            if (ModelState.IsValid)
            {
                var pollDao = new PollDao();
                var result = pollDao.UpdatePoll(poll);
                if (result)
                {
                    SetNotification("Cập nhật Poll thành công", "success");
                    RedirectToAction("Index", "Poll");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Poll không thành công ");
                }
            }
            return View();
        }
        
        public ActionResult Delete(int Id)
        {
            var result = new PollDao().DeletePoll(Id);
            if (result)
            {
                SetNotification("Xoá Poll thành công", "success");
                RedirectToAction("Index", "Poll");
            }
            else
            {
                ModelState.AddModelError("", "Xoá Poll không thành công ");
            }
            return View();
        }    
    }
}