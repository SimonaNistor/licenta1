using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Extensions
{
    public class Workflow
    {
        public static string executeSearch(int searchId)
        {
            var search = new SearchesManager().GetById(searchId);
            string best = HtmlParser.Nou(search.Keywords);
            return best;
        }
        


    }
}
