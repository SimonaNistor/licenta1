using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    public class CMSDetails
    {

        public int Id { get; set; }
        public int CMSId { get; set; }
        public String Name { get; set; }
        public int OrderIndex { get; set; }
        public int HtmlTypeId { get; set; }
        public String Content { get; set; }
    }
}
