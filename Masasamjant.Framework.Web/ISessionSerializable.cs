namespace Masasamjant.Web
{
    /// <summary>
    /// Represents object instance that can be saved into session as string.
    /// </summary>
    public interface ISessionSerializable
    {
        /// <summary>
        /// Gets the string representation of the object to be saved in session.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        string ToSessionString();

        /// <summary>
        /// Reads the values from the string representation saved in session.
        /// </summary>
        /// <param name="value">The string representation of the object.</param>
        void ReadSessionString(string value);
    }
}
