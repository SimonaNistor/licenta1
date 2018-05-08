using Microsoft.AspNetCore.Mvc;
using Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace LandingPage.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ContactFormController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public ContactFormController(IHostingEnvironment emv)
        {
            _hostingEnvironment = emv;
        }

        [Route("admin/contactform/getall")]
        public JsonResult GetAll()
        {
            var all = new ContactFormManager().GetAll();
            return Json(new { data = all });
        }
        [Route("admin/contactform")]
        public IActionResult Index()
        {
            var all = new ContactFormManager().GetAll();
            return View(all);
        }

        [Route("admin/contactform/deleteItem")]
        public JsonResult DeleteItem(int ItemId)
        {
            var _contactFormItemManager = new ContactFormManager();
            var item = _contactFormItemManager.GetById(ItemId);
            if (item.Id > 0)
            {
                _contactFormItemManager.Delete(ItemId);

                return Json(true);
            }
            else
                return Json(false);
        }
       
    }
}