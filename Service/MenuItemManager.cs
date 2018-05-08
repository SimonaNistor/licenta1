using System;
using DBModels;
using Database;
using System.Collections.Generic;

namespace Service
{
    public class MenuItemManager
    {
        IMenuItemRepository _repo;

        public MenuItemManager(IMenuItemRepository repo)
        {
            _repo = repo;
        }

        public MenuItemManager()
        {
            _repo = new MenuItemRepository();
        }

        public int Create(MenuItem menuItem)
        {
            return _repo.Create(menuItem);
        }

        public void Delete(int itemId)
        {
            _repo.Delete(itemId);
        }

        public MenuItem GetById(int itemId)
        {
            return _repo.GetById(itemId);
        }

        public int Update(MenuItem menuItem)
        {
            return _repo.Update(menuItem);
        }

        public List<MenuItem> GetAll()
        {
            return _repo.GetAll();
        }

        public List<MenuItem> GetAllSortedAndVisible()
        {
            return _repo.GetAllSortedAndVisible();
        }
    }
}
