using System.Text.Json;

namespace Masasamjant.Web.Sessions
{
    /// <summary>
    /// Provides helper methods to <see cref="ISessionStorage"/> interface.
    /// </summary>
    public static class SessionStorageHelper
    {
        /// <summary>
        /// Gets or creates session identifier string.
        /// </summary>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="createSessionIdentifier">The delegate to create new session identifier string.</param>
        /// <param name="sessionIdentifierKey">The session identifier key. Default is <see cref="HttpDefaultKeys.SessionIdentifierKey"/>.</param>
        /// <returns>A session identifier string.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="sessionIdentifierKey"/> is empty or only whitespace.</exception>
        public static string GetSessionIdentifier(this ISessionStorage session, Func<string> createSessionIdentifier, string sessionIdentifierKey = HttpDefaultKeys.SessionIdentifierKey)
        {
            if (string.IsNullOrWhiteSpace(sessionIdentifierKey))
                throw new ArgumentNullException(nameof(sessionIdentifierKey), "The session identifier key is empty or only whitespace.");

            var sessionIdentifier = session.GetString(sessionIdentifierKey);

            if (string.IsNullOrWhiteSpace(sessionIdentifier))
            {
                sessionIdentifier = createSessionIdentifier();
                session.SetString(sessionIdentifierKey, sessionIdentifier);
            }

            return sessionIdentifier;
        }

        /// <summary>
        /// Gets <see cref="Guid"/> saved to session.
        /// </summary>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="key">The session key.</param>
        /// <returns>A found <see cref="Guid"/> or <c>null.</c></returns>
        public static Guid? GetGuid(this ISessionStorage session, string key)
        {
            var value = session.GetString(key);
            return Guid.TryParse(value, out var guid) ? guid : null;
        }

        /// <summary>
        /// Sets <see cref="Guid"/> to session.
        /// </summary>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="key">The session key.</param>
        /// <param name="value">The value to set.</param>
        public static void SetGuid(this ISessionStorage session, string key, Guid value)
        {
            session.SetString(key, value.ToString());
        }

        /// <summary>
        /// Gets <see cref="int"/> saved to session.
        /// </summary>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="key">The session key.</param>
        /// <returns>A found <see cref="int"/> or <c>null.</c></returns>
        public static int? GetInt32(this ISessionStorage session, string key) 
        {
            var value = session.GetString(key);
            return int.TryParse(value, out var result) ? result : null;
        }

        /// <summary>
        /// Sets <see cref="int"/> to session.
        /// </summary>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="key">The session key.</param>
        /// <param name="value">The value to set.</param>
        public static void SetInt32(this ISessionStorage session, string key, int value)
        {
            session.SetString(key, value.ToString());
        }

        /// <summary>
        /// Gets <see cref="long"/> saved to session.
        /// </summary>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="key">The session key.</param>
        /// <returns>A found <see cref="long"/> or <c>null.</c></returns>
        public static long? GetInt64(this ISessionStorage session, string key)
        {
            var value = session.GetString(key);
            return long.TryParse(value, out var result) ? result : null;
        }

        /// <summary>
        /// Sets <see cref="long"/> to session.
        /// </summary>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="key">The session key.</param>
        /// <param name="value">The value to set.</param>
        public static void SetInt64(this ISessionStorage session, string key, long value)
        {
            session.SetString(key, value.ToString());
        }

        /// <summary>
        /// Gets <typeparamref name="TEnum"/> saved to session.
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type.</typeparam>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="key">The session key.</param>
        /// <returns>A found <typeparamref name="TEnum"/> or <c>null.</c></returns>
        public static TEnum? GetEnum<TEnum>(this ISessionStorage session, string key) where TEnum : struct, Enum
        {
            var value = session.GetString(key);
            return Enum.TryParse<TEnum>(value, out var result) ? result : null;
        }

        /// <summary>
        /// Sets <typeparamref name="TEnum"/> to session.
        /// </summary>
        /// <typeparam name="TEnum">The enumeration type.</typeparam>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="key">The session key.</param>
        /// <param name="value">The value to set.</param>
        public static void SetEnum<TEnum>(this ISessionStorage session, string key, TEnum value) where TEnum : struct, Enum
        {
            session.SetString(key, value.ToString());
        }

        /// <summary>
        /// Get instance of <typeparamref name="T"/> saved to session.
        /// </summary>
        /// <typeparam name="T">The type of instance that can be serialized to JSON.</typeparam>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="key">The session key.</param>
        /// <param name="serializerOptions">The <see cref="JsonSerializerOptions"/> or <c>null</c> to use default.</param>
        /// <returns>A instance of <typeparamref name="T"/> or <c>null</c>, if not found.</returns>
        /// <exception cref="InvalidOperationException">If cannot deserialize value from session to <typeparamref name="T"/> instance.</exception>
        public static T? GetInstance<T>(this ISessionStorage session, string key, JsonSerializerOptions? serializerOptions = null)
        {
            var value = session.GetString(key);

            if (string.IsNullOrWhiteSpace(value))
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(value, serializerOptions);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Deserializing '{typeof(T)}' from JSON string failed.", exception);
            }
        }

        /// <summary>
        /// Set instance of <typeparamref name="T"/> to session.
        /// </summary>
        /// <typeparam name="T">The type of instance that can be serialized to JSON.</typeparam>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="instance">The instance to set.</param>
        /// <param name="key">The session key.</param>
        /// <param name="serializerOptions">The <see cref="JsonSerializerOptions"/> or <c>null</c> to use default.</param>
        /// <exception cref="InvalidOperationException">If cannot serialize <paramref name="instance"/> to JSON.</exception>
        public static void SetInstance<T>(this ISessionStorage session, T instance, string key, JsonSerializerOptions? serializerOptions = null)
        {
            try
            {
                var json = JsonSerializer.Serialize(instance, serializerOptions);
                session.SetString(key, json);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Serializing '{typeof(T)}' to JSON string failed.", exception);
            }
        }
    }
}
