namespace Masasamjant.Web.ViewModels.Navigation
{
    [TestClass]
    public class NavigationContentBuilderUnitTest : NavigationUnitTest
    {
        [TestMethod]
        public void Test_Build()
        {
            var elements = new NavigationElements("ul", "li");
            var items = GetNavigationItems();
            var context = new NavigationContext(elements, items);
            var builder = new NavigationContentBuilder();
            var content = builder.Build(context);
            var html = content.ToHtmlString();
            Assert.AreEqual(ExpectedUlNavigation, html);

            elements = new NavigationElements("div", string.Empty);
            context = new NavigationContext(elements, items);
            content = builder.Build(context);
            html = content.ToHtmlString();
            Assert.AreEqual(ExpectedDivNavigation, html);
        }
    }
}
