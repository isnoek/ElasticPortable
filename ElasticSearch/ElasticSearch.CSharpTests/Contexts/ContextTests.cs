using ElasticSearch.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.CSharpTests.Contexts
{
    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void TestContextSetup()
        {
            var context = new ElasticSearchContext("http://www.nu.nl");
            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Url);

        }
    }
}
