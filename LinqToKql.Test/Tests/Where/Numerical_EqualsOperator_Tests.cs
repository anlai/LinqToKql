using LinqToKql.Test.Models;

namespace LinqToKql.Test.Tests.Where
{
    [TestClass]
    public class Numerical_EqualsOperator_Tests
    {

        private const string ResultEqualsInt = "resources | where instances == 1";
        private const string ResultNotEqualsInt = "resources | where instances != 1";

        [TestMethod]
        public void ToKql_WhereIntEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.instances == 1);

            var kql = q.ToKql();

            Assert.AreEqual(ResultEqualsInt, kql);
        }

        [TestMethod]
        public void ToKql_WhereIntNotEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.instances != 1);

            var kql = q.ToKql();

            Assert.AreEqual(ResultNotEqualsInt, kql);
        }

        [TestMethod]
        public void ToKql_WhereInitEqualsMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.instances.Equals(1));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEqualsInt, kql);
        }

        [TestMethod]
        public void ToKql_WhereInitEqualsStaticMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => int.Equals(x.instances, 1));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEqualsInt, kql);
        }

        [TestMethod]
        public void ToKql_WhereInitNotEqualsStaticMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !int.Equals(x.instances, 1));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNotEqualsInt, kql);
        }
    }
}
