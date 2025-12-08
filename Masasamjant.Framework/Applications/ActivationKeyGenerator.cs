namespace Masasamjant.Applications
{
    /// <summary>
    /// Represents activation key generator.
    /// </summary>
    public sealed class ActivationKeyGenerator : IActivationKeyGenerator
    {
        private const int MinInitialSeed = 25674500;
        private const int MaxInitialSeed = 1995674500;
        private readonly Dictionary<char, char> numberToLetterMap;
        private readonly Dictionary<char, char> numberToNumberMap;
        private readonly Random random;

        /// <summary>
        /// Initializes new instance of the <see cref="ActivationKeyGenerator"/> class with <see cref="ActivationKeyProperties.Default"/> properties.
        /// </summary>
        public ActivationKeyGenerator()
            : this(ActivationKeyProperties.Default)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="ActivationKeyGenerator"/> class with specified properties.
        /// </summary>
        /// <param name="keyProperties">The activation key properties.</param>
        /// <remarks>Reads character mappings from properties so changing them after this do not have effect to this instance.</remarks>
        public ActivationKeyGenerator(IActivationKeyProperties keyProperties)
        {
            this.random = RandomHelper.CreateRandom();
            this.KeyProperties = keyProperties;
            this.numberToLetterMap = keyProperties.NumberToLetterMap.ToDictionary();
            this.numberToNumberMap = keyProperties.NumberToNumberMap.ToDictionary();
        }

        /// <summary>
        /// Gets the activation key properties.
        /// </summary>
        public IActivationKeyProperties KeyProperties { get; }

        /// <summary>
        /// Creates an activation key without prefix.
        /// </summary>
        /// <param name="previousSeed">The seed of the previous activation key that was created. Can be <c>null</c>, if first activation key is generated.</param>
        /// <returns>A activation key.</returns>
        public ActivationKey CreateActivationKey(ActivationKeySeed? previousSeed)
        {
            return CreateActivationKeyValue(null, previousSeed);
        }

        /// <summary>
        /// Creates an activation key with specified prefix.
        /// </summary>
        /// <param name="prefix">The key prefix.</param>
        /// <param name="previousSeed">The seed of the previous activation key that was created. Can be <c>null</c>, if first activation key is generated.</param>
        /// <returns>A activation key.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="prefix"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <see cref="ActivationKeyProperties.UsePrefix"/> is <c>true</c> and value of <paramref name="prefix"/> length is not equal to <see cref="ActivationKeyProperties.PrefixLength"/>.</exception>
        public ActivationKey CreateActivationKey(string prefix, ActivationKeySeed? previousSeed)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentNullException(nameof(prefix), "The value cannot be empty or only whitespace.");

            if (KeyProperties.UsePrefix && prefix.Length != KeyProperties.PrefixLength)
                throw new ArgumentOutOfRangeException(nameof(prefix), prefix.Length, $"The value must be exactly {KeyProperties.PrefixLength} characters.");

            return CreateActivationKeyValue(prefix, previousSeed);
        }

        private ActivationKey CreateActivationKeyValue(string? prefix, ActivationKeySeed? previousSeed)
        {
            var value = CalculateNextValue(previousSeed, out var currentSeed);
            var key = ConcateFinalKey(value, prefix);
            return new ActivationKey(key, currentSeed);
        }

        private string CalculateNextValue(ActivationKeySeed? previousSeed, out ActivationKeySeed currentSeed)
        {
            int componentsLength = KeyProperties.ComponentCount * KeyProperties.ComponentLength;
            currentSeed = GetNextSeed(previousSeed);
            string value = currentSeed.Value.ToString();
            if (value.Length < componentsLength)
                value += random.GetString(componentsLength - value.Length, CharHelper.AsciiDigits);

            var components = SplitToComponents(value);
            
            BuildKeyComponents(components);

            return string.Join(KeyProperties.ComponentSeparator, components);
        }

        private void BuildKeyComponents(string[] components)
        {
            for (int index = 0; index < components.Length; index++)
            {
                var component = components[index];
                int j = random.Next(0, component.Length);
                int k = random.Next(0, component.Length);
                var arr = component.ToCharArray();
                if (j == k)
                    arr[j] = GetMappedLetter(arr[j]);
                else
                {
                    arr[j] = GetMappedLetter(arr[j]);
                    arr[k] = GetMappedNumber(arr[k]);
                }

                components[index] = new string(arr);
            }
        }

        private ActivationKeySeed GetNextSeed(ActivationKeySeed? currentSeed)
        {
            if (!currentSeed.HasValue || currentSeed.Value.Value < MinInitialSeed)
            {
                decimal initial = random.Next(MinInitialSeed, MaxInitialSeed);
                return new ActivationKeySeed(initial);
            }
            else
                return currentSeed.Value.Next();
        }

        private string ConcateFinalKey(string value, string? prefix)
        {
            if (string.IsNullOrEmpty(prefix) || !KeyProperties.UsePrefix)
                return value;

            return string.Join(KeyProperties.ComponentSeparator, prefix, value);
        }

        private string[] SplitToComponents(string value)
        {
            string component = "";

            var components = new List<string>(KeyProperties.ComponentCount);

            foreach (var c in value)
            {
                if (component.Length == KeyProperties.ComponentLength)
                {
                    components.Add(component);
                    
                    if (components.Count == KeyProperties.ComponentCount)
                        break;

                    component = "";
                }

                component += c.ToString();
            }

            if (component.Length > 0 && components.Count < KeyProperties.ComponentCount)
                components.Add(component);

            return components.ToArray();
        }

        private char GetMappedLetter(char c)
        {
            if (numberToLetterMap.TryGetValue(c, out char mapped))
                return mapped;
            
            return c;
        }

        private char GetMappedNumber(char c)
        {
            if (numberToNumberMap.TryGetValue(c, out char mapped))
                return mapped;
            
            return c;
        }
    }
}
