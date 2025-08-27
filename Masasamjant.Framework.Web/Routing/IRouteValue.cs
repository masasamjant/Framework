namespace Masasamjant.Web.Routing
{
    /// <summary>
    /// Represents named route value.
    /// </summary>
    public interface IRouteValue
    {
        /// <summary>
        /// Gets route value name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets route value.
        /// </summary>
        object Value { get; }
    }
}
