using DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace LandingPage.Models.ScriptsViewModels
{
    public class ScriptsViewModels
    {
        [Display(Name="Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Script")]
        public string Script { get; set; }
        [Display(Name = "Header")]
        public bool Header { get; set; }
        [Display(Name = "Footer")]
        public bool Footer { get; set; }

        public static explicit operator ScriptsViewModels(Scripts model)
        {
            return new ScriptsViewModels
            {
                Id = model.Id,
                Name = model.Name,
                Script = model.Script,
                Header = model.Header,
                Footer = model.Footer

            };
        }

        public Scripts TransformScriptsVM(ScriptsViewModels model)
        {
            return new Scripts
            {
                Id = model.Id,
                Name = model.Name,
                Script = model.Script,
                Header = model.Header,
                Footer = model.Footer
            };
        }
    }
}
