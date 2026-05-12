namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Defines types of <see cref="PresentationMessage"/>.
    /// </summary>
    public enum PresentationMessageType : int
    {
        /// <summary>
        /// Information message.
        /// </summary>
        Information = 0,

        /// <summary>
        /// Question message (Yes/No) represented to user.
        /// </summary>
        Question = 1,

        /// <summary>
        /// Warning message of action that can be re-tried or canceled.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Error message.
        /// </summary>
        Error = 3
    }
}
