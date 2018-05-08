using System;
using System.Collections.Generic;
using System.Text;
using Database;
using DBModels;

namespace Service
{
    public class CMSManager
    {
        ICMSRepository _repo;

        public CMSManager(ICMSRepository repo)
        {
            _repo = repo;
        }

        public CMSManager()
        {
            _repo = new CMSRepository();
        }

        public int Create(CMS cms)
        {
            return _repo.Create(cms);
        }

        public void Delete(int itemId)
        {
            _repo.Delete(itemId);
        }

        public CMS GetById(int id)
        {
            return _repo.GetById(id);
        }

        public int Update(CMS cms)
        {
            return _repo.Update(cms);
        }

        public List<CMS> GetAll()
        {
            return _repo.GetAll();
        }

        public List<CMS> GetByMenuItemId(int id)
        {
            return _repo.GetByMenuItemId(id);
        }
    }
}
