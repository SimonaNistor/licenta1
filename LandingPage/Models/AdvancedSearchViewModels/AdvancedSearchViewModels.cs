using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DBModels;

namespace LandingPage.Models.AdvancedSearchViewModels
{
    public class AdvancedSearchViewModels
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "HtmlTypeId")]
        public int HtmlTypeId { get; set; }
        [Display(Name = "Value")]
        public string Value { get; set; }

        public static explicit operator AdvancedSearchViewModels(AdvancedSearch model)
        {
            return new AdvancedSearchViewModels
            {
                Id = model.Id,
                HtmlTypeId = model.HtmlTypeId,
                Value = model.Value

            };
        }

        public AdvancedSearch TransformMenuItemVM(AdvancedSearchViewModels model)
        {
            return new AdvancedSearch
            {
                Id = model.Id,
                HtmlTypeId = model.HtmlTypeId,
                Value = model.Value
            };
        }
    }
}
