using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    public class Searches
    {
        public int Id { get; set; }
        public String Keywords { get; set; }
        public DateTime Date { get; set; }
        public String Ip { get; set; }
    }
}
