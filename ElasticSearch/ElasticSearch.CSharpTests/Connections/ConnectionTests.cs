using ElasticSearch.Connections;
using ElasticSearch.CSharpTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.CSharpTests.Connections
{
    [TestClass]
    public class ConnectionTests
    {
        private String ElasticUrl = "http://localhost:9200";

        [TestInitialize]
        public void setUp()
        {
            Task<bool> IsAvailable = ServerUtilities.IsElasticServerAvailable();
            IsAvailable.Wait();
            if (!IsAvailable.Result)
            {
                throw new Exception("No elastic server to test on");
            }
        }

        [TestMethod]
        public void TestConnectionInitialize()
        {
            var connection = new HttpConnection(ElasticUrl);
            Assert.IsNotNull(connection);
            
        }
    }
}
