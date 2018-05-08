using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DBModels;
using LandingPage.Models.CMSViewModels;
using Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using test;
using Microsoft.AspNetCore.Authorization;

namespace LandingPage.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class CMSController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public CMSController(IHostingEnvironment emv)
        {
            _hostingEnvironment = emv;
        }

        [Route("admin/cms/getall")]
        public JsonResult GetAll()
        {
            var all = new CMSManager().GetAll();
            return Json(new { data = all });
        }
        [Route("admin/cms")]
        public IActionResult Index()
        {
            var all = new CMSManager().GetAll();
            return View(all);
        }



        [Route("admin/cms/deleteItem")]
        public JsonResult DeleteItem(int ItemId)
        {
            var _cmsItemManager = new CMSManager();
            var item = _cmsItemManager.GetById(ItemId);
            if (item.Id > 0)
            {
                _cmsItemManager.Delete(ItemId);

                return Json(true);
            }
            else
                return Json(false);
        }

        [Route("admin/cms/createItem")]
        [HttpGet]
        public IActionResult Create(int id)

        {
            ViewBag.MenuItems = new SelectList(new MenuItemManager().GetAll(), "ItemId", "Name");
            ViewBag.HtmlTypes = new SelectList(new HtmlTypesManager().GetAll(), "Id", "Name");

            var viewModel = new CMSViewModels();
            if (id > 0)
            {
                viewModel = (CMSViewModels)(new CMSManager().GetById(id));
            }

            return View(viewModel);

        }
        [Route("admin/cms/createItem")]
        [HttpPost]
         public async Task<IActionResult> Create(CMSViewModels item, IFormFile FileUploadId)
        {
            var content = "";
            if (item.HtmlType == 1)
            {
                content = item.ContentId;
            }
            else
                if (item.HtmlType == 2)
            {
                content = item.TextAreaId;
            }
            else

                if (item.HtmlType == 3)
            {
                content = item.HtmlEditorId;
            }
            else
            {
                if (item.FileUploadId != null)
            {
                    var filename = item.FileUploadId.FileName;
                    var path = Path.Combine(_hostingEnvironment.WebRootPath+"\\siteimages", filename);
                    var relpath = "/siteimages/"+filename;

                    content = relpath;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await item.FileUploadId.CopyToAsync(stream);
                    }
                }
            }
           ViewBag.MenuItems = new SelectList(new MenuItemManager().GetAll(), "ItemId", "Name");
           ViewBag.HtmlTypes = new SelectList(new HtmlTypesManager().GetAll(), "Id", "Name");
            item.Content = content;

            if (ModelState.IsValid)
            {
                if (item.Id > 0)
                {
                    if (string.IsNullOrWhiteSpace(item.Content))
                    {
                        var oldItem = new CMSManager().GetById(item.Id);
                        item.Content = oldItem.Content;
                    }
                    new CMSManager().Update(new CMSViewModels().Transform(item));
                }
            else
                new CMSManager().Create(new CMSViewModels().Transform(item));

                return RedirectToAction("Index");
            }
            return View(item);
        }
        
    }
}