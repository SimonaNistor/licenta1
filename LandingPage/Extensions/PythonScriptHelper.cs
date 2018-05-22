using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
//using IronPython.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Scripting.Hosting;

namespace LandingPage.Extensions
{
    public class PythonScriptHelper
    {
        //public static string PatchParameter()
        //{
        //    string python = @"C:\Python27\python.exe";
        //    //var python = Process.Start(@"cmd.exe ", @"/c C:\Users\Simona\Desktop\licenta1\LandingPage\Extensions\scrip1.py");
        //    // python app to call 
        //    string myPythonApp = "scrip1.py";

        //    //// dummy parameters to send Python script 
        //    //int x = 2;
        //    //int y = 5;

        //    // Create new process start info 
        //    ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

        //    // make sure we can read the output from stdout 
        //    myProcessStartInfo.UseShellExecute = false;
        //    myProcessStartInfo.RedirectStandardOutput = true;

        //    // start python app with 3 arguments  
        //    // 1st arguments is pointer to itself,  
        //    // 2nd and 3rd are actual arguments we want to send 
        //    //myProcessStartInfo.Arguments = myPythonApp + " " + x + " " + y;

        //    Process myProcess = new Process();
        //    // assign start information to the process 
        //    myProcess.StartInfo = myProcessStartInfo;

        //    //Trace.WriteLine("Calling Python script with arguments "+ x + " and {1} " + y);
        //    // start the process 
        //    myProcess.Start();

        //    // Read the standard output of the app we called.  
        //    // in order to avoid deadlock we will read output first 
        //    // and then wait for process terminate: 
        //    StreamReader myStreamReader = myProcess.StandardOutput;
        //    //string myString = myStreamReader.ReadLine();

        //    /*if you need to read multiple lines, you might use: 
        //        string myString = myStreamReader.ReadToEnd() */

        //    // wait exit signal from the app we called and then close it. 
        //    //myProcess.WaitForExit();
        //    //myProcess.Close();

        //    // write the output we got from python app 
        //    //string s = "Value received from script: " + myString;
        //    return "ceva";
        //}

        //public static string Incercare()
        //{
        //    ScriptEngine engine = Python.CreateEngine();
        //    ScriptScope scope = engine.CreateScope();
        //    engine.ExecuteFile(@"C:\Users\Simona\Desktop\licenta1\LandingPage\Extensions\incercare.py", scope);//@"C:\path\hello.py", scope);
        //    var result = scope.GetVariable("a");
        //    string s = (string)result;
        //    return s;
        //}


    }
}
