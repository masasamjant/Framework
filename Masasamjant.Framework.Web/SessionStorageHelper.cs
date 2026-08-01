using System.Globalization;
using System.Text.Json;

namespace Masasamjant.Web
{
    /// <summary>
    /// Provides helper methods to <see cref="ISessionStorage"/> interface.
    /// </summary>
    public static class SessionStorageHelper
    {
        /// <summary>
        /// Gets a <see cref="Guid"/> value from the session storage by the specified key.
        /// </summary>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The <see cref="Guid"/> value if found; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static Guid? GetGuid(this ISessionStorage session, string key)
        {
            ValidateSessionAndKey(session, key);
            var value = session.GetString(key);
            return Guid.TryParse(value, out var guid) ? guid : null;
        }

        /// <summary>
        /// Sets a <see cref="Guid"/> value in the session storage with the specified key.
        /// </summary>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The <see cref="Guid"/> value to set.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static void SetGuid(this ISessionStorage session, string key, Guid value)
        {
            ValidateSessionAndKey(session, key);
            session.SetString(key, value.ToString());
        }

        /// <summary>
        /// Gets a <see cref="int"/> value from the session storage by the specified key.
        /// </summary>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The <see cref="int"/> value if found; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static int? GetInt32(this ISessionStorage session, string key)
        {
            ValidateSessionAndKey(session, key);
            var value = session.GetString(key);
            return int.TryParse(value, out var result) ? result : null;
        }

        /// <summary>
        /// Sets a <see cref="int"/> value in the session storage with the specified key.
        /// </summary>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The <see cref="int"/> value to set.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static void SetInt32(this ISessionStorage session, string key, int value)
        {
            ValidateSessionAndKey(session, key);
            session.SetString(key, value.ToString());
        }

        /// <summary>
        /// Gets a <see cref="long"/> value from the session storage by the specified key.
        /// </summary>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The <see cref="long"/> value if found; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static long? GetInt64(this ISessionStorage session, string key)
        {
            ValidateSessionAndKey(session, key);
            var value = session.GetString(key);
            return long.TryParse(value, out var result) ? result : null;
        }

        /// <summary>
        /// Sets a <see cref="long"/> value in the session storage with the specified key.
        /// </summary>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The <see cref="long"/> value to set.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static void SetInt64(this ISessionStorage session, string key, long value)
        {
            ValidateSessionAndKey(session, key);
            session.SetString(key, value.ToString());
        }

        /// <summary>
        /// Gets a <see cref="double"/> value from the session storage by the specified key.
        /// </summary>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <param name="culture">The culture used to parse value or <c>null</c> for invariant culture.</param>
        /// <param name="style">The number styles used to parse the value.</param>
        /// <returns>The <see cref="double"/> value if found; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static double? GetDouble(this ISessionStorage session, string key, CultureInfo? culture = null, NumberStyles style = NumberStyles.Any)
        {
            ValidateSessionAndKey(session, key);
            var value = session.GetString(key);
            return double.TryParse(value, style, culture ?? CultureInfo.InvariantCulture, out var result) ? result : null;
        }

        /// <summary>
        /// Sets a <see cref="double"/> value in the session storage with the specified key.
        /// </summary>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The <see cref="double"/> value to set.</param>
        /// <param name="culture">The culture used to format value or <c>null</c> for invariant culture.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static void SetDouble(this ISessionStorage session, string key, double value, CultureInfo? culture = null)
        {
            ValidateSessionAndKey(session, key);
            session.SetString(key, value.ToString(culture ?? CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Gets a <typeparamref name="TEnum"/> value from the session storage by the specified key.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum value.</typeparam>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The <typeparamref name="TEnum"/> value if found; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static TEnum? GetEnum<TEnum>(this ISessionStorage session, string key) where TEnum : struct, Enum
        {
            ValidateSessionAndKey(session, key);
            var value = session.GetString(key);
            return Enum.TryParse<TEnum>(value, out var result) ? result : null;
        }

        /// <summary>
        /// Sets a <typeparamref name="TEnum"/> value in the session storage with the specified key.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum value.</typeparam>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The <typeparamref name="TEnum"/> value to set.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static void SetEnum<TEnum>(this ISessionStorage session, string key, TEnum value) where TEnum : struct, Enum
        {
            ValidateSessionAndKey(session, key);
            session.SetString(key, value.ToString());
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> value from the session storage by the specified key.
        /// </summary>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <param name="culture">The culture used to parse value or <c>null</c> for invariant culture.</param>
        /// <param name="style">The date time styles used to parse the value.</param>
        /// <returns>The <see cref="DateTime"/> value if found; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static DateTime? GetDateTime(this ISessionStorage session, string key, CultureInfo? culture = null, DateTimeStyles style = DateTimeStyles.None)
        {
            ValidateSessionAndKey(session, key);
            var value = session.GetString(key);
            return DateTime.TryParse(value, culture ?? CultureInfo.InvariantCulture, style, out var result) ? result : null;
        }

        /// <summary>
        /// Sets a <see cref="DateTime"/> value in the session storage with the specified key.
        /// </summary>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The <see cref="DateTime"/> value to set.</param>
        /// <param name="culture">The culture used to format value or <c>null</c> for invariant culture.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static void SetDateTime(this ISessionStorage session, string key, DateTime value, CultureInfo? culture = null)
        {
            ValidateSessionAndKey(session, key);
            session.SetString(key, value.ToString(culture ?? CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Deserializes a JSON string from the session storage by the specified key to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <param name="options">The JSON serializer options.</param>
        /// <returns>The deserialized object of type <typeparamref name="T"/> if found; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">If deserialization fails.</exception>
        public static T? JsonDeserialize<T>(this ISessionStorage session, string key, JsonSerializerOptions? options = null)
        {
            ValidateSessionAndKey(session, key);

            var value = session.GetString(key);

            if (string.IsNullOrWhiteSpace(value))
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(value, options);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Deserializing '{typeof(T)}' from JSON string failed.", exception);
            }
        }

        /// <summary>
        /// Serializes an object of type <typeparamref name="T"/> to a JSON string and stores it in the session storage with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The object to serialize.</param>
        /// <param name="options">The JSON serializer options.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> or <paramref name="key"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">If serialization fails.</exception>
        public static void JsonSerialize<T>(this ISessionStorage session, string key, T value, JsonSerializerOptions? options = null)
        {
            ValidateSessionAndKey(session, key);

            try
            {
                var json = JsonSerializer.Serialize(value, options);
                session.SetString(key, json);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Serializing '{typeof(T)}' to JSON string failed.", exception);
            }
        }

        /// <summary>
        /// Save string representation of instance of <typeparamref name="T"/> to session using specified key.
        /// </summary>
        /// <typeparam name="T">The type of the instance to save.</typeparam>
        /// <param name="session">The session storage instance.</param>
        /// <param name="instance">The instance to save.</param>
        /// <param name="key">The key of the value to set.</param>
        /// <exception cref="ArgumentNullException">If any of the parameters is <c>null</c>.</exception>
        public static void SaveToSession<T>(this ISessionStorage session, T instance, string key) where T : ISessionSerializable
        {
            ArgumentNullException.ThrowIfNull(session);
            ArgumentNullException.ThrowIfNull(instance);
            ArgumentNullException.ThrowIfNull(key);

            var value = instance.ToSessionString();
            session.SetString(key, value);
        }

        /// <summary>
        /// Load instance of <typeparamref name="T"/> from session using specified key.
        /// </summary>
        /// <typeparam name="T">The type of the instance to load.</typeparam>
        /// <param name="session">The session storage instance.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The instance of <typeparamref name="T"/> loaded from the session, or <c>null</c> if the key does not exist.</returns>
        /// <exception cref="ArgumentNullException">If any of the parameters is <c>null</c>.</exception>
        public static T? LoadFromSession<T>(this ISessionStorage session, string key) where T : ISessionSerializable, new()
            => LoadFromSession(session, () => new T(), key);

        /// <summary>
        /// Load instance of <typeparamref name="T"/> from session using specified key.
        /// </summary>
        /// <typeparam name="T">The type of the instance to load.</typeparam>
        /// <param name="session">The session storage instance.</param>
        /// <param name="createNew">A function to create a new instance if the key does not exist in the session.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The instance of <typeparamref name="T"/> loaded from the session, or <c>null</c> if the key does not exist.</returns>
        /// <exception cref="ArgumentNullException">If any of the parameters is <c>null</c>.</exception>
        public static T? LoadFromSession<T>(this ISessionStorage session, Func<T> createNew, string key) where T : ISessionSerializable
        {
            ArgumentNullException.ThrowIfNull(session);
            ArgumentNullException.ThrowIfNull(createNew);
            ArgumentNullException.ThrowIfNull(key);

            var value = session.GetString(key);
            if (value == null)
                return default;
            var model = createNew();
            model.ReadSessionString(value);
            return model;
        }


        private static void ValidateSessionAndKey(ISessionStorage session, string key)
        {
            ArgumentNullException.ThrowIfNull(session);
            ArgumentNullException.ThrowIfNull(key);
        }
    }
}
