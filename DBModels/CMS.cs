using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    public class CMS
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int MenuItemId { get; set; }
        public int HtmlType { get; set; }
        public String Content { get; set; }
    }
}
