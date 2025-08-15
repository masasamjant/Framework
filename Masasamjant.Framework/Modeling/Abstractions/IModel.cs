namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model in application that can be either domain or data model.
    /// </summary>
    public interface IModel : IAddHandler, IUpdateHandler, IRemoveHandler
    {
        /// <summary>
        /// Gets the version bytes or empty array.
        /// </summary>
        byte[] Version { get; }

        /// <summary>
        /// Gets the <see cref="ModelIdentity"/> of the model or <c>null</c>, if 
        /// model is new and available only on current context.
        /// </summary>
        /// <returns>A <see cref="ModelIdentity"/> or <c>null</c>.</returns>
        ModelIdentity? GetIdentity();

        /// <summary>
        /// Gets <see cref="Version"/> as upper-case base-64 string.
        /// </summary>
        /// <returns>A <see cref="Version"/> as upper-case base-64 string or empty string.</returns>
        string GetVersionString();

        /// <summary>
        /// Validates model state.
        /// </summary>
        /// <exception cref="ModelValidationException">If model is not valid.</exception>
        void Validate();
    }
}
