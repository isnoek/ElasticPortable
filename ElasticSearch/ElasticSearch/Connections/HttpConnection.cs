using ElasticSearch.Enums;
using ElasticSearch.Results;
using Newtonsoft.Json;
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

        private async Task<IEnumerable<SearchResult<TResultType>>> termSearch<TResultType>(String index,
            String type,
            Dictionary<String,String> searchTerms) where TResultType:class,new()
        {
            return new List<SearchResult<TResultType>>().AsEnumerable();
        }

        private async Task<IEnumerable<SearchResult<TResultType>>> matchSearch<TResultType>(String index,
            String type,
            Dictionary<String, String> searchTerms) where TResultType : class, new()
        {
            return new List<SearchResult<TResultType>>().AsEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResultType"></typeparam>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="searchTerms"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchResult<TResultType>>> fetch<TResultType>(String index,
            String type,
            Dictionary<String,String> searchTerms,
            SearchType searchType=SearchType.MATCH) where TResultType:class,new()
        {
            //preconditions
            if (String.IsNullOrEmpty(index))
            {
                throw new Exception("Index can not be null or empty");
            }

            if (String.IsNullOrEmpty(type))
            {
                throw new Exception("You need to specify a type");
            }
            if (!searchTerms.Keys.Any())
            {
                return findAllTerms<TResultType>(index, type, searchTerms);
            }
            switch(searchType)
            {
                case SearchType.MATCH:return await matchSearch<TResultType>(index, type, searchTerms);
                case SearchType.TERM:return await termSearch<TResultType>(index, type, searchTerms);
                default:break;
            }
            return new List<SearchResult<TResultType>>().AsEnumerable();
        }

        private IEnumerable<SearchResult<TResultType>> findAllTerms<TResultType>(string index, string type, Dictionary<string, string> searchTerms) where TResultType : class, new()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> insert(String indexName,String type,Object o)
        {
            if (String.IsNullOrEmpty(indexName))
            {
                throw new Exception("Indexname is null or empty");
            }

            if (o==null)
            {
                throw new Exception("Can not insert a null object");
            }
            String uri = $"{serverUri}/{indexName}/{type}";
            StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(o));
            using(var client=new HttpClient())
            {
                await client.PostAsync(uri, jsonContent);
            }

            return true;
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
