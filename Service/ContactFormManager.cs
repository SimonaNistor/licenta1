using System;
using System.Collections.Generic;
using System.Text;
using Database;
using DBModels;

namespace Service
{
    public class ContactFormManager
    {
        IContactFormRepository _repo;

        public ContactFormManager(IContactFormRepository repo)
        {
            _repo = repo;
        }

        public ContactFormManager()
        {
            _repo = new ContactFormRepository();
        }

        public int Create(ContactForm contactForm)
        {
            return _repo.Create(contactForm);
        }

        public void Delete(int Id)
        {
            _repo.Delete(Id);
        }

        public ContactForm GetById(int Id)
        {
            return _repo.GetById(Id);
        }

        public int Update(ContactForm contactForm) {
            return _repo.Update(contactForm);
        }

        public List<ContactForm> GetAll() {
            return _repo.GetAll();
        }

    }
}
