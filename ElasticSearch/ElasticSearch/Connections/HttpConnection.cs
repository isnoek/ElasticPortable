﻿using System;
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
            var message= await new HttpClient().PostAsync(baseUrl, content);
            return message.ToString();
        }
    }
}
