using LinqToKql.Test.Models;

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
        private const string ResultNameInList = "resources | where name in ('test1', 'test2')";
        private const string ResultNameNotInList = "resources | where name !in ('test1', 'test2')";
        private const string ResultNameInCaseInSensitiveList = "resources | where name in~ ('test1', 'test2')";
        private const string ResultNameNotInCaseInSensitiveList = "resources | where name !in~ ('test1', 'test2')";

        #region In List

        [TestMethod]
        public void ToKql_WhereStringInArrayInit_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => (new string[] { "test1", "test2" }).Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameInList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringInListInit_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => new List<string> { "test1", "test2" }.Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameInList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringInArray_Success()
        {
            var arr = new string[] { "test1", "test2" };
            var q = Kql.Create<AzureResource>().Where(x => arr.Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameInList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringInList_Success()
        {
            var list = new List<string> { "test1", "test2" };
            var q = Kql.Create<AzureResource>().Where(x => list.Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameInList, kql);
        }

        #endregion

        #region Not In List

        [TestMethod]
        public void ToKql_WhereStringNotInArrayInit_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !(new string[] { "test1", "test2" }).Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameNotInList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotInListInit_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !(new List<string> { "test1", "test2" }.Contains(x.name)));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameNotInList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotInArray_Success()
        {
            var arr = new string[] { "test1", "test2" };
            var q = Kql.Create<AzureResource>().Where(x => !arr.Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameNotInList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotInList_Success()
        {
            var list = new List<string> { "test1", "test2" };
            var q = Kql.Create<AzureResource>().Where(x => !list.Contains(x.name));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameNotInList, kql);
        }

        #endregion

        #region In List Case Insensitive

        [TestMethod]
        public void ToKql_WhereStringInCaseInsensitiveArrayInit_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => (new string[] { "test1", "test2" }).Contains(x.name, StringComparer.OrdinalIgnoreCase));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameInCaseInSensitiveList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringInCaseInsensitiveListInit_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => new List<string> { "test1", "test2" }.Contains(x.name, StringComparer.OrdinalIgnoreCase));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameInCaseInSensitiveList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringInCaseInsensitiveArray_Success()
        {
            var arr = new string[] { "test1", "test2" };
            var q = Kql.Create<AzureResource>().Where(x => arr.Contains(x.name, StringComparer.OrdinalIgnoreCase));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameInCaseInSensitiveList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringInCaseInsensitiveList_Success()
        {
            var list = new List<string> { "test1", "test2" };
            var q = Kql.Create<AzureResource>().Where(x => list.Contains(x.name, StringComparer.OrdinalIgnoreCase));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameInCaseInSensitiveList, kql);
        }

        #endregion

        #region Not In List Case Insensitive

        [TestMethod]
        public void ToKql_WhereStringNotInCaseInsensitiveArrayInit_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !((new string[] { "test1", "test2" }).Contains(x.name, StringComparer.OrdinalIgnoreCase)));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameNotInCaseInSensitiveList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotInCaseInsensitiveListInit_Success()
        {
            var q = Kql.Create<AzureResource>().Where(x => !(new List<string> { "test1", "test2" }.Contains(x.name, StringComparer.OrdinalIgnoreCase)));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameNotInCaseInSensitiveList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotInCaseInsensitiveArray_Success()
        {
            var arr = new string[] { "test1", "test2" };
            var q = Kql.Create<AzureResource>().Where(x => !arr.Contains(x.name, StringComparer.OrdinalIgnoreCase));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameNotInCaseInSensitiveList, kql);
        }

        [TestMethod]
        public void ToKql_WhereStringNotInCaseInsensitiveList_Success()
        {
            var list = new List<string> { "test1", "test2" };
            var q = Kql.Create<AzureResource>().Where(x => !list.Contains(x.name, StringComparer.OrdinalIgnoreCase));

            var kql = q.ToKql();

            Assert.AreEqual(ResultNameNotInCaseInSensitiveList, kql);
        }

        #endregion
    }
}
