using System;
using System.Collections.Generic;
using System.Text;
using Database;
using DBModels;

namespace Service
{
    public class NewsletterUsersManager
    {
        INewsletterUsersRepository _repo;

        public NewsletterUsersManager(INewsletterUsersRepository repo)
        {
            _repo = repo;
        }

        public NewsletterUsersManager()
        {
            _repo = new NewsletterUsersRepository();
        }

        public int Create(string email, DateTime date, string ip)
        {
            return _repo.Create(email,date,ip);
        }

        public void Delete(int userId)
        {
            _repo.Delete(userId);
        }

        public NewsletterUsers GetById(int id)
        {
            return _repo.GetById(id);
        }

        public List<NewsletterUsers> GetAll()
        {
            return _repo.GetAll();
        }

        public NewsletterUsers GetByEmail(string email)
        {
            return _repo.GetByEmail(email);
        }
    }
}
