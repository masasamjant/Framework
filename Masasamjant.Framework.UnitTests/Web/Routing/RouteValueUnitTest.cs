namespace Masasamjant.Web.Routing
{
    [TestClass]
    public class RouteValueUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new RouteValue(string.Empty, "1"));
            Assert.ThrowsException<ArgumentNullException>(() => new RouteValue("  ", "1"));
            var routeValue = new RouteValue("id", 1);
            Assert.AreEqual("id", routeValue.Name);
            Assert.AreEqual(1, routeValue.Value);
        }

        [TestMethod]
        public void Test_ToString()
        {
            var routeValue = new RouteValue("id", 1);
            var expected = "id=1";
            var actual = routeValue.ToString(); 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Equals()
        {
            var routeValue = new RouteValue("id", 1);
            Assert.IsFalse(routeValue.Equals(null));
            Assert.IsFalse(routeValue.Equals(new RouteValue("id", 2)));
            Assert.IsFalse(routeValue.Equals(new RouteValue("sid", 1)));
            Assert.IsTrue(routeValue.Equals(routeValue));
            Assert.IsTrue(routeValue.Equals(new RouteValue("id", 1)));
            Assert.IsFalse(routeValue.Equals(DateTime.Now));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var routeValue = new RouteValue("id", 1);
            var otherValue = new RouteValue("id", 1);
            Assert.AreEqual(routeValue.GetHashCode(), otherValue.GetHashCode());
        }
    }
}
