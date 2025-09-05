using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Provides helper methods to create <see cref="IHtmlContent"/> for <c>label</c> elements.
    /// </summary>
    public static class HtmlLabelHelper
    {
        public static IHtmlContent HtmlLabelFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, string text, HtmlHelperContext? context = null)
        {
            var htmlAttributes = html.CreateHtmlAttributesDictionary();
            context?.ApplyAttributes(htmlAttributes);
            return html.LabelFor(expression, text, htmlAttributes);
        }

        public static IHtmlContent HtmlLabel(this IHtmlHelper html, string text, string target, HtmlHelperContext? context = null)
        {
            var htmlAttributes = html.CreateHtmlAttributesDictionary();
            context?.ApplyAttributes(htmlAttributes);
            return html.Label(target, target, htmlAttributes);
        }
    }
}
