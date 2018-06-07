using DBModels;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Extensions
{
    public class Workflow
    {
        public static string executeSearch(string keywords)
        {
            //var search = new SearchesManager().GetById(searchId);
            string best = HtmlParser.Nou(keywords);
            return best;
        }

        public static string execute()
        {
            var x = new SearchesManager().GetAll();
            var search = new SearchesManager().GetById(x.Count);
            string best = HtmlParser.Nou(search.Keywords);
            return best;
        }

        public static int search()
        {
            var x = new SearchesManager().GetAll();
            //List<Searches> lista = new List<Searches>();
            //lista = 
            int y = x.Count;
            return y;
        }

    }
}
