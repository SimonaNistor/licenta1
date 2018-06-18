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
            var z = new SearchesManager().GetLastEntry();
            string best = HtmlParser.Nou(z.Keywords);
            return best;
        }

        public static string getBestScore()
        {
            string y = Workflow.getLastSearchKeywords();
            double x = Math.Round(HtmlParser.getBestStat(y)*100,2);
            return x.ToString();
        }

        public static string getLastSearchKeywords()
        {
            var z = new SearchesManager().GetLastEntry();
            return z.Keywords;
        }

        public static string getCurrentSearch()
        {
            var z = new SearchesManager().GetLastEntry();
            return z.Keywords;
        }

        //public static List<int> executeStatistics()
        //{
        //    var x = new SearchesManager().GetAll();
        //    var z = new SearchesManager().GetLastEntry();
        //    List<int> numere = HtmlParser.numere(z.Keywords);
        //    return numere;
        //}

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
