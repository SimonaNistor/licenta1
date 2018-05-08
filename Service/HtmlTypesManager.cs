using Database;
using DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class HtmlTypesManager
    {
        IHtmlTypesRepository _repo;

        public HtmlTypesManager(IHtmlTypesRepository repo)
        {
            _repo = repo;
        }

        public HtmlTypesManager()
        {
            _repo = new HtmlTypesRepository();
        }

        public int Create(HtmlTypes htmlTypes)
        {
            return _repo.Create(htmlTypes);
        }

        public int Update(HtmlTypes htmlTypes)
        {
            return _repo.Update(htmlTypes);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        public HtmlTypes GetById(int id)
        {
            return _repo.GetById(id);
        }

        public List<HtmlTypes> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
