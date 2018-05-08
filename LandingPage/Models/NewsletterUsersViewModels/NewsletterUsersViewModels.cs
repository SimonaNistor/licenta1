using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DBModels;

namespace LandingPage.Models.NewsletterUsersViewModels
{
    public class NewsletterUsersViewModels
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Ip")]
        public string Ip { get; set; }

        public static explicit operator NewsletterUsersViewModels(DBModels.NewsletterUsers model)
        {
            return new NewsletterUsersViewModels
            {
                Id = model.Id,
                Email = model.Email,
                Date = model.Date,
                Ip = model.Ip
            };

        }

        public DBModels.NewsletterUsers Transform(NewsletterUsersViewModels model)
        {
            return new DBModels.NewsletterUsers
            {
                Id = model.Id,
                Email = model.Email,
                Date = model.Date,
                Ip = model.Ip
            };
        }
    }
}
