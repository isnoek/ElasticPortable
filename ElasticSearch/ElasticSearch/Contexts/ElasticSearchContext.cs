using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Contexts
{
    
    class ElasticSearchContext
    {
        public String Url { get; private set; }

        public ElasticSearchContext(String url)
        {
            Url = url;
        }




    }
}
