using LinqToKql.Test.Models;

namespace LinqToKql.Test.Tests
{
    [TestClass]
    public class ToKql_Combined
    {
        [TestMethod]
        public void ToKql_WhereAndOrder_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name == "test" || x.name == "test2").OrderBy(x => x.name);

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name == 'test' or name == 'test2' | sort name", kql);
        }

        [TestMethod]
        public void ToKql_WhereAndWhere_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name == "test").Where(x => x.type == "microsoft.web/site");

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name == 'test' | where type == 'microsoft.web/site'", kql);
        }
    }
}
