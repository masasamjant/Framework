using Microsoft.AspNetCore.Mvc.Rendering;

namespace Masasamjant.Web
{
    [TestClass]
    public class HtmlContentHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_ToHtmlString()
        {
            var builder = new TagBuilder("div");
            builder.AddCssClass("cls");
            var inner = new TagBuilder("span");
            inner.InnerHtml.Append("Test");
            builder.InnerHtml.AppendHtml(inner);
            var expected = "<div class=\"cls\"><span>Test</span></div>";
            var actual = HtmlContentHelper.ToHtmlString(builder);
            Assert.AreEqual(expected, actual);
        }
    }
}
