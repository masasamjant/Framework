namespace Masasamjant.Web.Routing
{
    /// <summary>
    /// Represents provider of routes.
    /// </summary>
    public interface IRouteProvider
    {
        /// <summary>
        /// Gets the route specified by name.
        /// </summary>
        /// <param name="name">The route name.</param>
        /// <returns>A route specified by name or empty string.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="name"/> is <c>null</c>, empty, or only whitespace.</exception>
        string GetRoute(string name);
    }
}
