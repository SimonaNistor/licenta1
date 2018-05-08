using DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models.MenuItemViewModels
{
    public class MenuItemViewModels
    {
        [Display(Name = "Id")]
        public int ItemId { get; set; }
        [Display(Name = "Nume")]
        public String Name { get; set; }
        [Display(Name = "Nr. Ordine")]
        public int OrderIndex { get; set; }
        [Display(Name = "Sectiune")]
        public String Section { get; set; }
        [Display(Name = "Vizibil?")]
        public Boolean IsVisible { get; set; }

        public static explicit operator MenuItemViewModels(MenuItem model)
        {
            return new MenuItemViewModels{
                ItemId = model.ItemId,
                Name = model.Name,
                OrderIndex = model.OrderIndex,
                Section = model.Section,
                IsVisible = model.IsVisible
               
            };
        }

        public MenuItem TransformMenuItemVM(MenuItemViewModels model)
        {
            return new MenuItem
            {
                ItemId = model.ItemId,
                Name = model.Name,
                OrderIndex = model.OrderIndex,
                Section = model.Section,
                IsVisible = model.IsVisible
            };
        }
    }
}
