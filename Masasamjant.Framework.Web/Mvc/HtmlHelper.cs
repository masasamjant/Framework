using Masasamjant.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Provides general HTML helper methods.
    /// </summary>
    public static class HtmlHelper
    {
        /// <summary>
        /// Value for "name" attribute when value is not provided.
        /// </summary>
        public const string NoName = "none";

        /// <summary>
        /// Combine HTML attributes from <paramref name="otherAttributes"/> to <paramref name="currentAttributes"/>.
        /// </summary>
        /// <param name="currentAttributes">The current HTML attributes.</param>
        /// <param name="otherAttributes">The other HTML attributes.</param>
        /// <param name="replace"><c>true</c> to replace value in <paramref name="currentAttributes"/> with value from <paramref name="otherAttributes"/> if contains duplicated; <c>false</c> to ignore.</param>
        /// <returns>A combined HTML attributes.</returns>
        public static IDictionary<string, object> CombineAttributes(IDictionary<string, object> currentAttributes, IDictionary<string, object>? otherAttributes, bool replace = false)
        {
            if (otherAttributes == null || otherAttributes.Count == 0)
            {
                return new Dictionary<string, object>(currentAttributes);
            }

            var duplicateBehavior = replace ? DuplicateBehavior.Replace : DuplicateBehavior.Ignore;
            return DictionaryHelper.Combine(currentAttributes, otherAttributes, duplicateBehavior);
        }

        /// <summary>
        /// Create <see cref="HtmlHelperContext"/> instance.
        /// </summary>
        /// <param name="html">The <see cref="IHtmlHelper"/>.</param>
        /// <param name="enabled"><c>true</c>  if element is enabled and does not have "disabled" attribute; <c>false</c> otherwise.</param>
        /// <param name="id">The value of "id" attribute.</param>
        /// <param name="title">The value of "title" attribute.</param>
        /// <param name="css">The value of "class" attribute.</param>
        /// <param name="emptyOptionLabel">The text of empty "option" element.</param>
        /// <param name="htmlAttributes">The custom attributes.</param>
        /// <param name="replaceCurrentHtmlAttributes"><c>true</c> if attributes in <paramref name="htmlAttributes"/> replace current attributes; <c>false</c> otherwise.</param>
        /// <returns>A <see cref="HtmlHelperContext"/>.</returns>
        public static HtmlHelperContext Context(this IHtmlHelper html, bool enabled = true, string? id = null, string? title = null, string? css = null, string? emptyOptionLabel = null, 
            IDictionary<string, object>? htmlAttributes = null, bool replaceCurrentHtmlAttributes = false)
            => new HtmlHelperContext(enabled, id, title, css, emptyOptionLabel, htmlAttributes, replaceCurrentHtmlAttributes);

        /// <summary>
        /// Change <see cref="HtmlHelperContext.IsEnabled"/> value.
        /// </summary>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>A <paramref name="context"/>.</returns>
        public static HtmlHelperContext UseEnabled(this HtmlHelperContext context, bool value)
        {
            context.IsEnabled = value;
            return context;
        }

        /// <summary>
        /// Change <see cref="HtmlHelperContext.Id"/> value.
        /// </summary>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>A <paramref name="context"/>.</returns>
        public static HtmlHelperContext UseId(this HtmlHelperContext context, string? value)
        {
            context.Id = value;
            return context;
        }

        /// <summary>
        /// Change <see cref="HtmlHelperContext.Title"/> value.
        /// </summary>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>A <paramref name="context"/>.</returns>
        public static HtmlHelperContext UseTitle(this HtmlHelperContext context, string? value)
        {
            context.Title = value;
            return context;
        }

        /// <summary>
        /// Change <see cref="HtmlHelperContext.CssClass"/> value.
        /// </summary>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>A <paramref name="context"/>.</returns>
        public static HtmlHelperContext UseCssClass(this HtmlHelperContext context, string? value)
        {
            context.CssClass = value;
            return context;
        }

        /// <summary>
        /// Change <see cref="HtmlHelperContext.EmptyOptionLabel"/> value.
        /// </summary>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>A <paramref name="context"/>.</returns>
        public static HtmlHelperContext UseEmptyOptionLabel(this HtmlHelperContext context, string? value)
        {
            context.EmptyOptionLabel = value;
            return context;
        }

        /// <summary>
        /// Change <see cref="HtmlHelperContext.HtmlAttributes"/> value.
        /// </summary>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <param name="value">The value to set or <c>null</c> to set empty.</param>
        /// <returns>A <paramref name="context"/>.</returns>
        public static HtmlHelperContext UseHtmlAttributes(this HtmlHelperContext context, IDictionary<string, object>? value)
        {
            context.HtmlAttributes = value ?? new Dictionary<string, object>();
            return context;
        }

        /// <summary>
        /// Change <see cref="HtmlHelperContext.ReplaceCurrentHtmlAttributes"/> value.
        /// </summary>
        /// <param name="context">The <see cref="HtmlHelperContext"/>.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>A <paramref name="context"/>.</returns>
        public static HtmlHelperContext UseReplaceCurrentHtmlAttributes(this HtmlHelperContext context, bool value)
        {
            context.ReplaceCurrentHtmlAttributes = value;
            return context;
        }

        internal static IDictionary<string, object> CreateHtmlAttributesDictionary(this IHtmlHelper _)
            => new Dictionary<string, object>();
    }
}
