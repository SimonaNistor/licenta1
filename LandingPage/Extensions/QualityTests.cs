using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LandingPage.Extensions
{
    enum posibilitati
    {
        CLASS
    }

    public class QualityTests
    {
        public static int detect(string snippet, string keywords)
        {
            List<Dictionary<string, int>> keywordsList = new List<Dictionary<string, int>>();
            char[] delimiters = { '\n' };//, ' ', ';', '(', ')', ':', '\'', '\"' };
            string[] code = snippet.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<string> lista = new List<string>();
            List<string> param = new List<string>();
            foreach (string s in code)
            {
                if (s != "")
                {
                    lista.Add(s);
                }
            }
            List<string> numeClase = new List<string>();
            string[] keys = keywords.Split('@');
            List<string> listaPosibilitati = new List<string>();
            listaPosibilitati.Add("class");

            bool _class = false;
            List<string> result = new List<string>();
            string className = "";

            for(int i=0; i<lista.Count();i++)
            {
                if (lista[i].StartsWith("/*") && lista[i].EndsWith("*/"))
                {
                    lista[i] = "";
                }
                else
                {
                    if (lista[i].Contains("/*"))
                    {
                        if (lista[i].Contains("*/"))
                        {
                            int offset1 = lista[i].IndexOf("/*");
                            int offset2 = lista[i].IndexOf("*/");
                            if (offset1 >= 0 && offset2 >= 0)
                            {
                                lista[i] = lista[i].Substring(offset1, offset2);
                            }
                        }
                        int offset = lista[i].IndexOf("/*");
                        if (offset >= 0)
                        {
                            lista[i] = lista[i].Substring(0, offset);
                        }
                        int j = i;
                        while (!lista[j].Contains("*/"))
                        {
                            if (lista[i].Contains("*/"))
                            {
                                offset = lista[i].IndexOf("/*");
                                if (offset >= 0)
                                {
                                    lista[i] = lista[i].Substring(0, offset);
                                }
                            }
                            else
                            {
                                if (lista[i].EndsWith("*/"))
                                {
                                    lista[i] = "";
                                }
                            }
                            j++;
                        }
                    }
                }
            }

            for (int i = 0; i < lista.Count(); i++)
            {
                if (!lista[i].StartsWith("//"))
                {
                    if (lista[i].Contains("//"))
                    {
                        int offset = lista[i].IndexOf("//");
                        if (offset >= 0)
                        {
                            lista[i] = lista[i].Substring(0, offset);
                        }

                    }
                    //List<string> p = new List<string>();
                    //string[] c = lista[i].Split(' ');
                    //foreach (string v in c)
                    //{
                    //    p.Add(v);
                    //}

                    //if (lista[i] == "class" && lista[i - 1] != "//" && lista[i - 1] != "/*")
                    //{
                    //    className = lista[i + 1];
                    //    while (lista[i] != "public")
                    //    {
                    //        result.Add(lista[i]);
                    //    }
                    //    result.Add("};");
                    //}
                }
                else
                {
                    lista[i] = "";
                }
            }

            for (int i = 0; i < lista.Count(); i++)
            {
                if(lista[i]=="")
                {
                    lista.RemoveAt(i);
                }
                if(lista[i].Contains("\t"))
                {
                    int j = i;
                    while(lista[i].Contains("\t"))
                    {
                        lista[i]=lista[i].Replace("\t", "");
                    }
                }
            }
            //get nume clase
            
            for (int i = 0; i < lista.Count(); i++)
            {
                if (lista[i].Contains("class"))
                {
                    char[] delimitators = { ' ', ':' };
                    string[] ceva = lista[i].Split(delimitators);
                    List<string> listaCeva = new List<string>();
                    foreach (string s in ceva)
                    {
                        listaCeva.Add(s);
                    }
                    for (int increment = 0; increment < listaCeva.Count(); increment++)
                    {
                        if (listaCeva[increment] == "class")
                        {
                            if (listaCeva[increment + 1] != "")
                            {
                                numeClase.Add(listaCeva[increment + 1]);
                            }
                        }
                    }

                }
                if (lista[i].Contains("private:"))
                {
                    List<string> listaCeva = new List<string>();
                    int j = i;
                    while (!lista[i].Contains("public:") && !lista[i].Contains("protected:"))
                    {
                        i++;
                        if(lista[i]!="" && lista[i]!="public:" && lista[i]!="private:")
                        {
                            listaCeva.Add(lista[i]);
                        }
                    }
                    i = j;
                    foreach(string s in listaCeva)
                    {
                        string[] ceva = s.Split(" ");
                        StringBuilder p = new StringBuilder();
                        for (int x = 0; x < ceva.Length - 1; x++)
                        {
                            p.Append(ceva[x] + " ");
                        }
                        param.Add(p.ToString());
                    }
                    
                }
            }
            
            foreach (string k in keys)
            {
                switch(k)
                {
                    case "class":
                        {
                            //for (int i = 0; i < lista.Count(); i++)
                            //{
                            //    if (lista[i].Contains("class"))
                            //    {
                            //        string[] ceva = lista[i].Split(" ");
                            //        List<string> listaCeva = new List<string>();
                            //        foreach (string s in ceva)
                            //        {
                            //            listaCeva.Add(s);
                            //        }
                            //        for (int increment = 0; increment < listaCeva.Count(); increment++)
                            //        {
                            //            if (listaCeva[increment] == "class")
                            //            {
                            //                numeClase.Add(listaCeva[increment + 1]);
                            //            }
                            //        }
                            //        int j = i;
                            //        while (!lista[j].Contains("};"))
                            //        {
                            //            result.Add(lista[j]);
                            //            j++;
                            //        }
                            //        result.Add("};");

                            //    }
                            //}
                        }
                        break;
                    case "destructor":
                        {
                            //for (int i = 0; i < lista.Count(); i++)
                            //{
                            //    string destructorPattern = @"^~[A-Za-z0-9]()";
                            //    if (Regex.IsMatch(lista[i], destructorPattern))
                            //    {
                            //        int j = i;
                            //        while (!lista[j].Contains("}"))
                            //        {
                            //            result.Add(lista[j]);
                            //            j++;
                            //        }
                            //        result.Add("}");

                            //    }
                            //}
                        }
                        break;
                    case "constructor":
                        {
                            //for (int i = 0; i < lista.Count(); i++)
                            //{
                            //    string destructorPattern = @"^[A-Za-z0-9]([A-Za-z0-9,]*)|[A-Za-z0-9]([A-Za-z0-9,]*):[A-Za-z0-9]([A-Za-z0-9])";
                            //    if (Regex.IsMatch(lista[i], destructorPattern))
                            //    {
                            //        int j = i;
                            //        while (!lista[j].Contains("}"))
                            //        {
                            //            result.Add(lista[j]);
                            //            j++;
                            //        }
                            //        result.Add("}");

                            //    }
                            //}
                        }
                        break;
                    case "getter":
                        //for (int i = 0; i < lista.Count(); i++)
                        //{
                        //    if(lista[i].Contains("private:"))
                        //    {
                        //        int j = i;
                        //        while (!lista[j].Contains("public:")||!lista[j].Contains("protected:"))
                        //        {
                        //            result.Add(lista[j]);
                        //            j++;
                        //        }
                        //    }
                        //}
                        break;
                    case "constructor cu parametri":
                        //constructor cu parametri
                        //for (int i = 0; i < lista.Count(); i++)
                        //{
                        //    foreach (string n in numeClase)
                        //    {
                        //        if (lista[i].Contains(n) && lista[i].Contains("(") && lista[i].Contains(")") && !lista[i].Contains(":")
                        //            && !lista[i].Contains("operator=")
                        //            && !lista[i].Contains("operator+")
                        //            && !lista[i].Contains("operator-")
                        //            && !lista[i].Contains("operator*")
                        //            && !lista[i].Contains("operator/")
                        //            && !lista[i].Contains("operator%")
                        //            && !lista[i].Contains("operator^")
                        //            && !lista[i].Contains("operator&")
                        //            && !lista[i].Contains("operator|")
                        //            && !lista[i].Contains("operator~!")
                        //            && !lista[i].Contains("operator,")
                        //            && !lista[i].Contains("operator<")
                        //            && !lista[i].Contains("operator>")
                        //            && !lista[i].Contains("operator<=")
                        //            && !lista[i].Contains("operator>=")
                        //            && !lista[i].Contains("operator++")
                        //            && !lista[i].Contains("operator--")
                        //            && !lista[i].Contains("operator<<")
                        //            && !lista[i].Contains("operator>>")
                        //            && !lista[i].Contains("operator&=")
                        //            && !lista[i].Contains("operator!=")
                        //            && !lista[i].Contains("operator&&")
                        //            && !lista[i].Contains("operator||")
                        //            && !lista[i].Contains("operator+=")
                        //            && !lista[i].Contains("operator-=")

                        //&& !lista[i].Contains("operator[]")
                        //&& !lista[i].Contains("operator()"))
                        //        {
                        //            foreach (string p in param)
                        //            {
                        //                if (lista[i].Contains(p))
                        //                {
                        //                    int j = i;
                        //                    while (!lista[j].Contains("return"))
                        //                    {
                        //                        result.Add(lista[j]);
                        //                        j++;
                        //                    }
                        //                    result.Add(lista[j]);
                        //                    result.Add(lista[j + 1]);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        break;
                    case "constructor fara parametri":
                        //constructor fara parametri
                        //for (int i = 0; i < lista.Count(); i++)
                        //{
                        //    foreach (string n in numeClase)
                        //    {
                        //        if (lista[i].Contains(n) && lista[i].Contains("()") && !lista[i].Contains(":")
                        //            && !lista[i].Contains("operator=")
                        //            && !lista[i].Contains("operator+")
                        //            && !lista[i].Contains("operator-")
                        //            && !lista[i].Contains("operator*")
                        //            && !lista[i].Contains("operator/")
                        //            && !lista[i].Contains("operator%")
                        //            && !lista[i].Contains("operator^")
                        //            && !lista[i].Contains("operator&")
                        //            && !lista[i].Contains("operator|")
                        //            && !lista[i].Contains("operator~!")
                        //            && !lista[i].Contains("operator,")
                        //            && !lista[i].Contains("operator<")
                        //            && !lista[i].Contains("operator>")
                        //            && !lista[i].Contains("operator<=")
                        //            && !lista[i].Contains("operator>=")
                        //            && !lista[i].Contains("operator++")
                        //            && !lista[i].Contains("operator--")
                        //            && !lista[i].Contains("operator<<")
                        //            && !lista[i].Contains("operator>>")
                        //            && !lista[i].Contains("operator&=")
                        //            && !lista[i].Contains("operator!=")
                        //            && !lista[i].Contains("operator&&")
                        //            && !lista[i].Contains("operator||")
                        //            && !lista[i].Contains("operator+=")
                        //            && !lista[i].Contains("operator-=")
                        //&& !lista[i].Contains("operator[]")
                        //&& !lista[i].Contains("operator()"))
                        //        {
                        //            foreach (string p in param)
                        //            {
                        //                if (lista[i].Contains(p))
                        //                {
                        //                    int j = i;
                        //                    while (!lista[j].Contains("return"))
                        //                    {
                        //                        result.Add(lista[j]);
                        //                        j++;
                        //                    }
                        //                    result.Add(lista[j]);
                        //                    result.Add(lista[j + 1]);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        break;
                    case "constructor cu incrementare":
                        //for (int i = 0; i < lista.Count(); i++)
                        //{
                        //    foreach (string n in numeClase)
                        //    {
                        //        if (lista[i].Contains(n) && lista[i].Contains("(") && lista[i].Contains(")") && lista[i].Contains(":")
                        //            && !lista[i].Contains("operator=")
                        //            && !lista[i].Contains("operator+")
                        //            && !lista[i].Contains("operator-")
                        //            && !lista[i].Contains("operator*")
                        //            && !lista[i].Contains("operator/")
                        //            && !lista[i].Contains("operator%")
                        //            && !lista[i].Contains("operator^")
                        //            && !lista[i].Contains("operator&")
                        //            && !lista[i].Contains("operator|")
                        //            && !lista[i].Contains("operator~!")
                        //            && !lista[i].Contains("operator,")
                        //            && !lista[i].Contains("operator<")
                        //            && !lista[i].Contains("operator>")
                        //            && !lista[i].Contains("operator<=")
                        //            && !lista[i].Contains("operator>=")
                        //            && !lista[i].Contains("operator++")
                        //            && !lista[i].Contains("operator--")
                        //            && !lista[i].Contains("operator<<")
                        //            && !lista[i].Contains("operator>>")
                        //            && !lista[i].Contains("operator&=")
                        //            && !lista[i].Contains("operator!=")
                        //            && !lista[i].Contains("operator&&")
                        //            && !lista[i].Contains("operator||")
                        //            && !lista[i].Contains("operator+=")
                        //            && !lista[i].Contains("operator-=")
                        //&& !lista[i].Contains("operator[]")
                        //&& !lista[i].Contains("operator()"))
                        //        {
                        //            foreach (string p in param)
                        //            {
                        //                if (lista[i].Contains(p))
                        //                {
                        //                    int j = i;
                        //                    while (!lista[j].Contains("return"))
                        //                    {
                        //                        result.Add(lista[j]);
                        //                        j++;
                        //                    }
                        //                    result.Add(lista[j]);
                        //                    result.Add(lista[j + 1]);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        break;
                    case "operator=":
                        //for (int i = 0; i < lista.Count(); i++)
                        //{
                        //    foreach (string n in numeClase)
                        //    {
                        //        if (lista[i].Contains(n) && lista[i].Contains("()") && !lista[i].Contains(":")
                        //            && lista[i].Contains("operator="))
                        //        {
                        //            foreach (string p in param)
                        //            {
                        //                if (lista[i].Contains(p))
                        //                {
                        //                    int j = i;
                        //                    while (!lista[j].Contains("return"))
                        //                    {
                        //                        result.Add(lista[j]);
                        //                        j++;
                        //                    }
                        //                    result.Add(lista[j]);
                        //                    result.Add(lista[j + 1]);
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        break;
                }
            }


            for (int i = 0; i < lista.Count(); i++)
            {
                foreach (string n in numeClase)
                {
                    if (lista[i].Contains(n) && lista[i].Contains("(") && lista[i].Contains(")") && lista[i].Contains(":")
                        && !lista[i].Contains("operator=")
                        && !lista[i].Contains("operator+")
                        && !lista[i].Contains("operator-")
                        && !lista[i].Contains("operator*")
                        && !lista[i].Contains("operator/")
                        && !lista[i].Contains("operator%")
                        && !lista[i].Contains("operator^")
                        && !lista[i].Contains("operator&")
                        && !lista[i].Contains("operator|")
                        && !lista[i].Contains("operator~!")
                        && !lista[i].Contains("operator,")
                        && !lista[i].Contains("operator<")
                        && !lista[i].Contains("operator>")
                        && !lista[i].Contains("operator<=")
                        && !lista[i].Contains("operator>=")
                        && !lista[i].Contains("operator++")
                        && !lista[i].Contains("operator--")
                        && !lista[i].Contains("operator<<")
                        && !lista[i].Contains("operator>>")
                        && !lista[i].Contains("operator&=")
                        && !lista[i].Contains("operator!=")
                        && !lista[i].Contains("operator&&")
                        && !lista[i].Contains("operator||")
                        && !lista[i].Contains("operator+=")
                        && !lista[i].Contains("operator-=")
                        && !lista[i].Contains("operator[]")
                        && !lista[i].Contains("operator()"))
                    {
                        foreach (string p in param)
                        {
                            if (lista[i].Contains(p))
                            {
                                int j = i;
                                while (!lista[j].Contains("return"))
                                {
                                    result.Add(lista[j]);
                                    j++;
                                }
                                //result.Add(lista[j]);
                                //result.Add("}");
                            }
                        }
                    }
                }
            }


            //for (int i = 0; i < lista.Count(); i++)
            //{
            //    if (lista[i].Contains("private:"))
            //    {
            //        int j = i;
            //        while (!lista[j].Contains("public:") || !lista[j].Contains("protected:"))
            //        {
            //            result.Add(lista[j]);
            //            j++;
            //        }
            //    }
            //}

            return 1;
        }

        //public static string selectQuality(List<string> code, string keywords)
        //{
        //    int counter = 0;
        //    foreach (string s in code)
        //    {
        //        counter++;
        //    }
        //}
    }
}
