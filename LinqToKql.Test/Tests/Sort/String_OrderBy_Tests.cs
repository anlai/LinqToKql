using LinqToKql.Test.Models;

namespace LinqToKql.Test.Tests.Sort
{
    [TestClass]
    public class String_OrderBy_Tests
    {
        [TestMethod]
        public void ToKql_OrderByStringAsc_Success()
        {
            var q = Kql.Create<AzureResource>().OrderBy(x => x.name);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort name asc", kql);
        }
        
        [TestMethod]
        public void ToKql_OrderByStringDesc_Success()
        {
            var q = Kql.Create<AzureResource>().OrderByDescending(x => x.name);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort name desc", kql);
        }

        [TestMethod]
        public void ToKql_OrderByStringDescSecondAscending_Success()
        {
            var q = Kql.Create<AzureResource>().OrderByDescending(x => x.name).ThenBy(x => x.location);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort name desc, location asc", kql);
        }

        [TestMethod]
        public void To_KqlOrderByTwoProperties_Success()
        {
            var q = Kql.Create<AzureResource>().OrderBy(x => x.name).ThenBy(x => x.location);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort name asc, location asc", kql);
        }

        [TestMethod]
        public void To_KqlOrderByThreeProperties_Success()
        {
            var q = Kql.Create<AzureResource>().OrderBy(x => x.name).ThenBy(x => x.location).ThenBy(x => x.tenantId);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort name asc, location asc, tenantId asc", kql);
        }

        [TestMethod]
        public void To_KqlOrderByThreePropertiesLastDescending_Success()
        {
            var q = Kql.Create<AzureResource>().OrderBy(x => x.name).ThenBy(x => x.location).ThenByDescending(x => x.tenantId);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort name asc, location asc, tenantId desc", kql);
        }

        [TestMethod]
        public void To_KqlOrderByTwoPropertiesSecondDescending_Success()
        {
            var q = Kql.Create<AzureResource>().OrderBy(x => x.name).ThenByDescending(x => x.location);

            var kql = q.ToKql();

            Assert.AreEqual("resources | sort name asc, location desc", kql);
        }
    }
}
