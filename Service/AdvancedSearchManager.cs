using Database;
using DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class AdvancedSearchManager
    {
        IAdvancedSearchRepository _repo;

        public AdvancedSearchManager(IAdvancedSearchRepository repo)
        {
            _repo = repo;
        }

        public AdvancedSearchManager()
        {
            _repo = new AdvancedSearchRepository();
        }

        public int Create(AdvancedSearch menuItem)
        {
            return _repo.Create(menuItem);
        }

        public void Delete(int itemId)
        {
            _repo.Delete(itemId);
        }

        public AdvancedSearch GetById(int itemId)
        {
            return _repo.GetById(itemId);
        }

        public List<AdvancedSearch> GetByHtmlTypeId(int itemId)
        {
            return _repo.GetByHtmlTypeId(itemId);
        }

        public int Update(AdvancedSearch menuItem)
        {
            return _repo.Update(menuItem);
        }

        public List<AdvancedSearch> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
