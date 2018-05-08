using DBModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models.CMSDetailsViewModels
{
    public class CMSDetailsViewModels
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "CMSId")]
        public int CMSId { get; set; }
        [Display(Name = "Nume")]
        public String Name { get; set; }
        [Display(Name = "Index")]
        public int OrderIndex { get; set; }
        [Display(Name = "Tip HTML")]
        public int HtmlTypeId { get; set; }
        [Display(Name = "Continut")]
        public String Content { get; set; }
        public IFormFile FileUploadId { get; set; }
        public String ContentId { get; set; }
        public String TextAreaId { get; set; }
        public String HtmlEditorId { get; set; }



        public static explicit operator CMSDetailsViewModels(CMSDetails model)
        {
            return new CMSDetailsViewModels
            {
                Id = model.Id,
                CMSId = model.CMSId,
                Name = model.Name,
                OrderIndex = model.OrderIndex,
                HtmlTypeId = model.HtmlTypeId,
                Content = model.Content,
                ContentId = model.Content,
                TextAreaId = model.Content,
                HtmlEditorId = model.Content

            };

        }

        public CMSDetails Transform(CMSDetailsViewModels model)
        {
            return new CMSDetails
            {
                Id = model.Id,
                CMSId=model.CMSId,
                Name = model.Name,
                OrderIndex = model.OrderIndex,
                HtmlTypeId = model.HtmlTypeId,
                Content = model.Content
            };
        }
    }
}
