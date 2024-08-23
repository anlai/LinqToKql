using LinqToKql.Test.Models;
using System.Linq;

namespace LinqToKql.Test
{
    /// <summary>
    /// Test methods on where clauses only
    /// </summary>
    [TestClass]
    public class ToKql_String_Where
    {

        #region Operand : ==

        [TestMethod]
        public void ToKql_WhereStringEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name == "test");

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name == 'test'", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name != "test");

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name != 'test'", kql);
        }

        #endregion

        #region Method : Equals

        [TestMethod]
        public void ToKql_WhereStringEqualsMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name.Equals("test"));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name == 'test'", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotEqualsMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !x.name.Equals("test"));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name != 'test'", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringEqualsWithCompareMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name.Equals("test", StringComparison.Ordinal));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name == 'test'", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringEqualsCaseInsensitiveMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name.Equals("test", StringComparison.OrdinalIgnoreCase));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name =~ 'test'", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotEqualsCaseInsensitiveMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !x.name.Equals("test", StringComparison.OrdinalIgnoreCase));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name !~ 'test'", kql);
        }

        #endregion







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
