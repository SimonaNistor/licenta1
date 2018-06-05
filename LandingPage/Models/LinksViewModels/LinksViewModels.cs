using DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models.LinksViewModels
{
    public class LinksViewModels
    {
        [Display(Name = "id")]
        public int id { get; set; }
        [Display(Name = "limbaj")]
        public String limbaj { get; set; }
        [Display(Name = "tags")]
        public String tags { get; set; }
        [Display(Name = "link")]
        public String link { get; set; }

        public static explicit operator LinksViewModels(Links model)
        {
            return new LinksViewModels
            {
                id = model.id,
                limbaj = model.limbaj,
                tags = model.tags,
                link = model.link

            };
        }

        public Links TransformLinksVM(LinksViewModels model)
        {
            return new Links
            {
                id = model.id,
                limbaj = model.limbaj,
                tags = model.tags,
                link = model.link
            };
        }
    }
}
