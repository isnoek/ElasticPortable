using ElasticSearch.Connections;
using ElasticSearch.CSharpTests.Models;
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
        private String TestIndexName = "testindex";
        private String TestType = "testmodel-nl";

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
        public void testCreateIndex()
        {
            HttpConnection connection = new HttpConnection(ElasticUrl);
            Task createTask=connection.createIndex(TestIndexName);
            createTask.Wait();

            Task<bool> indexExist = connection.indexExists(TestIndexName);
            indexExist.Wait();
            Assert.IsTrue(indexExist.Result);

            Task deleteIndex=connection.deleteIndex(TestIndexName);
            deleteIndex.Wait();
        }

        [TestMethod]
        public void testInsert()
        {
            HttpConnection connection = new HttpConnection(ElasticUrl);
            Task createTask = connection.createIndex(TestIndexName);
            createTask.Wait();

            Task<bool> indexExists = connection.indexExists(TestIndexName);
            indexExists.Wait();
            Assert.IsTrue(indexExists.Result);

            TestModel model = new TestModel() { FirstName = "Bill", LastName = "Gates" };
            Task<bool> insertTask = connection.insert(TestIndexName,TestType, model);
            insertTask.Wait();

            Task deleteIndex = connection.deleteIndex(TestIndexName);
            deleteIndex.Wait();

            indexExists = connection.indexExists(TestIndexName);
            indexExists.Wait();
            Assert.IsFalse(indexExists.Result);

        }
        [TestMethod]
        public void TestConnectionInitialize()
        {
            var connection = new HttpConnection(ElasticUrl);
            Assert.IsNotNull(connection);
            
        }
    }
}
