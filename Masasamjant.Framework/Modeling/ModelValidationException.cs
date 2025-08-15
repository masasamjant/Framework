using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Modeling
{
    /// <summary>
    /// Represents exception thrown by <see cref="IModel.Validate"/> when model is not valid.
    /// </summary>
    public class ModelValidationException : Exception
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ModelValidationException"/> class.
        /// </summary>
        /// <param name="model">The validated model.</param>
        /// <param name="errors">The validation errors.</param>
        public ModelValidationException(IModel model, IEnumerable<ModelError> errors)
            : this("The specified model is not valid.", model, errors)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="ModelValidationException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="model">The validated model.</param>
        /// <param name="errors">The validation errors.</param>
        public ModelValidationException(string message, IModel model, IEnumerable<ModelError> errors)
            : this(message, model, errors, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="ModelValidationException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="model">The validated model.</param>
        /// <param name="errors">The validation errors.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public ModelValidationException(string message, IModel model, IEnumerable<ModelError> errors, Exception? innerException)
            : base(message, innerException)
        {
            Model = model;
            Errors = errors;
        }

        /// <summary>
        /// Gets the validated model.
        /// </summary>
        public IModel Model { get; }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        public IEnumerable<ModelError> Errors { get; }
    }
}