using System.Collections;

namespace Masasamjant
{
    /// <summary>
    /// Represents one-to-one character map.
    /// </summary>
    public sealed class CharacterMap : IEnumerable<CharacterMapping>
    {
        private readonly List<CharacterMapping> mappings;

        /// <summary>
        /// Initializes new instance of the <see cref="CharacterMap"/> class with specified characters.
        /// </summary>
        /// <param name="characters"></param>
        public CharacterMap(IDictionary<char, char> characters)
            : this()
        {
            try
            {
                foreach (var keyValue in characters)
                {
                    Add(keyValue.Key, keyValue.Value);
                }
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException("The dictionary contains character(s) mapped more than once.", nameof(characters), exception);
            }
        }

        /// <summary>
        /// Initializes new empty instance of the <see cref="CharacterMap"/> class.
        /// </summary>
        public CharacterMap()
        {
            mappings = new List<CharacterMapping>();
        }

        /// <summary>
        /// Gets the mapped source characters.
        /// </summary>
        public IEnumerable<char> Sources
        {
            get { return Mappings.Select(x => x.Source); }
        }

        /// <summary>
        /// Gets the mapped destination characters.
        /// </summary>
        public IEnumerable<char> Destinations
        {
            get { return Mappings.Select(x => x.Destination); }
        }

        /// <summary>
        /// Gets all <see cref="CharacterMapping"/> mappings.
        /// </summary>
        public IEnumerable<CharacterMapping> Mappings
        {
            get
            {
                foreach (var mapping in mappings)
                    yield return mapping;
            }
        }

        /// <summary>
        /// Gets the count of mappings.
        /// </summary>
        public int Count
        {
            get { return mappings.Count; }
        }

        /// <summary>
        /// Add mapping between specified source and destination characters.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <param name="destination">The destination character.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="source"/> is already mapped as source or destination characters.
        /// -or-
        /// If <paramref name="destination"/> is already mapped as source or destination character.
        /// </exception>
        public CharacterMapping Add(char source, char destination)
        {
            var mapping = new CharacterMapping(source, destination);

            if (Contains(mapping))
                return mapping;

            if (Mappings.Any(x => x.Source == source))
                throw new ArgumentException("The character is already mapped as source.", nameof(source));

            if (Mappings.Any(x => x.Destination == source))
                throw new ArgumentException("The character is already mapped as destination.", nameof(source));

            if (Mappings.Any(x => x.Source == destination))
                throw new ArgumentException("The character is already mapped as source.", nameof(destination));

            if (Mappings.Any(x => x.Destination == destination))
                throw new ArgumentException("The character is already mapped as destination.", nameof(destination));

            mappings.Add(mapping);

            return mapping;
        }

        /// <summary>
        /// Remove all mappings.
        /// </summary>
        public void Clear()
        {
            mappings.Clear();
        }

        /// <summary>
        /// Remove mapping between specified source and destination character, 
        /// if such mapping exist.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <param name="destination">The destination character.</param>
        /// <returns><c>true</c> if mapping existed and removed; <c>false</c> otherwise.</returns>
        public bool Remove(char source, char destination)
        {
            return Remove(new CharacterMapping(source, destination));
        }

        /// <summary>
        /// Remove specified <see cref="CharacterMapping"/> from map.
        /// </summary>
        /// <param name="mapping">The <see cref="CharacterMapping"/> to remove.</param>
        /// <returns><c>true</c> if mapping existed and removed; <c>false</c> otherwise.</returns>
        public bool Remove(CharacterMapping mapping)
        {
            return mappings.Remove(mapping);
        }

        /// <summary>
        /// Check if map contains mapping between specified source and destination characters.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <param name="destination">The destination character.</param>
        /// <returns><c>true</c> if mapping exist; <c>false</c> otherwise.</returns>
        public bool Contains(char source, char destination)
        {
            return Contains(new CharacterMapping(source, destination));
        }

        /// <summary>
        /// Check if map constains specified <see cref="CharacterMapping"/>.
        /// </summary>
        /// <param name="mapping">The <see cref="CharacterMapping"/>.</param>
        /// <returns><c>true</c> if contains <paramref name="mapping"/>; <c>false</c> otherwise.</returns>
        public bool Contains(CharacterMapping mapping)
        {
            return mappings.Contains(mapping);
        }

        /// <summary>
        /// Gets <see cref="CharacterMapping"/> where specified charecter is either source or destination.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns>A <see cref="CharacterMapping"/> or <c>null</c> if no mapping where <paramref name="c"/> is source or destination.</returns>
        public CharacterMapping? GetMapping(char c)
        {
            var mappings = Mappings.Where(x => x.Source == c || x.Destination == c).ToArray();

            if (mappings.Length == 0)
                return null;
            else if (mappings.Length == 1)
                return mappings[0];
            else
                throw new InvalidOperationException($"More than one mapping found for {c}.");
        }

        /// <summary>
        /// Gets <see cref="IEnumerator{CharacterMapping}"/> to get all mappings.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{CharacterMapping}"/>.</returns>
        public IEnumerator<CharacterMapping> GetEnumerator()
        {
            foreach (var mapping in mappings)
                yield return mapping;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
