namespace Masasamjant.Reflection
{
    /// <summary>
    /// Represents result of type loading.
    /// </summary>
    public sealed class TypeLoadResult
    {
        /// <summary>
        /// Initializes new instance of the <see cref="TypeLoadResult"/> class that indicates 
        /// succeeded type loading.
        /// </summary>
        /// <param name="type">The loaded type.</param>
        public TypeLoadResult(Type type)
        {
            Type = type;
            Exception = null;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="TypeLoadResult"/> class that indicates 
        /// faulted type loading.
        /// </summary>
        /// <param name="exception">The occurred exception.</param>
        public TypeLoadResult(Exception exception)
        {
            Type = null;
            Exception = exception;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="TypeLoadResult"/> class that indicates 
        /// that type not exist.
        /// </summary>
        public TypeLoadResult()
        { }

        /// <summary>
        /// Gets the loaded type.
        /// </summary>
        public Type? Type { get; }
    
        /// <summary>
        /// Gets the exception occurred why loading type.
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        /// Gets whether or not type was loaded. 
        /// Meaning <see cref="Type"/> is not <c>null</c>.
        /// </summary>
        public bool IsLoaded
        {
            get { return Type != null; }
        }

        /// <summary>
        /// Gets whether or not loading faulted. 
        /// Meanning <see cref="Exception"/> is not <c>null</c>.
        /// </summary>
        public bool IsFaulted
        {
            get { return Exception != null; }
        }

        /// <summary>
        /// Gets whether or not type was not found. 
        /// Meaning that such type not exists.
        /// </summary>
        public bool NotFound
        {
            get { return !IsFaulted && !IsLoaded; }
        }
    }
}
