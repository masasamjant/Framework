namespace Masasamjant.Linq
{
    [TestClass]
    public class SortOrderHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_ToSql()
        {
            Assert.AreEqual(string.Empty, SortOrderHelper.ToSql((SortOrder)999)); // Undefined
            Assert.AreEqual(string.Empty, SortOrderHelper.ToSql(SortOrder.None));
            Assert.AreEqual("ASC", SortOrderHelper.ToSql(SortOrder.Ascending));
            Assert.AreEqual("DESC", SortOrderHelper.ToSql(SortOrder.Descending));
        }
    }
}
