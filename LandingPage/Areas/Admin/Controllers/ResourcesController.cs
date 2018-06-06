using DBModels;
using LandingPage.Models.ResourcesViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LandingPage.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ResourcesController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public ResourcesController(IHostingEnvironment emv)
        {
            _hostingEnvironment = emv;
        }

        [Route("admin/resources/getall")]
        public JsonResult GetAll()
        {
            var all = new ResourcesManager().GetAll();
            return Json(new { data = all });
        }
        [Route("admin/resources")]
        public IActionResult Index()
        {
            var all = new ResourcesManager().GetAll();
            return View(all);
        }



        [Route("admin/resources/deleteItem")]
        public JsonResult DeleteItem(int id)
        {
            var _resourcesManager = new ResourcesManager();
            var item = _resourcesManager.GetById(id);
            if (item.Id > 0)
            {
                _resourcesManager.Delete(id);

                return Json(true);
            }
            else
                return Json(false);
        }

        [Route("admin/resources/createItem")]
        [HttpGet]
        public IActionResult Create(int id)
        {
            var viewModel = new ResourcesViewModels();
            if (id > 0)
            {
                viewModel = (ResourcesViewModels)(new ResourcesManager().GetById(id));
            }

            return View(viewModel);

        }
        [Route("admin/resources/createItem")]
        [HttpPost]
        public IActionResult Create(ResourcesViewModels item)
        {
            if (ModelState.IsValid)
            {
                if (item.Id > 0)

                    new ResourcesManager().Update(new ResourcesViewModels().TransformResourcesVM(item));
                else
                    new ResourcesManager().Create(new ResourcesViewModels().TransformResourcesVM(item));

                return RedirectToAction("Index");
            }
            return View(item);
        }

        //[Route("admin/resources/upload")]
        //[HttpGet]
        //public IActionResult Upload(int id)//ce primesc aici?
        //{
        //    return Json(new { success = true, message = "V-ati abonat cu succes la newsletter" });

        //}
                
        [HttpPost]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            
            var filePath = Path.GetTempFileName();

            var item = new ResourcesViewModels();

            foreach (var formFile in files)
            {

                if (formFile.Length > 0)
                {
                    item.Type = formFile.ContentType.ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        String s = "";
                        stream.Position = 0;
                        StreamReader sr = new StreamReader(stream);
                        s = sr.ReadToEnd();
                        item.Code = s;
                        new ResourcesManager().Create(new ResourcesViewModels().TransformResourcesVM(item));

                    }
                }
            }

            return RedirectToAction("Index"); //Ok(new { count = files.Count, size, filePath });
        }
    }
}
