using DBModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.Models.CMSDetailsViewModels;

namespace LandingPage.Models.CMSViewModels
{
    public class CMSViewModels
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Nume")]
        public String Name { get; set; }
        [Display(Name = "Menu Item")]
        public int MenuItemId { get; set; }
        [Display(Name = "Tip HTML")]
        public int HtmlType { get; set; }
        [Display(Name = "Continut")]
        public String Content { get; set; }

        public IFormFile FileUploadId { get; set; }
        public String ContentId { get; set; }
        public String TextAreaId { get; set; }
        public String HtmlEditorId { get; set; }


        public List<LandingPage.Models.CMSDetailsViewModels.CMSDetailsViewModels> CMSDetails;


        public static explicit operator CMSViewModels(CMS model)
        {
            return new CMSViewModels
            {
                Id = model.Id,
                Name = model.Name,
                MenuItemId = model.MenuItemId,
                HtmlType = model.HtmlType,
                Content = model.Content,
                ContentId = model.Content,
                TextAreaId = model.Content,
                HtmlEditorId = model.Content
            };

        }

        public CMS Transform(CMSViewModels model)
        {
            return new CMS
            {
                Id = model.Id,
                Name = model.Name,
                MenuItemId = model.MenuItemId,
                HtmlType = model.HtmlType,
                Content = model.Content
            };
        }
    }
}
