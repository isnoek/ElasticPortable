using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Connections
{
    public class HttpConnection
    {
        private String serverUri;
        public HttpConnection(String elasticServerUri)
        {
            serverUri = elasticServerUri;
        }

        public async Task<String> performQuery(String url,HttpContent content)
        {
            var baseUrl = serverUri;
            if (!baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl + "/";
            }
            baseUrl = baseUrl + url;
            String message = String.Empty;
            using (var client = new HttpClient())
            {
                message = client.PostAsync(baseUrl, content).ToString();
            }
            return message;
        }
        public async Task<bool> indexExists(String indexName)
        {
            if (String.IsNullOrEmpty(indexName))
            {
                throw new Exception("Index name is null or empty");
            }
            var indexUrl = $"{serverUri}/{indexName}";
            using(var client=new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Head, indexUrl);
                var response = await client.SendAsync(message);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
        }

        public async Task deleteIndex(String indexName)
        {
            if (String.IsNullOrEmpty(indexName))
            {
                throw new Exception("Index name is null or empty");
            }

            var indexUrl = $"{serverUri}/{indexName}";
            using(var client=new HttpClient())
            {
                await client.DeleteAsync(indexUrl);
            }
        }

        public async Task createIndex(String indexName)
        {
            if (String.IsNullOrEmpty(indexName))
            {
                throw new Exception("Indexname is null or empty");
            }
            var indexUrl = $"{serverUri}/{indexName}";
            using(var client=new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, indexUrl);
                await client.SendAsync(message);
            }
        }
    }
}
