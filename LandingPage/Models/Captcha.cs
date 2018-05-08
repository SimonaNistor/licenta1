using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Models
{
    public class Captcha
    {
        public bool success { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }


    }
}
