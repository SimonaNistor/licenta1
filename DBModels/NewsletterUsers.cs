using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    public class NewsletterUsers
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Ip { get; set; }
    }
}
