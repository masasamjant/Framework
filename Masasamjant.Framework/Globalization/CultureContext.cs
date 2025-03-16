using System.Globalization;

namespace Masasamjant.Globalization
{
    /// <summary>
    /// Represents context where specified cultures replace current cultures. When instance is creates, then specified 
    /// cultures will be used instead of current ones. When context is disposed, original cultures are restored.
    /// </summary>
    public sealed class CultureContext : IDisposable
    {
        private readonly CultureInfo? restoreCulture;
        private readonly CultureInfo? restoreUICulture;
        private bool disposed;

        /// <summary>
        /// Initializes new instance of the <see cref="CultureContext"/> class with specified culture to use as <see cref="CultureInfo.CurrentCulture"/>
        /// and <see cref="CultureInfo.CurrentUICulture"/> while context is in use.
        /// </summary>
        /// <param name="culture">The culture to use as <see cref="CultureInfo.CurrentCulture"/> and <see cref="CultureInfo.CurrentUICulture"/> or <c>null</c>, if cultures not changed.</param>
        public CultureContext(CultureInfo? culture)
            : this(culture, culture)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="CultureContext"/> class with specified cultures to use while context
        /// is in use.
        /// </summary>
        /// <param name="currentCulture">The current culture to use or <c>null</c>, if current culture not changed.</param>
        /// <param name="currentUICulture">The current UI culture to use or <c>null</c>, if current culture not changed.</param>
        public CultureContext(CultureInfo? currentCulture, CultureInfo? currentUICulture)
        {
            if (currentCulture != null)
            {
                restoreCulture = CultureInfo.CurrentCulture;
                CultureInfo.CurrentCulture = currentCulture;
            }

            if (currentUICulture != null)
            {
                restoreUICulture = CultureInfo.CurrentUICulture;
                CultureInfo.CurrentUICulture = currentUICulture;
            }
        }

        /// <summary>
        /// Finalizes current instance and restores original cultures.
        /// </summary>
        ~CultureContext()
        {
            RestoreCultures();
        }

        /// <summary>
        /// Disposes current instance and restores original cultures.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If instance is already disposed.</exception>
        public void Dispose()
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);
            disposed = true;
            RestoreCultures();
            GC.SuppressFinalize(this);
        }

        private void RestoreCultures()
        {
            if (restoreCulture != null)
                CultureInfo.CurrentCulture = restoreCulture;

            if (restoreUICulture != null)
                CultureInfo.CurrentUICulture = restoreUICulture;
        }
    }
}
