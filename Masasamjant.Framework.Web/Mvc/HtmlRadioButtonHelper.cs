using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Provides helper methods to create <see cref="IHtmlContent"/> for <c>radio</c> input elements.
    /// </summary>
    public static class HtmlRadioButtonHelper
    {
        public static IHtmlContent HtmlRadioButtonFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, object? value, string? onchange = null, HtmlHelperContext? context = null)
        {
            var htmlAttributes = html.CreateHtmlAttributesDictionary();
            context?.ApplyAttributes(htmlAttributes);
            if (!string.IsNullOrWhiteSpace(onchange))
                htmlAttributes["onchange"] = onchange;
            return html.RadioButtonFor(expression, value, htmlAttributes);
        }

        public static IHtmlContent HtmlRadioButton(this IHtmlHelper html, string? name, object? value, bool? isChecked, string? onchange = null, HtmlHelperContext? context = null)
        {
            var htmlAttributes = html.CreateHtmlAttributesDictionary();
            context?.ApplyAttributes(htmlAttributes);

            if (!string.IsNullOrWhiteSpace(onchange))
                htmlAttributes["onchange"] = onchange;

            return html.RadioButton(name, value, isChecked, htmlAttributes);
        }
    }
}
