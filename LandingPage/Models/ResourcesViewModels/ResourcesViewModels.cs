using DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models.ResourcesViewModels
{
    public class ResourcesViewModels
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Code")]
        public String Code { get; set; }
        [Display(Name = "Type")]
        public String Type { get; set; }

        public static explicit operator ResourcesViewModels(Resources model)
        {
            return new ResourcesViewModels
            {
                Id = model.Id,
                Code = model.Code,
                Type = model.Type
            };
        }

        public Resources TransformResourcesVM(ResourcesViewModels model)
        {
            return new Resources
            {
                Id = model.Id,
                Code = model.Code,
                Type = model.Type
            };
        }
    }
}
