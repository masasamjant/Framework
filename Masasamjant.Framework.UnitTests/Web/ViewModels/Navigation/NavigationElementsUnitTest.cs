namespace Masasamjant.Web.ViewModels.Navigation
{
    [TestClass]
    public class NavigationElementsUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var elements = new NavigationElements("div", "div");
            Assert.AreEqual("div", elements.NavigationContainerElement);
            Assert.AreEqual("div", elements.NavigationItemContainerElement);
            Assert.AreEqual(NavigationElements.DefaultNavigationItemElement, elements.NavigationItemElement);
            Assert.AreEqual(string.Empty, elements.NavigationContainerElementCssClass);
            Assert.AreEqual(string.Empty, elements.NavigationItemContainerElementCssClass);

            elements = new NavigationElements(" UL ", " LI ", " SPAN ");
            Assert.AreEqual("ul", elements.NavigationContainerElement);
            Assert.AreEqual("li", elements.NavigationItemContainerElement);
            Assert.AreEqual("span", elements.NavigationItemElement);

            Assert.ThrowsException<ArgumentNullException>(() => new NavigationElements(string.Empty, "div"));
            Assert.ThrowsException<ArgumentNullException>(() => new NavigationElements("  ", "div"));
            Assert.ThrowsException<ArgumentNullException>(() => new NavigationElements("div", "div", string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => new NavigationElements("div", "div", "   "));
        }
    }
}
