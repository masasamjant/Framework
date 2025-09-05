using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Masasamjant.Web
{
    [TestClass]
    public class UrlHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Action()
        {
            IUrlHelper url = new TestUrlHelper();
            IActionDescriptor actionDescriptor = ActionDescriptorHelper.CreateActionDescriptor("Action", "Controller");
            object routeValues = new { id = "id" };
            var expected = "Controller/Action?id=id";
            var actual = UrlHelper.Action(url, actionDescriptor, routeValues);
            Assert.AreEqual(expected, actual);
            actionDescriptor = ActionDescriptorHelper.CreateActionDescriptor("Action", "Controller", "Area");
            expected = "Area/Controller/Action?id=id";
            actual = UrlHelper.Action(url, actionDescriptor, routeValues);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IsValidUrl()
        {
            Assert.IsFalse(UrlHelper.IsValidUrl(""));
            Assert.IsFalse(UrlHelper.IsValidUrl("/Controller/Action"));
            Assert.IsFalse(UrlHelper.IsValidUrl("~/Controller/Action"));
            Assert.IsTrue(UrlHelper.IsValidUrl("ftp://Controller/Action"));
            Assert.IsFalse(UrlHelper.IsValidUrl("ftp://Controller/Action", [Uri.UriSchemeMailto]));
        }

        [TestMethod]
        public void Test_IsValidHttpUrl()
        {
            Assert.IsFalse(UrlHelper.IsValidHttpUrl("ftp://Controller/Action"));
            Assert.IsTrue(UrlHelper.IsValidHttpUrl("http://Controller/Action"));
            Assert.IsTrue(UrlHelper.IsValidHttpUrl("https://Controller/Action"));
        }

        private class TestUrlHelper : IUrlHelper
        {
            public ActionContext ActionContext => throw new NotImplementedException();

            public string? Action(UrlActionContext actionContext)
            {
                var sb = new StringBuilder();
                var dictionary = actionContext.Values as RouteValueDictionary;

                if (dictionary != null)
                {
                    if (dictionary.ContainsKey("area"))
                    {
                        sb.Append(dictionary["area"]);
                        sb.Append('/');
                    }
                }

                sb.Append(actionContext.Controller);
                sb.Append('/');
                sb.Append(actionContext.Action);

                if (dictionary != null)
                {
                    bool first = true;

                    foreach (var keyValue in dictionary)
                    {
                        if (keyValue.Key == "area")
                            continue;

                        if (first)
                        {
                            sb.Append('?');
                            sb.Append($"{keyValue.Key}={keyValue.Value}");
                            first = false;
                        }
                        else
                        {
                            sb.Append('&');
                            sb.Append($"{keyValue.Key}={keyValue.Value}");
                        }
                    }
                }

                return sb.ToString();
            }

            [return: NotNullIfNotNull("contentPath")]
            public string? Content(string? contentPath)
            {
                throw new NotImplementedException();
            }

            public bool IsLocalUrl([NotNullWhen(true), StringSyntax("Uri")] string? url)
            {
                throw new NotImplementedException();
            }

            public string? Link(string? routeName, object? values)
            {
                throw new NotImplementedException();
            }

            public string? RouteUrl(UrlRouteContext routeContext)
            {
                throw new NotImplementedException();
            }
        }
    }
}
