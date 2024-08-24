using LinqToKql.Test.Models;

namespace LinqToKql.Test.Tests.Sort
{
    [TestClass]
    public class String_OrderBy_Tests
    {
        [TestMethod]
        public void ToKql_OrderByStringAsc_Success()
        {
            var q = Kql.Create<AzureResource>().OrderBy(x => x.name);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort name asc", kql);
        }
        [TestMethod]
        public void ToKql_OrderByStringDesc_Success()
        {
            var q = Kql.Create<AzureResource>().OrderByDescending(x => x.name);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort name desc", kql);
        }
    }
}
