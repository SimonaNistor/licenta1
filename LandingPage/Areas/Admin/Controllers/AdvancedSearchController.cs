using LandingPage.Models.AdvancedSearchViewModels;
using LandingPage.Models.CMSDetailsViewModels;
using LandingPage.Models.CMSViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LandingPage.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AdvancedSearchController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        
        public AdvancedSearchController(IHostingEnvironment emv)
        {
            _hostingEnvironment = emv;
        }

        [Authorize]
        [Route("admin/advancedSearch/getall")]
        public JsonResult GetAll(int cmsId)
        {
            var all = new List<DBModels.AdvancedSearch>();
            if (cmsId == 0)
            { all = new AdvancedSearchManager().GetAll(); }
            else
            {
                all = new AdvancedSearchManager().GetByHtmlTypeId(cmsId);
            }
            return Json(new { data = all });
        }
        [Authorize]
        [Route("admin/advancedSearch")]
        public IActionResult Index(int cmsId)
        {
            var all = new List<DBModels.AdvancedSearch>();
            if (cmsId == 0)
            { all = new AdvancedSearchManager().GetAll(); }
            else
            {
                all = new AdvancedSearchManager().GetByHtmlTypeId(cmsId);
            }
            ViewBag.cmsId = cmsId;
            return View(all);
        }
        

        public IActionResult NewHome()
        {
            var ImportAll = new CMSManager().GetAll();

            var CMSViewModelList = new List<CMSViewModels>();
            foreach (var item in ImportAll)
            {
                var CMSViewModel = (CMSViewModels)item;
                CMSViewModel.CMSDetails = new List<CMSDetailsViewModels>();
                CMSViewModelList.Add(CMSViewModel);
                var detail = new CMSDetailsManager().GetByCMSId(item.Id);
                if (detail.Count > 0)
                {

                    foreach (var cmsDetail in detail)
                    {
                        var cms = (CMSDetailsViewModels)cmsDetail;
                        CMSViewModel.CMSDetails.Add(cms);
                    }
                }
            }
            return View(CMSViewModelList);
        }

        [Authorize]
        [Route("admin/advancedSearch/deleteItem")]
        public JsonResult DeleteItem(int ItemId)
        {
            var _cmsDetailsItemManager = new AdvancedSearchManager();
            var item = _cmsDetailsItemManager.GetById(ItemId);
            if (item.Id > 0)
            {
                _cmsDetailsItemManager.Delete(ItemId);

                return Json(true);
            }
            else
                return Json(false);
        }

        [Authorize]
        [Route("admin/advancedSearch/createItem")]
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.HtmlTypeId = new SelectList(new HtmlTypesManager().GetAll(), "Id", "Name");

            var viewModel = new AdvancedSearchViewModels();
            if (id > 0)
            {
                viewModel = (AdvancedSearchViewModels)(new AdvancedSearchManager().GetById(id));
            }

            return View(viewModel);

        }



        [Authorize]
        [Route("admin/advancedSearch/createItem")]
        [HttpPost]
        public async Task<IActionResult> Create(AdvancedSearchViewModels item)
        {
            if (ModelState.IsValid)
            {
                if (item.Id > 0)
                {
                    new AdvancedSearchManager().Update(new AdvancedSearchViewModels().TransformMenuItemVM(item));
                }
                else
                    new AdvancedSearchManager().Create(new AdvancedSearchViewModels().TransformMenuItemVM(item));

                return RedirectToAction("Index");
                
                //Html.ActionLink("Open Invoice", "ActionName", "ControllerName", new { id = Model.InvoiceID }, new { target = "_blank" });
            }
            return View(item);
        }

        [Route("admin/advancedSearch/chooseSearch")]
        [HttpGet]
        public IActionResult chooseSearch(int id)
        {
            ViewBag.HtmlTypeId = new SelectList(new HtmlTypesManager().GetAll(), "Id", "Name");
            //var x = new AdvancedSearchManager().GetById(ViewBag.HtmlTypeId);
            ViewBag.Values = new SelectList(new AdvancedSearchManager().GetAll(), "Id", "Value");

            var viewModel = new AdvancedSearchViewModels();
            if (id > 0)
            {
                viewModel = (AdvancedSearchViewModels)(new AdvancedSearchManager().GetById(id));
            }

            return View(viewModel);

        }

        [Route("admin/advancedSearch/chooseSearch")]
        [HttpPost]
        public async Task<IActionResult> chooseSearch(AdvancedSearchViewModels item)
        {
            if (ModelState.IsValid)
            {
                //return RedirectToAction("Index");
                return RedirectToAction("NewHome");
            }
            return View(item);
        }
        
        public ActionResult CascadingDropDownList()
        {
            return View();
        }

        public JsonResult GetCascadeCategories()
        {
            //using (var northwind = new HttpContext)
            //{
            //    return Json(northwind.Categories
            //        .Select(c => new { CategoryId = c.CategoryID, CategoryName = c.CategoryName }).ToList());
            //}
            var x = new HtmlTypesManager().GetAll();
            return Json(x);
        }

        //public JsonResult GetCascadeProducts(int? categories)
        //{
        //    using (var northwind = GetContext())
        //    {
        //        var products = northwind.Products.AsQueryable();

        //        if (categories != null)
        //        {
        //            products = products.Where(p => p.CategoryID == categories);
        //        }

        //        return Json(products.Select(p => new { ProductID = p.ProductID, ProductName = p.ProductName }).ToList());
        //    }
        //}

        //public JsonResult GetCascadeOrders(int? products)
        //{
        //    using (var northwind = new SampleEntitiesDataContext())
        //    {
        //        var orders = northwind.OrderDetails.AsQueryable();

        //        if (products != null)
        //        {
        //            orders = orders.Where(o => o.ProductID == products);
        //        }

        //        return Json(orders.Select(o => new { OrderID = o.OrderID, ShipCity = o.Order.ShipCity }).ToList());
        //    }
        //}

    }
}
