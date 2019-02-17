using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchfightCore;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

using System.Net.Http.Headers;

using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MSNSearch
{
    public class MSNSearch :ISearch
    {


        private Hashtable _Parameters = new Hashtable();




        #region ISearch Members

        public void Initialitation(Hashtable parameters)
        {
            this._Parameters = parameters;
        }

        public string SearchTitle()
        {
            return "MSN Search";
        }

        public long ExecuteSearch(string input)
        {
            
            return Task.Run(()=>MakeRequest(input)).Result;

        }

        async Task<long> MakeRequest(string input)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", this._Parameters["Ocp-Apim-Subscription-Key"].ToString());

            // Request parameters
            queryString["q"] = System.Web.HttpUtility.UrlEncode(input);
            var uri = this._Parameters["MainUrl"].ToString() + queryString;

            string json = await client.GetStringAsync(uri);

            dynamic details = JObject.Parse(json);
            object valor = details.webPages.totalEstimatedMatches; //searchInformation.totalResults

            return long.Parse(valor.ToString());//list.Count;
        }

        #endregion

    }
}
