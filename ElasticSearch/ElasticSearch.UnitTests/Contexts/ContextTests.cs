using ElasticSearch.Contexts;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.UnitTests.Contexts
{
    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void TestValidUrl()
        {
            ElasticSearchContext context = new ElasticSearchContext("http://www.nu.nl");

            Assert.IsNotNull(context);
            Assert.IsTrue(!String.IsNullOrEmpty(context.Url));
        }

        [TestMethod]
        public void TestInvalidUrl()
        {
            Assert.ThrowsException<Exception>(() =>
            {
                try
                {
                    var context = new ElasticSearchContext("uiyuiyuiy");
                } catch(Exception e)
                {
                    throw new Exception("Something wrong URI", e);
                }
            });
        }
    }
}
