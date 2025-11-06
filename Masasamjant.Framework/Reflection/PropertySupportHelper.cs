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

#pragma warning disable CS0618 // Type or member is obsolete
            if (support.HasFlag(PropertySupport.Public) || support.HasFlag(PropertySupport.PublicGetter) || support.HasFlag(PropertySupport.PublicSetter))
                flags |= BindingFlags.Public;

            if (support.HasFlag(PropertySupport.NonPublic) || support.HasFlag(PropertySupport.NonPublicGetter) || support.HasFlag(PropertySupport.NonPublicSetter))
                flags |= BindingFlags.NonPublic;

            if (support.HasFlag(PropertySupport.Getter) || support.HasFlag(PropertySupport.PublicGetter) || support.HasFlag(PropertySupport.NonPublicGetter))
                flags |= BindingFlags.GetProperty;

            if (support.HasFlag(PropertySupport.Setter) || support.HasFlag(PropertySupport.PublicSetter) || support.HasFlag(PropertySupport.NonPublicSetter))
                flags |= BindingFlags.SetProperty;
#pragma warning restore CS0618 // Type or member is obsolete
            
            return flags;
        }
    }
}
