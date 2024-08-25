using LinqToKql.Test.Models;

namespace LinqToKql.Test.Tests.Sort
{
    [TestClass]
    public class Numerical_OrderBy_Tests
    {
        [TestMethod]
        public void ToKql_OrderByIntAsc_Success()
        {
            var q = Kql.Create<AzureResource>().OrderBy(x => x.instances);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort instances asc", kql);
        }

        [TestMethod]
        public void ToKql_OrderByIntDesc_Success()
        {
            var q = Kql.Create<AzureResource>().OrderByDescending(x => x.instances);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort instances desc", kql);
        }
    }
}
