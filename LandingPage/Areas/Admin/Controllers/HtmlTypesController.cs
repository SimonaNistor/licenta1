using LandingPage.Models.HtmlTypesViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace LandingPage.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class HtmlTypesController : Controller
    {   [Route("admin/HtmlTypes/getall")]
        public JsonResult GetAll()
        {
            var _repo = new HtmlTypesManager();
            return Json(new { data = _repo.GetAll() });
        }

        [Route("admin/HtmlTypes/deleteType")]
        public JsonResult Delete(int id)
        {
            var _repo = new HtmlTypesManager();
            var item = _repo.GetById(id);
            if (item.Id > 0)
            {
                _repo.Delete(item.Id);
                return Json(true);
            }
            else return Json(false);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/HtmlTypes/createType")]
        [HttpGet]
        public IActionResult Create(int id)
        {
            var viewModel = new HtmlTypesViewModels();
            if (id > 0)
            {
                viewModel = (HtmlTypesViewModels)(new HtmlTypesManager().GetById(id));
            }

            return View(viewModel);

        }
        [Route("admin/HtmlTypes/createType")]
        [HttpPost]
        public IActionResult Create(HtmlTypesViewModels htmlType)
        {
            
            if (ModelState.IsValid)
            {
                if (htmlType.Id > 0)

                    new HtmlTypesManager().Update(new HtmlTypesViewModels().Transform(htmlType));
                else
                    new HtmlTypesManager().Create(new HtmlTypesViewModels().Transform(htmlType));

                return RedirectToAction("Index");
            }
            return View(htmlType);
        }
    }
}