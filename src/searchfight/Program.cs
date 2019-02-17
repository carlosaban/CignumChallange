using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using SearchfightCore;
using System.Configuration;
namespace Searchfight
{
    class Program
    {
        static void Main(string[] args)
        {
            


            System.Console.WriteLine(SearchfightCore.SearchManager.Instance.BenchmarkReport(args));
            System.Console.WriteLine(".....Press Enter to continue.......");
            System.Console.ReadLine();
            
        }

    }
}

