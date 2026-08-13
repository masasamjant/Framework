using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Represents equality comparer of character maps that check only if maps have same mappings.
    /// </summary>
    public class CharacterMapEqualityComparer : IEqualityComparer<CharacterMap>
    {
        /// <summary>
        /// Check if character mappings both contains same mappings or are empty.
        /// </summary>
        /// <param name="x">The first character map.</param>
        /// <param name="y">The second character map.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="x"/> and <paramref name="y"/> has same mappings or both are empty.
        /// -or-
        /// <c>true</c> if <paramref name="x"/> and <paramref name="y"/> are both <c>null</c>.
        /// -or-
        /// <c>false</c> otherwise.
        /// </returns>
        public bool Equals(CharacterMap? x, CharacterMap? y)
        {
            if (x != null && y != null)
            {
                if (x.Count != y.Count) 
                    return false;

                if (x.Count == 0)
                    return y.Count == 0;

                foreach (var mapping in x)
                {
                    if (!y.Contains(mapping))
                        return false;
                }

                return true;
            }

            return x is null && y is null;
        }

        /// <summary>
        /// Gets hash code for specified character map.
        /// </summary>
        /// <param name="obj">The character map.</param>
        /// <returns>A hash code for <paramref name="obj"/>.</returns>
        public int GetHashCode([DisallowNull] CharacterMap obj)
        {
            int code = 0;

            foreach (var mapping in obj)
                code ^= mapping.GetHashCode();

            return code;
        }
    }
}
