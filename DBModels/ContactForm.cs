using System;
using System.Collections.Generic;
using System.Text;

namespace DBModels
{
    public class ContactForm
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }
        public String Message { get; set; }

    }
}
