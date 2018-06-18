using LandingPage.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using Service;

namespace LandingPage.Areas.Admin.Controllers
{
    public class ChartsController:Controller
    {
        public ActionResult StandardBar()
        {
            
            return View();
        }
        
        public JObject keywords()
        {
            dynamic product = new JObject();
            

            string y = Workflow.getLastSearchKeywords();
            List<int> n = new List<int>();
            n = HtmlParser.getStatsKeywords(y);
            int res = 0;
            List<string> lista = new List<string>();
            for(int i=0;i<n.Count();i++)
            {
                lista.Add((i + 1).ToString());
            }
            int j = 1;
                for(int i=0;i<n.Count();i++)
                {
                    product[""+j.ToString()] = n[i];
                j++;
                }
            //((IDictionary<string, object>)product)["test"] = res;
            return product;// n.toJson;//Json(x);
        }

        public JObject language()
        {
            dynamic product = new JObject();
            List<int> n = new List<int>();
            n = HtmlParser.getStatsLanguage();
            int j = 1;
            for (int i = 0; i < n.Count(); i++)
            {
                product["" + j.ToString()] = n[i];
                j++;
            }
            return product;
        }

        public JObject finalScores()
        {
            dynamic product = new JObject();


            string y = Workflow.getLastSearchKeywords();
            List<double> n = new List<double>();
            n = HtmlParser.getStatsFinal(y);
            //List<int> n = new List<int>();
            //n = n.ConvertAll(Convert.ToInt32);
            int j = 1;
            for (int i = 0; i < n.Count(); i++)
            {
                product["" + j.ToString()] = n[i]*100;
                j++;
            }
            //((IDictionary<string, object>)product)["test"] = res;
            return product;// n.toJson;//Json(x);
        }

        public JObject searchesStatisticsCautareUnCuvant()
        {
            dynamic product = new JObject();

            List<string> lista = new List<string>();
            List<int> valori = new List<int>();

            var x = new SearchesManager().GetAll();

            foreach(var cautare in x)
            {
                if (!cautare.Keywords.Contains("@"))
                { 
                    if (lista.Count() == 0)
                    {
                        lista.Add(cautare.Keywords);
                        valori.Add(1);
                    }
                    else
                    {
                        int k = 0;
                        for (int i = 0; i < lista.Count(); i++)
                        {
                            if (cautare.Keywords == lista[i])
                            {
                                valori[i]++;
                                k++;
                            }
                        }
                        if (k == 0)
                        {
                            lista.Add(cautare.Keywords);
                            valori.Add(1);
                        }
                    }
                }
            }

            
            for (int i = 0; i < valori.Count(); i++)
            {
                product["" + lista[i]] = valori[i];
            }
            return product;
        }

        public JObject searchesStatisticsCautareMultipla()
        {
            dynamic product = new JObject();
            

            List<string> listaCautariMultiple = new List<string>();
            List<int> valoriCautariMultiple = new List<int>();

            var x = new SearchesManager().GetAll();

            foreach (var cautare in x)
            {
                if (cautare.Keywords.Contains("@"))
                {
                    if (listaCautariMultiple.Count() == 0)
                    {
                        listaCautariMultiple.Add(cautare.Keywords);
                        valoriCautariMultiple.Add(1);
                    }
                    else
                    {
                        int k = 0;
                        for (int i = 0; i < listaCautariMultiple.Count(); i++)
                        {
                            if (cautare.Keywords == listaCautariMultiple[i])
                            {
                                valoriCautariMultiple[i]++;
                                k++;
                            }
                        }
                        if (k == 0)
                        {
                            listaCautariMultiple.Add(cautare.Keywords);
                            valoriCautariMultiple.Add(1);
                        }
                    }
                }
            }


            for (int i = 0; i < valoriCautariMultiple.Count(); i++)
            {
                product["" + listaCautariMultiple[i]] = valoriCautariMultiple[i];
            }
            return product;
        }
    }
}
