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
using test.Models.MenuItemViewModels;

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

        [Route("admin/menuitem/createItem")]
        [HttpGet]
        public IActionResult Create(int id)
        {
            var viewModel = new MenuItemViewModels();
            if (id > 0)
            {
                viewModel = (MenuItemViewModels)(new MenuItemManager().GetById(id));
            }

            return View(viewModel);

        }
        [Route("admin/menuitem/createItem")]
        [HttpPost]
        public IActionResult Create(MenuItemViewModels menuItem)
        {
            if (ModelState.IsValid)
            {
                if (menuItem.ItemId > 0)

                    new MenuItemManager().Update(new MenuItemViewModels().TransformMenuItemVM(menuItem));
                else
                    new MenuItemManager().Create(new MenuItemViewModels().TransformMenuItemVM(menuItem));

                return RedirectToAction("Index");
            }
            return View(menuItem);
        }

    }
}