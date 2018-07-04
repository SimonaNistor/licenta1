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
        NEW = 1, DEFINE = 1, CLASS = 1, VOID = 1, ELSE = 1, IF = 1, WHILE = 1, LIST = -1, ARRAYLIST = -1
    }



    public static class KeywordsDetect
    {

        public static void initialize(Dictionary<string, int> constructor,
                                       Dictionary<string, int> constructorCu,
                                       Dictionary<string, int> constructorFara,
                                       Dictionary<string, int> constructorCopiere,
                                       Dictionary<string, int> destructor,
                                       Dictionary<string, int> mostenire,
                                       Dictionary<string, int> operatorExcl,
                                       Dictionary<string, int> operatorEgal,
                                       Dictionary<string, int> operatorEgalEgal,
                                        Dictionary<string, int> operatorPlus,
                                        Dictionary<string, int> operatorPlusPlus,
                                        Dictionary<string, int> operatorMinus,
                                        Dictionary<string, int> operatorMinusMinus,
                                        Dictionary<string, int> operatorInmultire,
                                        Dictionary<string, int> operatorImpartire,
                                        Dictionary<string, int> operatorCast,
                                        Dictionary<string, int> operatorIndex,
                                        Dictionary<string, int> operatorMare,
                                        Dictionary<string, int> operatorMareEgal,
                                        Dictionary<string, int> operatorMic,
                                        Dictionary<string, int> operatorMicEgal,
                                        Dictionary<string, int> operatorMicMic,
                                        Dictionary<string, int> operatorMareMare,
                                        Dictionary<string, int> getter,
                                       Dictionary<string, int> setter,
                                       Dictionary<string, int> stat
                                       )
        {
            constructor.Add("constructor", 50);
            constructor.Add("return", 2);
            constructor.Add("public", 1);
            constructor.Add("private", 1);
            constructor.Add("destructor", -10);
            constructor.Add("()", 1);
            constructorCu.Add("constructor fara parametri", 50);
            constructorCu.Add("()", 20);
            constructorFara.Add("constructor cu parametri", 50);
            constructorCopiere.Add("constructor de copiere", 50);
            constructorCopiere.Add("&", 1);
            constructorCopiere.Add("const", 3);
            ////////////////////////////////////////////////
            destructor.Add("destructor", 50);
            destructor.Add("~", 2);
            destructor.Add("constructor", -10);
            destructor.Add("public", 1);
            destructor.Add("private", 1);
            destructor.Add("()", 2);
            ///////////////////////////////////////////////
            mostenire.Add("Derived", 50);
            mostenire.Add(": public", 50);
            //////////////////////////////////////////////
            operatorExcl.Add("operator!", 50);
            /////////////////////////////////////////////
            operatorEgal.Add("operator=", 50);
            operatorEgal.Add("supraincarcare", 10);
            /////////////////////////////////////////////
            operatorEgalEgal.Add("operator==", 50);
            /////////////////////////////////////////////
            operatorPlus.Add("operator+", 50);
            /////////////////////////////////////////////
            operatorPlusPlus.Add("operator++", 50);
            /////////////////////////////////////////////
            operatorMinus.Add("operator-", 50);
            /////////////////////////////////////////////
            operatorMinusMinus.Add("operator--", 50);
            /////////////////////////////////////////////
            operatorInmultire.Add("operator*", 50);
            /////////////////////////////////////////////
            operatorImpartire.Add("operator/", 50);
            /////////////////////////////////////////////
            operatorCast.Add("cast", 50);
            /////////////////////////////////////////////
            operatorIndex.Add("operator[]", 50);
            /////////////////////////////////////////////
            operatorMare.Add("operator>", 50);
            /////////////////////////////////////////////
            operatorMareEgal.Add("operator>=", 50);
            /////////////////////////////////////////////
            operatorMic.Add("operator<", 50);
            /////////////////////////////////////////////
            operatorMicEgal.Add("operator<=", 50);
            /////////////////////////////////////////////
            operatorMicMic.Add("operator<<", 50);
            /////////////////////////////////////////////
            operatorMareMare.Add("operator>>", 50);
            //////////////////////////////////////////////
            getter.Add("getter", 50);
            //////////////////////////////////////////////
            setter.Add("setter", 50);
            //////////////////////////////////////////////
            stat.Add("autoincrementare camp static", 40);
            stat.Add("static", 50);
        }
        public static int detect(string snippet, string keywords)
        {
            List<Dictionary<string, int>> keywordsList = new List<Dictionary<string, int>>();
            Dictionary<string, int> constructor = new Dictionary<string, int>();
            Dictionary<string, int> constructorCu = new Dictionary<string, int>();
            Dictionary<string, int> constructorFara = new Dictionary<string, int>();
            Dictionary<string, int> constructorCopiere = new Dictionary<string, int>();
            Dictionary<string, int> destructor = new Dictionary<string, int>();
            Dictionary<string, int> mostenire = new Dictionary<string, int>();
            Dictionary<string, int> operatorExcl = new Dictionary<string, int>();//operator!
            Dictionary<string, int> operatorEgal = new Dictionary<string, int>();//operator=
            Dictionary<string, int> operatorEgalEgal = new Dictionary<string, int>();//operator==
            Dictionary<string, int> operatorPlus = new Dictionary<string, int>();//operator+
            Dictionary<string, int> operatorPlusPlus = new Dictionary<string, int>();//operator++
            Dictionary<string, int> operatorMinus = new Dictionary<string, int>();//operator-
            Dictionary<string, int> operatorMinusMinus = new Dictionary<string, int>();//operator--
            Dictionary<string, int> operatorInmultire = new Dictionary<string, int>();//operator*
            Dictionary<string, int> operatorImpartire = new Dictionary<string, int>();//operator/
            Dictionary<string, int> operatorCast = new Dictionary<string, int>();//operator cast
            Dictionary<string, int> operatorIndex = new Dictionary<string, int>();//operator[]
            Dictionary<string, int> operatorMare = new Dictionary<string, int>();//operator>
            Dictionary<string, int> operatorMareEgal = new Dictionary<string, int>();//operator>=
            Dictionary<string, int> operatorMic = new Dictionary<string, int>();//operator<
            Dictionary<string, int> operatorMicEgal = new Dictionary<string, int>();//operator<=
            Dictionary<string, int> operatorMicMic = new Dictionary<string, int>();//operator>>
            Dictionary<string, int> operatorMareMare = new Dictionary<string, int>();//operator<<
            Dictionary<string, int> getter = new Dictionary<string, int>();
            Dictionary<string, int> setter = new Dictionary<string, int>();
            Dictionary<string, int> stat = new Dictionary<string, int>();//static
            initialize(constructor, constructorCu, constructorFara, constructorCopiere, destructor,
                mostenire, operatorExcl,
                operatorEgal, operatorEgalEgal, operatorPlus, operatorPlusPlus, operatorMinus, operatorMinusMinus, operatorInmultire,
                operatorImpartire, operatorCast, operatorIndex, operatorMare, operatorMareEgal, operatorMic,
                operatorMicEgal, operatorMicMic, operatorMareMare, getter, setter, stat);
            keywordsList.Add(constructor);
            keywordsList.Add(constructorCu);
            keywordsList.Add(constructorFara);
            keywordsList.Add(constructorCopiere);
            keywordsList.Add(destructor);
            keywordsList.Add(mostenire);
            keywordsList.Add(operatorExcl);
            keywordsList.Add(operatorEgal);
            keywordsList.Add(operatorEgalEgal);
            keywordsList.Add(operatorPlus);
            keywordsList.Add(operatorPlusPlus);
            keywordsList.Add(operatorMinus);
            keywordsList.Add(operatorMinusMinus);
            keywordsList.Add(operatorInmultire);
            keywordsList.Add(operatorImpartire);
            keywordsList.Add(operatorCast);
            keywordsList.Add(operatorIndex);
            keywordsList.Add(operatorMare);
            keywordsList.Add(operatorMareEgal);
            keywordsList.Add(operatorMic);
            keywordsList.Add(operatorMicEgal);
            keywordsList.Add(operatorMicMic);
            keywordsList.Add(operatorMareMare);
            keywordsList.Add(getter);
            keywordsList.Add(setter);
            keywordsList.Add(stat);

            List<string> liniiCod = new List<string>();
            string[] liniiDespartite = snippet.Split('\n');
            foreach (string s in liniiDespartite)
            {
                if (s != "" && s != " ")
                {
                    liniiCod.Add(s);
                }
            }

            //string[] code = snippet.Split(delimiters);
            //List<string> lista = new List<string>();
            //foreach(string s in code)
            //{
            //    if(s!="")
            //    {
            //        lista.Add(s);
            //    }
            //}

            string[] keys = keywords.Split('@');
            List<string> keysList = new List<string>();
            foreach (string k in keys)
            {
                keysList.Add(k);
            }
            int points = 0;
            foreach (var dictionary in keywordsList)
            {
                foreach (string k in keysList)
                {
                    if (dictionary.FirstOrDefault(x => x.Value == 50).Key.ToString() == k)
                    {
                        for (int i = 0; i < liniiCod.Count(); i++)
                        {
                            points = points + verifyOne(liniiCod[i], dictionary);
                        }
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
                    if (x.Contains(key))
                    {
                        points += value;
                    }
                    //if (x == key)
                    //{
                    //    points += value;
                    //}
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
                if (x.Contains(key))
                {
                    points += value;
                }
                //if (x == key)
                //{
                //    points += value;
                //}
            }
            return points;
        }
    }
}
