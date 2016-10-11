using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ElasticSearch.CSharpTests.Models
{
    [JsonObject(MemberSerialization =MemberSerialization.OptIn)]
    public class TestModel
    {
        [JsonProperty(PropertyName ="firstname")]
        public String FirstName { get; set; }
        [JsonProperty(PropertyName = "lastname")]
        public String LastName { get; set; }

        public TestModel()
        {
            FirstName = String.Empty;
            LastName = String.Empty;
        }
    }
}
