namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Represents view model that can be saved into session as string.
    /// </summary>
    public interface ISessionSerializable
    {
        /// <summary>
        /// Gets string to save in session.
        /// </summary>
        /// <returns>A string to save in session.</returns>
        string ToSessionString();

        /// <summary>
        /// Read values from string save in session.
        /// </summary>
        /// <param name="value">The value obtained from <see cref="ToSessionString"/>.</param>
        void ReadSessionString(string value);
    }
}
