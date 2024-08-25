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

        private const string ResultEquals = "resources | where name == 'test'";
        private const string ResultNotEquals = "resources | where name != 'test'";
        private const string ResultEqualsCaseInsensitive = "resources | where name =~ 'test'";
        private const string ResultNotEqualsCaseInsensitive = "resources | where name !~ 'test'";

        #region Operand : ==

        [TestMethod]
        public void ToKql_WhereStringEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name == "test");

            var kql = q.ToKql();

            Assert.AreEqual(ResultEquals, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name != "test");

            var kql = q.ToKql();

            Assert.AreEqual(ResultNotEquals, kql);
        }

        #endregion

        #region Method : Equals

        [TestMethod]
        public void ToKql_WhereStringEqualsMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name.Equals("test"));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEquals, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotEqualsMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !x.name.Equals("test"));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNotEquals, kql);
        }

        [DataRow(StringComparison.Ordinal, ResultEquals)]
        [DataRow(StringComparison.OrdinalIgnoreCase, ResultEqualsCaseInsensitive)]
        [DataRow(StringComparison.CurrentCulture, ResultEquals)]
        [DataRow(StringComparison.CurrentCultureIgnoreCase, ResultEqualsCaseInsensitive)]
        [DataRow(StringComparison.InvariantCulture, ResultEquals)]
        [DataRow(StringComparison.InvariantCultureIgnoreCase, ResultEqualsCaseInsensitive)]
        [DataTestMethod]
        public void ToKql_WhereStringEqualsWithStringComparison_Success(StringComparison comparison, string result)
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name.Equals("test", comparison));

            var kql = q.ToKql();

            Assert.AreEqual(result, kql);
        }

        [DataRow(StringComparison.Ordinal, ResultNotEquals)]
        [DataRow(StringComparison.OrdinalIgnoreCase, ResultNotEqualsCaseInsensitive)]
        [DataRow(StringComparison.CurrentCulture, ResultNotEquals)]
        [DataRow(StringComparison.CurrentCultureIgnoreCase, ResultNotEqualsCaseInsensitive)]
        [DataRow(StringComparison.InvariantCulture, ResultNotEquals)]
        [DataRow(StringComparison.InvariantCultureIgnoreCase, ResultNotEqualsCaseInsensitive)]
        [DataTestMethod]
        public void ToKql_WhereStringNotEqualsWithStringComparison_Success(StringComparison comparison, string result)
        {
            var q = Kql.Create<AzureResource>().Where(x => !x.name.Equals("test", comparison));

            var kql = q.ToKql();

            Assert.AreEqual(result, kql);
        }

        #endregion
    }
}
