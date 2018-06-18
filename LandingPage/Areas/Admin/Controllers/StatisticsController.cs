using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Areas.Admin.Controllers
{
    public class StatisticsController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public StatisticsController(IHostingEnvironment emv)
        {
            _hostingEnvironment = emv;
        }

        [Route("statistics")]
        public IActionResult statistics()
        {
            return View();
        }
    }
}
