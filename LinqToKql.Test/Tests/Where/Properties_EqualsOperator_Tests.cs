using LinqToKql.Test.Models;

namespace LinqToKql.Test.Tests.Where
{
    [TestClass]
    public class Properties_EqualsOperator_Tests
    {
        private const string ResultEquals = "resources | where name == id";
        private const string ResultNotEquals = "resources | where name != id";
        private const string ResultEqualsCaseInsensitive = "resources | where name =~ id";
        private const string ResultNotEqualsCaseInsensitive = "resources | where name !~ id";

        [TestMethod]
        public void ToKql_WherePropertiesEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name == x.id);

            var kql = q.ToKql();

            Assert.AreEqual(ResultEquals, kql);
        }

        [TestMethod]
        public void ToKql_WherePropertiesEqualsMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name.Equals(x.id));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEquals, kql);
        }

        [TestMethod]
        public void ToKql_WherePropertiesEqualsStaticMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => string.Equals(x.name, x.id));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEquals, kql);
        }

        [DataRow(StringComparison.Ordinal, ResultEquals)]
        [DataRow(StringComparison.OrdinalIgnoreCase, ResultEqualsCaseInsensitive)]
        [DataRow(StringComparison.CurrentCulture, ResultEquals)]
        [DataRow(StringComparison.CurrentCultureIgnoreCase, ResultEqualsCaseInsensitive)]
        [DataRow(StringComparison.InvariantCulture, ResultEquals)]
        [DataRow(StringComparison.InvariantCultureIgnoreCase, ResultEqualsCaseInsensitive)]
        [DataTestMethod]
        public void ToKql_WherePropertiesEqualsMethodWithStringComparison_Success(StringComparison comparison, string result)
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name.Equals(x.id, comparison));

            var kql = q.ToKql();

            Assert.AreEqual(result, kql);
        }

        [DataRow(StringComparison.Ordinal, ResultEquals)]
        [DataRow(StringComparison.OrdinalIgnoreCase, ResultEqualsCaseInsensitive)]
        [DataRow(StringComparison.CurrentCulture, ResultEquals)]
        [DataRow(StringComparison.CurrentCultureIgnoreCase, ResultEqualsCaseInsensitive)]
        [DataRow(StringComparison.InvariantCulture, ResultEquals)]
        [DataRow(StringComparison.InvariantCultureIgnoreCase, ResultEqualsCaseInsensitive)]
        [DataTestMethod]
        public void ToKql_WherePropertiesEqualsStaticMethodWithStringComparison_Success(StringComparison comparison, string result)
        {
            var q = Kql.Create<AzureResource>().Where(x => string.Equals(x.name, x.id, comparison));

            var kql = q.ToKql();

            Assert.AreEqual(result, kql);
        }

        [TestMethod]
        public void ToKql_WherePropertiesNotEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.name != x.id);

            var kql = q.ToKql();

            Assert.AreEqual(ResultNotEquals, kql);
        }

        [TestMethod]
        public void ToKql_WherePropertiesNotEqualsMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !x.name.Equals(x.id));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNotEquals, kql);
        }

        [TestMethod]
        public void ToKql_WherePropertiesNotEqualsStaticMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !string.Equals(x.name, x.id));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNotEquals, kql);
        }

        [DataRow(StringComparison.Ordinal, ResultNotEquals)]
        [DataRow(StringComparison.OrdinalIgnoreCase, ResultNotEqualsCaseInsensitive)]
        [DataRow(StringComparison.CurrentCulture, ResultNotEquals)]
        [DataRow(StringComparison.CurrentCultureIgnoreCase, ResultNotEqualsCaseInsensitive)]
        [DataRow(StringComparison.InvariantCulture, ResultNotEquals)]
        [DataRow(StringComparison.InvariantCultureIgnoreCase, ResultNotEqualsCaseInsensitive)]
        [DataTestMethod]
        public void ToKql_WherePropertiesNotEqualsMethodWithStringComparison_Success(StringComparison comparison, string result)
        {
            var q = Kql.Create<AzureResource>().Where(x => !x.name.Equals(x.id, comparison));

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
        public void ToKql_WherePropertiesNotEqualsStaticMethodWithStringComparison_Success(StringComparison comparison, string result)
        {
            var q = Kql.Create<AzureResource>().Where(x => !string.Equals(x.name, x.id, comparison));

            var kql = q.ToKql();

            Assert.AreEqual(result, kql);
        }
    }
}
