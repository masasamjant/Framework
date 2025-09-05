namespace Masasamjant.Web.ViewModels.Navigation
{
    [TestClass]
    public class NavigationContentBuilderHelperUnitTest : NavigationUnitTest
    {
        [TestMethod]    
        public void Test_Build()
        {
            var elements = new NavigationElements("ul", "li");
            var items = GetNavigationItems();
            var context = new NavigationContext(elements, items);
            var builder = new NavigationContentBuilder();
            var html = NavigationContentBuilderHelper.Build(builder, context);
            Assert.AreEqual(ExpectedUlNavigation, html);
        }

        [TestMethod]
        public void Test_BuildAndWrite()
        {
            var elements = new NavigationElements("ul", "li");
            var items = GetNavigationItems();
            var context = new NavigationContext(elements, items);
            var builder = new NavigationContentBuilder();
            var writer = new StringWriter();
            NavigationContentBuilderHelper.BuildAndWrite(builder, context, writer);
            var html = writer.ToString();
            Assert.AreEqual(ExpectedUlNavigation, html);
        }
    }
}
