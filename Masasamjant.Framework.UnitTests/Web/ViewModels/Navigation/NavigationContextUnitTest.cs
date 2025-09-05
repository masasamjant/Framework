namespace Masasamjant.Web.ViewModels.Navigation
{
    [TestClass]
    public class NavigationContextUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var elements = new NavigationElements("div", "div");
            var context = new NavigationContext(elements, Enumerable.Empty<INavigationItem>());
            Assert.IsTrue(ReferenceEquals(elements, context.Elements));
            Assert.IsFalse(context.Items.Any());

            var item = new NavigationItem(text: "Test", url: "http://controller/action");
            var items = new List<INavigationItem>()
            {
                item
            };
            context = new NavigationContext(elements, items);
            Assert.IsTrue(context.Items.Any() && context.Items.Contains(item));
        }

        [TestMethod]
        public void Test_AddItem()
        {
            var elements = new NavigationElements("div", "div");
            var context = new NavigationContext(elements, Enumerable.Empty<INavigationItem>());
            var item = new NavigationItem(text: "Test", url: "http://controller/action");
            var result = context.AddItem(item);
            Assert.IsTrue(ReferenceEquals(context, result));
            Assert.IsTrue(context.Items.Any() && context.Items.Contains(item));
        }

        [TestMethod]
        public void Test_RemoveItem()
        {
            var elements = new NavigationElements("div", "div");
            var context = new NavigationContext(elements, Enumerable.Empty<INavigationItem>());
            var item = new NavigationItem(text: "Test", url: "http://controller/action");
            context.AddItem(item);
            Assert.IsTrue(context.Items.Any() && context.Items.Contains(item));
            var result = context.RemoveItem(item);
            Assert.IsTrue(ReferenceEquals(context, result));
            Assert.IsFalse(context.Items.Any());
        }

        [TestMethod]
        public void Test_ClearItems()
        {
            var elements = new NavigationElements("div", "div");
            var context = new NavigationContext(elements, Enumerable.Empty<INavigationItem>());
            var item = new NavigationItem(text: "Test", url: "http://controller/action");
            context.AddItem(item);
            Assert.IsTrue(context.Items.Any());
            context.ClearItems();
            Assert.IsFalse(context.Items.Any());
        }
    }
}
