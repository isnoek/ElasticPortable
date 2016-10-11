using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Results
{
    public class SearchResult<TContainedType> where TContainedType:class,new()
    {
        public String Id { get; set; }
        public TContainedType Value { get; set; }

        public SearchResult()
        {
            Id = String.Empty;
            Value = default(TContainedType);
        }
    }
}
