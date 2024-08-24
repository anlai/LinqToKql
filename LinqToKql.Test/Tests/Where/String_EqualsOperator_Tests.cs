using LinqToKql.Test.Models;

namespace LinqToKql.Test.Tests.Where
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://learn.microsoft.com/en-us/kusto/query/equals-cs-operator?view=microsoft-fabric
    /// </remarks>
    [TestClass]
    public class String_EqualsOperator_Tests
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
        public void ToKql_WhereStringEqualsOrdinalMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name.Equals("test", StringComparison.Ordinal));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name == 'test'", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringEqualsOrdinalIgnoreCaseMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name.Equals("test", StringComparison.OrdinalIgnoreCase));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name =~ 'test'", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotEqualsOrdinalIgnoreCaseMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !x.name.Equals("test", StringComparison.OrdinalIgnoreCase));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name !~ 'test'", kql);
        }

        #endregion
    }
}
