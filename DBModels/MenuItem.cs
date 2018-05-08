using System;
using System.ComponentModel.DataAnnotations;

namespace DBModels
{
    public class MenuItem
    {
        [Display(Name = "Id")]
        public int ItemId { get; set; }
        public String Name { get; set; }
        public int OrderIndex { get; set; }
        public String Section { get; set; }
        public Boolean IsVisible { get; set; }
    }
}
