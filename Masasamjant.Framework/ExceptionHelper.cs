namespace Masasamjant
{
    /// <summary>
    /// Provides helper and extension methods to exceptions.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Gets all exceptions of specified root exception.
        /// </summary>
        /// <param name="exception">The initial exception.</param>
        /// <returns>A <paramref name="exception"/> and all its inner exceptions.</returns>
        public static IEnumerable<Exception> GetAll(this Exception exception)
        {
            if (exception.InnerException == null)
                yield return exception;
            else
            {
                var current = exception;

                while (current != null)
                {
                    yield return current;
                    current = current.InnerException;
                }
            }
        }

        /// <summary>
        /// Gets first, the inner most, exception of specified root exception.
        /// </summary>
        /// <param name="exception">The initial exception.</param>
        /// <returns>A first occurred exception.</returns>
        public static Exception GetFirst(this Exception exception) => GetAll(exception).Last();

        /// <summary>
        /// Converts <see cref="AggregateException"/> to <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="aggregateException">The <see cref="AggregateException"/>.</param>
        /// <param name="message">The exception message or <c>null</c> to use message of <paramref name="aggregateException"/>.</param>
        /// <returns>A <see cref="InvalidOperationException"/> created from <paramref name="aggregateException"/>.</returns>
        public static InvalidOperationException ToInvalidOperationException(this AggregateException aggregateException, string? message = null)
        {
            var flattenException = aggregateException.Flatten();

            if (string.IsNullOrWhiteSpace(message))
                message = flattenException.Message;

            var innerExceptions = flattenException.InnerExceptions.ToArray();

            if (innerExceptions.Length == 0)
                return new InvalidOperationException(message);
            else if (innerExceptions.Length == 1)
                return new InvalidOperationException(message, flattenException.InnerExceptions.First());
            else
            {
                Exception? innerException = null;

                foreach (var iteration in innerExceptions.IterateBackward())
                    innerException = new Exception(iteration.Item.Message, innerException);

                return new InvalidOperationException(message, innerException);
            }
        }
    }
}
