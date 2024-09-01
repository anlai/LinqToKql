using LinqToKql.Test.Models;

namespace LinqToKql.Test.Tests.Where
{
    [TestClass]
    public class DateTime_Operators_Tests
    {

        private const string ResultEqualsDateOnly = "resources | where dateCreated == datetime(2024-01-01)";
        private const string ResultEqualsDateTime = "resources | where dateCreated == datetime(2024-01-01 08:10:03)";

        [TestMethod]
        public void ToKql_DateOnlyEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated == new DateTime(2024, 1, 1));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEqualsDateOnly, kql);
        }

        [TestMethod]
        public void ToKql_DateOnlyEqualsMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated.Equals(new DateTime(2024, 1, 1)));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEqualsDateOnly, kql);
        }

        [TestMethod]
        public void ToKql_DateOnlyEqualsStaticMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => DateTime.Equals(x.dateCreated, new DateTime(2024, 1, 1)));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEqualsDateOnly, kql);
        }

        [TestMethod]
        public void ToKql_DateTimeEqualsOp_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated == new DateTime(2024, 1, 1, 8, 10, 3));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEqualsDateTime, kql);
        }

        [TestMethod]
        public void ToKql_DateTimeEqualsMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated.Equals(new DateTime(2024, 1, 1, 8, 10, 3)));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEqualsDateTime, kql);
        }

        [TestMethod]
        public void ToKql_DateTimeEqualsStaticMethod_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => DateTime.Equals(x.dateCreated, new DateTime(2024, 1, 1, 8, 10, 3)));

            var kql = q.ToKql();

            Assert.AreEqual(ResultEqualsDateTime, kql);
        }




        // equals
        // now vs artibrary date
        // <
        // >
        // date + time period
        // date - time period
        // now


    }
}
