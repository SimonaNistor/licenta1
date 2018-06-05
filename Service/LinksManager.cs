using Database;
using DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class LinksManager
    {
        ILinksRepository _repo;

        public LinksManager(ILinksRepository repo)
        {
            _repo = repo;
        }

        public LinksManager()
        {
            _repo = new LinksRepository();
        }

        public int Create(Links link)
        {
            return _repo.Create(link);
        }

        public void Delete(int itemId)
        {
            _repo.Delete(itemId);
        }

        public Links GetById(int id)
        {
            return _repo.GetById(id);
        }

        public int Update(Links link)
        {
            return _repo.Update(link);
        }

        public List<Links> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Links> GetByLanguage(string lang)
        {
            return _repo.GetByLanguage(lang);
        }
    }
}
