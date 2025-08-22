namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents component that notifies about errors using <see cref="Error"/> event.
    /// </summary>
    public interface IProvideError
    {
        /// <summary>
        /// Notifies when error occurs.
        /// </summary>
        event EventHandler<ErrorEventArgs>? Error;
    }
}
