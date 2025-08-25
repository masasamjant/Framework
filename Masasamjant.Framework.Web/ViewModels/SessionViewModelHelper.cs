using Masasamjant.Web.Sessions;

namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Provides helper methods to save <see cref="ISessionSerializable"/> to session.
    /// </summary>
    public static class SessionViewModelHelper
    {
        /// <summary>
        /// Save content of specified <typeparamref name="TViewModel"/> to session.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="viewModel">The <typeparamref name="TViewModel"/>.</param>
        /// <param name="key">The session key.</param>
        public static void SaveViewModel<TViewModel>(this ISessionStorage session, TViewModel viewModel, string key) where TViewModel : ISessionSerializable
        {
            var value = viewModel.ToSessionString();
            session.SetString(key, value);
        }

        /// <summary>
        /// Get the <typeparamref name="TViewModel"/> instance from session.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="key">The session key.</param>
        /// <returns>A <typeparamref name="TViewModel"/> read from session or <c>null</c>, if not exist.</returns>
        public static TViewModel? GetViewModel<TViewModel>(this ISessionStorage session, string key) where TViewModel : ISessionSerializable, new()
            => session.GetViewModel(() => new TViewModel(), key);

        /// <summary>
        /// Gets the <typeparamref name="TViewModel"/> instance from session.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="session">The <see cref="ISessionStorage"/>.</param>
        /// <param name="createEmpty">The function to create empty <typeparamref name="TViewModel"/> instance.</param>
        /// <param name="key">The session key.</param>
        /// <returns>A <typeparamref name="TViewModel"/> read from session or <c>null</c>, if not exist.</returns>
        public static TViewModel? GetViewModel<TViewModel>(this ISessionStorage session, Func<TViewModel> createEmpty, string key) where TViewModel : ISessionSerializable
        {
            var value = session.GetString(key);
            if (value == null)
                return default;
            var model = createEmpty();
            model.ReadSessionString(value);
            return model;
        }
    }
}
