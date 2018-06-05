using LandingPage.Models.LinksViewModels;
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
    public class LinksController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public LinksController(IHostingEnvironment emv)
        {
            _hostingEnvironment = emv;
        }

        [Route("admin/links/getall")]
        public JsonResult GetAll()
        {
            var all = new LinksManager().GetAll();
            return Json(new { data = all });
        }
        [Route("admin/links")]
        public IActionResult Index()
        {
            var all = new LinksManager().GetAll();
            return View(all);
        }



        [Route("admin/links/deleteItem")]
        public JsonResult DeleteItem(int id)
        {
            var _linksItemManager = new LinksManager();
            var item = _linksItemManager.GetById(id);
            if (item.id > 0)
            {
                _linksItemManager.Delete(id);

                return Json(true);
            }
            else
                return Json(false);
        }

        [Route("admin/links/createItem")]
        [HttpGet]
        public IActionResult Create(int id)
        {
            var viewModel = new LinksViewModels();
            if (id > 0)
            {
                viewModel = (LinksViewModels)(new LinksManager().GetById(id));
            }

            return View(viewModel);

        }
        [Route("admin/links/createItem")]
        [HttpPost]
        public IActionResult Create(LinksViewModels item)
        {
            if (ModelState.IsValid)
            {
                if (item.id > 0)

                    new LinksManager().Update(new LinksViewModels().TransformLinksVM(item));
                else
                    new LinksManager().Create(new LinksViewModels().TransformLinksVM(item));

                return RedirectToAction("Index");
            }
            return View(item);
        }
    }
}
