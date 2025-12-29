namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Validates model state.
        /// </summary>
        /// <exception cref="ModelValidationException">If model is not valid.</exception>
        void Validate();
    }
}
