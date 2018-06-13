using LandingPage.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet;
using Newtonsoft.Json.Linq;

namespace LandingPage.Areas.Admin.Controllers
{
    public class ChartsController:Controller
    {
        public ActionResult StandardBar()
        {
            
            return View();
        }
        
        public JObject functie()
        {
            
            dynamic product = new JObject();
            

            string y = Workflow.getLastSearchKeywords();
            List<int> n = new List<int>();
            n = HtmlParser.numere(y);
            int res = 0;
            List<string> lista = new List<string>();
            for(int i=0;i<n.Count();i++)
            {
                lista.Add((i + 1).ToString());
            }
            foreach(int x in n)
            {
                for(int i=1;i<=n.Count();i++)
                {
                    product[""+i.ToString()] = x;
                }
            }
            string p = "ceva";
            product["" +p] = 80;
            //((IDictionary<string, object>)product)["test"] = res;
            return product;// n.toJson;//Json(x);
        }
    }
}
