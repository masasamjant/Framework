namespace Masasamjant.Web.Mvc
{
    [TestClass]
    public class HtmlHelperContextUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var context = new HtmlHelperContext();
            Assert.IsNull(context.Id);
            Assert.IsTrue(context.IsEnabled);
            Assert.IsNull(context.Title);
            Assert.IsNull(context.CssClass);
            Assert.IsNull(context.EmptyOptionLabel);
            Assert.AreEqual(0, context.HtmlAttributes.Count);
            Assert.IsFalse(context.ReplaceCurrentHtmlAttributes);

            var htmlAttributes = new Dictionary<string, object>()
            {
                { "style", "color:blue;" }
            };

            context = new HtmlHelperContext(false, "id", "title", "css", "empty", htmlAttributes, true);
            Assert.IsFalse(context.IsEnabled);
            Assert.AreEqual("id", context.Id);
            Assert.AreEqual("title", context.Title);
            Assert.AreEqual("css", context.CssClass);
            Assert.AreEqual("empty", context.EmptyOptionLabel);
            Assert.IsTrue(context.HtmlAttributes.Count == 1 && context.HtmlAttributes.ContainsKey("style") && Equals(htmlAttributes["style"], "color:blue;"));
            Assert.IsTrue(context.ReplaceCurrentHtmlAttributes);
        }

        [TestMethod]
        public void Test_ApplyAttributes()
        {
            var htmlAttributes = new Dictionary<string, object>();
            var context = new HtmlHelperContext();
            context.ApplyAttributes(htmlAttributes);
            Assert.AreEqual(0, htmlAttributes.Count);

            htmlAttributes = new Dictionary<string, object>()
            {
                { "style", "color:blue;" }
            };

            context = new HtmlHelperContext(false, "id", "title", "css", "empty", null, true);
            context.ApplyAttributes(htmlAttributes);
            Assert.IsTrue(htmlAttributes.ContainsKey("disabled"));
            Assert.AreEqual("id", htmlAttributes["id"]);
            Assert.AreEqual("title", htmlAttributes["title"]);
            Assert.AreEqual("css", htmlAttributes["class"]);
            Assert.AreEqual("color:blue;", htmlAttributes["style"]);

            htmlAttributes = new Dictionary<string, object>()
            {
                { "style", "color:blue;" }
            };

            context.IsEnabled = true;
            context.HtmlAttributes["style"] = "color:red;";
            context.ApplyAttributes(htmlAttributes);
            Assert.IsFalse(htmlAttributes.ContainsKey("disabled"));
            Assert.AreEqual("id", htmlAttributes["id"]);
            Assert.AreEqual("title", htmlAttributes["title"]);
            Assert.AreEqual("css", htmlAttributes["class"]);
            Assert.AreEqual("color:red;", htmlAttributes["style"]);

            htmlAttributes = new Dictionary<string, object>()
            {
                { "style", "color:blue;" }
            };

            context.ReplaceCurrentHtmlAttributes = false;
            context.ApplyAttributes(htmlAttributes);
            Assert.IsFalse(htmlAttributes.ContainsKey("disabled"));
            Assert.AreEqual("id", htmlAttributes["id"]);
            Assert.AreEqual("title", htmlAttributes["title"]);
            Assert.AreEqual("css", htmlAttributes["class"]);
            Assert.AreEqual("color:blue;", htmlAttributes["style"]);
        }
    }
}
