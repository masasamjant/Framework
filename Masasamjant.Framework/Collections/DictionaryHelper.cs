using Masasamjant.Collections.Adapters;
using System.Collections.ObjectModel;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Provides helper methods to <see cref="IDictionary{TKey, TValue}"/> and <see cref="IReadOnlyDictionary{TKey, TValue}"/> interfaces.
    /// </summary>
    public static class DictionaryHelper
    {
        /// <summary>
        /// Check if two <see cref="IDictionary{TKey, TValue}"/>s have same key/value pairs.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="left">The left <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="right">The right <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are considered as equal meaning they have same key/value pairs; <c>false</c> otherwise.</returns>
        public static bool AreEqual<TKey, TValue>(IDictionary<TKey, TValue> left, IDictionary<TKey, TValue> right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left.Count != right.Count)
                return false;

            if (left.Count > 0)
            {
                foreach (var key in left.Keys)
                {
                    if (!right.ContainsKey(key))
                        return false;

                    var leftValue = left[key];
                    var rightValue = right[key];

                    if (ReferenceEquals(leftValue, rightValue))
                        continue;

                    if (!Equals(leftValue, rightValue))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Creates <see cref="IReadOnlyDictionary{TKey, TValue}"/> based on specified <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source <see cref="IDictionary{TKey, TValue}"/> or <c>null</c> to create empty.</param>
        /// <returns>A <see cref="IReadOnlyDictionary{TKey, TValue}"/>.</returns>
        public static IReadOnlyDictionary<TKey, TValue> CreateReadOnly<TKey, TValue>(IDictionary<TKey, TValue>? source = null) where TKey : notnull
        {
            return new ReadOnlyDictionary<TKey, TValue>(source ?? new Dictionary<TKey, TValue>());
        }

        /// <summary>
        /// Creates <see cref="IDictionary{TKey, TValue}"/> in read-only state from specified <see cref="IReadOnlyDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The <see cref="IReadOnlyDictionary{TKey, TValue}"/>.</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/>.</returns>
        public static IDictionary<TKey, TValue> AsDictionary<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source)
        {
            if (source is IDictionary<TKey, TValue> dictionary)
                return dictionary;
            else
                return new ReadOnlyDictionaryAdapter<TKey, TValue>(source);
        }

        /// <summary>
        /// Gets or adds value to <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="valueProvider">The deleagate to get value for <paramref name="key"/>, if value not exist.</param>
        /// <returns>A value get from <paramref name="dictionary"/> or value get from <paramref name="valueProvider"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueProvider) where TKey : notnull
        {
            CollectionHelper.CheckNotReadOnly(dictionary, nameof(dictionary));

            if (dictionary.TryGetValue(key, out var value))
                return value;

            value = valueProvider(key);
            dictionary[key] = value;
            return value;
        }

        /// <summary>
        /// Gets or adds value to <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="add">The value to add, if value not exist.</param>
        /// <returns>A value get from <paramref name="dictionary"/> or <paramref name="add"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue add) where TKey : notnull
            => GetOrAdd(dictionary, key, k => add);

        /// <summary>
        /// Combine two <see cref="IDictionary{TKey, TValue}"/> to single <see cref="IDictionary{TKey, TValue}"/> using specified <see cref="DuplicateBehavior"/> for duplicate keys.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="first">The first <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="second">The second <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="duplicateBehavior">The <see cref="DuplicateBehavior"/> to how possible duplicate keys are handled.</param>
        /// <param name="maxItemCount">The how many items dictionaries should have at max. Default value is <see cref="int.MaxValue"/>.</param>
        /// <returns>A new <see cref="IDictionary{TKey, TValue}"/> that is compination of <paramref name="first"/> and <paramref name="second"/>.</returns>
        /// <exception cref="ArgumentException">
        /// If value of <paramref name="duplicateBehavior"/> is not defined.
        /// -or-
        /// If <paramref name="dictionaries"/> has more than <paramref name="maxItemCount"/> items in total.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="maxItemCount"/> is not greater than 0.</exception>
        /// <exception cref="NotSupportedException">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Insert"/>.</exception>
        /// <exception cref="DuplicateKeyException{TKey}">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Error"/> and duplicate key is found.</exception>
        public static IDictionary<TKey, TValue> Combine<TKey, TValue>(IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second, DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore, int maxItemCount = int.MaxValue) where TKey : notnull
            => Combine([first, second], duplicateBehavior, maxItemCount);

        /// <summary>
        /// Combine three <see cref="IDictionary{TKey, TValue}"/> to single <see cref="IDictionary{TKey, TValue}"/> using specified <see cref="DuplicateBehavior"/> for duplicate keys.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="first">The first <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="second">The second <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="third">The third <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="duplicateBehavior">The <see cref="DuplicateBehavior"/> to how possible duplicate keys are handled.</param>
        /// <param name="maxItemCount">The how many items dictionaries should have at max. Default value is <see cref="int.MaxValue"/>.</param>
        /// <returns>A new <see cref="IDictionary{TKey, TValue}"/> that is compination of <paramref name="first"/>, <paramref name="second"/> and <paramref name="third"/>.</returns>
        /// <exception cref="ArgumentException">
        /// If value of <paramref name="duplicateBehavior"/> is not defined.
        /// -or-
        /// If <paramref name="dictionaries"/> has more than <paramref name="maxItemCount"/> items in total.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="maxItemCount"/> is not greater than 0.</exception>
        /// <exception cref="NotSupportedException">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Insert"/>.</exception>
        /// <exception cref="DuplicateKeyException{TKey}">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Error"/> and duplicate key is found.</exception>
        public static IDictionary<TKey, TValue> Combine<TKey, TValue>(IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second, IDictionary<TKey, TValue> third, DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore, int maxItemCount = int.MaxValue) where TKey : notnull
            => Combine([first, second, third], duplicateBehavior, maxItemCount);

        /// <summary>
        /// Combine several <see cref="IDictionary{TKey, TValue}"/> to single <see cref="IDictionary{TKey, TValue}"/> using specified <see cref="DuplicateBehavior"/> for duplicate keys.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionaries">The dictionaries to combine.</param>
        /// <param name="duplicateBehavior">The <see cref="DuplicateBehavior"/> to how possible duplicate keys are handled.</param>
        /// <param name="maxItemCount">The how many items <paramref name="dictionaries"/> should have at max. Default value is <see cref="int.MaxValue"/>.</param>
        /// <returns>A new <see cref="IDictionary{TKey, TValue}"/> that is compination of <paramref name="dictionaries"/>.</returns>
        /// <exception cref="ArgumentException">
        /// If value of <paramref name="duplicateBehavior"/> is not defined.
        /// -or-
        /// If <paramref name="dictionaries"/> has more than <paramref name="maxItemCount"/> items in total.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="maxItemCount"/> is not greater than 0.</exception>
        /// <exception cref="NotSupportedException">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Insert"/>.</exception>
        /// <exception cref="DuplicateKeyException{TKey}">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Error"/> and duplicate key is found.</exception>
        public static IDictionary<TKey, TValue> Combine<TKey, TValue>(IEnumerable<IDictionary<TKey, TValue>> dictionaries, DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore, int maxItemCount = int.MaxValue) where TKey : notnull
        {
            if (!Enum.IsDefined(duplicateBehavior))
                throw new ArgumentException("The value is not defined.", nameof(duplicateBehavior));

            if (duplicateBehavior == DuplicateBehavior.Insert)
                throw new NotSupportedException("The dictionary does not allow inserting duplicate keys.");

            if (maxItemCount < 1)
                throw new ArgumentOutOfRangeException(nameof(maxItemCount), maxItemCount, "The value must be greater than 0.");

            var combined = new Dictionary<TKey, TValue>();

            foreach (var dictionary in dictionaries)
            {
                if (dictionary.Count == 0)
                    continue;

                foreach (var keyValue in dictionary)
                {
                    if (combined.Count == maxItemCount)
                        throw new ArgumentException($"The dictionaries cannot have more than {maxItemCount} items in total.", nameof(dictionaries));

                    if (combined.ContainsKey(keyValue.Key))
                        AddDuplicate(combined, keyValue.Key, keyValue.Value, duplicateBehavior, "The dictionaries contains duplicate item(s).");
                    else
                        combined.Add(keyValue.Key, keyValue.Value);
                }
            }

            return combined;
        }

        /// <summary>
        /// Merge dictionaries. Those keys not present at <paramref name="source"/> are removed from <paramref name="destination"/>. If key exists both in <paramref name="source"/> and <paramref name="destination"/>, 
        /// then source value is set to destination. If key exists in <paramref name="source"/>, but not in <paramref name="destination"/>, then it is added to destination.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="destination">The destination dictionary.</param>
        /// <param name="source">The source dictionary.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> destination, IDictionary<TKey, TValue> source) where TKey : notnull
            => Merge(destination, [source]);

        /// <summary>
        /// Merges multiple dictionaries with specified destination dictionary. Those keys not present any of the <paramref name="sources"/> are removed from <paramref name="destination"/>.
        /// If key exists both in <paramref name="sources"/> and in <paramref name="destination"/>, then source value is set to destination. If key exists in <paramref name="sources"/>, but not 
        /// in <paramref name="destination"/>, then it is added to destination.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="destination">The destination dictionary.</param>
        /// <param name="sources">The source dictionaries.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> destination, IEnumerable<IDictionary<TKey, TValue>> sources) where TKey : notnull
        {
            CollectionHelper.CheckNotReadOnly(destination, nameof(destination));

            RemoveOrphants(destination, sources);
            AddOrReplace(destination, sources);
        }

        /// <summary>
        /// Adds or replaces values in specified dictionary from values in specified source dictionaries.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="destination">The dictionary to add or replace values.</param>
        /// <param name="sources">The dictionaries to read values.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> destination, IEnumerable<IDictionary<TKey, TValue>> sources) where TKey : notnull
        {
            CollectionHelper.CheckNotReadOnly(destination, nameof(destination));

            foreach (var source in sources)
            {
                if (source.Count == 0 || ReferenceEquals(destination, source))
                    continue;

                foreach (var keyValue in source)
                {
                    if (destination.ContainsKey(keyValue.Key))
                        destination[keyValue.Key] = keyValue.Value;
                    else
                        destination.Add(keyValue);
                }
            }
        }

        /// <summary>
        /// Adds or replaces values in specified dictionary from values in specified source dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="destination">The dictionary to add or replace values.</param>
        /// <param name="source">The dictionary to read values.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> destination, IDictionary<TKey, TValue> source) where TKey : notnull
        {
            CollectionHelper.CheckNotReadOnly(destination, nameof(destination));

            if (ReferenceEquals(destination, source) || source.Count == 0)
                return;

            foreach (var keyValue in source)
            {
                if (destination.ContainsKey(keyValue.Key))
                    destination[keyValue.Key] = keyValue.Value;
                else
                    destination.Add(keyValue);
            }
        }

        /// <summary>
        /// Remove items from specified destination dictionary where key do not exist in specified dictionaries.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="destination">The dictionary to remove items.</param>
        /// <param name="dictionaries">The dictionaries to check items.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void RemoveOrphants<TKey, TValue>(this IDictionary<TKey, TValue> destination, IEnumerable<IDictionary<TKey, TValue>> dictionaries) where TKey : notnull
        {
            CollectionHelper.CheckNotReadOnly(destination, nameof(destination));

            foreach (var keyValue in CopyEnumerable.CreateCopyEnumerable(destination))
            {
                bool remove = true;

                foreach (var source in dictionaries)
                {
                    if (source.Count == 0 || ReferenceEquals(destination, source))
                        continue;

                    if (source.ContainsKey(keyValue.Key))
                    {
                        remove = false;
                        break;
                    }
                }

                if (remove)
                    destination.Remove(keyValue.Key);
            }
        }

        /// <summary>
        /// Remove items from specified destination dictionary where key do not exist in comparison dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="destination">The dictionary to remove items.</param>
        /// <param name="compare">The dictionary to check items.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void RemoveOrphants<TKey, TValue>(this IDictionary<TKey, TValue> destination, IDictionary<TKey, TValue> compare) where TKey : notnull
        {
            CollectionHelper.CheckNotReadOnly(destination, nameof(destination));

            if (ReferenceEquals(destination, compare))
                return;

            foreach (var keyValue in CopyEnumerable.CreateCopyEnumerable(destination))
            {
                if (!compare.ContainsKey(keyValue.Key))
                    destination.Remove(keyValue.Key);
            }
        }

        /// <summary>
        /// Remove specified keys from <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="keys">The <see cref="IEnumerable{TKey}"/> of keys to remove.</param>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static void RemoveRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            CollectionHelper.CheckNotReadOnly(dictionary, nameof(dictionary));

            foreach (var key in keys)
                dictionary.Remove(key);
        }

        /// <summary>
        /// Tries to get value from <see cref="IDictionary{TKey, TValue}"/> and if value is get, then removes the key/value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="key">The key.</param>
        /// <param name="result">The value if returns <c>true</c>, otherwise default.</param>
        /// <returns><c>true</c> if <paramref name="result"/> get and removed from <paramref name="dictionary"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static bool TryGetAndRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue? result)
        {
            CollectionHelper.CheckNotReadOnly(dictionary, nameof(dictionary));

            if (dictionary.TryGetValue(key, out result))
            {
                dictionary.Remove(key);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Keep items in specified <see cref="IDictionary{TKey, TValue}"/> that match specified predicate and remove other items.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="keepPredicate">The predicate to match items to keep.</param>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static void Keep<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Predicate<KeyValuePair<TKey, TValue>> keepPredicate)
            => Keep(dictionary, new Func<KeyValuePair<TKey, TValue>, bool>(kv => keepPredicate(kv)));

        /// <summary>
        /// Keep items in specified <see cref="IDictionary{TKey, TValue}"/> that match specified predicate and remove other items.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="keepPredicate">The predicate to match items to keep.</param>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static void Keep<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> keepPredicate)
        {
            CollectionHelper.CheckNotReadOnly(dictionary, nameof(dictionary));

            if (dictionary.Count == 0)
                return;

            foreach (var keyValue in CopyEnumerable.CreateCopyEnumerable(dictionary))
                if (!keepPredicate(keyValue))
                    dictionary.Remove(keyValue);
        }

        /// <summary>
        /// Keep items in specified <see cref="IDictionary{TKey, TValue}"/> where key match specified predicate and remove other items.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="keepPredicate">The predicate to match keys of items to keep.</param>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static void Keep<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Predicate<TKey> keepPredicate)
            => Keep(dictionary, new Func<TKey, bool>(k => keepPredicate(k)));

        /// <summary>
        /// Keep items in specified <see cref="IDictionary{TKey, TValue}"/> where key match specified predicate and remove other items.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="keepPredicate">The predicate to match keys of items to keep.</param>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static void Keep<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, bool> keepPredicate)
        {
            CollectionHelper.CheckNotReadOnly(dictionary, nameof(dictionary));

            if (dictionary.Count == 0)
                return;

            foreach (var keyValue in CopyEnumerable.CreateCopyEnumerable(dictionary))
                if (!keepPredicate(keyValue.Key))
                    dictionary.Remove(keyValue.Key);
        }

        /// <summary>
        /// Remove items from specified <see cref="IDictionary{TKey, TValue}"/> that match specified predicate and keep other items.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="removePredicate">The predicate to match items to remove.</param>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static void Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Predicate<KeyValuePair<TKey, TValue>> removePredicate)
            => Remove(dictionary, new Func<KeyValuePair<TKey, TValue>, bool>(kv => removePredicate(kv)));

        /// <summary>
        /// Remove items from specified <see cref="IDictionary{TKey, TValue}"/> that match specified predicate and keep other items.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="removePredicate">The predicate to match items to remove.</param>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static void Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> removePredicate)
        {
            CollectionHelper.CheckNotReadOnly(dictionary, nameof(dictionary));

            if (dictionary.Count == 0)
                return;

            foreach (var keyValue in CopyEnumerable.CreateCopyEnumerable(dictionary))
                if (removePredicate(keyValue))
                    dictionary.Remove(keyValue);
        }

        /// <summary>
        /// Remove items from specified <see cref="IDictionary{TKey, TValue}"/> where key match specified predicate and keep other items.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="removePredicate">The predicate to match keys of items to remove.</param>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static void Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Predicate<TKey> removePredicate)
            => Remove(dictionary, new Func<TKey, bool>(k => removePredicate(k)));

        /// <summary>
        /// Remove items from specified <see cref="IDictionary{TKey, TValue}"/> where key match specified predicate and keep other items.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="removePredicate">The predicate to match keys of items to remove.</param>
        /// <exception cref="ArgumentException">If <paramref name="dictionary"/> is in read-only state.</exception>
        public static void Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, bool> removePredicate)
        {
            CollectionHelper.CheckNotReadOnly(dictionary, nameof(dictionary));

            if (dictionary.Count == 0)
                return;

            foreach (var keyValue in CopyEnumerable.CreateCopyEnumerable(dictionary))
                if (removePredicate(keyValue.Key))
                    dictionary.Remove(keyValue.Key);
        }

        /// <summary>
        /// Create <see cref="IDictionary{T1, T2}"/> from enumerable of <see cref="Tuple{T1, T2}"/> there <typeparamref name="T1"/> will be key 
        /// and <typeparamref name="T2"/> will be value.
        /// </summary>
        /// <typeparam name="T1">The type of the first item and key.</typeparam>
        /// <typeparam name="T2">The type of the second item and key.</typeparam>
        /// <param name="tuples">The enumerable of <see cref="Tuple{T1, T2}"/>.</param>
        /// <param name="duplicateBehavior">The <see cref="DuplicateBehavior"/> of possible duplicate keys of <typeparamref name="T1"/>.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If value of <paramref name="duplicateBehavior"/> is not defined.</exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Insert"/>.</exception>
        /// <exception cref="DuplicateKeyException{T1}">If value of <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Error"/> and <paramref name="tuples"/> contains items that would create duplicate key.</exception>
        public static IDictionary<T1, T2> FromTuples<T1, T2>(IEnumerable<Tuple<T1, T2>> tuples, DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore) where T1 : notnull
        {
            if (!Enum.IsDefined(duplicateBehavior))
                throw new ArgumentException("The value is not defined.", nameof(duplicateBehavior));

            if (duplicateBehavior == DuplicateBehavior.Insert)
                throw new NotSupportedException("The dictionary does not allow inserting duplicate keys.");

            var dictionary = new Dictionary<T1, T2>();

            foreach (var tuple in tuples)
            {
                if (dictionary.ContainsKey(tuple.Item1))
                {
                    AddDuplicate(dictionary, tuple.Item1, tuple.Item2, duplicateBehavior, "The tuples contains item that would create duplicate key.");
                }
                else
                    dictionary.Add(tuple.Item1, tuple.Item2);
            }

            return dictionary;
        }

        /// <summary>
        /// Create enumerable of <see cref="Tuple{TKey, TValue}"/> from specified <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <returns>A enumerable of <see cref="Tuple{TKey, TValue}"/>.</returns>
        public static IEnumerable<Tuple<TKey, TValue>> ToTuples<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            foreach (var keyValue in dictionary)
                yield return new Tuple<TKey, TValue>(keyValue.Key, keyValue.Value);
        }

        /// <summary>
        /// Check if <see cref="IDictionary{TKey, TValue}"/> contains all specified keys. If <paramref name="dictionary"/> or <paramref name="keys"/> is empty, 
        /// then returns <c>false</c>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="keys">The keys to check.</param>
        /// <returns><c>true</c> if <paramref name="dictionary"/> contains all keys in <paramref name="keys"/>; <c>false</c> otherwise.</returns>
        public static bool ContainsKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, params TKey[] keys) => ContainsKeys(dictionary, keys.AsEnumerable());

        /// <summary>
        /// Check if <see cref="IDictionary{TKey, TValue}"/> contains all specified keys. If <paramref name="dictionary"/> or <paramref name="keys"/> is empty, 
        /// then returns <c>false</c>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="keys">The <see cref="IEnumerable{TKey}"/>.</param>
        /// <returns><c>true</c> if <paramref name="dictionary"/> contains all keys in <paramref name="keys"/>; <c>false</c> otherwise.</returns>
        public static bool ContainsKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            if (dictionary.Count == 0 || !keys.Any())
                return false;

            foreach (var key in keys)
            {
                if (!dictionary.ContainsKey(key))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Create dictionary from specified items of <typeparamref name="T"/> where item is both key and value. Possible duplicate items are ignored.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="items">The source items.</param>
        /// <returns>A dictionary from <paramref name="items"/> where item is key and value.</returns>
        public static IDictionary<T, T> ToDictionary<T>(IEnumerable<T> items) where T : notnull, IEquatable<T>
        {
            var dictionary = new Dictionary<T, T>();

            foreach (var item in items)
            {
                if (!dictionary.ContainsKey(item))
                    dictionary[item] = item;
            }

            return dictionary;
        }

        private static void AddDuplicate<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, TValue value, DuplicateBehavior duplicateBehavior, string duplicateErrorMessage) where TKey : notnull
        {
            switch (duplicateBehavior)
            {
                case DuplicateBehavior.Replace:
                    dictionary[key] = value;
                    break;
                case DuplicateBehavior.Error:
                    throw new DuplicateKeyException<TKey>(key, duplicateErrorMessage);
                default:
                    break;
            }
        }
    }
}
