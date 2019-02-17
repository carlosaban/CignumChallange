using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace SearchfightCore
{
    public interface ISearch
    {
         void Initialitation(Hashtable parameters);
         //IList<Uri> ListResult();
         string SearchTitle();
         long ExecuteSearch(string word);




    }
}
