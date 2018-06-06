using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingPage.Extensions
{
    public class LanguageDetect
    {
        public static void initialize(Dictionary<string, int> C,
                                       Dictionary<string, int> CPlusPlus, 
                                       Dictionary<string, int> Javascript, 
                                       Dictionary<string, int> Python,
                                       Dictionary<string, int> Java, 
                                       Dictionary<string, int> HTML,
                                       Dictionary<string, int> CSS)
        {
            //primitive variable declaration.
            C.Add("char", 2);
            C.Add("long", 2);
            C.Add("int", 2);
            C.Add("float", 2);
            C.Add("double", 2);
            //malloc function call
            C.Add("malloc", 2);
            //#include <something>
            C.Add("#include", 2);
            //pointer
            C.Add("**", 2);
            C.Add("*()", 2);
            // Array declaration.
            C.Add("[]", 1);
            // #define macro
            C.Add("#define", 1);
            // NULL constant
            C.Add("NULL", 1);
            // void keyword
            C.Add("void", 1);
            // (else )if statement
            C.Add("if", 1);
            C.Add("else", 1);
            // while loop
            C.Add("while", 1);
            // printf function
            C.Add("printf", 1);
            // new Keyword from C++
            C.Add("new", -1);
            // JS variable declaration
            C.Add("var", -1);
            ///////////////////////////////////////////////////////
            // Primitive variable declaration.
            CPlusPlus.Add("char", 2);
            CPlusPlus.Add("long", 2);
            CPlusPlus.Add("int", 2);
            CPlusPlus.Add("float", 2);
            CPlusPlus.Add("double", 2);
            //// #include <whatever.h>
            CPlusPlus.Add("#include", 2);
            //// using namespace something
            CPlusPlus.Add("using", 2);
            CPlusPlus.Add("namespace", 2);
            //// template declaration
            CPlusPlus.Add("template", 2);
            //// std
            CPlusPlus.Add("std", 2);
            //// cout/cin/endl
            CPlusPlus.Add("cin", 2);
            CPlusPlus.Add("cout", 2);
            CPlusPlus.Add("endl", 2);
            //// Visibility specifiers
            CPlusPlus.Add("private", 2);
            CPlusPlus.Add("public", 2);
            CPlusPlus.Add("protected", 2);
            //// nullptr
            CPlusPlus.Add("nullptr", 2);
            //// new Keyword
            CPlusPlus.Add("new", 1);
            //// #define macro
            CPlusPlus.Add("#define", 1);
            //// class keyword
            CPlusPlus.Add("class", 1);
            //// void keyword
            CPlusPlus.Add("void", 1);
            //// (else )if statement
            CPlusPlus.Add("if", 1);
            CPlusPlus.Add("else", 1);
            //// while loop
            CPlusPlus.Add("while", 1);
            //// Scope operator
            ///////////////////////////////////nu stiu daca sa pun CPlusPlus.Add("::", 1);
            //// Java List/ArrayList
            CPlusPlus.Add("ArrayList<>()", -1);
            ///////////////////////////////////////////////////////
            Javascript.Add("undefined", 2);
            ///////////////////////////////////////////////////////
            Python.Add("def", 2);
            ///////////////////////////////////////////////////////
            Java.Add("System", 2);
            ///////////////////////////////////////////////////////
            HTML.Add("!DOCTYPE", 2);
            ///////////////////////////////////////////////////////
            CSS.Add("style", -50);
        }
        public static int detect(string snippet, string keyword)
        {
            List<Dictionary<string, int>> keywordsList = new List<Dictionary<string, int>>();
            Dictionary<string, int> C = new Dictionary<string, int>();
            Dictionary<string, int> CPlusPlus = new Dictionary<string, int>();
            Dictionary<string, int> Javascript = new Dictionary<string, int>();
            Dictionary< string, int> Python = new Dictionary<string, int>();
            Dictionary<string, int> Java = new Dictionary<string, int>();
            Dictionary< string, int> HTML = new Dictionary<string, int>();
            Dictionary<string, int> CSS = new Dictionary<string, int>();
            initialize(C, CPlusPlus, Javascript, Python, Java, HTML, CSS);
            keywordsList.Add(C);
            keywordsList.Add(CPlusPlus);
            //
            char[] delimiters = { '\n', ' ', ';', '(', ')', '.', '<', '>', '/' };
            string[] code = snippet.Split(delimiters);
            List<string> lista = new List<string>();
            foreach (string s in code)
            {
                if (s != "")
                {
                    lista.Add(s);
                }
            }

            int points = 0;
            foreach (var dictionary in keywordsList)
            {
                if (dictionary.FirstOrDefault(x => x.Value == 50).Key.ToString() == keyword)
                {
                    for (int i = 0; i < lista.Count(); i++)
                    {
                        points = points + verifyOne(lista[i], dictionary);
                    }
                }
            }

            return points;
        }

        public static int verifyAll(string x, List<Dictionary<string, int>> list)
        {
            int points = 0;
            foreach (var keyword in list)
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
