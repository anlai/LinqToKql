using LinqToKql.Test.Models;

namespace LinqToKql.Test
{
    /// <summary>
    /// Test methods on where clauses only
    /// </summary>
    [TestClass]
    public class ToKql_String_Where
    {
        [TestMethod]
        public void ToKql_WhereStringEquals_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name == "test");

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name == \"test\"", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotEquals_Success() 
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name != "test");

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name != \"test\"", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringInArrayInit_Success() 
        {
            var q = Kql.Create<AzureResource>().Where(x => (new string[] { "test" }).Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name in (\"test\")", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotInArrayInit_Success() 
        {
            var q = Kql.Create<AzureResource>().Where(x => !(new string[] { "test" }).Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name !in (\"test\")", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringInArray_Success()
        {
            var arr = new string[] { "test" };
            var q = Kql.Create<AzureResource>().Where(x => arr.Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name in (\"test\")", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotInArray_Success()
        {
            var arr = new string[] { "test" };
            var q = Kql.Create<AzureResource>().Where(x => !arr.Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name !in (\"test\")", kql);
        }

        // https://arcanecode.com/2023/01/09/fun-with-kql-contains-and-in/
        // contains
        // not contains
        // contains_cs
        // not contains_cs
        // startswith
        // endswith
    }
}
