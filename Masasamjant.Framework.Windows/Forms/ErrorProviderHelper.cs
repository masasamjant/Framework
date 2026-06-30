namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Provides helper methods for the <see cref="ErrorProvider"/> component.
    /// </summary>
    public static class ErrorProviderHelper
    {
        /// <summary>
        /// Add error to error provider targeting specified control.
        /// </summary>
        /// <remarks>If <paramref name="errorMessage"/> is <c>null</c>, empty, or whitespace, no error will be added.</remarks>
        /// <param name="errorProvider">The error provider.</param>
        /// <param name="errorTarget">The error target control.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorCount">The count of errors added.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="errorProvider"/> or <paramref name="errorTarget"/> is <c>null</c>.</exception>
        public static void AddError(this ErrorProvider errorProvider, Control errorTarget, string errorMessage, ref int errorCount)
        {
            ArgumentNullException.ThrowIfNull(errorProvider);
            ArgumentNullException.ThrowIfNull(errorTarget);

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                errorProvider.SetError(errorTarget, errorMessage);
                errorCount++;
            }
        }

        /// <summary>
        /// Remove error from error provider targeting specified control.
        /// </summary>
        /// <param name="errorProvider">The error provider.</param>
        /// <param name="errorTarget">The error target control.</param>
        /// <param name="errorCount">The count of errors added.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="errorProvider"/> or <paramref name="errorTarget"/> is <c>null</c>.</exception>
        public static void RemoveError(this ErrorProvider errorProvider, Control errorTarget, ref int errorCount)
        {
            ArgumentNullException.ThrowIfNull(errorProvider);
            ArgumentNullException.ThrowIfNull(errorTarget);

            if (errorCount > 0)
            {
                errorProvider.SetError(errorTarget, string.Empty);
                errorCount--;
            }
        }
    }
}
