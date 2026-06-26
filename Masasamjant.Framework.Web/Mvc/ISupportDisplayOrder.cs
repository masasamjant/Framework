namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents view item that supports display order by integer value.
    /// </summary>
    public interface ISupportDisplayOrder
    {
        /// <summary>
        /// Gets the display order. Smaller value means higher order.
        /// </summary>
        int DisplayOrder { get; }
    }
}
