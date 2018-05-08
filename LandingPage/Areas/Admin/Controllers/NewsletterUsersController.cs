using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LandingPage.Models.NewsletterUsersViewModels;
using Service;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace LandingPage.Areas.Admin.Controllers
{
    [Area("admin")]
    public class NewsletterUsersController : Controller
    {
        [Route("admin/NewsletterUsers/getall")]
        [Authorize]
        public JsonResult GetAll()
        {
            var _repo = new NewsletterUsersManager();
            return Json(new { data = _repo.GetAll() });
        }

        [Route("admin/NewsletterUsers/deleteUser")]
        [Authorize]
        public JsonResult Delete(int id)
        {
            var _repo = new NewsletterUsersManager();
            var user = _repo.GetById(id);
            if (user.Id > 0)
            {
                _repo.Delete(user.Id);
                return Json(true);
            }
            else return Json(false);
        }
        [Route("admin/NewsletterUsers/Create")]
        public JsonResult Create(string email)
        {
            var _repo = new NewsletterUsersManager();
            var user = _repo.GetByEmail(email);
            string emailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@";
            if(user != null)
            {
                return Json(new { success = false, message = "Subscriptie deja existenta pentru acest email" });
            }
            else
            if (email == null || !Regex.IsMatch(email, emailPattern))
            {
                return Json(new { success = false, message = "Email invalid" });
            }
            else
            {
                _repo.Create(email, DateTime.Now,Request.HttpContext.Connection.RemoteIpAddress.ToString());
                return Json(new { success = true, message = "V-ati abonat cu succes la newsletter" });
            }
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}