using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Contexts
{
    
    public class ElasticSearchContext
    {
        public String Url { get; private set; }
        private Uri parsedUri;

        public ElasticSearchContext(String url)
        {
            Url = url;
            parsedUri = new Uri(url);
        }




    }
}
