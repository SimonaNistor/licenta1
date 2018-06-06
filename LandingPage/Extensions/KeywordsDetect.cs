using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Extensions
{
    enum CPlusPlus
    {//nu e case sensitive in C#
        CHAR = 2,
        LONG = 2,
        INT = 2,
        FLOAT = 2,
        DOUBLE = 2,
        INCLUDE = 2,
        USING = 2,
        TEMPLATE = 2,
        STD = 2,
        COUT = 2, CIN = 2, ENDL = 2,
        PUBLIC = 2, PROTECTED = 2, PRIVATE = 2, NULLPTR = 2,
        NEW = 1, DEFINE = 1, CLASS = 1, VOID = 1, ELSE = 1, IF = 1, WHILE = 1, LIST = -1, ARRAYLIST = -1    }

    

    public static class KeywordsDetect
    {
        
        public static void initialize(Dictionary<string,int> constructor,
                                       Dictionary<string, int> destructor)
        {
            constructor.Add("constructor", 50);
            constructor.Add("return", 2);
            constructor.Add("public", 1);
            constructor.Add("private", 1);
            constructor.Add("destructor", -10);
            constructor.Add("()", 1);
            ////////////////////////////////////////////////
            destructor.Add("destructor", 50);
            destructor.Add("~", 2);
            destructor.Add("constructor", -10);
            destructor.Add("public", 1);
            destructor.Add("private", 1);
            destructor.Add("()", 1);
        }
        public static int detect(string snippet,string keyword)
        {
            List<Dictionary<string, int>> keywordsList = new List<Dictionary<string, int>>();
            Dictionary<string, int> constructor = new Dictionary<string, int>();
            Dictionary<string, int> destructor = new Dictionary<string, int>();
            initialize(constructor, destructor);
            keywordsList.Add(constructor);
            keywordsList.Add(destructor);
            char[] delimiters = { '\n', ' ', ';','(',')',':'};
            string[] code = snippet.Split(delimiters);
            List<string> lista = new List<string>();
            foreach(string s in code)
            {
                if(s!="")
                {
                    lista.Add(s);
                }
            }

            int points = 0;
            foreach(var dictionary in keywordsList)
            {
                if(dictionary.FirstOrDefault(x => x.Value == 50).Key.ToString() == keyword)
                {
                    for (int i = 0; i < lista.Count(); i++)
                    {
                        points = points + verifyOne(lista[i], dictionary);
                    }
                }
            }
            
            return points;
        }

        public static int verifyAll(string x, List<Dictionary<string,int>> list)
        {
            int points = 0;
            foreach(var keyword in list)
            {
                foreach (var pair in keyword)
                {
                    string key = pair.Key;
                    int value = pair.Value;
                    if (x == key)
                    {
                        points += value;
                    }
                }
            }
            return points;
        }

        public static int verifyOne(string x, Dictionary<string, int> dictionary)
        {
            int points = 0;
            foreach (var pair in dictionary)
            {
                string key = pair.Key;
                int value = pair.Value;
                if (x == key)
                {
                    points += value;
                }
            }
            return points;
        }
    }
}
