using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    class SQLHelper
    {
        public static string ConnectionString
        {
            get
            {
                //return //"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=LandingPage;Data Source=DESKTOP-34HVD4S";
                return "Data Source=DESKTOP-34HVD4S;Initial Catalog=LandingPage;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
        }
    }
}
