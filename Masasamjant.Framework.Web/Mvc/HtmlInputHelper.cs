using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Provides helper methods to create <see cref="IHtmlContent"/> for difference input elements.
    /// </summary>
    public static class HtmlInputHelper
    {
        /// <summary>
        /// Create <see cref="IHtmlContent"/> for <c>color</c> input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of color input element.</returns>
        public static IHtmlContent HtmlColorFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), "color", onchange, placeholder, context);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for date input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of date input element.</returns>
        public static IHtmlContent HtmlDateFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), "date", onchange, placeholder, context);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for datetime input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="local"><c>true</c> to create datetime-local input; <c>false</c> to create datetime input.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of datetime input element.</returns>
        public static IHtmlContent HtmlDateTimeFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, bool local =  false, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), local ? "datetime-local" : "datetime", onchange, placeholder, context);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for file input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of file input element.</returns>
        public static IHtmlContent HtmlFileFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), "file", onchange, placeholder, context);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for image input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of image input element.</returns>
        public static IHtmlContent HtmlImageFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), "image", onchange, placeholder, context);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for month input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of month input element.</returns>
        public static IHtmlContent HtmlMonthFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), "month", onchange, placeholder, context);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for time input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of time input element.</returns>
        public static IHtmlContent HtmlTimeFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), "time", onchange, placeholder, context);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for email input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="maxlength">The max length.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of email input element.</returns>
        public static IHtmlContent HtmlEmailFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, int? maxlength = null, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), "email", onchange, placeholder, context, maxlength);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for number input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="maxlength">The max length.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of number input element.</returns>
        public static IHtmlContent HtmlNumberFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, int? maxlength = null, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), "number", onchange, placeholder, context, maxlength);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for tel input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="maxlength">The max length.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of tel input element.</returns>
        public static IHtmlContent HtmlTelephoneFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, int? maxlength = null, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), "tel", onchange, placeholder, context, maxlength);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for range input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="step">The step.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of range input element.</returns>
        public static IHtmlContent HtmlRangeFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, int min, int max, int step = 1, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            var htmlAttributes = html.CreateHtmlAttributesDictionary();
            step = Math.Max(1, step);

            if (min > max)
                ObjectHelper.Swap(ref min, ref max);

            if (context != null && context.HtmlAttributes != null)
            {
                context.HtmlAttributes.Remove("min");
                context.HtmlAttributes.Remove("max");
                context.HtmlAttributes.Remove("step");
            }

            htmlAttributes["min"] = min;
            htmlAttributes["max"] = max;
            htmlAttributes["step"] = step;

            return HtmlInputFor(html, expression, htmlAttributes, "range", onchange, placeholder, context);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for url input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="maxlength">The max length.</param>
        /// <param name="onchange">The name of onchange client handler.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of url input element.</returns>
        public static IHtmlContent HtmlUrlFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, int? maxLength = null, string? onchange = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            return HtmlInputFor(html, expression, html.CreateHtmlAttributesDictionary(), "url", onchange, placeholder, context, maxLength);
        }

        private static IHtmlContent HtmlInputFor<TModel, TResult>(IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, IDictionary<string, object> htmlAttributes, string type, string? onchange, string? placeholder, HtmlHelperContext? context, int? maxLength = null)
        {
            context?.ApplyAttributes(htmlAttributes);

            if (!string.IsNullOrWhiteSpace(onchange))
                htmlAttributes["onchange"]  = onchange;

            if (!string.IsNullOrWhiteSpace(placeholder))
                htmlAttributes["placeholder"] = placeholder;

            if (maxLength.HasValue && maxLength.Value >= 0)
                htmlAttributes["maxlength"] = maxLength.Value;

            htmlAttributes["type"] = type;

            return html.TextBoxFor(expression, htmlAttributes);
        }
    }
}
