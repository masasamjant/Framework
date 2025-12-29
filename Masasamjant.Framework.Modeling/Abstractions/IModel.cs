namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model in application.
    /// </summary>
    public interface IModel : IAddHandler, IUpdateHandler, IRemoveHandler
    {
        /// <summary>
        /// Validates model state.
        /// </summary>
        /// <exception cref="ModelValidationException">If model is not valid.</exception>
        void Validate();
    }

    /// <summary>
    /// Represents model in application that is identified by <typeparamref name="TIdentifier"/>.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public interface IModel<TIdentifier> : IModel
        where TIdentifier : IEquatable<TIdentifier>, new()
    {
        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        TIdentifier Identifier { get; }
    }
}
