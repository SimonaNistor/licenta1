using HtmlAgilityPack;
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
        public static string Nou()
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
                    string[] y = x.Split(';');
                    foreach (string s in y)
                    {
                        stringBuilder.AppendLine("\n" + s.ToString());
                    }
                    result.Add(stringBuilder.ToString());
                }
            }
            



            ///////////////////////////////////////////////////////////////// ^merge

            string html;
            using (var client = new WebClient())
            {
                html = client.DownloadString("https://www.tutorialspoint.com/cplusplus/cpp_constructor_destructor.htm");
            }
            
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            StringBuilder sb = new StringBuilder();
            //string head = doc.DocumentNode.SelectNodes("//[@class='question-hiperlink']").ToString();

            foreach (HtmlTextNode node in doc.DocumentNode.SelectNodes("//text()"))
            {
                sb.AppendLine(node.Text);
            }
            string final = sb.ToString();
            
            return result[0];// webData;
        }
        
        
    }
}
