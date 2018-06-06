using LandingPage.Models.SearchesViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class SearchesController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public SearchesController(IHostingEnvironment emv)
        {
            _hostingEnvironment = emv;
        }

        [Route("admin/searches/getall")]
        public JsonResult GetAll()
        {
            var all = new SearchesManager().GetAll();
            return Json(new { data = all });
        }
        [Route("admin/searches")]
        public IActionResult Index()
        {
            var all = new SearchesManager().GetAll();
            return View(all);
        }



        [Route("admin/searches/deleteItem")]
        public JsonResult DeleteItem(int id)
        {
            var _resourcesManager = new SearchesManager();
            var item = _resourcesManager.GetById(id);
            if (item.Id > 0)
            {
                _resourcesManager.Delete(id);

                return Json(true);
            }
            else
                return Json(false);
        }

        [Route("admin/searches/create")]
        public IActionResult Create(string keywords)
        {
            var _repo = new SearchesManager();
            if (keywords == null)
            {
                return Json(new { success = false, message = "keywords gresite" });
            }
            else
            {
                
                _repo.Create(keywords, DateTime.Now, Request.HttpContext.Connection.RemoteIpAddress.ToString());
                return Json(new { success = true, message = "search succeeded" });
            }

        }
        
    }
}
