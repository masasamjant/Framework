namespace Masasamjant
{
    /// <summary>
    /// Provides platform helper methods.
    /// </summary>
    public static class PlatformHelper
    {
        /// <summary>
        /// Ensures that code is run in Windows platform. If not, throws <see cref="PlatformNotSupportedException"/>.
        /// </summary>
        /// <exception cref="PlatformNotSupportedException">If <see cref="OperatingSystem.IsWindows()"/> returns <c>false</c>.</exception>
        public static void EnsureIsWindows()
        {
            if (!OperatingSystem.IsWindows())
                throw new PlatformNotSupportedException("Supported only on Windows.");
        }
    }
}
