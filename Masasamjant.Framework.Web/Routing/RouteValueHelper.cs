namespace Masasamjant.Web.Routing
{
    /// <summary>
    /// Provides helper methods to <see cref="IRouteValue"/> interface.
    /// </summary>
    public static class RouteValueHelper
    {
        /// <summary>
        /// Converts <see cref="IEnumerable{IRouteValue}"/> to <see cref="IDictionary{string, object}"/>. If <paramref name="routeValues"/> contains duplicates, 
        /// then value of the first one is set.
        /// </summary>
        /// <param name="routeValues">The route values.</param>
        /// <returns>A <see cref="IDictionary{string, object}"/> of route values.</returns>
        public static IDictionary<string, object> ToDictionary(this IEnumerable<IRouteValue> routeValues)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var routeValue in routeValues) 
            {
                if (!dictionary.ContainsKey(routeValue.Name))
                    dictionary[routeValue.Name] = routeValue.Value;
            }

            return dictionary;
        }
    }
}
