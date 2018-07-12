using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LandingPage.Extensions
{
    public class QualityTests
    {
        public static string detect(string snippet, string general, string specific)
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
            List<string> listaPosibilitati = new List<string>();
            listaPosibilitati.Add("class");
            
            List<string> result = new List<string>();

            for (int i = 0; i < lista.Count(); i++)
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
                }
                else
                {
                    lista[i] = "";
                }
            }

            for (int i = 0; i < lista.Count(); i++)
            {
                if (lista[i].Contains("\t"))
                {
                    int j = i;
                    while (lista[i].Contains("\t"))
                    {
                        lista[i] = lista[i].Replace("\t", "");
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
                        if (lista[i] != "" && lista[i] != "public:" && lista[i] != "private:")
                        {
                            listaCeva.Add(lista[i]);
                        }
                    }
                    i = j;
                    foreach (string s in listaCeva)
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

            switch (general)
            {
                case "clasa":
                    switch (specific)
                    {
                        case "arata tot":
                            {
                                for (int i = 0; i < lista.Count(); i++)
                                {
                                    if (lista[i].Contains("class"))
                                    {
                                        string[] ceva = lista[i].Split(" ");
                                        List<string> listaCeva = new List<string>();
                                        foreach (string s in ceva)
                                        {
                                            listaCeva.Add(s);
                                        }
                                        for (int increment = 0; increment < listaCeva.Count(); increment++)
                                        {
                                            if (listaCeva[increment] == "class")
                                            {
                                                numeClase.Add(listaCeva[increment + 1]);
                                            }
                                        }
                                        int j = i;
                                        while (!lista[j].Contains("};"))
                                        {
                                            result.Add(lista[j]);
                                            j++;
                                        }
                                        result.Add("};");

                                    }
                                }
                            }
                            break;
                        case "destructor":
                            for (int i = 0; i < lista.Count(); i++)
                            {

                                if (lista[i].Contains("~") && !lista[i].Contains("void") && lista[i].Contains("()"))
                                {
                                    {
                                        int j = i;
                                        while (!(lista[j].Contains("delete[]") && lista[j + 1].Contains("}")) && !(lista[j].Contains("throw") && lista[j + 1].Contains("}")))
                                        {
                                            result.Add(lista[j]);
                                            j++;
                                        }
                                        result.Add(lista[j]);
                                        result.Add(lista[j + 1]);
                                    }

                                }
                            }
                            result.Add("");
                            break;
                        case "constructor cu parametri":
                            {
                                //constructor cu parametri
                                foreach (string n in numeClase)
                                {
                                    for (int i = 0; i < lista.Count(); i++)
                                    {
                                        if (lista[i].StartsWith(n) && lista[i].Contains("(") && lista[i].Contains(")") && !lista[i].Contains("void") && !lista[i].Contains("()")
                                            && !lista[i].Contains("~")
                                            && !lista[i].Contains("operator=")
                                            && !lista[i].Contains("operator+")
                                            && !lista[i].Contains("operator-")
                                            && !lista[i].Contains("operator*")
                                            && !lista[i].Contains("operator/")
                                            && !lista[i].Contains("operator%")
                                            && !lista[i].Contains("operator^")
                                            && !lista[i].Contains("operator&")
                                            && !lista[i].Contains("operator|")
                                            && !lista[i].Contains("operator!")
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
                                            {
                                                int j = i;
                                                while (!lista[j].Contains("}"))//(lista[j].Contains("}") && lista[j + 1].Contains("}")))// || !(lista[j].Contains("}") && !lista[j + 1].Contains("}")))
                                                {
                                                    result.Add(lista[j]);
                                                    j++;
                                                }
                                                result.Add(lista[j]);
                                                //result.Add(lista[j + 1]);
                                                result.Add("");
                                            }
                                        }
                                        
                                    }
                                    
                                }
                            }
                            
                            break;
                        case "constructor fara parametri":
                            foreach (string n in numeClase)
                            {
                                for (int i = 0; i < lista.Count(); i++)
                                {
                                    if (lista[i].StartsWith(n) && lista[i].Contains("()") && !lista[i].Contains("void")
                                        && !lista[i].Contains("~")
                                        && !lista[i].Contains("operator=")
                                        && !lista[i].Contains("operator+")
                                        && !lista[i].Contains("operator-")
                                        && !lista[i].Contains("operator*")
                                        && !lista[i].Contains("operator/")
                                        && !lista[i].Contains("operator%")
                                        && !lista[i].Contains("operator^")
                                        && !lista[i].Contains("operator&")
                                        && !lista[i].Contains("operator|")
                                        && !lista[i].Contains("operator!")
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
                                        {
                                            int j = i;
                                            while (!lista[j].Contains("}"))//(lista[j].Contains("}") && lista[j + 1].Contains("}")))// || !(lista[j].Contains("}") && !lista[j + 1].Contains("}")))
                                            {
                                                result.Add(lista[j]);
                                                j++;
                                            }
                                            result.Add(lista[j]);
                                            //result.Add(lista[j + 1]);
                                            result.Add("");
                                        }
                                    }

                                }

                            }
                            break;
                    }
                    break;

                case "getter":
                    {
                        for (int i = 0; i < lista.Count(); i++)
                        {
                            if (lista[i].Contains("private:"))
                            {
                                int j = i;
                                while (!lista[j].Contains("public:") || !lista[j].Contains("protected:"))
                                {
                                    result.Add(lista[j]);
                                    j++;
                                }
                            }
                        }
                    }
                    result.Add("");
                    break;
                case "operator":
                    switch (specific)
                    {
                        case "==":
                            verificaOperator(lista, "operator==", "", result);
                            break;
                        case "[]":
                            verificaOperator(lista, "operator[]", "", result);
                            break;
                        case "=":
                            verificaOperator(lista, "operator=", "const", result);
                            break;
                        case "!":
                            verificaOperator(lista, "operator!", "", result);
                            break;
                        case "<":
                            verificaOperator(lista, "operator<", "bool", result);
                            break;
                        case "+":
                            verificaOperator(lista, "operator+", "", result);
                            break;
                        case "++":
                            verificaOperator(lista, "operator++", "", result);
                            break;
                        case "-":
                            verificaOperator(lista, "operator-", "", result);
                            break;
                        case "--":
                            verificaOperator(lista, "operator--", "", result);
                            break;
                        case "*":
                            verificaOperator(lista, "operator*", "", result);
                            break;
                        case "/":
                            verificaOperator(lista, "operator/", "", result);
                            break;
                        case ">":
                            verificaOperator(lista, "operator>", "", result);
                            break;
                        case ">>":
                            verificaOperator(lista, "operator>>", "", result);
                            break;
                        case ">=":
                            verificaOperator(lista, "operator>=", "", result);
                            break;
                        case "<<":
                            verificaOperator(lista, "operator<<", "", result);
                            break;
                        case "<=":
                            verificaOperator(lista, "operator<=", "", result);
                            break;
                    }

                    break;
            }


            var res = string.Join("\n", result);
            return res;
        }

        public static void verificaOperator(List<string> lista, string specific, string verifAditionala, List<string> result)
        {
            for (int i = 0; i < lista.Count(); i++)
            {

                if (lista[i].Contains(specific) && !lista[i].Contains("void") && lista[i].Contains(verifAditionala))
                {
                    int j = i;
                    while (!(lista[j].Contains("return") && lista[j + 1].Contains("}")) && !(lista[j].Contains("throw") && lista[j + 1].Contains("}")))
                    {
                        result.Add(lista[j]);
                        j++;
                    }
                    result.Add(lista[j]);
                    result.Add(lista[j + 1]);
                    result.Add("");
                }
            }
            
        }
    }
}
