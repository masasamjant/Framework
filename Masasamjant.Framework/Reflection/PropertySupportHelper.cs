using System.Reflection;

namespace Masasamjant.Reflection
{
    /// <summary>
    /// Provides helper methods for <see cref="PropertySupport"/> enumeration.
    /// </summary>
    public static class PropertySupportHelper
    {
        /// <summary>
        /// Creates <see cref="BindingFlags"/> based on specified <see cref="PropertySupport"/> value.
        /// </summary>
        /// <param name="support">The required property support.</param>
        /// <param name="instance"><c>true</c> if binding should be for instance property; <c>false</c> otherwise.</param>
        /// <returns>A <see cref="BindingFlags"/>.</returns>
        public static BindingFlags GetBindingFlags(this PropertySupport support, bool instance)
        {
            if (support.Equals(PropertySupport.None))
                return BindingFlags.Default;

            BindingFlags flags = instance ? BindingFlags.Instance : BindingFlags.Static;

            if (support.HasFlag(PropertySupport.Public))
                flags |= BindingFlags.Public;

            if (support.HasFlag(PropertySupport.NonPublic))
                flags |= BindingFlags.NonPublic;

            if (support.HasFlag(PropertySupport.Getter))
                flags |= BindingFlags.GetProperty;

            if (support.HasFlag(PropertySupport.Setter))
                flags |= BindingFlags.SetProperty;
            
            return flags;
        }
    }
}
