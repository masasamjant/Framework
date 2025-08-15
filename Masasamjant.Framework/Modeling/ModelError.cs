using Masasamjant.Serialization;
using System.Text.Json.Serialization;

namespace Masasamjant.Modeling
{
    /// <summary>
    /// Represents model error.
    /// </summary>
    public sealed class ModelError : IJsonSerializable
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ModelError"/> class.
        /// </summary>
        /// <param name="propertyName">The name of error property.</param>
        /// <param name="errorMessage">The error message.</param>
        public ModelError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Initializes new default instance of the <see cref="ModelError"/> class.
        /// </summary>
        public ModelError()
        { }

        /// <summary>
        /// Gets the name of error property.
        /// </summary>
        [JsonInclude]
        public string PropertyName { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets the error message.
        /// </summary>
        [JsonInclude]
        public string ErrorMessage { get; internal set; } = string.Empty;
    }
}
