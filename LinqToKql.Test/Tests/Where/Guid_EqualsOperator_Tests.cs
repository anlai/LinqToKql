using LinqToKql.Test.Models;

namespace LinqToKql.Test.Tests.Where
{
    [TestClass]
    public class Guid_EqualsOperator_Tests
    {

        private const string DefaultGuid = "a3fdacc4-709e-4c2c-a3f1-ab6b53be5f0f";
        private const string ResultGuidEquals = "resources | where tenantId == 'a3fdacc4-709e-4c2c-a3f1-ab6b53be5f0f'";

        [TestMethod]
        public void ToKql_WhereNewGuidEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.tenantId == new Guid(DefaultGuid));

            var kql = q.ToKql();

            Assert.AreEqual(ResultGuidEquals, kql);
        }

        [TestMethod]
        public void ToKql_WhereGuidParseEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.tenantId == Guid.Parse(DefaultGuid));

            var kql = q.ToKql();

            Assert.AreEqual(ResultGuidEquals, kql);
        }

        [TestMethod]
        public void ToKql_WhereGuidVarEqualsOp_Success()
        {
            var guid = Guid.Parse(DefaultGuid);
            var q = Kql.Create<AzureResource>().Where(x => x.tenantId == guid);

            var kql = q.ToKql();

            Assert.AreEqual(ResultGuidEquals, kql);
        }
    }
}
