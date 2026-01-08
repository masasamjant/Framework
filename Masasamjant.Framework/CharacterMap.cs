using System.Collections;

namespace Masasamjant
{
    /// <summary>
    /// Represents one-to-one character map that accepts any character mappings.
    /// </summary>
    public class CharacterMap : IEnumerable<CharacterMapping>, ICloneable
    {
        private readonly List<CharacterMapping> mappings;

        /// <summary>
        /// Initializes new instance of the <see cref="CharacterMap"/> class with specified characters.
        /// </summary>
        /// <param name="characters">The mapped characters.</param>
        /// <exception cref="ArgumentException">If <paramref name="characters"/> contains invalid character mappings.</exception>
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
            catch (CharacterMappingException exception)
            {
                throw new ArgumentException("The dictionary contains invalid character mapping.", nameof(characters), exception);
            }
            catch (Exception exception)
            {
                throw new ArgumentException("The mapping from specified characters failed.", nameof(characters), exception);
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
        /// Gets a value indicating whether this map is read-only.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Add mapping between specified source and destination characters.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <param name="destination">The destination character.</param>
        /// <exception cref="CharacterMappingException">
        /// If <paramref name="source"/> is already mapped as source character.
        /// -or-
        /// If <paramref name="destination"/> is already mapped as destination character.
        /// </exception>
        /// <exception cref="InvalidOperationException">If <see cref="IsReadOnly"/> is <c>true</c>.</exception>
        public CharacterMapping Add(char source, char destination)
        {
            CheckReadOnly();

            ValidateCharacters(source, destination);

            var mapping = new CharacterMapping(source, destination);

            if (Contains(mapping))
                return mapping;

            if (Mappings.Any(x => x.Source == source))
                throw new CharacterMappingException(source, destination, "The source character is already mapped as source.");

            if (Mappings.Any(x => x.Destination == destination))
                throw new CharacterMappingException(source, destination, "The destination character is already mapped as destination.");

            mappings.Add(mapping);

            return mapping;
        }

        /// <summary>
        /// Remove all mappings.
        /// </summary>
        /// <exception cref="InvalidOperationException">If <see cref="IsReadOnly"/> is <c>true</c>.</exception>
        public void Clear()
        {
            CheckReadOnly();
            mappings.Clear();
        }

        /// <summary>
        /// Remove mapping between specified source and destination character, 
        /// if such mapping exist.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <param name="destination">The destination character.</param>
        /// <returns><c>true</c> if mapping existed and removed; <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">If <see cref="IsReadOnly"/> is <c>true</c>.</exception>
        public bool Remove(char source, char destination)
        {
            return Remove(new CharacterMapping(source, destination));
        }

        /// <summary>
        /// Remove specified <see cref="CharacterMapping"/> from map.
        /// </summary>
        /// <param name="mapping">The <see cref="CharacterMapping"/> to remove.</param>
        /// <returns><c>true</c> if mapping existed and removed; <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">If <see cref="IsReadOnly"/> is <c>true</c>.</exception>
        public bool Remove(CharacterMapping mapping)
        {
            CheckReadOnly();
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
        /// Gets the destination character for specified source character.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <returns>A destination character or <c>null</c>, if <paramref name="source"/> not mapped.</returns>
        public char? GetDestination(char source)
        {
            var mapping = GetMapping(source);
            if (mapping.HasValue && mapping.Value.Source == source)
                return mapping.Value.Destination;
            else
                return null;
        }

        /// <summary>
        /// Gets the source character for specified destination character.
        /// </summary>
        /// <param name="destination">The destination character.</param>
        /// <returns>A source character or <c>null</c>, if <paramref name="destination"/> not mapped.</returns>
        public char? GetSource(char destination)
        {
            var mapping = GetMapping(destination);
            if (mapping.HasValue && mapping.Value.Destination == destination)
                return mapping.Value.Source;
            else
                return null;
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

        /// <summary>
        /// Set this character map as read-only.
        /// </summary>
        /// <returns>A current instance.</returns>
        public CharacterMap SetReadOnly()
        {
            IsReadOnly = true;
            return this;
        }

        /// <summary>
        /// Convert this character map to dictionary where key is source character and value is destination character.
        /// </summary>
        /// <returns>A dictionary of mapped characters.</returns>
        public Dictionary<char, char> ToDictionary()
        {
            var dict = new Dictionary<char, char>();
            foreach (var mapping in Mappings)
            {
                dict[mapping.Source] = mapping.Destination;
            }
            return dict;
        }

        /// <summary>
        /// Create copy from this character map.
        /// </summary>
        /// <returns>A copy from this character map.</returns>
        /// <remarks>If this map is in read-only state, the copy is not.</remarks>
        public virtual CharacterMap Clone()
        {
            var map = new CharacterMap();
            foreach (var mapping in this)
                map.Add(mapping.Source, mapping.Destination);
            return map;
        }

        /// <summary>
        /// Check if object instance is same type of character map and has same mappings with this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is same type of character map with same mappings; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is CharacterMap map && GetType().Equals(map.GetType()))
            {
                if (Count != map.Count)
                    return false;

                if (Count == 0)
                    return map.Count == 0;

                foreach (var mapping in Mappings)
                {
                    if (!map.Contains(mapping))
                        return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            int code = GetType().GetHashCode();

            foreach (var mapping in Mappings)
                code ^= mapping.GetHashCode();

            return code;
        }

        /// <summary>
        /// Derived classes can override this method to validate source and destination characters.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <param name="destination">The destination character.</param>
        /// <exception cref="CharacterMappingException">If source or destination character is not valid.</exception>
        protected virtual void ValidateCharacters(char source, char destination)
        {
            return;
        }

        private void CheckReadOnly()
        {
            if (IsReadOnly)
                throw new InvalidOperationException("The character map is read-only.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
