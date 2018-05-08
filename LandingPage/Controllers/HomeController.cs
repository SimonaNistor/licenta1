using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DBModels;
using LandingPage.Models;
using LandingPage.Models.CMSDetailsViewModels;
using LandingPage.Models.CMSViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service;
using test.Models;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
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

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
 
    [HttpPost]
        public JsonResult ContactForm(string fname, string lname, string email, string message, string recaptcha)
        {
            try
            {
                var response = recaptcha;
                //secret that was generated in key value pair
                string secret = "6LegpE8UAAAAAB00Vr6SIxde9cnqKfZrbbw3CA7y";

                var client = new WebClient();
                var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                var captchaResponse = JsonConvert.DeserializeObject<Captcha>(reply);

                //when response is false check for the error message
                if (!captchaResponse.success)
                {

                    return Json(new { success = false, message = "invalid Captcha" });
                }
                else
                {

                    var _contactFormManager = new ContactFormManager();
                    var modelContactForm = new ContactForm();
                    string emailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@";
                    //if (fname == null || lname == null || email == null || message == null || message.Length < 10 || Regex.IsMatch(email, emailPattern) == false)
                    //{
                    //    return Json(false);
                    //}
                    if(fname==null)
                    {
                        return Json(new { success = false, message = "Please enter a first name." });
                    }
                    else
                        if (lname == null)
                    {
                        return Json(new { success = false, message = "Please enter a last name." });
                    }
                    else
                        if (email == null)
                    {
                        return Json(new { success = false, message = "Please enter an email" });
                    }
                    else
                        if (message == null)
                    {
                        return Json(new { success = false, message = "Please enter a message" });
                    }
                    else
                        if(message.Length < 10)
                    {
                        return Json(new { success = false, message = "Please enter a longer message" });
                    }
                    else
                    if(Regex.IsMatch(email, emailPattern) == false)
                    {
                        return Json(new { success = false, message = "Please enter a valid email" });
                    }
                    else
                    {
                        modelContactForm.FirstName = fname;
                        modelContactForm.LastName = lname;
                        modelContactForm.EmailAddress = email;
                        modelContactForm.Message = message;
                        var createId = _contactFormManager.Create(modelContactForm);
                        return Json(new { success = true, message = "Ati completat cu succes" });
                    }
                }
            }
            catch { return Json(false); };
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
