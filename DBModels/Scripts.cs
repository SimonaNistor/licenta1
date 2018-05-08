using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    public class Scripts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Script { get; set; }
        public bool Header { get; set; }
        public bool Footer { get; set; }
    }
}
