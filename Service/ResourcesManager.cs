using Database;
using DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ResourcesManager
    {
        IResourceRepository _repo;

        public ResourcesManager(IResourceRepository repo)
        {
            _repo = repo;
        }

        public ResourcesManager()
        {
            _repo = new ResourcesRepository();
        }

        public int Create(Resources res)
        {
            return _repo.Create(res);
        }

        public void Delete(int itemId)
        {
            _repo.Delete(itemId);
        }

        public Resources GetById(int itemId)
        {
            return _repo.GetById(itemId);
        }

        public int Update(Resources res)
        {
            return _repo.Update(res);
        }

        public List<Resources> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Resources> GetByType(string type)
        {
            return _repo.GetByType(type);
        }
    }
}
