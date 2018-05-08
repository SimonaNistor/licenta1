using System;
using System.Collections.Generic;
using System.Text;
using Database;
using DBModels;

namespace Service
{
    public class CMSDetailsManager
    {
        ICMSDetailsRepository _repo;

        public CMSDetailsManager(ICMSDetailsRepository repo)
        {
            _repo = repo;
        }

        public CMSDetailsManager()
        {
            _repo = new CMSDetailsRepository();
        }

        public int Create(CMSDetails cmsdetails)
        {
            return _repo.Create(cmsdetails);
        }

        public void Delete(int itemId)
        {
            _repo.Delete(itemId);
        }

        public CMSDetails GetById(int id)
        {
            return _repo.GetById(id);
        }

        public int Update(CMSDetails cmsdetails)
        {
            return _repo.Update(cmsdetails);
        }

        public List<CMSDetails> GetAll()
        {
            return _repo.GetAll();
        }

        public List<CMSDetails> GetByCMSId(int id)
        {
            return _repo.GetByCMSId(id);
        }
    }
}
