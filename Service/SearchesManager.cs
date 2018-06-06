using Database;
using DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class SearchesManager
    {
        ISearchesRepository _repo;

        public SearchesManager(ISearchesRepository repo)
        {
            _repo = repo;
        }

        public SearchesManager()
        {
            _repo = new SearchesRepository();
        }

        public int Create(String keywords, DateTime date, String ip)
        {
            return _repo.Create(keywords,date,ip);
        }

        public void Delete(int itemId)
        {
            _repo.Delete(itemId);
        }

        public Searches GetById(int itemId)
        {
            return _repo.GetById(itemId);
        }

        public int Update(Searches menuItem)
        {
            return _repo.Update(menuItem);
        }

        public List<Searches> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
