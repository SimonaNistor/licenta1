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
            ///////////////////////////////////////////////////////////////// ^merge

            string html;
            using (var client = new WebClient())
            {
                html = client.DownloadString("http://stackoverflow.com/questions/2038104");
            }
            
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            StringBuilder sb = new StringBuilder();
            foreach (HtmlTextNode node in doc.DocumentNode.SelectNodes("//text()"))
            {
                sb.AppendLine(node.Text);
            }
            string final = sb.ToString();
            return final;// webData;
        }
        
        
    }
}
