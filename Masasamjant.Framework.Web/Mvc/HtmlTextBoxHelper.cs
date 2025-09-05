using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Provides helper methods to create <see cref="IHtmlContent"/> for <c>text</c> input elements.
    /// </summary>
    public static class HtmlTextBoxHelper
    {
        /// <summary>
        /// Create <see cref="IHtmlContent"/> for <c>text</c> input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="maxLength">The maximum value length.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of <c>text</c> input element.</returns>
        public static IHtmlContent HtmlTextBoxFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, int? maxLength = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            var htmlAttributes = html.CreateHtmlAttributesDictionary();

            context?.ApplyAttributes(htmlAttributes);

            if (maxLength.HasValue && maxLength.Value >= 0)
                htmlAttributes["maxlength"] = maxLength.Value;

            if (!string.IsNullOrWhiteSpace(placeholder))
                htmlAttributes["placeholder"] = placeholder;

            return html.TextBoxFor(expression, htmlAttributes);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for <c>password</c> input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="maxLength">The maximum value length.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of <c>password</c> input element.</returns>
        public static IHtmlContent HtmlPasswordFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, int? maxLength = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            var htmlAttributes = html.CreateHtmlAttributesDictionary();

            context?.ApplyAttributes(htmlAttributes);

            if (maxLength.HasValue && maxLength.Value >= 0)
                htmlAttributes["maxlength"] = maxLength.Value;

            if (!string.IsNullOrWhiteSpace(placeholder))
                htmlAttributes["placeholder"] = placeholder;

            return html.PasswordFor(expression, htmlAttributes);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for <c>textarea</c> input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="maxLength">The maximum value length.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of <c>textarea</c> input element.</returns>
        public static IHtmlContent HtmlTextAreaFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, int? maxLength = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            var htmlAttributes = html.CreateHtmlAttributesDictionary();

            context?.ApplyAttributes(htmlAttributes);

            if (maxLength.HasValue && maxLength.Value >= 0)
                htmlAttributes["maxlength"] = maxLength.Value;

            if (!string.IsNullOrWhiteSpace(placeholder))
                htmlAttributes["placeholder"] = placeholder;

            return html.TextAreaFor(expression, htmlAttributes);
        }

        /// <summary>
        /// Create <see cref="IHtmlContent"/> for <c>textarea</c> input element.
        /// </summary>
        /// <typeparam name="TModel">The type of model.</typeparam>
        /// <typeparam name="TResult">The type of result.</typeparam>
        /// <param name="html">The <see cref="IHtmlHelper{TModel}"/>.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="rows">The number of rows.</param>
        /// <param name="columns">The number of columns.</param>
        /// <param name="maxLength">The maximum value length.</param>
        /// <param name="placeholder">The placeholder value.</param>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <returns>A <see cref="IHtmlContent"/> of <c>textarea</c> input element.</returns>
        public static IHtmlContent HtmlTextAreaFor<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, int rows, int columns, int? maxLength = null, string? placeholder = null, HtmlHelperContext? context = null)
        {
            var htmlAttributes = html.CreateHtmlAttributesDictionary();

            context?.ApplyAttributes(htmlAttributes);

            if (maxLength.HasValue && maxLength.Value >= 0)
                htmlAttributes["maxlength"] = maxLength.Value;

            if (!string.IsNullOrWhiteSpace(placeholder))
                htmlAttributes["placeholder"] = placeholder;

            return html.TextAreaFor(expression, rows, columns, htmlAttributes);
        }
    }
}
