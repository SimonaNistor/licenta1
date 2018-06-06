using DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models.SearchesViewModels
{
    public class SearchesViewModels
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Keywords")]
        public String Keywords { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Ip")]
        public String Ip { get; set; }

        public static explicit operator SearchesViewModels(Searches model)
        {
            return new SearchesViewModels
            {
                Id = model.Id,
                Keywords = model.Keywords,
                Date = model.Date,
                Ip = model.Ip

            };
        }

        public Searches TransformSearchesVM(SearchesViewModels model)
        {
            return new Searches
            {
                Id = model.Id,
                Keywords = model.Keywords,
                Date = model.Date,
                Ip = model.Ip
            };
        }
    }
}
