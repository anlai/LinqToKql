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

        [TestMethod]
        public void ToKql_DateTimeLessThanToday_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated < DateTime.Now);

            var kql = q.ToKql();

            Assert.AreEqual("resources | where dateCreated < now()", kql);
        }

        [TestMethod]
        public void ToKql_DateTimeLessThanEqualToday_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated <= DateTime.Now);

            var kql = q.ToKql();

            Assert.AreEqual("resources | where dateCreated <= now()", kql);
        }

        [TestMethod]
        public void ToKql_DateTimeGreaterThanToday_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated > DateTime.Now);

            var kql = q.ToKql();

            Assert.AreEqual("resources | where dateCreated > now()", kql);
        }

        [TestMethod]
        public void ToKql_DateTimeGreaterEqualThanToday_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated >= DateTime.Now);

            var kql = q.ToKql();

            Assert.AreEqual("resources | where dateCreated >= now()", kql);
        }

        [DataRow(1)]
        [DataRow(-1)]
        [DataTestMethod]
        public void ToKql_DateTimeNowAddMinute_Success(int minutes) 
        { 
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated < DateTime.Now.AddMinutes(minutes));

            var kql = q.ToKql();

            Assert.AreEqual($"resources | where dateCreated < datetime_add('minute',{minutes},now())", kql);
        }

        [DataRow(1)]
        [DataRow(-1)]
        [DataTestMethod]
        public void ToKql_DateTimeNowAddHour_Success(int hours) 
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated < DateTime.Now.AddHours(hours));

            var kql = q.ToKql();

            Assert.AreEqual($"resources | where dateCreated < datetime_add('hour',{hours},now())", kql);
        }

        [DataRow(1)]
        [DataRow(-1)]
        [DataTestMethod]
        public void ToKql_DateTimeNowAddDay_Success(int days) 
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated < DateTime.Now.AddDays(days));

            var kql = q.ToKql();

            Assert.AreEqual($"resources | where dateCreated < datetime_add('day',{days},now())", kql);
        }

        [DataRow(1)]
        [DataRow(-1)]
        [DataTestMethod]
        public void ToKql_DateTimeNowAddMonth_Success(int months) 
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated < DateTime.Now.AddMonths(months));

            var kql = q.ToKql();

            Assert.AreEqual($"resources | where dateCreated < datetime_add('month',{months},now())", kql);
        }

        [DataRow(1)]
        [DataRow(-1)]
        [DataTestMethod]
        public void ToKql_DateTimeNowAddYear_Success(int years) 
        {
            var q = Kql.Create<AzureResource>().Where(x => x.dateCreated < DateTime.Now.AddYears(years));

            var kql = q.ToKql();

            Assert.AreEqual($"resources | where dateCreated < datetime_add('year',{years},now())", kql);
        }
    }
}
