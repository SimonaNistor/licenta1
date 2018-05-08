using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.Models.ScriptsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace LandingPage.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ScriptsController : Controller
    {
        [Route("admin/scripts/getall")]
        public JsonResult GetAll()
        {
            var _repo = new ScriptsManager();
            return Json(new { data = _repo.GetAll() });
        }

        [Route("admin/scripts/deleteScript")]
        public JsonResult Delete(int id)
        {
            var _repo = new ScriptsManager();
            var script = _repo.GetById(id);
            if (script.Id > 0)
            {
                _repo.Delete(script.Id);
                return Json(true);
            }
            else return Json(false);
        }

        [Route("admin/scripts/createScript")]
        [HttpPost]
        public IActionResult Create(ScriptsViewModels model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                    new ScriptsManager().Update(new ScriptsViewModels().TransformScriptsVM(model));
                else
                    new ScriptsManager().Create(new ScriptsViewModels().TransformScriptsVM(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Route("admin/scripts/createScript")]
        [HttpGet]
        public IActionResult Create(int id)
        {
            var viewModel = new ScriptsViewModels();
            if (id > 0)
            {
                viewModel = (ScriptsViewModels)(new ScriptsManager().GetById(id));
            }

            return View(viewModel);

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}