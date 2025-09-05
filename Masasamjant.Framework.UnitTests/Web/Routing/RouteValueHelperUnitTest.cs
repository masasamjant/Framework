namespace Masasamjant.Web.Routing
{
    [TestClass]
    public class RouteValueHelperUnitTest : UnitTest
    {
        [TestMethod]    
        public void Test_ToDictionary()
        {
            var routeValues = new List<IRouteValue>()
            {
                new RouteValue("name", "Test"),
                new RouteValue("id", 1),
                new RouteValue("name", "Rest")
            };

            var result = RouteValueHelper.ToDictionary(routeValues);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result["id"]);
            Assert.AreEqual("Test", result["name"]);

            routeValues.Clear();
            result = RouteValueHelper.ToDictionary(routeValues);
            Assert.AreEqual(0, result.Count);
        }
    }
}
