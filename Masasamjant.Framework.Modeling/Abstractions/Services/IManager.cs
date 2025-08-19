namespace Masasamjant.Modeling.Abstractions.Services
{
    /// <summary>
    /// Represents service that manages <typeparamref name="TModel"/> instances 
    /// by adding, updating and removing the from non-volatile memory like database or file.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IManager<TModel> where TModel : IModel
    {
        /// <summary>
        /// Adds new <typeparamref name="TModel"/> to non-volatile memory.
        /// </summary>
        /// <param name="model">The model to add.</param>
        /// <returns>A <typeparamref name="TModel"/> after it has been added.</returns>
        Task<TModel> AddAsync(TModel model);

        /// <summary>
        /// Updates specified <typeparamref name="TModel"/> in non-volatile memory.
        /// </summary>
        /// <param name="model">The model to update.</param>
        /// <returns>A <typeparamref name="TModel"/> after it has been updated.</returns>
        Task<TModel> UpdateAsync(TModel model);

        /// <summary>
        /// Removes aka deleted specified <typeparamref name="TModel"/> from non-volatile memory.
        /// </summary>
        /// <param name="model">The model to delete.</param>
        /// <returns>A <typeparamref name="TModel"/> after it has been removed.</returns>
        Task<TModel> RemoveAsync(TModel model);
    }
}
