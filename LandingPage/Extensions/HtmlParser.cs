using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LandingPage.Extensions
{
    public class HtmlParser
    {
        public static string Nou(string keyword)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://www.tutorialspoint.com/cplusplus/cpp_constructor_destructor.htm");

            string webData = System.Text.Encoding.UTF8.GetString(raw);
            char[] delimiterChars = { '<','>'};
            string[] code = webData.Split(delimiterChars);
            StringBuilder ceva = new StringBuilder();
            int count = 0;
            foreach (string cod in code)
            {
                count++;
                ceva.AppendLine(code.ToString());
            }
            string x = "empty";
            List<string> result = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i=0;i<count;i++)
            {
                if (code[i].ToString() == "pre class=\"prettyprint notranslate\"")
                {
                    x = code[i+1].ToString();
                    x = replaceAll(x);
                    result.Add(x.ToString());
                }
            }

            return selectBestSnippet(result, keyword);//result[2];
        }



        public static string replaceAll(string x)
        {
            x = x.Replace("&lt;&lt;", "<<");
            x = x.Replace("&lt;", "<");
            x = x.Replace("&gt;&gt;", ">>");
            x = x.Replace("&gt;", ">");
            x = x.Replace("&amp;&amp;", "&&");
            return x;
        }

        public static string selectBestSnippet(List<string> code, string keyword)
        {
            int counter = 0;
            foreach(string s in code)
            {
                counter++;
            }
            List<int> values = new List<int>();
            for(int i=0;i<counter;i++)
            {
                values.Add(KeywordsDetect.detect(code[i], keyword));
            }
            int maxValue = values.Max();
            int maxIndex = values.IndexOf(maxValue);
            //var result = values.OrderByDescending(w => w).Take(3);
            //List<int> results = result.ToList();
            //return code[results[1]];
            return code[maxIndex];
        }
    }
}
