using test.Models.MenuItemViewModels;
using Microsoft.AspNetCore.Mvc;
using Service;
using Microsoft.AspNetCore.Authorization;

namespace test.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class MenuItemsController : Controller
    {
        [Route("admin/menuitem/getall")]
        public JsonResult GetAll()
        {
            var all = new MenuItemManager().GetAll();
            return Json(new { data = all });
        }
        [Route("admin/menuitem/index")]
        public IActionResult Index()
        {
            var all = new MenuItemManager().GetAll();
            return View(all);
        }


    
    [Route("admin/menuitem/deleteItem")]
    
    public JsonResult DeleteItem(int ItemId)
    {
        var _menuItemManager = new MenuItemManager();
        var item =  _menuItemManager.GetById(ItemId);
        if (item.ItemId > 0)
        {
            _menuItemManager.Delete(ItemId);
           
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
            if (id>0)
            {
                viewModel = (MenuItemViewModels)(new MenuItemManager().GetById(id));
            }
            
            return View(viewModel);

        }
        [Route("admin/menuitem/createItem")]
        [HttpPost]
        public IActionResult Create(MenuItemViewModels menuItem)
        {         
            if(ModelState.IsValid)
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