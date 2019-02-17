using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Reflection;
using System.Configuration;

namespace SearchfightCore
{
    /// <summary>
    /// Sigleton design pattern
    /// </summary>
    public class SearchManager
    {
        private List<ISearch> _Searchers = new List<ISearch>();
        private static SearchManager _instance;

        public static SearchManager Instance { 
            get { 
                if (_instance != null ) return _instance;

                _instance = new SearchManager();
                return _instance;
            }
        
        }
        private SearchManager()
        {
            try
            {
                var l_configSettings = (ConfigSearchSection)ConfigurationManager.GetSection("ConfigSearchSection");

                foreach (var l_element in l_configSettings.ConfigSearchs.AsEnumerable())
                {

                    string path = System.IO.Directory.GetCurrentDirectory();


                    //Assembly oassembly = Assembly.Load(l_element.assembly);
                    //Type ot = oassembly.GetType(l_element.assemblyClass);
                    //ISearch oservice = (ISearch)Activator.CreateInstance(ot);

                    //Assembly oassembly = Assembly.Load(l_element.assembly);
                    //Type ot = oassembly.GetType(l_element.assemblyClass);
                    ISearch oservice = this.CreateInstance(l_element.assembly); //(ISearch)Activator.CreateInstance(ot);

                    Hashtable parametros = new Hashtable();
                    foreach (var l_subElement in l_element.ConfigSearchParameters.AsEnumerable())
                    {
                        parametros.Add(l_subElement.Key, l_subElement.Value);
                    }
                    oservice.Initialitation(parametros);

                    this._Searchers.Add(oservice);
                }
                
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }
        public List<ISearch> Searchers
        {
            get { return _Searchers; }
        }
        
        public string BenchmarkReport(string[] inputs)
        {
            StringBuilder result = new StringBuilder();

            string SearchTotalWinner= string.Empty;
            string SearchWinner= string.Empty;
            long partialResult;
            long winner = 0;
            long TotalWinner = 0;

            Hashtable HTInputs = new Hashtable();
            Hashtable HTSearchResult = new Hashtable();

            foreach (var itemInput in inputs)
            {

                result.AppendFormat("{0}: ", itemInput);

                
                foreach (var item in Searchers)
                {
                    partialResult = item.ExecuteSearch(itemInput);
                    if (!HTInputs.ContainsKey(item.SearchTitle()))
                    {
                        HTInputs.Add(item.SearchTitle(), itemInput);
                        HTSearchResult.Add(item.SearchTitle(), partialResult);
                        
                    }
                    else
                    {
                        winner = long.Parse( HTSearchResult[item.SearchTitle()].ToString());

                        HTInputs[item.SearchTitle()] = (winner > partialResult) ? HTInputs[item.SearchTitle()].ToString(): itemInput;
                        HTSearchResult[item.SearchTitle()]  = (winner > partialResult) ? winner : partialResult;                    }
                    
                        TotalWinner = (TotalWinner < partialResult) ? partialResult : TotalWinner;
                        SearchTotalWinner = (TotalWinner <= partialResult) ? itemInput : SearchTotalWinner;
                        result.AppendFormat("{0}:  {1} ", item.SearchTitle(), partialResult);

                }
                result.AppendLine();
            }
            foreach (DictionaryEntry SearchWinnersItems in HTInputs)
            {
                result.AppendFormat("{0} Winner: {1}", SearchWinnersItems.Key, SearchWinnersItems.Value);
                result.AppendLine();
            }

            result.AppendFormat("Total Winner: {0}", SearchTotalWinner);
            result.AppendLine();



            return result.ToString();
        }
        private ISearch CreateInstance(string className)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            var DLL = Assembly.LoadFile(path +"\\"+ className +  ".dll");


            ISearch IsearchImplementation;
            foreach (Type type in DLL.GetExportedTypes())
            {
                if (type.Name == className)
                {
                    IsearchImplementation = (ISearch)Activator.CreateInstance(type);
                    return IsearchImplementation;

                }

            }
            return null;

        }
    }
    
}
