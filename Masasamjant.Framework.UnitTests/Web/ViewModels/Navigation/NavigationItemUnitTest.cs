namespace Masasamjant.Web.ViewModels.Navigation
{
    [TestClass]
    public class NavigationItemUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var navigationItem = new NavigationItem();
            Assert.AreEqual(string.Empty, navigationItem.Text);
            Assert.AreEqual(string.Empty, navigationItem.Url);
            Assert.AreEqual(0, navigationItem.RouteParameters.Count);
            Assert.AreEqual(string.Empty, navigationItem.CssClass);
            Assert.AreEqual(string.Empty, navigationItem.DisabledCssClass);
            Assert.AreEqual(true, navigationItem.IsEnabled);
            Assert.AreEqual(0, navigationItem.HtmlAttributes.Count);

            navigationItem = new NavigationItem(text: null, url: null, cssClass: null, disabledCssClass: null);
            Assert.AreEqual(string.Empty, navigationItem.Text);
            Assert.AreEqual(string.Empty, navigationItem.Url);
            Assert.AreEqual(string.Empty, navigationItem.CssClass);
            Assert.AreEqual(string.Empty, navigationItem.DisabledCssClass);
            Assert.AreEqual(true, navigationItem.IsEnabled);

            var routeParameters = new Dictionary<string, object?>() { { "id", 1 } };
            var htmlAttributes = new Dictionary<string, object?>() { { "style", "color:blue" } };
            navigationItem = new NavigationItem("Test", "http://Controller/Action", routeParameters, "css", "disabled-css", false, htmlAttributes);
            Assert.AreEqual("Test", navigationItem.Text);
            Assert.AreEqual("http://Controller/Action", navigationItem.Url);
            Assert.IsTrue(navigationItem.RouteParameters.ContainsKey("id") && navigationItem.RouteParameters["id"]!.Equals(1));
            Assert.AreEqual("css", navigationItem.CssClass);
            Assert.AreEqual("disabled-css", navigationItem.DisabledCssClass);
            Assert.AreEqual(false, navigationItem.IsEnabled);
            Assert.IsTrue(navigationItem.HtmlAttributes.ContainsKey("style") && navigationItem.HtmlAttributes["style"]!.Equals("color:blue"));

            Assert.ThrowsException<ArgumentException>(() => new NavigationItem(url: ""));
        }

        [TestMethod]
        public void Test_Url()
        {
            var navigationItem = new NavigationItem();
            Assert.AreEqual(string.Empty, navigationItem.Url);
            navigationItem.Url = "http://controller/action";
            Assert.AreEqual("http://controller/action", navigationItem.Url);
            Assert.ThrowsException<ArgumentException>(() => navigationItem.Url = "ftp://controller/action");
        }

        [TestMethod]
        public void Test_GetFullUrl()
        {
            var routeParameters = new Dictionary<string, object?>() { { "id", 1 } };
            var navigationItem = new NavigationItem();
            var url = navigationItem.GetFullUrl();
            Assert.AreEqual(string.Empty, url);
            
            navigationItem.Url = "http://controller/action";
            url = navigationItem.GetFullUrl();
            Assert.AreEqual("http://controller/action", url);

            navigationItem.Url = "http://controller/action?";
            url = navigationItem.GetFullUrl();
            Assert.AreEqual("http://controller/action", url);

            navigationItem = new NavigationItem(url: "http://controller/action", routeParameters: routeParameters);
            url = navigationItem.GetFullUrl();
            Assert.AreEqual("http://controller/action?id=1", url);

            navigationItem = new NavigationItem(url: "http://controller/action?", routeParameters: routeParameters);
            url = navigationItem.GetFullUrl();
            Assert.AreEqual("http://controller/action?id=1", url);

            navigationItem = new NavigationItem(url: "http://controller/action?name=test", routeParameters: routeParameters);
            url = navigationItem.GetFullUrl();
            Assert.AreEqual("http://controller/action?name=test&id=1", url);

            routeParameters.Add("name", "name");
            navigationItem = new NavigationItem(url: "http://controller/action", routeParameters: routeParameters);
            url = navigationItem.GetFullUrl();
            Assert.AreEqual("http://controller/action?id=1&name=name", url);
        }
    }
}
