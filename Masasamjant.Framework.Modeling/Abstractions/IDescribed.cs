namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model that support default description.
    /// </summary>
    public interface IDescribed : IModel
    {
        /// <summary>
        /// Gets the default description.
        /// </summary>
        string? Description { get; }
    }
}
