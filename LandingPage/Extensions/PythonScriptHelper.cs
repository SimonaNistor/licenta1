using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using IronPython.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Scripting.Hosting;

namespace LandingPage.Extensions
{
    public class PythonScriptHelper
    {
        public static string PatchParameter(string parameter, int serviceid)
        {
            var engine = Python.CreateEngine(); // Extract Python language engine from their grasp
            var scope = engine.CreateScope(); // Introduce Python namespace (scope)
            var d = new Dictionary<string, object>
            {
                { "serviceid", serviceid},
                { "parameter", parameter}
            }; // Add some sample parameters. Notice that there is no need in specifically setting the object type, interpreter will do that part for us in the script properly with high probability

            scope.SetVariable("params", d); // This will be the name of the dictionary in python script, initialized with previously created .NET Dictionary
            ScriptSource source = engine.CreateScriptSourceFromFile("wwwroot/python/scrip1.py"); // Load the script
            object result = source.Execute(scope);
            parameter = scope.GetVariable<string>("something"); // To get the finally set variable 'parameter' from the python script
            return parameter;
        }


    }
}
