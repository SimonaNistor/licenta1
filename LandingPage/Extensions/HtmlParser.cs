using DBModels;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Configuration;
using NeuralNetworks.Library;
using Service;
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
        public static string Nou(string keywords)
        {
            List<string> rezultateCautare = cautare();

            return selectBestSnippet(rezultateCautare, keywords);
        }

        public static List<string> nextBest(string keywords)
        {
            List<string> rezultateCautare = cautare();

            return selectNextBest(rezultateCautare, keywords);
        }

        public static List<double> getStatsFinal(string keywords)
        {
            List<string> rez = cautare();
            return detectionScores(rez, keywords);
        }

        public static double getBestStat(string keywords)
        {
            List<string> rez = cautare();
            List<double> lista = new List<double>();
            lista = detectionScores(rez, keywords);
            double r = lista.Max();
            return r;
        }

        public static List<int> getStatsKeywords(string keywords)
        {
            List<string> rezultateCautare = cautare();

            List<int> numere = new List<int>();
            foreach (string x in rezultateCautare)
            {
                numere.Add(KeywordsDetect.detect(x, keywords));
                //numere += KeywordsDetect.detect(x, keywords);
            }
            return numere;
        }

        public static List<int> getStatsLanguage()
        {
            List<string> rezultateCautare = cautare();

            List<int> numere = new List<int>();
            foreach (string x in rezultateCautare)
            {
                numere.Add(LanguageDetect.detect(x));
                //numere += KeywordsDetect.detect(x, keywords);
            }
            return numere;
        }

        public static string Quality(string k1, string k2)
        {
            List<string> listaToateCoduri = new List<string>();
            List<Resources> listaAlteSurse = new ResourcesManager().GetAll();
            foreach (Resources res in listaAlteSurse)
            {
                listaToateCoduri.Add(res.Code);
            }

            return QualityTests.detect(listaToateCoduri[0], k1, k2);
        }

        public static List<string> cautare()
        {

            List<string> listaToateCoduri = new List<string>();
            List<Links> listaLinkuri = new LinksManager().GetAll();
            List<Resources> listaAlteSurse = new ResourcesManager().GetAll();


            //get all code from links
            List<string> listaIntermediara = new List<string>();
            foreach (Links link in listaLinkuri)
            {
                listaIntermediara = editareCod(link.link);
                foreach (String cod in listaIntermediara)
                {
                    listaToateCoduri.Add(cod);
                }
                listaIntermediara.Clear();
            }

            //get all code from bd
            foreach (Resources res in listaAlteSurse)
            {
                listaToateCoduri.Add(res.Code);
            }

            return listaToateCoduri;
        }

        public static List<string> editareCod(string link) //returneaza lista cu toate codurile din link
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData(link);
            string webData = System.Text.Encoding.UTF8.GetString(raw);
            char[] delimiterChars = { '<', '>' };
            string[] code = webData.Split(delimiterChars);
            StringBuilder ceva = new StringBuilder();
            int count = 0;
            foreach (string cod in code)
            {
                count++;
                ceva.AppendLine(code.ToString());
            }
            string x = "";
            List<string> result = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (code[i].ToString() == "pre class=\"prettyprint notranslate\"")
                {
                    x = code[i + 1].ToString();
                    x = replaceAll(x);
                    result.Add(x.ToString());
                }
            }
            return result;
        }

        public static string replaceAll(string x)
        {
            x = x.Replace("&lt;&lt;", "<<");
            x = x.Replace("&lt;", "<");
            x = x.Replace("&gt;&gt;", ">>");
            x = x.Replace("&gt;", ">");
            x = x.Replace("&amp;&amp;", "&&");
            x = x.Replace("&amp;", "&");
            return x;
        }



        public static string selectBestSnippet(List<string> code, string keywords)//cu algoritmii
        {
            int counter = 0;
            foreach (string s in code)
            {
                counter++;
            }
            List<int> pointsKeywords = new List<int>();
            List<int> pointsLanguage = new List<int>();
            for (int i = 0; i < counter; i++)
            {
                pointsKeywords.Add(KeywordsDetect.detect(code[i], keywords));
                pointsLanguage.Add(LanguageDetect.detect(code[i]));
            }

            NeuralNetwork network = Network.Functie();
            List<double> listResults = new List<double>();
            for (int i = 0; i < counter; i++)
            {
                listResults.Add(Network.MakeExamplePredictions(network, pointsKeywords[i], pointsLanguage[i]));
            }

            double maxValue = listResults.Max();
            int maxIndex = listResults.IndexOf(maxValue);
            return code[maxIndex];
        }

        public static List<double> detectionScores(List<string> code, string keywords)//cu algoritmii
        {
            int counter = 0;
            foreach (string s in code)
            {
                counter++;
            }
            List<int> pointsKeywords = new List<int>();
            List<int> pointsLanguage = new List<int>();
            for (int i = 0; i < counter; i++)
            {
                pointsKeywords.Add(KeywordsDetect.detect(code[i], keywords));
                pointsLanguage.Add(LanguageDetect.detect(code[i]));
            }

            NeuralNetwork network = Network.Functie();
            List<double> listResults = new List<double>();
            for (int i = 0; i < counter; i++)
            {
                listResults.Add(Network.MakeExamplePredictions(network, pointsKeywords[i], pointsLanguage[i]));
            }

            double maxValue = listResults.Max();
            int maxIndex = listResults.IndexOf(maxValue);
            return listResults;
        }

        public static List<string> selectNextBest(List<string> code, string keywords)//cu algoritmii
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            int counter = 0;
            foreach (string s in code)
            {
                counter++;
            }
            List<int> pointsKeywords = new List<int>();
            List<int> pointsLanguage = new List<int>();
            for (int i = 0; i < counter; i++)
            {
                pointsKeywords.Add(KeywordsDetect.detect(code[i], keywords));
                pointsLanguage.Add(LanguageDetect.detect(code[i]));
            }

            NeuralNetwork network = Network.Functie();
            List<double> listResults = new List<double>();
            for (int i = 0; i < counter; i++)
            {
                //listResults.Add(Network.MakeExamplePredictions(network, pointsKeywords[i], pointsLanguage[i]));
                dict.Add(code[i], (int)Network.MakeExamplePredictions(network, pointsKeywords[i], pointsLanguage[i]));
            }

            var myList = dict.ToList();
            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            List<string> listaRez = new List<string>();
            if(myList.Count()>=4)
            {
                listaRez.Add(myList[1].Key);
                listaRez.Add(myList[2].Key);
                listaRez.Add(myList[3].Key);

            }
            

            //double maxValue = listResults.Max();
            //int maxIndex = listResults.IndexOf(maxValue);
            return listaRez;// code[maxIndex];
        }
    }
}
