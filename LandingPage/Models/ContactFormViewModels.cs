using DBModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LandingPage.Models.CMSDetailsViewModels;

namespace LandingPage.Models.ContactFormViewModels
{
    public class ContactFormViewModels
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "FirstName")]
        public String FirstName { get; set; }
        [Display(Name = "LastName")]
        public String LastName { get; set; }
        [Display(Name = "EmailAddress")]
        public String EmailAddress { get; set; }
        [Display(Name = "Message")]
        public String Message { get; set; }


        public static explicit operator ContactFormViewModels(ContactForm model)
        {
            return new ContactFormViewModels
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                Message=model.Message
            };
        }

        public ContactForm TransformContactFormVM(ContactFormViewModels model)
        {
            return new ContactForm
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                Message = model.Message
            };
        }
    }
}