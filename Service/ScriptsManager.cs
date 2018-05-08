using System;
using System.Collections.Generic;
using System.Text;
using DBModels;
using Database;

namespace Service
{
    public class ScriptsManager
    {
        IScriptsRepository _repo;

        public ScriptsManager(IScriptsRepository repo)
        {
            _repo = repo;
        }

        public ScriptsManager()
        {
            _repo = new ScriptsRepository();
        }

        public int Create(Scripts cms)
        {
            return _repo.Create(cms);
        }

        public void Delete(int itemId)
        {
            _repo.Delete(itemId);
        }

        public Scripts GetById(int id)
        {
            return _repo.GetById(id);
        }

        public int Update(Scripts cms)
        {
            return _repo.Update(cms);
        }

        public List<Scripts> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Scripts> GetHeaders()
        {
            return _repo.GetHeaders();
        }

        public List<Scripts> GetFooters()
        {
            return _repo.GetFooters();
        }
    }
}
