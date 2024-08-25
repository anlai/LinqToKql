using LinqToKql.Test.Models;

namespace LinqToKql.Test.Tests.Where
{
    [TestClass]
    public class Numerical_EqualsOperator_Tests
    {
        [TestMethod]
        public void ToKql_WhereIntEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.instances == 1);

            var kql = q.ToKql();

            Assert.AreEqual("resources | where instances == 1", kql);
        }

        [TestMethod]
        public void ToKql_WhereIntNotEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.instances != 1);

            var kql = q.ToKql();

            Assert.AreEqual("resources | where instances != 1", kql);
        }
    }
}
