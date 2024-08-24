using LinqToKql.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToKql.Test.Tests.Where
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://learn.microsoft.com/en-us/kusto/query/in-cs-operator?view=microsoft-fabric
    /// </remarks>
    [TestClass]
    public class String_InOperator_Tests
    {
        [TestMethod]
        public void ToKql_WhereStringInArrayInit_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => (new string[] { "test", "test2" }).Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name in ('test', 'test2')", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringInArray_Success()
        {
            var arr = new string[] { "test" };
            var q = Kql.Create<AzureResource>().Where(x => arr.Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name in ('test')", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotInArrayInit_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !(new string[] { "test1", "test2" }).Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name !in ('test1', 'test2')", kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotInArray_Success()
        {
            var arr = new string[] { "test" };
            var q = Kql.Create<AzureResource>().Where(x => !arr.Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual("resources | where name !in ('test')", kql);
        }
    }
}
