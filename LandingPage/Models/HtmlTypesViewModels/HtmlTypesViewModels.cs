using DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models.HtmlTypesViewModels
{
    public class HtmlTypesViewModels
    {
        [Display(Name="Id")]
        public int Id { get; set; }
        [Display(Name="Nume")]
        public String Name { get; set; }

        public static explicit operator HtmlTypesViewModels(HtmlTypes model)
        {
            return new HtmlTypesViewModels
            {
                Id = model.Id,
                Name = model.Name
            };

        }

        public HtmlTypes Transform(HtmlTypesViewModels model)
        {
            return new HtmlTypes
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
