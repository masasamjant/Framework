namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents <see cref="IModel"/> that support preparation before it is ready for use.
    /// </summary>
    public interface ISupportPrepareModel : IModel
    {
        /// <summary>
        /// Prepares model before it is ready for use. This should be used immediately after retrieving 
        /// model from non-volatile memory and passing it to further.
        /// </summary>
        void PrepareModel();
    }
}
