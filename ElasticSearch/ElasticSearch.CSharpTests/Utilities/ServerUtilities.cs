using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.CSharpTests.Utilities
{
    public static class ServerUtilities
    {
        /// <summary>
        /// Tests whether an elasticserver is available at the url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<bool> IsElasticServerAvailable(String url="http://localhost:9200/_cluster/health")
        {
            if (String.IsNullOrEmpty(url))
            {
                return false;
            } else
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(url);

                        return (response.StatusCode == System.Net.HttpStatusCode.OK);
                    }
                    
                } catch
                {
                    return false;
                }
            }
        }
    }
}
